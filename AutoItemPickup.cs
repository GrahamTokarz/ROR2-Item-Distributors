using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Permissions;
using AutoItemPickup.Configuration;
using AutoItemPickup.Enums;
using AutoItemPickup.Hooks;
using BepInEx;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour.HookGen;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable CS0618 // Type or member is obsolete
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
#pragma warning restore CS0618 // Type or member is obsolete

namespace AutoItemPickup
{

    [BepInPlugin("com.kuberoot.autoitempickup", "AutoItemPickup", "2.1.0")]
    public class AutoItemPickup : BaseUnityPlugin
    {
        private static readonly MethodInfo GenericPickupController_AttemptGrant =
            typeof(GenericPickupController).GetMethod("AttemptGrant",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

        private static readonly FieldInfo GenericPickupController_consumed =
            typeof(GenericPickupController).GetField("consumed",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        private bool distributorNeedsUpdate;

        private HookManager hookManager;

        public ItemDistributor Distributor { get; private set; }

        public Config ModConfig { get; private set; }

        public void OnEnable()
        {
            // Debug code for local multiplayer testing
            // On.RoR2.Networking.NetworkManagerSystemSteam.OnClientConnect += (s, u, t) => { };

            ModConfig = new Config(this, Logger);

            hookManager = new HookManager(this);
            hookManager.RegisterHooks();

            On.RoR2.PlayerCharacterMasterController.OnBodyDeath += (orig, self) =>
            {
                orig(self);
                UpdateTargets();
            };
            On.RoR2.PlayerCharacterMasterController.OnBodyStart += (orig, self) =>
            {
                orig(self);
                UpdateTargets();
            };

            IL.RoR2.PickupDropletController.CreatePickup += ModifyCreatePickup;

            On.RoR2.PlayerCharacterMasterController.Awake += OnPlayerAwake;

            PlayerCharacterMasterController.onPlayerAdded += UpdateTargetsWrapper;
            PlayerCharacterMasterController.onPlayerRemoved += UpdateTargetsWrapper;

            ModConfig.distributeToDeadPlayers.SettingChanged += (_, _) => UpdateTargets();
            ModConfig.distributionMode.SettingChanged += (_, _) =>
                Distributor = ItemDistributor.GetItemDistributor(ModConfig.distributionMode.Value, this);
        }

        public void OnDisable()
        {
            hookManager.UnregisterHooks();

            // TODO: Check if this works for non-hooks
            // Cleanup any leftover hooks
            HookEndpointManager.RemoveAllOwnedBy(
                HookEndpointManager.GetOwner((Action)OnDisable));

            PlayerCharacterMasterController.onPlayerAdded -= UpdateTargetsWrapper;
            PlayerCharacterMasterController.onPlayerRemoved -= UpdateTargetsWrapper;
        }

        private void UpdateTargetsWrapper(PlayerCharacterMasterController player)
        {
            UpdateTargets();
        }

        private void OnPlayerAwake(On.RoR2.PlayerCharacterMasterController.orig_Awake orig,
            PlayerCharacterMasterController self)
        {
            orig(self);

            if (!NetworkServer.active) return;
            var master = self.master;
            if (master) master.onBodyStart += obj => UpdateTargets();
        }

        private void ModifyCreatePickup(ILContext il)
        {
            var cursor = new ILCursor(il);

            cursor.GotoNext(MoveType.After, i => i.MatchCall<GenericPickupController>("CreatePickup"));
            cursor.Emit(OpCodes.Dup);
            cursor.Emit(OpCodes.Ldarg_0);
            cursor.EmitDelegate<Action<GenericPickupController, PickupDropletController>>(
                (pickupController, self) =>
                {
                    var behaviour = self.GetComponent<OverrideDistributorBehaviour>();
                    if (behaviour)
                    {
                        var newBehaviour = pickupController.gameObject.AddComponent<OverrideDistributorBehaviour>();
                        newBehaviour.Distributor = behaviour.Distributor;
                    }
                });
        }

        [Server]
        public static void GrantItem(GenericPickupController item, CharacterMaster master)
        {
            if (master.hasBody)
            {
                GenericPickupController_AttemptGrant.Invoke(item, new object[] { master.GetBody() });
            }
            else
            {
                // The game no longer supports granting items to dead players; do it manually instead
                var itemIndex = PickupCatalog.GetPickupDef(item.pickupIndex)?.itemIndex ?? ItemIndex.None;
                if (itemIndex != ItemIndex.None)
                {
                    master.inventory.GiveItem(itemIndex);

                    var playerCharacterMasterController = master.playerCharacterMasterController;
                    var networkUser = playerCharacterMasterController != null
                        ? playerCharacterMasterController.networkUser
                        : null;
                    var pickupDef = PickupCatalog.GetPickupDef(item.pickupIndex);
                    // Based on RoR2.GenericPickupController.HandlePickupMessage
                    Chat.AddMessage(new Chat.PlayerPickupChatMessage
                    {
                        subjectAsNetworkUser = networkUser,
                        baseToken = "PLAYER_PICKUP",
                        pickupToken = pickupDef?.nameToken ?? PickupCatalog.invalidPickupToken,
                        pickupColor = pickupDef?.baseColor ?? Color.black,
                        pickupQuantity = (uint)master.inventory.GetItemCount(itemIndex)
                    }.ConstructChatString());

                    GenericPickupController_consumed.SetValue(item, true);
                    Destroy(item.gameObject);
                }
            }
        }

        //NOTE: Currently ShouldDistribute has no check for OverrideDistributorBehaviour. Items will only be distributed if whitelisted.
        // public bool ShouldDistribute(GenericPickupController pickup, Cause cause)
        // {
        //     if (pickup == null || pickup.pickupIndex == PickupIndex.none)
        //         return false;
        //
        //     var overrideDistributorBehaviour = pickup.GetComponent<OverrideDistributorBehaviour>();
        //     if (overrideDistributorBehaviour != null && overrideDistributorBehaviour.Distributor != null &&
        //         overrideDistributorBehaviour.Distributor.IsValid())
        //         return true;
        //
        //     return ModConfig.ShouldDistribute(pickup.pickupIndex, cause);
        // }

        private void UpdateTargets()
        {
            distributorNeedsUpdate = true;
        }

        public void PreDistributeItemInternal(Cause cause)
        {
            if (Distributor == null)
                Distributor = ItemDistributor.GetItemDistributor(ModConfig.distributionMode.Value, this);

            if (distributorNeedsUpdate)
            {
                distributorNeedsUpdate = false;
                Distributor?.UpdateTargets();
            }
        }

        public void DistributeItemInternal(GenericPickupController item, Cause cause)
        {
            var distributor = GetDistributorInternal(item.gameObject, cause);

            try
            {
                distributor.DistributeItem(item);
            }
            catch (Exception e)
            {
                Logger.LogError($"Caught AutoItemPickup distributor exception:\n{e}\n{e.StackTrace}");
            }
        }

        public void DistributeItem(GenericPickupController item, Cause cause)
        {
            PreDistributeItemInternal(cause);
            DistributeItemInternal(item, cause);
        }

        public void DistributeItems(IEnumerable<GenericPickupController> items, Cause cause)
        {
            PreDistributeItemInternal(cause);
            foreach (var item in items) DistributeItemInternal(item, cause);
        }

        public ItemDistributor GetDistributorInternal(GameObject item, Cause cause)
        {
            if (item != null)
            {
                var overrideDistributorBehaviour = item.GetComponent<OverrideDistributorBehaviour>();
                if (overrideDistributorBehaviour != null)
                    return overrideDistributorBehaviour.Distributor;
            }

            return Distributor;
        }

        public ItemDistributor GetDistributor(GameObject item, Cause cause)
        {
            PreDistributeItemInternal(cause);
            return GetDistributorInternal(item, cause);
        }
    }
}
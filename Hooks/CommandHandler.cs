using AutoItemPickup.Enums;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace AutoItemPickup.Hooks
{

    public class CommandHandler : AbstractHookHandler
    {
        public override void RegisterHooks()
        {
            On.RoR2.PickupDropletController.CreateCommandCube += On_PickupDropletController_CreateCommandCube;

            TeleporterInteraction.onTeleporterChargedGlobal += OnTeleporterCharged;
        }

        public override void UnregisterHooks()
        {
            On.RoR2.PickupDropletController.CreateCommandCube -= On_PickupDropletController_CreateCommandCube;

            TeleporterInteraction.onTeleporterChargedGlobal -= OnTeleporterCharged;
        }

        private void On_PickupDropletController_CreateCommandCube(On.RoR2.PickupDropletController.orig_CreateCommandCube orig, PickupDropletController self)
        {
            if (ModConfig.ShouldDistributeCommand(self.createPickupInfo.pickupIndex, Cause.Drop))
                Plugin.GetDistributor(
                    hookManager.GetHandler<PickupDropletOnCollisionOverrideHandler>().CurrentDroplet.gameObject,
                    Cause.Drop)?.DistributeItem(ref self.createPickupInfo, target => target.hasBody);
            else if (TeleporterInteraction.instance &&
                     TeleporterInteraction.instance.isCharged &&
                     ModConfig.ShouldDistributeCommand(self.createPickupInfo.pickupIndex, Cause.Teleport))
                self.createPickupInfo.position = GetTeleporterCommandTargetPosition();

            orig(self);
        }

        private Vector3 GetTeleporterCommandTargetPosition()
        {
            Vector3 spawnposition;

            var center = TeleporterInteraction.instance.transform.position;

            var angle = Random.Range(0, Mathf.PI * 2f);
            float distance = Random.Range(4, 15);
            spawnposition = center + Mathf.Sin(angle) * distance * Vector3.forward +
                            Mathf.Cos(angle) * distance * Vector3.right + Vector3.up * 10;

            return spawnposition;
        }

        private void TeleportToTeleporter(NetworkBehaviour obj)
        {
            TeleportHelper.TeleportGameObject(obj.gameObject, GetTeleporterCommandTargetPosition());
        }

        private void OnTeleporterCharged(TeleporterInteraction obj)
        {
            if (!NetworkServer.active || !ModConfig.teleportCommandOnTeleport.Value) return;

            var originalPickups = InstanceTracker.GetInstancesList<PickupPickerController>();

            var pickups = new PickupPickerController[originalPickups.Count];

            originalPickups.CopyTo(pickups);

            var teleporterPosition = TeleporterInteraction.instance.transform.position;

            foreach (var pickup in pickups)
            {
                var pickupIndexNetworker = pickup.GetComponent<PickupIndexNetworker>();
                if (pickupIndexNetworker && (pickup.transform.position - teleporterPosition).sqrMagnitude > 100 &&
                    ModConfig.ShouldDistributeCommand(pickupIndexNetworker.pickupIndex, Cause.Teleport))
                    TeleportToTeleporter(pickup);
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using AutoItemPickup.Enums;
using AutoItemPickup.ItemDistributors;
using RoR2;
using UnityEngine;

namespace AutoItemPickup
{

    public abstract class ItemDistributor
    {
        public delegate bool TargetFilter(CharacterMaster target);

        private readonly AutoItemPickup plugin;

        protected ItemDistributor(AutoItemPickup plugin)
        {
            this.plugin = plugin;
            UpdateTargets();
        }

        public abstract CharacterMaster GetTarget(Vector3 position, PickupIndex pickupIndex,
            TargetFilter extraFilter = null);

        public virtual void DistributeItem(ref GenericPickupController.CreatePickupInfo pickupInfo,
            TargetFilter extraFilter = null)
        {
            var target = GetTarget(pickupInfo.position, pickupInfo.pickupIndex, extraFilter);
            if (target == null) return;
            var spawnPosition = target.GetBody().transform.position + Vector3.up * 2;

            pickupInfo.position = spawnPosition;
        }

        public virtual void DistributeItem(GenericPickupController item, TargetFilter extraFilter = null)
        {
            var target = GetTarget(item.transform.position, item.pickupIndex, extraFilter);
            if (target == null) return;
            AutoItemPickup.GrantItem(item, target);
        }

        public abstract void UpdateTargets();

        public virtual bool IsValid()
        {
            return true;
        }

        protected bool IsValidTarget(CharacterMaster master)
        {
            return plugin.ModConfig.CheckTarget(master);
        }

        protected bool IsValidTarget(PlayerCharacterMasterController player)
        {
            return player != null && IsValidTarget(player.master);
        }

        protected List<PlayerCharacterMasterController> GetValidTargets()
        {
            return PlayerCharacterMasterController.instances.Where(IsValidTarget).ToList();
        }

        public static ItemDistributor GetItemDistributor(Mode mode, AutoItemPickup plugin)
        {
            switch (mode)
            {
                case Mode.Closest:
                    return new ClosestDistributor(plugin);

                case Mode.LeastItems:
                    return new LeastItemsDistributor(plugin);

                case Mode.Random:
                    return new RandomDistributor(plugin);

                case Mode.Sneaky:
                    return new Sneaky(plugin);

                case Mode.Bodyguard:
                    return new Bodyguard(plugin);

                case Mode.Snooky:
                    return new Snooky(plugin);

                case Mode.T1:
                    return new T1Distributor(plugin);

                case Mode.T2:
                    return new T2Distributor(plugin);

                case Mode.T22:
                    return new T2Distributor2(plugin);

                case Mode.Sequential:
                default:
                    return new SequentialDistributor(plugin);
            }
        }
    }
}
/**
using System.Collections.Generic;
using RoR2;
using System;
using UnityEngine;
using System.Diagnostics;

namespace AutoItemPickup.ItemDistributors
{
    class SOO : ItemDistributor
    {
        CharacterMaster[] playerDistribution;
        int index = 0;

        public override void UpdateTargets()
        {
            var oldTarget = playerDistribution?[index];

            List<PlayerCharacterMasterController> filteredPlayers = GetValidTargets();
            playerDistribution = new CharacterMaster[filteredPlayers.Count];

            index = -1;

            for (int i = 0; i < filteredPlayers.Count; i++)
            {
                var player = filteredPlayers[i];

                playerDistribution[i] = player.master;
                if (player.master == oldTarget)
                    index = i;
            }

            if (index == -1)
                index = UnityEngine.Random.Range(0, playerDistribution.Length);
        }

        public override void DistributeItem(GenericPickupController item)
        {
            CharacterMaster target = GetTarget(item.transform.position, item.pickupIndex);
            UnityEngine.Object.Destroy(item.gameObject);
        }

        public override CharacterMaster GetTarget(UnityEngine.Vector3 Position, PickupIndex pickupIndex)
        {
            if (playerDistribution.Length == 0)
                return null;

            CharacterMaster target = playerDistribution[index];
            //UnityEngine.Debug.Log($"Checking if {target.playerCharacterMasterController?.networkUser?.userName} that has hasBody {target.hasBody} is valid: {IsValidTarget(target)}");
            index = (++index) % playerDistribution.Length;

            ulong seed = (ulong)(RoR2.Run.instance.GetStartTimeUtc().Ticks ^ (long)(RoR2.Run.instance.stageClearCount << 16));
            ItemDef item = ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex(pickupIndex.ToString().Substring(10)));
            target.inventory.GiveItem(item);
            target.inventory.ShrineRestackInventory(new Xoroshiro128Plus(seed));
            GenericPickupController.SendPickupMessage(target, pickupIndex);
            return null;
        }
    }
}
**/
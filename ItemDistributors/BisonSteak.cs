/**
using System.Collections.Generic;
using RoR2;

namespace AutoItemPickup.ItemDistributors
{
    class BisonSteak : ItemDistributor
    {
        CharacterMaster[] playerDistribution;
        int index = 0;
        ItemDef item = ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex(AutoItemPickup.BisonItemName));

        public override void UpdateTargets()
        {
            item = ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex(AutoItemPickup.BisonItemName));
            var oldTarget = playerDistribution?[index];

            List<PlayerCharacterMasterController> filteredPlayers = GetValidTargets();
            playerDistribution = new CharacterMaster[filteredPlayers.Count];

            index = -1;

            for (int i = 0; i < filteredPlayers.Count; i++)
            {
                var player = filteredPlayers[i];

                playerDistribution[i] = player.master;
                if(player.master == oldTarget)
                    index = i;
            }

            if(index == -1)
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
            target.inventory.GiveItem(ItemCatalog.FindItemIndex(AutoItemPickup.BisonItemName));
            GenericPickupController.SendPickupMessage(target, PickupCatalog.FindPickupIndex("ItemIndex." + AutoItemPickup.BisonItemName));
            return null;
        }
    }
}
**/
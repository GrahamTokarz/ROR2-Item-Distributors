/**using System.Collections.Generic;
using RoR2;
using System;


namespace AutoItemPickup.ItemDistributors
{
    class StageChest : ItemDistributor
    {
        CharacterMaster[] targets;
        int level = -1;


        public override void UpdateTargets()
        {
            List<PlayerCharacterMasterController> filteredPlayers = GetValidTargets();
            targets = new CharacterMaster[filteredPlayers.Count];

            for (int i = 0; i < filteredPlayers.Count; i++)
            {
                var player = filteredPlayers[i];

                targets[i] = player.master;
            }

        }


        public int[] itemAr = { -1, -1, 61, 19, 79, -1, 13, 46, -1, 30, 25, 60, 26, -1, 93, 65, 27, 94, -1, 35, -1, -1, -1, -1, 0, 78, -1, -1, 55, -1, 59, -1, -1, -1, -1, 73, 11, -1, -1, 6, -1, 66, 33, -1, 81, -1, -1, -1, -1, -1, 56, 34, -1, 44, 58, -1, 67, -1, -1, -1, 69, 37, 41, 91, 3, 2, -1, 7, -1, 52, -1, 70, -1, 20, -1, -1, -1, 84, 36, 5, 18, -1, -1, 16, 51, 68, 10, 62, 76, -1, 40, -1, 57, 64, 95, 77, -1, 85, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 12, -1, -1, 86, 29, -1, -1, -1, 75, 38, 4, -1, 9, 74, 88, 15, 92, -1, 82, 17, 45, 72, -1, -1, 53, -1, 63, -1, 49, -1, -1, 87, -1, -1, -1, -1, -1, -1, -1, 1, 42, -1, -1, 83, 90, -1, 32, -1, 50, 8, 48, 89, 54, 23, 39, 24, 22, 43, 80, -1, -1, -1, 47, -1, -1, 14, 21, -1, -1, 71, -1, -1, -1, 31, 28 };
        public int[] itemArLUNVOID = { -1, -1, 61, 19, 79, -1, 13, 46, 100, 30, 25, 60, 26, 200, 93, 65, 27, 94, 201, 35, -1, -1, -1, -1, 0, 78, -1, -1, 55, 202, 59, 203, -1, -1, -1, 73, 11, 204, -1, 6, -1, 66, 33, -1, 81, -1, -1, -1, 205, -1, 56, 34, 206, 44, 58, 207, 67, 208, 300, 301, 69, 37, 41, 91, 3, 2, 101, 7, -1, 52, -1, 70, 102, 20, 103, 104, -1, 84, 36, 5, 18, -1, -1, 16, 51, 68, 10, 62, 76, -1, 40, -1, 57, 64, 95, 77, -1, 85, 105, 106, 107, 108, 109, 110, 111, 112, -1, -1, 12, -1, -1, 86, 29, 209, -1, 113, 75, 38, 4, 210, 9, 74, 88, 15, 92, -1, 82, 17, 45, 72, -1, -1, 53, 114, 63, 115, 49, -1, 116, 87, -1, -1, -1, -1, -1, -1, -1, 1, 42, 117, -1, 83, 90, -1, 32, 210, 50, 8, 48, 89, 54, 23, 39, 24, 22, 43, 80, -1, -1, -1, 47, 400, -1, 14, 21, 211, -1, 71, -1, 212, -1, 31, 28 };
        public override void DistributeItem(GenericPickupController item)
        {
            CharacterMaster target = GetTarget(item.transform.position, item.pickupIndex);
            if (target != null)
            {
                UnityEngine.Object.Destroy(item.gameObject);
            }
        }

        public override CharacterMaster GetTarget(UnityEngine.Vector3 Position, PickupIndex pickupIndex)
        {
            if (itemArLUNVOID[(int)pickupIndex.itemIndex] == -1)
            {
                return null;
            } else
            {
                return targets[0];
            }
        }

        public override void BossDrops()
        {
            double numToGive = 0;
            foreach (var player in targets)
            {
                if (!player.IsDeadAndOutOfLivesServer())
                {
                    numToGive = (int)player.GetBody().level;
                }
            }
            //numToGive *= (1 + (.5 * Math.Floor((double) (RoR2.Run.instance.stageClearCount / 5))));
            numToGive = 5;
            foreach (var player in targets)
            {
                player.inventory.GiveRandomItems((int) Math.Round(numToGive), false, true);
                player.inventory.GiveRandomEquipment();
            }
        }
    }
}
**/
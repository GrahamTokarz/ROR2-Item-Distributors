/**using System.Collections.Generic;
using RoR2;
using System;
using System.Diagnostics;
using UnityEngine;

namespace AutoItemPickup.ItemDistributors
{
    class Duplicate : ItemDistributor
    {
        (CharacterMaster, Transform, Vector3?)[] targets;
        int level = -1;
        Stopwatch stopwatch = new Stopwatch();
        int bossItems = 0;
        bool tpEnded = true;

        public override void UpdateTargets()
        {
            stopwatch.Start();
            List<PlayerCharacterMasterController> filteredPlayers = GetValidTargets();
            targets = new (CharacterMaster, Transform, Vector3?)[filteredPlayers.Count];

            for (int i = 0; i < filteredPlayers.Count; i++)
            {
                var player = filteredPlayers[i];
                var master = player.master;

                targets[i] = (master, master.hasBody ? master.GetBodyObject().transform : null, master.hasBody ? null : (Vector3?)master.deathFootPosition);
            }

        }

        public override void DistributeItem(GenericPickupController item)
        {
            Log.Info(bossItems);
            Log.Info(tpEnded);
            Log.Info(stopwatch.ElapsedMilliseconds);
            if (stopwatch.ElapsedMilliseconds > 5000)
            {
                tpEnded = true;
            }

            if (bossItems == 0 && tpEnded)
            {
                CharacterMaster target = GetTarget(item.transform.position, item.pickupIndex);
                if (target != null)
                {
                    UnityEngine.Object.Destroy(item.gameObject);
                    string strM = target.GetBody().GetUserName() + " thought you could use a " + PickupDropTable.GetName(item);
                    Chat.AddMessage(strM);
                }
            }

            if (bossItems > 0)
            {
                CharacterMaster target = GetTarget(item.transform.position, item.pickupIndex);
                if (target != null)
                {
                    UnityEngine.Object.Destroy(item.gameObject);
                }
                bossItems -= 1;
            } else if (!tpEnded)
            {
                UnityEngine.Object.Destroy(item.gameObject);
            }
        }

        public int[] itemAr = { -1, -1, 61, 19, 79, -1, 13, 46, -1, 30, 25, 60, 26, -1, 93, 65, 27, 94, -1, 35, -1, -1, -1, -1, 0, 78, -1, -1, 55, -1, 59, -1, -1, -1, -1, 73, 11, -1, -1, 6, -1, 66, 33, -1, 81, -1, -1, -1, -1, -1, 56, 34, -1, 44, 58, -1, 67, -1, -1, -1, 69, 37, 41, 91, 3, 2, -1, 7, -1, 52, -1, 70, -1, 20, -1, -1, -1, 84, 36, 5, 18, -1, -1, 16, 51, 68, 10, 62, 76, -1, 40, -1, 57, 64, 95, 77, -1, 85, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 12, -1, -1, 86, 29, -1, -1, -1, 75, 38, 4, -1, 9, 74, 88, 15, 92, -1, 82, 17, 45, 72, -1, -1, 53, -1, 63, -1, 49, -1, -1, 87, -1, -1, -1, -1, -1, -1, -1, 1, 42, -1, -1, 83, 90, -1, 32, -1, 50, 8, 48, 89, 54, 23, 39, 24, 22, 43, 80, -1, -1, -1, 47, -1, -1, 14, 21, -1, -1, 71, -1, -1, -1, 31, 28 };

        public override CharacterMaster GetTarget(UnityEngine.Vector3 position, PickupIndex pickupIndex)
        {
            if (itemAr[(int)pickupIndex.itemIndex] == -1)
            {
                return null;
            }
            foreach (var player in targets)
            {
                ItemDef item = ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex(pickupIndex.ToString().Substring(10)));
                player.Item1.inventory.GiveItem(item);
                GenericPickupController.SendPickupMessage(player.Item1, pickupIndex);
            }

            CharacterMaster closestPlayer = null;
            float closestDistance = float.MaxValue;

            Vector3 itemPosition = position;

            foreach (var player in targets)
            {
                Vector3? playerPos = player.Item2 ? player.Item2.position : player.Item3;
                if (!playerPos.HasValue) continue;

                float distance = (itemPosition - playerPos.Value).sqrMagnitude;
                if (distance < closestDistance)
                {
                    closestPlayer = player.Item1;
                    closestDistance = distance;
                }
            }
            return closestPlayer;
        }

        public override void BossDrops()
        {
            bossItems = TeleporterInteraction.instance.shrineBonusStacks + 1;
            tpEnded = false;
            stopwatch.Restart();
        }
    }
}
**/
using System.Collections.Generic;
using RoR2;

namespace AutoItemPickup.ItemDistributors
{
    class Bodyguard : ItemDistributor
    {
        public Bodyguard(AutoItemPickup plugin) : base(plugin) { }

        CharacterMaster[] targets;
        int phoenix = -1;
        public override void UpdateTargets()
        {
            List<PlayerCharacterMasterController> filteredPlayers = GetValidTargets();
            targets = new CharacterMaster[filteredPlayers.Count - 1];
            if (phoenix == -1)
            {
                phoenix = UnityEngine.Random.Range(0, filteredPlayers.Count);
            }

            int j = 0;
            for (int i = 0; i < filteredPlayers.Count; i++)
            {
                if (i != phoenix)
                {
                    var player = filteredPlayers[i];

                    targets[j] = player.master;
                    j++;
                }
            }
        }

        public override CharacterMaster GetTarget(UnityEngine.Vector3 Position, PickupIndex pickupIndex, TargetFilter extrafilter = null)
        {
            Log.Info(pickupIndex);
            return targets[UnityEngine.Random.Range(0, targets.Length)];
        }
    }
}

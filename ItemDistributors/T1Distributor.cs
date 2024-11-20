using System.Collections.Generic;
using RoR2;
using System;

namespace AutoItemPickup.ItemDistributors
{
    class T1Distributor : ItemDistributor
    {
        public T1Distributor(AutoItemPickup plugin) : base(plugin) { }

        CharacterMaster[] targets;

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

        // G - 27
        // B - 22
        // M - 26
        // J - 22

        public int[] itemAr = { -1, -1, 61, 19, 79, -1, 13, 46, -1, 30, 25, 60, 26, -1, 93, 65, 27, 94, -1, 35, -1, -1, -1, -1, 0, 78, -1, -1, 55, -1, 59, -1, -1, -1, -1, 73, 11, -1, -1, 6, -1, 66, 33, -1, 81, -1, -1, -1, -1, -1, 56, 34, -1, 44, 58, -1, 67, -1, -1, -1, 69, 37, 41, 91, 3, 2, -1, 7, -1, 52, -1, 70, -1, 20, -1, -1, -1, 84, 36, 5, 18, -1, -1, 16, 51, 68, 10, 62, 76, -1, 40, -1, 57, 64, 95, 77, -1, 85, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 12, -1, -1, 86, 29, -1, -1, -1, 75, 38, 4, -1, 9, 74, 88, 15, 92, -1, 82, 17, 45, 72, -1, -1, 53, -1, 63, -1, 49, -1, -1, 87, -1, -1, -1, -1, -1, -1, -1, 1, 42, -1, -1, 83, 90, -1, 32, -1, 50, 8, 48, 89, 54, 23, 39, 24, 22, 43, 80, -1, -1, -1, 47, -1, -1, 14, 21, -1, -1, 71, -1, -1, -1, 31, 28 };
        public double[] MDist = { 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 25, 8, 0, 0, 20, 0, 0, 0, 1, 0, 0, 6, 6, 0, 7, 10, 1, 7, 0, 0, 10, 0, 4, 0, 10, 12, 3, 0, 7, 0, 1, 0, 6, 0, 0, 4, 0, 0, 0, 0, 0, 0, 1, 6, 12, 15, 2, 15, 0, 0, 1, 0, 0, 6, 0, 10, 0, 10, 0, 0, 0, 0, 0, 10, 0, 15, 0, 10, 1, 1, 20, 0, 1, 20, 0, 0, 0, 0, 0, 20, 0, 20, 0, 40 };
        public double[] JDist = { 8, 34, 6, 0, 0, 4, 0, 0, 14, 1, 0, 0, 5, 6, 1, 0, 4, 0, 0, 0, 1, 1, 7, 0, 0, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 42, 0, 0, 0, 0, 0, 8, 0, 1, 0, 41, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 10, 40, 0, 0, 25, 23, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 30, 0, 40, 0, 0, 30 };
        public double[] GDist = { 10, 0, 0, 0, 5, 0, 0, 0, 6, 0, 7, 15, 4, 8, 0, 0, 8, 0, 0, 5, 0, 0, 8, 2, 0, 10, 10, 2, 0, 4, 0, 4, 4, 4, 0, 0, 3, 15, 3, 0, 13, 3, 0, 0, 0, 4, 13, 0, 0, 0, 0, 3, 0, 0, 0, 9, 4, 8, 6, 4, 4, 0, 0, 0, 0, 10, 0, 0, 16, 15, 0, 10, 4, 5, 0, 0, 15, 0, 10, 4, 0, 0, 3, 0, 0, 0, 1, 0, 10, 0, 40, 0, 0, 0, 20, 29 };
        public double[] BDist = { 0, 10, 10, 0, 0, 4, 10, 5, 10, 0, 20, 0, 0, 5, 0, 5, 5, 10, 0, 0, 0, 1, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 5, 5, 0, 0, 0, 0, 30, 0, 3, 0, 5, 5, 10, 0, 0, 15, 0, 3, 3, 0, 10, 0, 0, 0, 0, 0, 0, 0, 30, 0, 0, 0, 0, 0, 10, 0, 0, 0, 35, 0, 0, 0, 20, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 20, 0, 60, 0, 0, 0, 20, 0, 0 };

        public String[] usernames = new string[4];
        Random rand = new Random();

        public override CharacterMaster GetTarget(UnityEngine.Vector3 Position, PickupIndex pickupIndex, TargetFilter extraFilter = null)
        {
            String goesTo = "Err";
            int idx = itemAr[(int)pickupIndex.itemIndex];
            if (idx == -1)
            {
                goesTo = "Err";
            }
            else
            {
                double constant = 0;
                double x = 0.8;
                double xx = 0.2;
                double xxx = 0;

                double mVal = constant + x * MDist[idx] + xx * MDist[idx] * MDist[idx] + xxx * MDist[idx] * MDist[idx] * MDist[idx];
                double jVal = constant + x * JDist[idx] + xx * JDist[idx] * JDist[idx] + xxx * JDist[idx] * JDist[idx] * JDist[idx];
                double gVal = constant + x * GDist[idx] + xx * GDist[idx] * GDist[idx] + xxx * GDist[idx] * GDist[idx] * GDist[idx];
                double bVal = constant + x * BDist[idx] + xx * BDist[idx] * BDist[idx] + xxx * BDist[idx] * BDist[idx] * BDist[idx];
                double sum = mVal + jVal + gVal + bVal;
                double mPer = mVal / sum;
                double jPer = mPer + jVal / sum;
                double gPer = jPer + gVal / sum;
                double bPer = gPer + bVal / sum;

                Log.Info(mPer);
                Log.Info(jPer);
                Log.Info(gPer);
                Log.Info(bPer);

                double val = rand.NextDouble();
                Log.Info(val);
                if (val <= mPer)
                {
                    goesTo = "Onyx";
                }
                else if (val <= jPer)
                {
                    goesTo = "SpyseaRice";
                }
                else if (val <= gPer)
                {
                    goesTo = "Monkeytoes999";
                }
                else if (val <= bPer)
                {
                    goesTo = "calculatorismyfriend";
                }
                Log.Info(goesTo);

            }
            var i = 0;
            foreach (var player in targets)
            {
                if (player.IsDeadAndOutOfLivesServer())
                {
                    if (usernames[i] == goesTo)
                    {
                        return player;
                    }
                }
                else
                {
                    usernames[i] = player.GetBody().GetUserName();
                    if (player.GetBody().GetUserName() == goesTo)
                    {
                        return player;
                    }
                }
                i++;
            }
            return targets[UnityEngine.Random.Range(0, targets.Length)];
        }
    }
}

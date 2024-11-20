using System.Collections.Generic;
using RoR2;
using System;

namespace AutoItemPickup.ItemDistributors
{
    class T2Distributor2 : ItemDistributor
    {
        public T2Distributor2(AutoItemPickup plugin) : base(plugin) { }

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

        public string[] itemAr = { "BossDamageBonus", "SecondarySkillMagazine", "FlatHealth", "Firework", "Mushroom", "HealWhileSafe", "Crowbar", "FragileDamageBonus", "SprintBonus", "NearbyDamageBonus", "IgniteOnKill", "CritGlasses", "Medkit", "AttackSpeedAndMoveSpeed", "Tooth", "OutOfCombatArmor", "Hoof", "PersonalShield", "HealingPotion", "ArmorPlate", "GoldOnHurt", "TreasureCache", "Syringe", "StickyBomb", "StunChanceOnHit", "BarrierOnKill", "Bear", "BleedOnHit", "WardOnLevel", "Missile", "Bandolier", "WarCryOnMultiKill", "SlowOnHit", "DeathMark", "EquipmentMagazine", "BonusGoldPackOnKill", "HealOnCrit", "Feather", "MoveSpeedOnKill", "StrengthenBurn", "Infusion", "FireRing", "Seed", "TPHealingNova", "ExecuteLowHealthElite", "Phasing", "AttackSpeedOnCrit", "Thorns", "SprintOutOfCombat", "RegeneratingScrap", "SprintArmor", "IceRing", "FreeChest", "PrimarySkillShuriken", "SquidTurret", "ChainLightning", "EnergizedOnEquipmentUse", "JumpBoost", "ExplodeOnDeath", "Clover", "BarrierOnOverHeal", "AlienHead", "ImmuneToDebuff", "RandomEquipmentTrigger", "KillEliteFrenzy", "Behemoth", "Dagger", "ExtraLife", "Icicle", "FallBoots", "GhostOnKill", "UtilitySkillMagazine", "Plant", "CritDamage", "NovaOnHeal", "MoreMissile", "IncreaseHealing", "LaserTurbine", "BounceNearby", "ArmorReductionOnHit", "Talisman", "DroneWeapons", "PermanentDebuffOnHit", "ShockNearby", "HeadHunter", "LightningStrikeOnHit", "MinorConstructOnKill", "RoboBallBuddy", "NovaOnLowHealth", "SprintWisp", "SiphonOnLowHealth", "FireballsOnHit", "ParentEgg", "BeetleGland", "BleedOnHitAndExplode", "Knurl" };
        public double[] MDist = { 2, 1, 0, 0, 6, 3, 0, 1, 5, 0, 0, 5, 5, 6, 0, 0, 12, 0, 4, 5, 0, 0, 6, 12, 1, 5, 5, 15, 1, 3, 0, 0, 2, 1, 2, 0, 0, 12, 0, 0, 40.04, 0, 1, 0, 6, 2, 0, 3, 2, 1.96, 0, 0, 0, 0, 1, 16, 1, 6, 0, 20, 0, 0, 5, 1, 0, 0, 0, 10, 0, 27, 0, 0, 0, 0, 0, 1, 10, 0, 0, 4, 1, 1, 20, 0, 0, 51, 0, 1, 3, 0, 0, 20, 0, 5, 0, 20 };
        public double[] JDist = { 3, 3, 0, 0, 0, 2.31, 0, 3, 0, 14, 0, 15, 5, 12, 0, 9, 1, 0, 3.69, 3, 0, 0, 6, 2, 0, 4, 5, 8, 1, 3, 1, 0, 4, 1, 2, 1, 13, 2, 2.31, 1.5, 4, 9, 1, 0, 2, 13, 13, 0, 2, 1.69, 1, 9, 0, 4, 0, 6.5, 1, 1, 1, 5, 0, 5, 3, 1, 1, 3, 1, 5, 21, 1, 0, 11, 0, 0, 0, 0, 2, 0, 2, 0, 1, 0, 0, 38, 0, 0, 0, 0, 2, 0, 24, 0, 40, 15, 0, 19 };
        public double[] GDist = { 0, 0, 0, 0, 0, 8, 4, 10, 8, 0, 5, 0, 9, 7, 5, 9, 4, 1, 5, 7, 0, 0, 0, 0, 0, 12, 5, 0, 1, 0, 5, 4, 0, 0, 2, 5, 20, 0, 6, 5, 0, 0, 3, 0, 10, 0, 9, 0, 9, 0, 2, 0, 0, 0, 0, 10, 0, 5, 5, 0, 0, 9, 0, 0, 9, 9, 8, 4, 0, 0, 9, 0, 9, 9, 0, 0, 7, 9, 9, 0, 4, 0, 0, 0, 5, 8, 9, 0, 0, 0, 0, 8, 0, 0, 50, 25 };
        public double[] BDist = { 10, 3, 1, 0, 4, 4, 0, 1, 14, 12, 0, 0, 5, 0, 0, 0, 10, 0, 0, 8, 4, 1, 0, 0, 0, 0, 22, 0, 1, 0, 0, 0, 25, 5, 1, 0, 0, 1, 0, 0, 2, 1, 0, 0, 19, 1, 0, 3, 4, 0, 19, 1, 1, 1, 0, 0, 1, 15, 0, 0, 5, 15, 8, 0, 0, 0, 0, 5, 25, 2, 0, 8, 0, 0, 3, 0, 24, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 50, 0, 40, 0, 0, 8 };

        public String[] usernames = new string[4];
        Random rand = new Random();

        public override CharacterMaster GetTarget(UnityEngine.Vector3 Position, PickupIndex pickupIndex, TargetFilter extraFilter = null)
        {
            List<PickupIndex> idxes = new List<PickupIndex>();
            foreach (string name in itemAr)
            {
                idxes.Add(PickupCatalog.FindPickupIndex("ItemIndex." + name));
            }



            String goesTo = "SQ";
            int idx = idxes.FindIndex(el => el.Equals(pickupIndex));
            if (idx == -1)
            {
                goesTo = "Err";
            } else
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

                //Log.Info(mPer);
                //Log.Info(jPer);
                //Log.Info(gPer);
                //Log.Info(bPer);

                double val = rand.NextDouble();
                //Log.Info(val);
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
                //Log.Info(goesTo);

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
            if (goesTo == "Err")
            {
                return null;
            }
            return targets[UnityEngine.Random.Range(0, targets.Length)];
        }
    }
}

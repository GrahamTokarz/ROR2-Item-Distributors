using System.Collections.Generic;
using RoR2;
using System;

namespace AutoItemPickup.ItemDistributors
{
    class Sneaky : ItemDistributor
    {
        CharacterMaster[] targets;

        public Sneaky(AutoItemPickup plugin) : base(plugin) { }

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

        public string[] itemAr = { "BossDamageBonus", "SecondarySkillMagazine", "FlatHealth", "Firework", "Mushroom", "HealWhileSafe", "Crowbar", "FragileDamageBonus", "SprintBonus", "NearbyDamageBonus", "IgniteOnKill", "CritGlasses", "Medkit", "AttackSpeedAndMoveSpeed", "Tooth", "OutOfCombatArmor", "Hoof", "PersonalShield", "HealingPotion", "ArmorPlate", "GoldOnHurt", "TreasureCache", "Syringe", "StickyBomb", "StunChanceOnHit", "BarrierOnKill", "Bear", "BleedOnHit", "WardOnLevel", "Missile", "Bandolier", "WarCryOnMultiKill", "SlowOnHit", "DeathMark", "EquipmentMagazine", "BonusGoldPackOnKill", "HealOnCrit", "Feather", "MoveSpeedOnKill", "StrengthenBurn", "Infusion", "FireRing", "Seed", "TPHealingNova", "ExecuteLowHealthElite", "Phasing", "AttackSpeedOnCrit", "Thorns", "SprintOutOfCombat", "RegeneratingScrap", "SprintArmor", "IceRing", "FreeChest", "PrimarySkillShuriken", "Squid", "ChainLightning", "EnergizedOnEquipmentUse", "JumpBoost", "ExplodeOnDeath", "Clover", "BarrierOnOverHeal", "AlienHead", "ImmuneToDebuff", "RandomEquipmentTrigger", "KillEliteFrenzy", "Behemoth", "Dagger", "ExtraLife", "Icicle", "FallBoots", "GhostOnKill", "UtilitySkillMagazine", "Plant", "CritDamage", "NovaOnHeal", "MoreMissile", "IncreaseHealing", "LaserTurbine", "BounceNearby", "ArmorReductionOnHit", "Talisman", "DroneWeapons", "PermanentDebuffOnHit", "ShockNearby", "HeadHunter", "LightningStrikeOnHit", "MinorConstructOnKill", "RoboBallBuddy", "NovaOnLowHealth", "SprintWisp", "SiphonOnLowHealth", "FireballsOnHit", "ParentEgg", "BeetleGland", "BleedOnHitAndExplode", "Knurl" };
        public double[] GDist = { 10, 0, 0, 0, 2, 5, 5, 10, 5, 0, 10, 0, 5, 0, 8, 8, 5, 5, 5, 5, 0, 0, 0, 0, 0, 3, 9, 0, 0, 0, 5, 5, 4, 4, 0, 5, 0, 6, 5, 6, 5, 9, 0, 0, 5, 3, 0, 4, 5, 0, 3, 9, 0, 0, 0, 6, 0, 6, 5, 0, 5, 0, 10, 0, 0, 5, 10, 5, 0, 5, 5, 0, 5, 0, 0, 0, 5, 10, 25, 0, 0, 5, 0, 0, 5, 0, 5, 5, 8, 15, 0, 0, 12, 5, 0, 50 };
        public double[] BDist = { 1, 5, 5, 0, 2, 0, 0, 0, 0, 10, 0, 5, 0, 4, 9, 0, 5, 0, 0, 10, 1, 1, 12, 5, 5, 0, 8, 7, 5, 6, 5, 3, 8, 4, 6, 0, 5, 2, 0, 0, 5, 3, 9, 1, 0, 3, 5, 4, 2, 0, 5, 3, 0, 4, 0, 5, 6, 6, 0, 10, 1, 5, 4, 5, 10, 3, 0, 5, 10, 0, 0, 10, 0, 3, 3, 5, 5, 0, 1, 5, 5, 0, 5, 5, 0, 10, 0, 0, 9, 15, 30, 15, 10, 0, 10, 1 };
        //public double[] TDist = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };


        public String[] usernames = new string[4];
        Random rand = new Random();

        public override CharacterMaster GetTarget(UnityEngine.Vector3 Position, PickupIndex pickupIndex, TargetFilter targetFilter = null)
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

                double gVal = constant + x * GDist[idx] + xx * GDist[idx] * GDist[idx] + xxx * GDist[idx] * GDist[idx] * GDist[idx];
                double bVal = constant + x * BDist[idx] + xx * BDist[idx] * BDist[idx] + xxx * BDist[idx] * BDist[idx] * BDist[idx];
                //double tVal = constant + x * TDist[idx] + xx * TDist[idx] * TDist[idx] + xxx * TDist[idx] * TDist[idx] * TDist[idx];
                double sum =  gVal + bVal; //+ tVal
                double gPer = gVal / sum;
                double bPer = gPer + bVal / sum;
                //double tPer = bPer + tVal / sum;

                //Log.Info(gPer);
                //Log.Info(bPer);

                double val = rand.NextDouble();
                //Log.Info(val);
                if (val <= gPer)
                {
                    goesTo = "Monkeytoes999";
                }
                else if (val <= bPer)
                {
                    goesTo = "calculatorismyfriend";
                }
                /**else if (val <= tPer)
                {
                    goesTo = "TrintaW";
                }**/
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

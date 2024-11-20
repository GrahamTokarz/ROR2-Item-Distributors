using System.Collections.Generic;
using RoR2;
using System;

namespace AutoItemPickup.ItemDistributors
{
    class Snooky : ItemDistributor
    {
        public Snooky(AutoItemPickup plugin) : base(plugin) { }

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
        public double[] GDist = { 15, 4, 2, 0, 0, 0, 0, 0, 12, 10, 5, 6, 10, 3, 0, 0, 5, 0, 0, 5, 3, 0, 0, 0, 6, 5, 6, 0, 3, 0, 0, 3, 10, 10, 0, 0, 0, 5, 0, 3, 0, 3, 2, 0, 17, 8, 0, 13, 0, 0, 3, 3, 0, 3, 0, 12, 0, 5, 0, 0, 4, 10, 0, 0, 10, 0, 0, 3, 19, 5, 0, 5, 0, 0, 10, 0, 0, 3, 5, 7, 0, 0, 0, 19, 0, 0, 0, 0, 10, 20, 50, 0, 10, 0, 0, 10 };
        public double[] BDist = { 3, 5, 0, 0, 0, 10, 5, 5, 5, 5, 12, 0, 5, 5, 5, 5, 5, 5, 0, 0, 0, 0, 5, 5, 0, 5, 5, 0, 0, 0, 5, 5, 0, 0, 6, 5, 20, 0, 5, 10, 2, 3, 0, 0, 5, 5, 10, 0, 5, 0, 0, 3, 0, 0, 0, 0, 6, 0, 5, 0, 0, 5, 0, 5, 10, 0, 15, 0, 0, 0, 10, 0, 10, 15, 0, 0, 0, 10, 0, 0, 10, 0, 0, 0, 10, 15, 15, 0, 0, 0, 0, 0, 0, 0, 70, 0 };
        public double[] JDist = { 3, 0, 3, 0, 6.9, 3, 0, 3, 3, 6.9, 6.9, 6.9, 3, 3, 0, 6.9, 3, 3.9, 6.9, 4.9, 0, 0, 6.9, 3, 3, 3, 3, 6.9, 0, 6.9, 0, 3, 6.9, 3, 3, 3, 3, 3, 3, 3, 0, 3, 3, 3, 3, 3, 6, 6.9, 3, 6.9, 0, 3, 0, 6.9, 0, 6.9, 0, 3, 4.6, 6.9, 6.9, 0, 6.9, 0, 3, 6.9, 4.9, 3, 6.9, 0, 3, 0, 3, 3, 3, 6.9, 3, 3, 6.9, 3, 0, 6.9, 3, 6.9, 3, 6.9, 6.9, 16.9, 16.9, 6.9, 11, 6.9, 6.9, 6.9, 6.9, 6.9 };


        public String[] usernames = new string[4];
        Random rand = new Random();

        public override CharacterMaster GetTarget(UnityEngine.Vector3 Position, PickupIndex pickupIndex, TargetFilter extrafilter = null)
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
                double jVal = constant + x * JDist[idx] + xx * JDist[idx] * JDist[idx] + xxx * JDist[idx] * JDist[idx] * JDist[idx];
                double sum = gVal + bVal + jVal;
                double gPer = gVal / sum;
                double bPer = gPer + bVal / sum;
                double jPer = bPer + jVal / sum;

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
                else if (val <= jPer)
                {
                    goesTo = "SpyseaRice";
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

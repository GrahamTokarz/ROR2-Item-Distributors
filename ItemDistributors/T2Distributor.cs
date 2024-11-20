using System.Collections.Generic;
using RoR2;
using System;

namespace AutoItemPickup.ItemDistributors
{
    class T2Distributor : ItemDistributor
    {
        public T2Distributor(AutoItemPickup plugin) : base(plugin) { }

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


        PickupIndex[] GPI = { PickupCatalog.FindPickupIndex("ItemIndex.Crowbar"), PickupCatalog.FindPickupIndex("ItemIndex.SprintBonus"), PickupCatalog.FindPickupIndex("ItemIndex.IgniteOnKill"), PickupCatalog.FindPickupIndex("ItemIndex.Tooth"), PickupCatalog.FindPickupIndex("ItemIndex.OutOfCombatArmor"), PickupCatalog.FindPickupIndex("ItemIndex.PersonalShield"), PickupCatalog.FindPickupIndex("ItemIndex.Syringe"), PickupCatalog.FindPickupIndex("ItemIndex.Bandolier"), PickupCatalog.FindPickupIndex("ItemIndex.WarCryOnMultiKill"), PickupCatalog.FindPickupIndex("ItemIndex.BonusGoldPackOnKill"), PickupCatalog.FindPickupIndex("ItemIndex.HealOnCrit"), PickupCatalog.FindPickupIndex("ItemIndex.StrengthenBurn"), PickupCatalog.FindPickupIndex("ItemIndex.Phasing"), PickupCatalog.FindPickupIndex("ItemIndex.AttackSpeedOnCrit"), PickupCatalog.FindPickupIndex("ItemIndex.ChainLightning"), PickupCatalog.FindPickupIndex("ItemIndex.ExplodeOnDeath"), PickupCatalog.FindPickupIndex("ItemIndex.KillEliteFrenzy"), PickupCatalog.FindPickupIndex("ItemIndex.Dagger"), PickupCatalog.FindPickupIndex("ItemIndex.FallBoots"), PickupCatalog.FindPickupIndex("ItemIndex.GhostOnKill"), PickupCatalog.FindPickupIndex("ItemIndex.CritDamage"), PickupCatalog.FindPickupIndex("ItemIndex.LaserTurbine"), PickupCatalog.FindPickupIndex("ItemIndex.ArmorReductionOnHit"), PickupCatalog.FindPickupIndex("ItemIndex.MinorConstructOnKill"), PickupCatalog.FindPickupIndex("ItemIndex.FireballsOnHit"), PickupCatalog.FindPickupIndex("ItemIndex.BleedOnHitAndExplode"), PickupCatalog.FindPickupIndex("ItemIndex.Knurl")};
        PickupIndex[] BPI = { PickupCatalog.FindPickupIndex("ItemIndex.BossDamageBonus"), PickupCatalog.FindPickupIndex("ItemIndex.FlatHealth"), PickupCatalog.FindPickupIndex("ItemIndex.Mushroom"), PickupCatalog.FindPickupIndex("ItemIndex.CritGlasses"), PickupCatalog.FindPickupIndex("ItemIndex.ArmorPlate"), PickupCatalog.FindPickupIndex("ItemIndex.GoldOnHurt"), PickupCatalog.FindPickupIndex("ItemIndex.Bear"), PickupCatalog.FindPickupIndex("ItemIndex.DeathMark"), PickupCatalog.FindPickupIndex("ItemIndex.TPHealingNova"), PickupCatalog.FindPickupIndex("ItemIndex.ExecuteLowHealthElite"), PickupCatalog.FindPickupIndex("ItemIndex.SprintOutOfCombat"), PickupCatalog.FindPickupIndex("ItemIndex.SprintArmor"), PickupCatalog.FindPickupIndex("ItemIndex.JumpBoost"), PickupCatalog.FindPickupIndex("ItemIndex.BarrierOnOverHeal"), PickupCatalog.FindPickupIndex("ItemIndex.AlienHead"), PickupCatalog.FindPickupIndex("ItemIndex.ImmuneToDebuff"), PickupCatalog.FindPickupIndex("ItemIndex.ExtraLife"), PickupCatalog.FindPickupIndex("ItemIndex.NovaOnHeal"), PickupCatalog.FindPickupIndex("ItemIndex.IncreaseHealing"), PickupCatalog.FindPickupIndex("ItemIndex.SprintWisp"), PickupCatalog.FindPickupIndex("ItemIndex.SiphonOnLowHealth"), PickupCatalog.FindPickupIndex("ItemIndex.BeetleGland")};
        PickupIndex[] MPI = { PickupCatalog.FindPickupIndex("ItemIndex.Firework"), PickupCatalog.FindPickupIndex("ItemIndex.Medkit"), PickupCatalog.FindPickupIndex("ItemIndex.AttackSpeedAndMoveSpeed"), PickupCatalog.FindPickupIndex("ItemIndex.TreasureCache"), PickupCatalog.FindPickupIndex("ItemIndex.StickyBomb"), PickupCatalog.FindPickupIndex("ItemIndex.StunChanceOnHit"), PickupCatalog.FindPickupIndex("ItemIndex.BleedOnHit"), PickupCatalog.FindPickupIndex("ItemIndex.WardOnLevel"), PickupCatalog.FindPickupIndex("ItemIndex.Missile"), PickupCatalog.FindPickupIndex("ItemIndex.Feather"), PickupCatalog.FindPickupIndex("ItemIndex.Seed"), PickupCatalog.FindPickupIndex("ItemIndex.Thorns"), PickupCatalog.FindPickupIndex("ItemIndex.RegeneratingScrap"), PickupCatalog.FindPickupIndex("ItemIndex.Squid"), PickupCatalog.FindPickupIndex("ItemIndex.EnergizedOnEquipmentUse"), PickupCatalog.FindPickupIndex("ItemIndex.Clover"), PickupCatalog.FindPickupIndex("ItemIndex.Plant"), PickupCatalog.FindPickupIndex("ItemIndex.MoreMissile"), PickupCatalog.FindPickupIndex("ItemIndex.BounceNearby"), PickupCatalog.FindPickupIndex("ItemIndex.DroneWeapons"), PickupCatalog.FindPickupIndex("ItemIndex.PermanentDebuffOnHit"), PickupCatalog.FindPickupIndex("ItemIndex.HeadHunter"), PickupCatalog.FindPickupIndex("ItemIndex.LightningStrikeOnHit"), PickupCatalog.FindPickupIndex("ItemIndex.RoboBallBuddy"), PickupCatalog.FindPickupIndex("ItemIndex.FireballsOnHit"), PickupCatalog.FindPickupIndex("ItemIndex.ParentEgg")};
        PickupIndex[] JPI = { PickupCatalog.FindPickupIndex("ItemIndex.SecondarySkillMagazine"), PickupCatalog.FindPickupIndex("ItemIndex.HealWhileSafe"), PickupCatalog.FindPickupIndex("ItemIndex.FragileDamageBonus"), PickupCatalog.FindPickupIndex("ItemIndex.NearbyDamageBonus"), PickupCatalog.FindPickupIndex("ItemIndex.Hoof"), PickupCatalog.FindPickupIndex("ItemIndex.HealingPotion"), PickupCatalog.FindPickupIndex("ItemIndex.BarrierOnKill"), PickupCatalog.FindPickupIndex("ItemIndex.SlowOnHit"), PickupCatalog.FindPickupIndex("ItemIndex.EquipmentMagazine"), PickupCatalog.FindPickupIndex("ItemIndex.MoveSpeedOnKill"), PickupCatalog.FindPickupIndex("ItemIndex.Infusion"), PickupCatalog.FindPickupIndex("ItemIndex.FireRing"), PickupCatalog.FindPickupIndex("ItemIndex.IceRing"), PickupCatalog.FindPickupIndex("ItemIndex.FreeChest"), PickupCatalog.FindPickupIndex("ItemIndex.PrimarySkillShuriken"), PickupCatalog.FindPickupIndex("ItemIndex.RandomEquipmentTrigger"), PickupCatalog.FindPickupIndex("ItemIndex.Behemoth"), PickupCatalog.FindPickupIndex("ItemIndex.Icicle"), PickupCatalog.FindPickupIndex("ItemIndex.UtilitySkillMagazine"), PickupCatalog.FindPickupIndex("ItemIndex.Talisman"), PickupCatalog.FindPickupIndex("ItemIndex.ShockNearby"), PickupCatalog.FindPickupIndex("ItemIndex.NovaOnLowHealth")};
        PickupIndex[] RPI = { PickupCatalog.FindPickupIndex("ItemIndex.FlatHealth"), PickupCatalog.FindPickupIndex("ItemIndex.HealWhileSafe"), PickupCatalog.FindPickupIndex("ItemIndex.SprintBonus"), PickupCatalog.FindPickupIndex("ItemIndex.CritGlasses"), PickupCatalog.FindPickupIndex("ItemIndex.Medkit"), PickupCatalog.FindPickupIndex("ItemIndex.AttackSpeedAndMoveSpeed"), PickupCatalog.FindPickupIndex("ItemIndex.Hoof"), PickupCatalog.FindPickupIndex("ItemIndex.Syringe"), PickupCatalog.FindPickupIndex("ItemIndex.SlowOnHit"), PickupCatalog.FindPickupIndex("ItemIndex.EquipmentMagazine"), PickupCatalog.FindPickupIndex("ItemIndex.DeathMark"), PickupCatalog.FindPickupIndex("ItemIndex.BonusGoldPackOnKill"), PickupCatalog.FindPickupIndex("ItemIndex.TPHealingNova"), PickupCatalog.FindPickupIndex("ItemIndex.Phasing"), PickupCatalog.FindPickupIndex("ItemIndex.Thorns"), PickupCatalog.FindPickupIndex("ItemIndex.EnergizedOnEquipmentUse")};

        // G - 27
        // B - 22
        // M - 26
        // J - 22

        public String[] usernames = new string[4];
        public int[] itemAr = { -1, -1, 61, 19, 79, -1, 13, 46, -1, 30, 25, 60, 26, -1, 93, 65, 27, 94, -1, 35, -1, -1, -1, -1, 0, 78, -1, -1, 55, -1, 59, -1, -1, -1, -1, 73, 11, -1, -1, 6, -1, 66, 33, -1, 81, -1, -1, -1, -1, -1, 56, 34, -1, 44, 58, -1, 67, -1, -1, -1, 69, 37, 41, 91, 3, 2, -1, 7, -1, 52, -1, 70, -1, 20, -1, -1, -1, 84, 36, 5, 18, -1, -1, 16, 51, 68, 10, 62, 76, -1, 40, -1, 57, 64, 95, 77, -1, 85, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 12, -1, -1, 86, 29, -1, -1, -1, 75, 38, 4, -1, 9, 74, 88, 15, 92, -1, 82, 17, 45, 72, -1, -1, 53, -1, 63, -1, 49, -1, -1, 87, -1, -1, -1, -1, -1, -1, -1, 1, 42, -1, -1, 83, 90, -1, 32, -1, 50, 8, 48, 89, 54, 23, 39, 24, 22, 43, 80, -1, -1, -1, 47, -1, -1, 14, 21, -1, -1, 71, -1, -1, -1, 31, 28 };


        public override CharacterMaster GetTarget(UnityEngine.Vector3 Position, PickupIndex pickupIndex, TargetFilter extraFilter = null)
        {
            foreach (var item in RPI)
            {
                if (item == pickupIndex)
                {
                    return targets[UnityEngine.Random.Range(0, targets.Length)];
                }
            }
            String goesTo = "Err";
            foreach (var item in GPI)
            {
                if (item == pickupIndex)
                {
                    goesTo = "Monkeytoes999";
                }
            }
            foreach (var item in BPI)
            {
                if (item == pickupIndex)
                {
                    goesTo = "calculatorismyfriend";
                }
            }
            foreach (var item in MPI)
            {
                if (item == pickupIndex)
                {
                    goesTo = "Onyx";
                    //goesTo = "Monkeytoes999";
                }
            }
            foreach (var item in JPI)
            {
                if (item == pickupIndex)
                {
                    goesTo = "SpyseaRice";
                    //goesTo = "ERR";
                }
            }
            Log.Info(goesTo);
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

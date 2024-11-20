/**
using System.Collections.Generic;
using RoR2;
using System;

namespace AutoItemPickup.ItemDistributors
{
    class Sequence : ItemDistributor
    {
        public Sequence(AutoItemPickup plugin) : base(plugin) { }

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

        public override void DistributeItem(GenericPickupController item)
        {
            CharacterMaster target = GetTarget(item.transform.position, item.pickupIndex);
            UnityEngine.Object.Destroy(item.gameObject);
        }


        public String[] usernames = new string[4];
        Random rand = new Random();
        float time = float.MaxValue;


        PickupIndex[] whites = { PickupCatalog.FindPickupIndex("ItemIndex.Crowbar"), PickupCatalog.FindPickupIndex("ItemIndex.SprintBonus"), PickupCatalog.FindPickupIndex("ItemIndex.IgniteOnKill"), PickupCatalog.FindPickupIndex("ItemIndex.Tooth"), PickupCatalog.FindPickupIndex("ItemIndex.OutOfCombatArmor"), PickupCatalog.FindPickupIndex("ItemIndex.PersonalShield"), PickupCatalog.FindPickupIndex("ItemIndex.Syringe"), PickupCatalog.FindPickupIndex("ItemIndex.BossDamageBonus"), PickupCatalog.FindPickupIndex("ItemIndex.FlatHealth"), PickupCatalog.FindPickupIndex("ItemIndex.Mushroom"), PickupCatalog.FindPickupIndex("ItemIndex.GoldOnHurt"), PickupCatalog.FindPickupIndex("ItemIndex.BarrierOnKill"), PickupCatalog.FindPickupIndex("ItemIndex.CritGlasses"), PickupCatalog.FindPickupIndex("ItemIndex.FragileDamageBonus"), PickupCatalog.FindPickupIndex("ItemIndex.NearbyDamageBonus"), PickupCatalog.FindPickupIndex("ItemIndex.Hoof"), PickupCatalog.FindPickupIndex("ItemIndex.HealingPotion"), PickupCatalog.FindPickupIndex("ItemIndex.SecondarySkillMagazine"), PickupCatalog.FindPickupIndex("ItemIndex.HealWhileSafe"), PickupCatalog.FindPickupIndex("ItemIndex.ArmorPlate"), PickupCatalog.FindPickupIndex("ItemIndex.Bear"), PickupCatalog.FindPickupIndex("ItemIndex.Firework"), PickupCatalog.FindPickupIndex("ItemIndex.Medkit"), PickupCatalog.FindPickupIndex("ItemIndex.AttackSpeedAndMoveSpeed"), PickupCatalog.FindPickupIndex("ItemIndex.TreasureCache"), PickupCatalog.FindPickupIndex("ItemIndex.StickyBomb"), PickupCatalog.FindPickupIndex("ItemIndex.StunChanceOnHit"), PickupCatalog.FindPickupIndex("ItemIndex.BleedOnHit"), PickupCatalog.FindPickupIndex("ItemIndex.WardOnLevel"), PickupCatalog.FindPickupIndex("ItemIndex.FlatHealth"), PickupCatalog.FindPickupIndex("ItemIndex.HealWhileSafe"), PickupCatalog.FindPickupIndex("ItemIndex.SprintBonus"), PickupCatalog.FindPickupIndex("ItemIndex.CritGlasses"), PickupCatalog.FindPickupIndex("ItemIndex.Medkit"), PickupCatalog.FindPickupIndex("ItemIndex.AttackSpeedAndMoveSpeed"), PickupCatalog.FindPickupIndex("ItemIndex.Hoof"), PickupCatalog.FindPickupIndex("ItemIndex.Syringe") };
        PickupIndex[] greens = { PickupCatalog.FindPickupIndex("ItemIndex.Bandolier"), PickupCatalog.FindPickupIndex("ItemIndex.WarCryOnMultiKill"), PickupCatalog.FindPickupIndex("ItemIndex.BonusGoldPackOnKill"), PickupCatalog.FindPickupIndex("ItemIndex.HealOnCrit"), PickupCatalog.FindPickupIndex("ItemIndex.StrengthenBurn"), PickupCatalog.FindPickupIndex("ItemIndex.Phasing"), PickupCatalog.FindPickupIndex("ItemIndex.AttackSpeedOnCrit"), PickupCatalog.FindPickupIndex("ItemIndex.ChainLightning"), PickupCatalog.FindPickupIndex("ItemIndex.ExplodeOnDeath"), PickupCatalog.FindPickupIndex("ItemIndex.SlowOnHit"), PickupCatalog.FindPickupIndex("ItemIndex.EquipmentMagazine"), PickupCatalog.FindPickupIndex("ItemIndex.MoveSpeedOnKill"), PickupCatalog.FindPickupIndex("ItemIndex.Infusion"), PickupCatalog.FindPickupIndex("ItemIndex.FireRing"), PickupCatalog.FindPickupIndex("ItemIndex.IceRing"), PickupCatalog.FindPickupIndex("ItemIndex.FreeChest"), PickupCatalog.FindPickupIndex("ItemIndex.PrimarySkillShuriken"), PickupCatalog.FindPickupIndex("ItemIndex.DeathMark"), PickupCatalog.FindPickupIndex("ItemIndex.TPHealingNova"), PickupCatalog.FindPickupIndex("ItemIndex.ExecuteLowHealthElite"), PickupCatalog.FindPickupIndex("ItemIndex.SprintOutOfCombat"), PickupCatalog.FindPickupIndex("ItemIndex.SprintArmor"), PickupCatalog.FindPickupIndex("ItemIndex.JumpBoost"), PickupCatalog.FindPickupIndex("ItemIndex.SlowOnHit"), PickupCatalog.FindPickupIndex("ItemIndex.EquipmentMagazine"), PickupCatalog.FindPickupIndex("ItemIndex.DeathMark"), PickupCatalog.FindPickupIndex("ItemIndex.BonusGoldPackOnKill"), PickupCatalog.FindPickupIndex("ItemIndex.TPHealingNova"), PickupCatalog.FindPickupIndex("ItemIndex.Phasing"), PickupCatalog.FindPickupIndex("ItemIndex.Thorns"), PickupCatalog.FindPickupIndex("ItemIndex.EnergizedOnEquipmentUse"), PickupCatalog.FindPickupIndex("ItemIndex.Missile"), PickupCatalog.FindPickupIndex("ItemIndex.Feather"), PickupCatalog.FindPickupIndex("ItemIndex.Seed"), PickupCatalog.FindPickupIndex("ItemIndex.Thorns"), PickupCatalog.FindPickupIndex("ItemIndex.RegeneratingScrap"), PickupCatalog.FindPickupIndex("ItemIndex.Squid"), PickupCatalog.FindPickupIndex("ItemIndex.EnergizedOnEquipmentUse") };
        PickupIndex[] reds = { PickupCatalog.FindPickupIndex("ItemIndex.KillEliteFrenzy"), PickupCatalog.FindPickupIndex("ItemIndex.NovaOnHeal"), PickupCatalog.FindPickupIndex("ItemIndex.Clover"), PickupCatalog.FindPickupIndex("ItemIndex.Plant"), PickupCatalog.FindPickupIndex("ItemIndex.MoreMissile"), PickupCatalog.FindPickupIndex("ItemIndex.BounceNearby"), PickupCatalog.FindPickupIndex("ItemIndex.DroneWeapons"), PickupCatalog.FindPickupIndex("ItemIndex.PermanentDebuffOnHit"), PickupCatalog.FindPickupIndex("ItemIndex.HeadHunter"), PickupCatalog.FindPickupIndex("ItemIndex.IncreaseHealing"), PickupCatalog.FindPickupIndex("ItemIndex.RandomEquipmentTrigger"), PickupCatalog.FindPickupIndex("ItemIndex.Behemoth"), PickupCatalog.FindPickupIndex("ItemIndex.Icicle"), PickupCatalog.FindPickupIndex("ItemIndex.UtilitySkillMagazine"), PickupCatalog.FindPickupIndex("ItemIndex.Talisman"), PickupCatalog.FindPickupIndex("ItemIndex.ShockNearby"),  PickupCatalog.FindPickupIndex("ItemIndex.Dagger"), PickupCatalog.FindPickupIndex("ItemIndex.FallBoots"), PickupCatalog.FindPickupIndex("ItemIndex.GhostOnKill"), PickupCatalog.FindPickupIndex("ItemIndex.CritDamage"), PickupCatalog.FindPickupIndex("ItemIndex.LaserTurbine"), PickupCatalog.FindPickupIndex("ItemIndex.ArmorReductionOnHit"), PickupCatalog.FindPickupIndex("ItemIndex.BarrierOnOverHeal"), PickupCatalog.FindPickupIndex("ItemIndex.AlienHead"), PickupCatalog.FindPickupIndex("ItemIndex.ImmuneToDebuff"), PickupCatalog.FindPickupIndex("ItemIndex.ExtraLife"), };
        PickupIndex[] yellows = { PickupCatalog.FindPickupIndex("ItemIndex.Pearl"), PickupCatalog.FindPickupIndex("ItemIndex.ShinyPearl"), PickupCatalog.FindPickupIndex("ItemIndex.BeetleGland"), PickupCatalog.FindPickupIndex("ItemIndex.LightningStrikeOnHit"), PickupCatalog.FindPickupIndex("ItemIndex.RoboBallBuddy"), PickupCatalog.FindPickupIndex("ItemIndex.FireballsOnHit"), PickupCatalog.FindPickupIndex("ItemIndex.ParentEgg"), PickupCatalog.FindPickupIndex("ItemIndex.MinorConstructOnKill"), PickupCatalog.FindPickupIndex("ItemIndex.FireballsOnHit"), PickupCatalog.FindPickupIndex("ItemIndex.BleedOnHitAndExplode"), PickupCatalog.FindPickupIndex("ItemIndex.Knurl"), PickupCatalog.FindPickupIndex("ItemIndex.SprintWisp"), PickupCatalog.FindPickupIndex("ItemIndex.NovaOnLowHealth"), PickupCatalog.FindPickupIndex("ItemIndex.SiphonOnLowHealth") };
        String[] Bert = new String[4];//{ "StickyBomb", "RegeneratingScrap", "IncreaseHealing", "FireballsOnHit" };
        String[] Matthew = { "Bear", "ChainLightning", "PermanentDebuffOnHit", "LightningStrikeOnHit" };
        String[] Graham = new String[4];//{ "AttackSpeedAndMoveSpeed", "Seed", "BounceNearby", "BleedOnHitAndExplode" };
        String[] Jon = { "BarrierOnKill", "Thorns", "Clover", "ParentEgg" };//new String[4];

        public override CharacterMaster GetTarget(UnityEngine.Vector3 Position, PickupIndex pickupIndex, TargetFilter extraFilter = null)
        {
            if (targets[0]._internalSurvivalTime < time)
            {
                String[] Bert = new String[4];//{ "StickyBomb", "RegeneratingScrap", "IncreaseHealing", "FireballsOnHit" };
                String[] Matthew = { "Bear", "ChainLightning", "PermanentDebuffOnHit", "LightningStrikeOnHit" };
                String[] Graham = new String[4];//{ "AttackSpeedAndMoveSpeed", "Seed", "BounceNearby", "BleedOnHitAndExplode" };
                String[] Jon = { "BarrierOnKill", "Thorns", "Clover", "ParentEgg" };//new String[4];
            }
            time = targets[0]._internalSurvivalTime;
           
            String goesTo = "Err";
            if (Jon[0] == null)
            {
                Jon[0] = whites[UnityEngine.Random.Range(0, whites.Length)].ToString().Substring(10);
                Jon[1] = greens[UnityEngine.Random.Range(0, greens.Length)].ToString().Substring(10);
                Jon[2] = reds[UnityEngine.Random.Range(0, reds.Length)].ToString().Substring(10);
                Jon[3] = yellows[UnityEngine.Random.Range(0, yellows.Length)].ToString().Substring(10);
                Log.Info(Jon[0]);
                Log.Info(Jon[1]);
                Log.Info(Jon[2]);
                Log.Info(Jon[3]);
            }

            if (Graham[0] == null)
            {
                Graham[0] = whites[UnityEngine.Random.Range(0, whites.Length)].ToString().Substring(10);
                Graham[1] = greens[UnityEngine.Random.Range(0, greens.Length)].ToString().Substring(10);
                Graham[2] = reds[UnityEngine.Random.Range(0, reds.Length)].ToString().Substring(10);
                Graham[3] = yellows[UnityEngine.Random.Range(0, yellows.Length)].ToString().Substring(10);
                Log.Info(Graham[0]);
                Log.Info(Graham[1]);
                Log.Info(Graham[2]);
                Log.Info(Graham[3]);
            }

            if (Bert[0] == null)
            {
                Bert[0] = whites[UnityEngine.Random.Range(0, whites.Length)].ToString().Substring(10);
                Bert[1] = greens[UnityEngine.Random.Range(0, greens.Length)].ToString().Substring(10);
                Bert[2] = reds[UnityEngine.Random.Range(0, reds.Length)].ToString().Substring(10);
                Bert[3] = yellows[UnityEngine.Random.Range(0, yellows.Length)].ToString().Substring(10);
                Log.Info(Bert[0]);
                Log.Info(Bert[1]);
                Log.Info(Bert[2]);
                Log.Info(Bert[3]);
            }

            if (Matthew[0] == null)
            {
                Matthew[0] = whites[UnityEngine.Random.Range(0, whites.Length)].ToString().Substring(10);
                Matthew[1] = greens[UnityEngine.Random.Range(0, greens.Length)].ToString().Substring(10);
                Matthew[2] = reds[UnityEngine.Random.Range(0, reds.Length)].ToString().Substring(10);
                Matthew[3] = yellows[UnityEngine.Random.Range(0, yellows.Length)].ToString().Substring(10);
                Log.Info(Matthew[0]);
                Log.Info(Matthew[1]);
                Log.Info(Matthew[2]);
                Log.Info(Matthew[3]);
            }

            int tier = 0;
            foreach (var white in whites)
            {
                if (white == pickupIndex)
                {
                    tier = 0;
                }
            }
            foreach (var green in greens)
            {
                if (green == pickupIndex)
                {
                    tier = 1;
                }
            }
            foreach (var red in reds)
            {
                if (red == pickupIndex)
                {
                    tier = 2;
                }
            }
            foreach (var yellow in yellows)
            {
                if (yellow == pickupIndex)
                {
                    tier = 3;
                }
            }
            int val = rand.Next(1, 3);
            if (val == 0)
            {
                //goesTo = "Monkeytoes999";
                goesTo = "Onyx";
            }
            else if (val == 1)
            {
                //goesTo = "Monkeytoes999";
                goesTo = "calculatorismyfriend";
                //goesTo = "SpyseaRice";
            }
            else if (val == 2)
            {
                goesTo = "Monkeytoes999";
            }
            else if (val == 3)
            {
                //goesTo = "Monkeytoes999";
                goesTo = "calculatorismyfriend";
            }
            var i = 0;
            foreach (var player in targets)
            {
                if (player.IsDeadAndOutOfLivesServer())
                {
                    if (usernames[i] == goesTo)
                    {
                        if (goesTo == "Onyx")
                        {
                            ItemDef item = ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex(Matthew[tier]));
                            player.inventory.GiveItem(item);
                            GenericPickupController.SendPickupMessage(player, PickupCatalog.FindPickupIndex("ItemIndex." + Matthew[tier]));
                        }
                        else if (goesTo == "SpyseaRice")
                        {
                            ItemDef item = ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex(Jon[tier]));
                            player.inventory.GiveItem(item);
                            GenericPickupController.SendPickupMessage(player, PickupCatalog.FindPickupIndex("ItemIndex." + Jon[tier]));
                        }
                        else if (goesTo == "Monkeytoes999")
                        {
                            ItemDef item = ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex(Graham[tier]));
                            player.inventory.GiveItem(item);
                            GenericPickupController.SendPickupMessage(player, PickupCatalog.FindPickupIndex("ItemIndex." + Graham[tier]));
                        }
                        else if (goesTo == "calculatorismyfriend")
                        {
                            ItemDef item = ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex(Bert[tier]));
                            player.inventory.GiveItem(item);
                            GenericPickupController.SendPickupMessage(player, PickupCatalog.FindPickupIndex("ItemIndex." + Bert[tier]));
                        }
                    }
                }
                else
                {
                    usernames[i] = player.GetBody().GetUserName();
                    if (player.GetBody().GetUserName() == goesTo)
                    {
                        if (usernames[i] == goesTo)
                        {
                            if (goesTo == "Onyx")
                            {
                                ItemDef item = ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex(Matthew[tier]));
                                player.inventory.GiveItem(item);
                                GenericPickupController.SendPickupMessage(player, PickupCatalog.FindPickupIndex("ItemIndex." + Matthew[tier]));
                            }
                            else if (goesTo == "SpyseaRice")
                            {
                                ItemDef item = ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex(Jon[tier]));
                                player.inventory.GiveItem(item);
                                GenericPickupController.SendPickupMessage(player, PickupCatalog.FindPickupIndex("ItemIndex." + Jon[tier]));
                            }
                            else if (goesTo == "Monkeytoes999")
                            {
                                ItemDef item = ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex(Graham[tier]));
                                player.inventory.GiveItem(item);
                                GenericPickupController.SendPickupMessage(player, PickupCatalog.FindPickupIndex("ItemIndex." + Graham[tier]));
                            }
                            else if (goesTo == "calculatorismyfriend")
                            {
                                ItemDef item = ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex(Bert[tier]));
                                player.inventory.GiveItem(item);
                                GenericPickupController.SendPickupMessage(player, PickupCatalog.FindPickupIndex("ItemIndex." + Bert[tier]));
                            }
                        }
                    }
                }
                i++;
            }
            return null;
        }
    }
}
**/
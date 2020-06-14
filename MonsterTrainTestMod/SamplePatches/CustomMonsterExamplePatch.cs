using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using MonsterTrainModdingAPI.Builders;
using MonsterTrainModdingAPI.Managers;
using MonsterTrainModdingAPI.Utilities;
using MonsterTrainModdingAPI.Enums.MTCardPools;
using System.Linq;
using UnityEngine;

namespace MonsterTrainTestMod.SamplePatches
{
    [HarmonyPatch(typeof(SaveManager), "SetupRun")]
    class AddToStartingDeck
    {
        // Creates a 0-cost 3/4 with Train Steward's card art
        static void Postfix(ref SaveManager __instance)
        {
            __instance.AddCardToDeck(CustomCardManager.GetCardDataByID("io.github.crazyjackel.SI"));
        }
    }

    class BlueEyesDataCreator
    {
        public static void RegisterCard()
        {
            new CardDataBuilder
            {
                CardID = "io.github.crazyjackel.SI",
                Name = "Shark Imp",
                Cost = 0,
                CardType = CardType.Monster,
                TargetsRoom = true,
                Targetless = false,
                BundleLoadingInfo = new AssetBundleLoadingInfo("testbundle", "SharkImp"),
                CardPoolIDs = new List<string> { MTCardPoolIDs.GetIDForType(typeof(MTCardPool_UnitsAllBanner)) },
                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectSpawnMonster",
                        TargetMode = TargetMode.DropTargetCharacter,
                        ParamCharacterDataBuilder = new CharacterDataBuilder
                        {
                            CharacterID = "io.github.crazyjackel.BI_C",
                            Name = "Blue-Eyes White Dragon",
                            Size = 5,
                            Health = 2500,
                            AttackDamage = 3000,
                            SkeletonAnimationBundleLoadingInfo = new AssetBundleLoadingInfo("skeletondata","SkeleAnim")
                        }
                    }
                },
                EffectTriggerBuilders = new List<CharacterTriggerDataBuilder>
                {
                    new CharacterTriggerDataBuilder
                    {
                        Trigger = CharacterTriggerData.Trigger.OnAttacking,
                        EffectBuilders = new List<CardEffectDataBuilder>
                        {
                            new CardEffectDataBuilder
                            {
                                EffectStateName = "CardEffectAddStatusEffect",
                                TargetMode = TargetMode.Self
                            }
                        }
                    }
                }
            }.BuildAndRegister();
        }
    }
}

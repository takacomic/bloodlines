using Bloodlines.src;
using HarmonyLib;
using Il2CppNewtonsoft.Json;
using Il2CppNewtonsoft.Json.Linq;
using Il2CppVampireSurvivors.Data;
using Il2CppVampireSurvivors.Data.Characters;
using Il2CppVampireSurvivors.Framework;
using Il2CppVampireSurvivors.Framework.NumberTypes;
using Il2CppVampireSurvivors.Graphics;
using Il2CppVampireSurvivors.Objects;
using Il2CppVampireSurvivors.Objects.Characters;
using Il2CppVampireSurvivors.Objects.Items;
using Il2CppVampireSurvivors.Objects.Pickups;
using Il2CppVampireSurvivors.UI;
using MelonLoader;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Il2Generic = Il2CppSystem.Collections.Generic;

namespace Bloodlines
{
    public static class ModInfo
    {
        public const string Name = "Bloodlines";
        public const string Description = "Easily add custom characters!";
        public const string Author = "Nick, Takacomic";
        public const string Company = "CorruptedInfluences";
        public const string Version = "0.2.2";
        public const string Download = "https://github.com/takacomic/bloodlines";
    }

    public class BloodlinesMod : MelonMod
    {
        public static readonly string ModDirectory = Path.Combine(Directory.GetCurrentDirectory(), "UserData", "Bloodlines");
        public static readonly string DataDirectory = Path.Combine(ModDirectory, "data");
        public Config Config { get; private set; }
        public CharacterManager manager { get; private set; }

        static DataManager dataManager;
        static GameManager gameManager;
        static BaseSpriteAnimation baseSpriteAnimation;
        static SpriteManager spriteManager;
        public static bool isDebug()
        {
#if DEBUG
            return true;
#endif
            return false;
        }

        public override void OnInitializeMelon()
        {
            if (!Directory.Exists(ModDirectory))
            {
                Directory.CreateDirectory(ModDirectory);
                Directory.CreateDirectory(DataDirectory);
            }

            Config = new Config(Path.Combine(ModDirectory, "config.cfg"), "Bloodlines");
            manager = new(ModDirectory, DataDirectory, Path.Combine(DataDirectory, "characters"));
        }

        public static CharacterManager getCharacterManager() => Melon<BloodlinesMod>.Instance.manager;

        public static bool isCustomCharacter(CharacterType characterType)
        {
            return getCharacterManager().characterDict.ContainsKey(characterType);
        }

        [HarmonyPatch("Il2CppInterop.HarmonySupport.Il2CppDetourMethodPatcher", "ReportException")]
        public static class Patch_Il2CppDetourMethodPatcher
        {
            public static bool Prefix(Exception ex)
            {
                MelonLogger.Error("During invoking native->managed trampoline", ex);

                if (ex.Data.Count > 0)
                {
                    MelonLogger.Error("Extra Data:");

                    foreach (DictionaryEntry de in ex.Data)
                        MelonLogger.Error("    Key: {0,-20}      Value: {1}",
                                          "'" + de.Key.ToString() + "'", de.Value);
                }

                return false;
            }
        }

        internal static JsonSerializerSettings serializerSettings = new()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore
        };

        static JArray ListToJArray(Il2Generic.List<CharacterData> list)
        {
            string result = JsonConvert.SerializeObject(list, serializerSettings);
            return JArray.Parse(result);
        }

        public static Timer _Timer;

        public static void TimerCallback(object stateInfo)
        {
            if (gameManager != null && gameManager.PlayerOne != null && gameManager.PlayerOne.PlayerStats != null)
            {
                PlayerModifierStats stats = gameManager.PlayerOne.PlayerStats;
                PropertyInfo[] statsProps = stats.GetType().GetProperties();
                System.Collections.Generic.List<string> ignoreFileds = new() { "ObjectClass", "Pointer", "WasCollected" };

                Melon<BloodlinesMod>.Logger.Msg("\n==============================\n");

                foreach (PropertyInfo prop in statsProps)
                {
                    if (prop.Name.Contains("BackingField") || ignoreFileds.Contains(prop.Name))
                        continue;

                    if (prop.PropertyType == typeof(EggFloat))
                        Melon<BloodlinesMod>.Logger
                            .Msg($"{prop.Name} = Value: <{(prop.GetValue(stats) as EggFloat).GetValue()}> EggValue: <{(prop.GetValue(stats) as EggFloat).GetEggValue()}>");
                    else if (prop.PropertyType == typeof(EggDouble))
                        Melon<BloodlinesMod>.Logger
                            .Msg($"{prop.Name} = Value: <{(prop.GetValue(stats) as EggDouble).GetValue()}> EggValue: <{(prop.GetValue(stats) as EggDouble).GetEggValue()}>");
                    else
                        Melon<BloodlinesMod>.Logger.Msg($"{prop.Name} = <{prop.GetValue(stats)}>");
                }
            }
        }

        [HarmonyPatch(typeof(Pickup))]
        class Pickup_Patch
        {
            // Bugfix: Allows gems to be picked up, even if the character is a big boy.
            [HarmonyPatch(nameof(Pickup.GoToThePlayer))]
            [HarmonyPostfix]
            static void GoToThePlayer_Postix(Gem __instance, MethodBase __originalMethod)
            {
                if (BloodlinesMod.isCustomCharacter(__instance.TargetPlayer._characterType))
                {
                    float distance = Vector2.Distance(__instance.position, __instance.TargetPlayer.position);
                    float closeEnough = 0.09f;

                    if (distance < closeEnough || float.IsInfinity(distance))
                    {
                        __instance.GetTaken();
                    }
                }
            }
        }

        [HarmonyPatch(typeof(GameManager))]
        class GameManager_Patch
        {
            [HarmonyPatch(nameof(GameManager.Construct))]
            [HarmonyPrefix]
            static void Construct_Prefix(GameManager __instance)
            {
                gameManager = __instance;
            }

            // InitializeGameSession
            [HarmonyPatch(nameof(GameManager.InitializeGameSession))]
            [HarmonyPostfix]
            static void InitializeGameSession_Postfix(GameManager __instance)
            {
                Melon<BloodlinesMod>.Logger.Msg($"GameManager.{MethodBase.GetCurrentMethod()?.Name}");
                if(isDebug()) BloodlinesMod._Timer = new Timer(TimerCallback, null, 0, 10000); // List stats every 10 seconds.
            }
        }

        [HarmonyPatch(typeof(RecapPage))]
        class RecapPage_Patch
        {
            [HarmonyPatch(nameof(RecapPage.OnShowStart))]
            [HarmonyPostfix]
            static void OnShowStart_Postfix(RecapPage __instance)
            {
                if (isDebug() && BloodlinesMod._Timer != null)
                {
                    BloodlinesMod._Timer.Dispose();
                    BloodlinesMod._Timer = null;
                }
            }
        }

        [HarmonyPatch(typeof(DataManager))]
        class DataManager_Patch
        {
            [HarmonyPatch(nameof(DataManager.Initialize))]
            [HarmonyPrefix]
            static void Initialize_Prefix(DataManager __instance)
            {
                dataManager = __instance;
            }

            [HarmonyPatch(nameof(DataManager.LoadBaseJObjects))]
            [HarmonyPostfix]
            static void LoadBaseJObjects_Postfix(DataManager __instance, object[] __args, MethodBase __originalMethod)
            {
                CharacterType iter = Enum.GetValues<CharacterType>().Max() + 1;

                foreach (CharacterDataModelWrapper characterWrapper in Melon<BloodlinesMod>.Instance.manager.characters)
                {
                    CharacterDataModel character = characterWrapper.Character;
                    CharacterType characterType = iter++;
                    characterWrapper.characterType = characterType;
                    Melon<BloodlinesMod>.Logger.Msg($"Adding character... {characterType} {character.CharName}");
                    character.CharacterType = characterType;

                    int skinNum = characterWrapper.SkinTypeInt(character.Skins[0].Id);

                    Sprite sprite = SpriteImporter.LoadSprite(characterWrapper.SkinPath(skinNum));
                    SpriteManager.RegisterSprite(sprite);

                    string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(characterWrapper.CharacterSettings, Newtonsoft.Json.Formatting.Indented,
                        new Newtonsoft.Json.JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                        });

                    JArray json = JArray.Parse(jsonString);
                    __instance._allCharactersJson.Add(characterType.ToString(), json);
                    Melon<BloodlinesMod>.Instance.manager.characterDict.Add(characterType, characterWrapper);
                }
            }
        }

        [HarmonyPatch(typeof(CharacterSelectionPage))]
        class CharacterSelectionPage_Patch
        {
            [HarmonyPatch(nameof(CharacterSelectionPage.ShowCharacterInfo))]
            [HarmonyPostfix]
            static void ShowCharacterInfo_Postfix(CharacterSelectionPage __instance, CharacterData charData, CharacterType cType, CharacterItemUI character, MethodBase __originalMethod)
            {
                if (isCustomCharacter(cType))
                {
                    /*Melon<BloodlinesMod>.Logger.Msg($"Setting the icon for {cType}");
                    CharacterDataModelWrapper ch = getCharacterManager().characterDict[cType];
                    int activeSkinIndex = __instance._skinSlots.FindIndex(new Func<Image, bool>((s) => s.sprite.name == "weaponLevelFull"));

                    if (activeSkinIndex == -1)
                    {
                        activeSkinIndex = 0;
                    }

                    Sprite sprite = SpriteImporter.LoadSprite(ch.SkinPath(activeSkinIndex));

                    __instance.Icon.sprite = sprite;
                    __instance._Name.text = charData.GetFullNameUntranslated();
                    __instance.Description.text = charData.description;
                    __instance.StatsPanel.SetCharacter(charData, cType);
                    __instance._EggCount.text = charData.exLevels.ToString();
                    __instance.SetWeaponIconSprite(charData);
                    __instance._selectedCharacterItemUI = character;
                    RectTransform CharacterInfoIconRectTransform = __instance.transform.FindChild("Panel/InfoPanel/Background/CharacterImage").GetComponent<RectTransform>();

                    int width = sprite.texture.width;
                    int height = sprite.texture.height;

                    // Resize to fit the info box better.
                    int long_side = width > height ? width : height;
                    int delta = 100 - long_side;

                    CharacterInfoIconRectTransform.sizeDelta = new Vector2(width + delta, height + delta);*/
                }
            }
        }
    }
}
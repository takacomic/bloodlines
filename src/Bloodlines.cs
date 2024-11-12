using Bloodlines.src.DataModels;
using Bloodlines.src.JsonModels;
using HarmonyLib;
using Il2CppNewtonsoft.Json;
using Il2CppNewtonsoft.Json.Linq;
using Il2CppSystem.Collections.Generic;
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
using Il2CppI2.Loc;
using MelonLoader;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using UnityEngine;
using static MelonLoader.MelonLogger;
using Il2Generic = Il2CppSystem.Collections.Generic;
using Il2CppVampireSurvivors.Data.Weapons;

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
                CharacterData characterData = gameManager.PlayerOne.CurrentSkinData;
                PropertyInfo[] dataProps = characterData.GetType().GetProperties();
                //System.Collections.Generic.List<string> ignoreFileds = new() { "ObjectClass", "Pointer", "WasCollected"/*, "walkFrameRate", "startFrameCount", "zeroPad", "frameRate", "bodyOffset"*/, "requiresRelic" };  CharacterData
                System.Collections.Generic.List<string> ignoreFileds = new() { "ObjectClass", "Pointer", "WasCollected", "walkFrameRate", "startFrameCount", "zeroPad", "frameRate", "bodyOffset", "requiresRelic", "startingWeapon" };

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

                Melon<BloodlinesMod>.Logger.Msg("\n==============================\n");
                Melon<BloodlinesMod>.Logger.Msg("\n==============================\n");

                foreach (PropertyInfo prop in dataProps)
                {
                    if (prop.Name.Contains("BackingField") || ignoreFileds.Contains(prop.Name))
                        continue;
                    Melon<BloodlinesMod>.Logger.Msg($"{prop.Name} = <{prop.GetValue(characterData)}>");
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
                    character.CharacterType = characterType;
                    
                    Melon<BloodlinesMod>.Logger.Msg($"Adding character... {characterType} {character.CharName}");

                    if (Melon<BloodlinesMod>.Instance.manager.sprites.Count == 0)
                    {
                        int skinNum = characterWrapper.SkinTypeInt(character.Skins[0].SkinType);
                        Sprite sprite = SpriteImporter.LoadSprite(characterWrapper.SkinPath(skinNum));
                        SpriteManager.RegisterSprite(sprite);
                    }

                    string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(characterWrapper.CharacterSettings, Newtonsoft.Json.Formatting.Indented,
                        new Newtonsoft.Json.JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                        });

                    JArray json = JArray.Parse(jsonString);
                    __instance._allCharactersJson.Add(characterType.ToString(), json);
                    Melon<BloodlinesMod>.Instance.manager.characterDict.Add(characterType, characterWrapper);
                }

                foreach (SpriteDataModelWrapper spriteWrapper in Melon<BloodlinesMod>.Instance.manager.sprites)
                {
                    Texture2D texture = SpriteImporter.LoadTexture(spriteWrapper.TexturePath);
                    texture.name = spriteWrapper.TextureNameWithoutExtension;
                    Sprite sprite = SpriteImporter.LoadSprite(texture, spriteWrapper.Sprite.Rect, spriteWrapper.Sprite.Pivot);
                    sprite.name = spriteWrapper.SpriteNameWithoutExtension;
                    SpriteManager.RegisterSprite(sprite);
                }
                /*


                StringBuilder everything = new StringBuilder();
                StringBuilder passives = new StringBuilder();
                StringBuilder baseWeaps = new StringBuilder();
                StringBuilder weaps = new StringBuilder();
                StringBuilder counters = new StringBuilder();
                foreach (WeaponType weaponType in Enum.GetValues<WeaponType>())
                {
                    //JObject jObject = new JObject(__instance._allWeaponDataJson);
                    //JToken jToken = jObject.GetValue(weaponType.ToString());
                    //Melon<BloodlinesMod>.Logger.Msg(jToken);
                    WeaponData weaponData = new WeaponData();
                    if (!LocalizationManager.GetTranslation(weaponData.GetLocalizedNameTerm(weaponType), true, 0, true, false, null, null, true).Contains("weaponLang/"))
                    {
                        /*bool isPowerUp = false;
                        string evoInto = "";
                        bool isSpecialOnly = false;
                        foreach (PropertyInfo prop in props)
                        {
                            if (prop.Name == "isPowerUp") isPowerUp = prop.GetValue(dict[weaponType], null);
                        }
                        everything.Append($"\"{weaponType}\", ");
                        if () passives.Append($"\"{weaponType}\", ");
                        if (!weap.isPowerUp && (weap.evoInto != null || weap.evoInto == "")) baseWeaps.Append($"\"{weaponType}\", ");
                        if (weaponType.ToString().Contains("COUNTER")) counters.Append($"\"{weaponType}\", ");
                        if (!weap.isSpecialOnly && !weap.isPowerUp) weaps.Append($"\"{weaponType}\", ");
                    }
                }
                Melon<BloodlinesMod>.Logger.Msg(everything);
                Melon<BloodlinesMod>.Logger.Msg("=================================");
                Melon<BloodlinesMod>.Logger.Msg("=================================");
                Melon<BloodlinesMod>.Logger.Msg("=================================");
                Melon<BloodlinesMod>.Logger.Msg(passives);
                Melon<BloodlinesMod>.Logger.Msg("=================================");
                Melon<BloodlinesMod>.Logger.Msg("=================================");
                Melon<BloodlinesMod>.Logger.Msg("=================================");
                Melon<BloodlinesMod>.Logger.Msg(baseWeaps);
                Melon<BloodlinesMod>.Logger.Msg("=================================");
                Melon<BloodlinesMod>.Logger.Msg("=================================");
                Melon<BloodlinesMod>.Logger.Msg("=================================");
                Melon<BloodlinesMod>.Logger.Msg(weaps);
                Melon<BloodlinesMod>.Logger.Msg("=================================");
                Melon<BloodlinesMod>.Logger.Msg("=================================");
                Melon<BloodlinesMod>.Logger.Msg("=================================");
                Melon<BloodlinesMod>.Logger.Msg(counters);
                Melon<BloodlinesMod>.Logger.Msg("=================================");
                Melon<BloodlinesMod>.Logger.Msg("=================================");
                Melon<BloodlinesMod>.Logger.Msg("=================================");*/
            }
        }

        [HarmonyPatch(typeof(CharacterController))]
        class CharacterController_Patch
        {
            [HarmonyPatch(nameof(CharacterController.InitCharacter))]
            [HarmonyPostfix]
            static void InitCharacter(CharacterController __instance, CharacterType characterType)
            {
                foreach (AnimDataModelWrapper animWrapper in Melon<BloodlinesMod>.Instance.manager.anims)
                {
                    if (!isCustomCharacter(characterType)) break;
                    PropertyInfo[] animProps = animWrapper.Anim.GetType().GetProperties();
                    foreach (PropertyInfo prop in animProps)
                    {
                        var value = prop.GetValue(animWrapper.Anim, null);
                        if (value != null)
                        {
                            AnimObjectModel value2 = (AnimObjectModel)value;
                            List<Sprite> sprites = new();
                            foreach (string name in value2.Sprites)
                            {
                                sprites.Add(SpriteManager.GetSprite(name));
                            }

                            __instance.Anims.AddAnimation(prop.Name.ToString().ToLower(), sprites, value2.Fps, value2.Loops);
                        }
                    }
                }
            }
        }
    }
}
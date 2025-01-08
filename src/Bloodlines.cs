using Bloodlines.src.DataModels;
using Bloodlines.src.JsonModels;
using HarmonyLib;
using Il2CppNewtonsoft.Json.Linq;
using Il2CppTMPro;
using Il2CppVampireSurvivors.App.Data;
using Il2CppVampireSurvivors.Data;
using Il2CppVampireSurvivors.Data.Characters;
using Il2CppVampireSurvivors.Framework;
using Il2CppVampireSurvivors.Graphics;
using Il2CppVampireSurvivors.Objects;
using Il2CppVampireSurvivors.Objects.Characters;
using Il2CppVampireSurvivors.Objects.Items;
using Il2CppVampireSurvivors.Objects.Pickups;
using Il2CppVampireSurvivors.Objects.Weapons;
using Il2CppVampireSurvivors.UI;
using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using static MelonLoader.MelonLogger;

namespace Bloodlines
{
    public static class ModInfo
    {
        public const string Name = "Bloodlines";
        public const string Description = "Easily add custom characters!";
        public const string Author = "Nick, Takacomic";
        public const string Company = "CorruptedInfluences";
        public const string Version = "0.3.8";
        public const string Download = "https://github.com/takacomic/bloodlines";
    }

    public class BloodlinesMod : MelonMod
    {
        public static readonly string ModDirectory = Path.Combine(Directory.GetCurrentDirectory(), "UserData", "Bloodlines");
        public static readonly string DataDirectory = Path.Combine(ModDirectory, "data");
        public Config Config { get; private set; }
        public CharacterManager manager { get; private set; }
        internal GameManager gameManager;
        internal int characterIDs = 10000;

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

        public static void HiddenWeaponLeveler(WeaponType w, CharacterController cControl)
        {
            if (AlreadyHiddenWeapon(w, cControl))
                foreach (Equipment e in cControl.WeaponsManager.HiddenEquipment)
                {
                    if (e.Type == w) e.LevelUp(true);
                    break;
                }
            else
                Melon<BloodlinesMod>.Instance.gameManager.AddHiddenWeapon(w, cControl);
        }
        public static bool AlreadyHiddenWeapon(WeaponType w, CharacterController cControl)
        {
            foreach (Equipment e in cControl.WeaponsManager.HiddenEquipment)
            {
                if (e.Type == w) return true;
            }
            return false;
        }

        internal static void ArcanaAdder( ArcanaType arcanaType)
        {
            Melon<BloodlinesMod>.Instance.gameManager.ArcanaManager.ActiveArcanas.Add(arcanaType);
            Melon<BloodlinesMod>.Instance.gameManager.ArcanaManager.TriggerArcana(arcanaType);
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

        /*internal static JsonSerializerSettings serializerSettings = new()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore
        };

        static JArray ListToJArray(Il2Generic.List<CharacterData> list)
        {
            string result = JsonConvert.SerializeObject(list, serializerSettings);
            return JArray.Parse(result);
        }*/
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

        [HarmonyPatch(typeof(DataManager))]
        class DataManager_Patch
        {
            [HarmonyPatch(nameof(DataManager.LoadBaseJObjects))]
            [HarmonyPostfix]
            static void LoadBaseJObjects_Postfix(DataManager __instance, object[] __args, MethodBase __originalMethod)
            {
                foreach (SpriteDataModelWrapper spriteWrapper in Melon<BloodlinesMod>.Instance.manager.sprites)
                {
                    if (SpriteImporter.textures.ContainsKey(spriteWrapper.TextureNameWithoutExtension))
                    {
                        Sprite sprite = SpriteImporter.LoadSprite(SpriteImporter.textures[spriteWrapper.TextureNameWithoutExtension], spriteWrapper.Sprite.Rect);
                        sprite.name = spriteWrapper.SpriteNameWithoutExtension;
                        SpriteManager.RegisterSprite(sprite);
                    }
                    else
                    {
                        Texture2D texture = SpriteImporter.LoadTexture(spriteWrapper.TexturePath);
                        texture.name = spriteWrapper.TextureNameWithoutExtension;
                        Sprite sprite = SpriteImporter.LoadSprite(texture, spriteWrapper.Sprite.Rect);
                        sprite.name = spriteWrapper.SpriteNameWithoutExtension;
                        SpriteManager.RegisterSprite(sprite);
                    }
                }

                foreach (CharacterDataModelWrapper characterWrapper in Melon<BloodlinesMod>.Instance.manager.characters)
                {
                    CharacterDataModel character = characterWrapper.Character;
                    if ( character.DlcSort == null )
                    {
                        CharacterType characterType = (CharacterType)Melon<BloodlinesMod>.Instance.characterIDs++;
                        characterWrapper.characterType = characterType;
                        character.CharacterType = characterType;

                        Melon<BloodlinesMod>.Logger.Msg($"Adding character... {characterType} {character.CharName}");
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

            [HarmonyPatch(nameof(DataManager.MergeInJsonData))]
            [HarmonyPrefix]
            static void MergeInJsonData_Patch(DataManager __instance, DataManagerSettings settings, DlcType dlcType)
            {
                foreach (CharacterDataModelWrapper characterWrapper in Melon<BloodlinesMod>.Instance.manager.characters)
                {
                    CharacterDataModel character = characterWrapper.Character;
                    if (dlcType == character.DlcSort)
                    {   
                        CharacterType characterType = (CharacterType)Melon<BloodlinesMod>.Instance.characterIDs++;
                        characterWrapper.characterType = characterType;
                        character.CharacterType = characterType;

                        Melon<BloodlinesMod>.Logger.Msg($"Adding character... {characterType} {character.CharName}");
                        string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(characterWrapper.CharacterSettings, Newtonsoft.Json.Formatting.Indented,
                        new Newtonsoft.Json.JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                        });

                        JArray json = JArray.Parse(jsonString);
                        JObject dlc = JObject.Parse(settings._CharacterDataJsonAsset.text);
                        dlc.Add(characterType.ToString(), json);
                        TextAsset textAsset = new TextAsset(dlc.ToString());
                        settings._CharacterDataJsonAsset = textAsset;
                        Melon<BloodlinesMod>.Instance.manager.characterDict.Add(characterType, characterWrapper);
                    }
                }
            }
        }

        [HarmonyPatch(typeof(CharacterController))]
        class CharacterController_Patch
        {
            [HarmonyPatch(nameof(CharacterController.InitCharacter))]
            [HarmonyPostfix]
            static void InitCharacter_Patch(CharacterController __instance, CharacterType characterType)
            {

                if (isCustomCharacter(characterType))
                {
                    float x = __instance._spriteRenderer.sprite.rect.width / 95;
                    if (x > 0.80f) x = 0.15f;
                    else if (x > 0.35f) x = 0.35f;
                    Il2CppSystem.Nullable<float> a = new Il2CppSystem.Nullable<float>();
                    a.value = __instance._spriteRenderer.sprite.rect.height / 100 + 0.03f;
                    __instance.setOrigin(x, a);

                    // Can remove due to idle anims
                    CharacterDataModelWrapper cWrapper = Melon<BloodlinesMod>.Instance.manager.characterDict[characterType];
                    if (cWrapper.Skin(__instance.CurrentCharacterData.currentSkin).AlwaysAnimated)
                    {
                        __instance.IsAnimForced = true;
                    }
                    if (cWrapper.Character.SizeScale != null)
                    {
                        Vector3 localScale = __instance.transform.localScale;
                        Vector2 sizeScale = (Vector2)cWrapper.Character.SizeScale;
                        Vector3 newScale = new Vector3(localScale.x * sizeScale.x, localScale.y * sizeScale.y, localScale.z);
                        __instance.gameObject.transform.localScale = newScale;
                    }
                }
            }

            [HarmonyPatch(nameof(CharacterController.LevelUp))]
            [HarmonyPostfix]
            static void LevelUp_Patch(CharacterController __instance)
            {
                if (isCustomCharacter(__instance.CharacterType))
                {
                    SkinObjectModelv0_3 skin = Melon<BloodlinesMod>.Instance.manager.characterDict[__instance.CharacterType].Skin(__instance.CurrentSkinData.currentSkin);
                    foreach (EquipmentModifierJsonModelv0_3 modifier in skin.EquipmentModifiers)
                    {
                        if (modifier.Level != __instance.Level)
                            continue;
                        else
                        {
                            foreach (WeaponType w in modifier.Weapons)
                                Melon<BloodlinesMod>.Instance.gameManager.AddWeapon(w, __instance);

                            foreach (WeaponType w in modifier.Accessories)
                                Melon<BloodlinesMod>.Instance.gameManager.AccessoriesFacade.AddAccessory(w, __instance);

                            foreach (WeaponType w in modifier.HiddenWeapons)
                                HiddenWeaponLeveler(w, __instance);

                            foreach (ArcanaType a in modifier.Arcana)
                                ArcanaAdder(a);

                            break;
                        }
                    }
                }
            }
        }

        [HarmonyPatch(typeof(GameManager))]
        class GameManager_Patch
        {
            [HarmonyPatch(nameof(GameManager.InitializeGameSessionPostLoad))]
            [HarmonyPostfix]
            static void InitializeGameSessionPostLoad_Patch(GameManager __instance)
            {
                Melon<BloodlinesMod>.Instance.gameManager = __instance;
                CharacterController cControl = __instance.PlayerOne;
                CharacterType characterType = cControl.CharacterType;

                if (isCustomCharacter(characterType))
                {
                    CharacterDataModelWrapper cWrapper = Melon<BloodlinesMod>.Instance.manager.characterDict[characterType];
                    SkinObjectModelv0_3 skin = cWrapper.Skin(cControl._currentSkinData.currentSkin);
                    foreach (ArcanaType a in skin.StartingArcana)
                    {
                        ArcanaAdder(a);
                    }

                    foreach (WeaponType w in skin.HiddenWeapons)
                    {
                        HiddenWeaponLeveler(w, cControl);
                    }
                }
            }
        }

        [HarmonyPatch(typeof(CharacterItemUI))]
        class CharacterItemUI_Patch
        {
            [HarmonyPatch(nameof(CharacterItemUI.SetIconSizes))]
            [HarmonyPrefix]
            static bool SetCharacterSprite_Patch(CharacterItemUI __instance)
            {
                CharacterType cType = __instance.CharacterItem._characterType;
                if (isCustomCharacter(cType))
                {
                    CharacterDataModelWrapper cM = getCharacterManager().characterDict[cType];
                    if (cM.Character.CustomPortrait != null || cM.Character.SmallPortrait)
                    {
                        if (cM.Character.SmallPortrait)
                        {
                            Vector2 size = new Vector2();
                            Sprite sprite = SpriteManager.GetSprite(cM.Skin(__instance._charItem.CharacterData.currentSkin).SpriteName);
                            float widthDiv = sprite.rect.width / sprite.rect.height;
                            float heightDiv = sprite.rect.height / sprite.rect.width;
                            if (widthDiv >= 1.3)
                            {
                                size.x = 135;
                                size.y = 100;
                            }
                            else if (heightDiv >= 1.3)
                            {
                                size.x = 100;
                                size.y = 135;
                            }
                            else
                            {
                                size.x = 120;
                                size.y = 120;
                            }

                            __instance._CharacterIcon.rectTransform.sizeDelta = size;
                            __instance.gameObject.GetComponent<RectTransform>().sizeDelta = size;
                        }
                        if (cM.Character.CustomPortrait != null)
                        {
                            __instance._CharacterIcon.overrideSprite = SpriteManager.GetSprite(cM.Character.CustomPortrait);
                            __instance._CharacterIcon.sprite = SpriteManager.GetSprite(cM.Character.CustomPortrait);
                        }
                        return false;
                    }
                }
                return true;
            }
        }

        [HarmonyPatch(typeof(CharacterSelectionPage))]
        class CharacterSelectionPage_Patch
        {
            [HarmonyPatch(nameof(CharacterSelectionPage.SetIconSizes))]
            [HarmonyPrefix]
            static bool SetIconSizes_Prefix(CharacterSelectionPage __instance, MethodBase __originalMethod)
            {
                CharacterType cType = __instance._currentType;
                if (isCustomCharacter(cType))
                {
                    CharacterDataModelWrapper cM = getCharacterManager().characterDict[cType];
                    if (cM.Character.SmallPortrait)
                    {
                        Vector2 size = new Vector2();
                        Sprite sprite = SpriteManager.GetSprite(cM.Skin(__instance._currentData.currentSkin).SpriteName);
                        float widthDiv = sprite.rect.width / sprite.rect.height;
                        float heightDiv = sprite.rect.height / sprite.rect.width;
                        if (widthDiv >= 1.3 )
                        {
                            size.x = 135;
                            size.y = 100;
                        }
                        else if (heightDiv >= 1.3)
                        {
                            size.x = 100;
                            size.y = 135;
                        }
                        else
                        {
                            size.x = 120;
                            size.y = 120;
                        }

                        __instance.gameObject.transform.FindChild("Panel").transform.FindChild("InfoPanel")
                            .transform.FindChild("Background").transform.FindChild("CharacterImage")
                            .GetComponent<RectTransform>().sizeDelta = size;
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
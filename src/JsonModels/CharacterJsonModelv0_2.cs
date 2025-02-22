﻿using Bloodlines.src.DataModels;
using Il2CppVampireSurvivors.Data;
using Il2CppVampireSurvivors.Data.Characters;
using Il2CppVampireSurvivors.Objects;
using MelonLoader;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using UnityEngine;

namespace Bloodlines.src.JsonModels
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class CharacterJsonModelv0_2
    {
        [JsonProperty("startingWeapon")]
        [JsonConverter(typeof(StringEnumConverter))]
        public WeaponType StartingWeapon { get; set; }

        [JsonProperty("prefix")]
        public string Prefix { get; set; }

        [JsonProperty("charName")]
        public string CharName { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("textureName")]
        public string TextureName { get; set; }

        [JsonProperty("spriteName")]
        public string SpriteName { get; set; }

        [JsonProperty("skins")]
        public List<SkinObjectModelv0_2> Skins { get; set; }

        [JsonProperty("currentSkinIndex")]
        public int CurrentSkinIndex { get; set; }

        [JsonProperty("walkingFrames")]
        public int WalkingFrames { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("isBought")]
        public bool IsBought { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("showcase", ItemConverterType = typeof(StringEnumConverter))]
        public List<WeaponType> Showcase { get; set; }

        [JsonProperty("statModifiers")]
        public List<StatModifierJsonModelv0_2> StatModifiers { get; set; }

        [JsonProperty("onEveryLevelUp")]
        public StatModifierJsonModelv0_2 OnEveryLevelUp { get; set; }

        [JsonProperty("bodyOffset")]
        public Vector2 BodyOffset { get; set; }

        [JsonProperty("portraitName")]
        public string PortraitName { get; set; }

        [JsonProperty("bgm")]
        public BgmType BGM { get; set; }

        public CharacterDataModelWrapper toCharacterDataModel()
        {
            CharacterDataModelWrapper modelWrapper = new();
            CharacterDataModel c = new();
            modelWrapper.CharacterSettings.Add(c);

            StatModifierJsonModelv0_2 stats = StatModifiers[0];

            PropertyInfo[] statsProps = stats.GetType().GetProperties();

            List<string> statsToFloat = new() { "Revivals" };

            foreach (PropertyInfo prop in statsProps)
            {
                if (c.GetType().GetProperty(prop.Name) == null)
                {
#if DEBUG
                    Melon<BloodlinesMod>.Logger.Msg($"No match for {prop.Name}");
#endif // DEBUG
                    continue;
                }

                var value = prop.GetValue(stats, null);

                if (statsToFloat.Contains(prop.Name))
                {
                    c.GetType().GetProperty(prop.Name).SetValue(c, Convert.ToSingle(value));
                }
                else
                {
                    c.GetType().GetProperty(prop.Name).SetValue(c, value);
                }
            }

            Melon<BloodlinesMod>.Logger.Msg($"PastStatProps");

            PropertyInfo[] myProps = GetType().GetProperties();

            foreach (PropertyInfo prop in myProps)
            {
                if (c.GetType().GetProperty(prop.Name) == null && prop.Name != "StatModifiers")
                {
#if DEBUG
                    Melon<BloodlinesMod>.Logger.Msg($"No match for {prop.Name}");
#endif // DEBUG

                    continue;
                }

                var value = prop.GetValue(this, null);

                if (prop.Name == "StatModifiers")
                {
                    foreach (StatModifierJsonModelv0_2 statMod in StatModifiers.Skip(1))
                        modelWrapper.CharacterSettings.Add(statMod.toCharacterDataModel());
                    Melon<BloodlinesMod>.Logger.Msg($"Past Stats Mod 2");
                }
                else if (prop.Name == "OnEveryLevelUp" && OnEveryLevelUp != null)
                {
                    c.OnEveryLevelUp = OnEveryLevelUp.toModifierStat();
                    Melon<BloodlinesMod>.Logger.Msg($"Past OnEveryLevelUp");
                }
                else
                {
                    c.GetType().GetProperty(prop.Name).SetValue(c, value);
                }
            }

            // Note: Looks like we cannot serialize Skin because of Il2Cpp.
            // Could probably use a custom parser to call the Il2Cpp version of newtonsoft to handle the Skin object, but too lazy right now.
            // Use SkinObjectModelv0_2 instead.
            return modelWrapper;
        }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class StatModifierJsonModelv0_2
    {
        [JsonProperty("level")]
        public float Level { get; set; }

        [JsonProperty("maxHp")]
        public float MaxHp { get; set; }

        [JsonProperty("armor")]
        public float Armor { get; set; }

        [JsonProperty("regen")]
        public float Regen { get; set; }

        [JsonProperty("moveSpeed")]
        public float MoveSpeed { get; set; }

        [JsonProperty("power")]
        public double Power { get; set; }

        [JsonProperty("area")]
        public float Area { get; set; }

        [JsonProperty("speed")]
        public float Speed { get; set; }

        [JsonProperty("duration")]
        public float Duration { get; set; }

        [JsonProperty("amount")]
        public float Amount { get; set; }

        [JsonProperty("luck")]
        public float Luck { get; set; }

        [JsonProperty("growth")]
        public float Growth { get; set; }

        [JsonProperty("greed")]
        public float Greed { get; set; }

        [JsonProperty("curse")]
        public float Curse { get; set; }

        [JsonProperty("magnet")]
        public float Magnet { get; set; }

        [JsonProperty("revivals")]
        public double Revivals { get; set; }

        [JsonProperty("rerolls")]
        public float Rerolls { get; set; }

        [JsonProperty("skips")]
        public float Skips { get; set; }

        [JsonProperty("banish")]
        public float Banish { get; set; }

        [JsonProperty("charm")]
        public int Charm { get; set; }

        [JsonProperty("shroud")]
        public float Shroud { get; set; }

        [JsonProperty("shields")]
        public float Shields { get; set; }

        [JsonProperty("cooldown")]
        public float Cooldown { get; set; }

        public ModifierStats toModifierStat()
        {
            ModifierStats m = new();

            PropertyInfo[] myProps = GetType().GetProperties();
            List<string> statsToFloat = new() { "Power" };

            foreach (PropertyInfo prop in GetType().GetProperties())
            {
                try
                {
                    if (m.GetType().GetProperty(prop.Name) == null)
                    {
                        continue;
                    }

                    var value = prop.GetValue(this, null);
                    if (statsToFloat.Contains(prop.Name))
                    {
                        m.GetType().GetProperty(prop.Name).SetValue(m, Convert.ToSingle(value));
                    }
                    else
                    {
                        m.GetType().GetProperty(prop.Name).SetValue(m, value);
                    }
                }
                catch (Exception e)
                {
                    e.Data.Add("CharacterDataModel.toCharacterDataModel().prop.Name", prop.Name);
                    throw;
                }
            }

            return m;
        }

        public CharacterDataModel toCharacterDataModel()
        {
            CharacterDataModel c = new();

            PropertyInfo[] myProps = GetType().GetProperties();
            List<string> statsToFloat = new() { "Revivals" };

            foreach (PropertyInfo prop in myProps)
            {
                try
                {
                    if (c.GetType().GetProperty(prop.Name) == null)
                    {
#if DEBUG
                        Melon<BloodlinesMod>.Logger.Msg($"No match for {prop.Name}");
#endif // DEBUG

                        continue;
                    }

                    var value = prop.GetValue(this, null);

                    if (statsToFloat.Contains(prop.Name))
                    {
                        c.GetType().GetProperty(prop.Name).SetValue(c, Convert.ToSingle(value));
                    }
                    else
                    {
                        c.GetType().GetProperty(prop.Name).SetValue(c, value);
                    }
                }
                catch (Exception e)
                {
                    e.Data.Add("CharacterDataModel.toCharacterDataModel().prop.Name", prop.Name);
                    throw;
                }
            }

            return c;
        }
    }

    public class SkinObjectModelv0_2
    {
        [JsonProperty("skinType")]
        public SkinType SkinType { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("textureName")]
        public string TextureName { get; set; }

        [JsonProperty("spriteName")]
        public string SpriteName { get; set; }

        [JsonProperty("frames")]
        public List<string> frames { get; set; } = new();

        [JsonProperty("walkingFrames")]
        public int WalkingFrames => frames?.Count ?? 1;

        [JsonProperty("unlocked")]
        public bool Unlocked { get; set; }

        [DefaultValue(false)]
        [JsonProperty("alwaysAnimated")]
        public bool AlwaysAnimated { get; set; }

        public static implicit operator Skin(SkinObjectModelv0_2 model)
        {
            Skin skin = new();

            skin.skinType = model.SkinType;
            skin.name = model.Name;
            skin.textureName = model.TextureName;
            skin.spriteName = model.SpriteName;
            skin.walkingFrames = model.WalkingFrames;
            skin.unlocked = model.Unlocked;

            return skin;
        }

        public static implicit operator SkinObjectModelv0_2(Skin skin)
        {
            SkinObjectModelv0_2 model = new();

            model.SkinType = skin.skinType;
            model.Name = skin.name;
            model.TextureName = skin.textureName;
            model.SpriteName = skin.spriteName;
            model.Unlocked = skin.unlocked;

            return model;
        }
    }
}
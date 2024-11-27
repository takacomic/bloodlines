using Bloodlines.src.DataModels;
using Il2Cpp;
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
    public class CharacterJsonModelv0_3
    {
        [JsonProperty("isBought")]
        public bool IsBought { get; set; }

        [JsonProperty("statModifiers")]
        public List<StatModifierJsonModelv0_3> StatModifiers { get; set; }

        [JsonProperty("alwaysHidden")]
        public bool AlwaysHidden { get; set; }

        //[JsonProperty("bgm")]
        //[DefaultValue(null)]
        //public BgmType BGM { get; set; }

        [JsonProperty("bodyOffset")]
        public Vector2? BodyOffset { get; set; }

        [JsonProperty("characterLangSheet")]
        public string? CharacterLangSheet { get; set; }

        [JsonProperty("charName")]
        public string? CharName { get; set; }

        [JsonProperty("charSelFrame")]
        public string? CharSelFrame { get; set; }

        [JsonProperty("charSelTexture")]
        public string? CharSelTexture { get; set; }

        [JsonProperty("currentSkin")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SkinType CurrentSkin { get; set; }

        //[JsonProperty("customStartingWeapon")]
        //public string CustomStartingWeapon { get; set; }

        [JsonProperty("debugEnemies")]
        public float DebugEnemies { get; set; }

        [JsonProperty("debugTime")]
        public float DebugTime { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("exLevels")]
        public int ExLevels { get; set; }

        [JsonProperty("exWeapons")]
        public List<string> ExWeapons { get; set; }

        [JsonProperty("frameRate")]
        public int FrameRate { get; set; }

        [JsonProperty("headOffsets")]
        public List<Vector2> HeadOffsets { get; set; }

        [JsonProperty("hidden")]
        public bool Hidden { get; set; }

        [JsonProperty("hiddenWeapons")]
        public List<string> HiddenWeapons { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("levelUpPresets")]
        [DefaultValue(null)]
        public List<Loadout> LevelUpPresets { get; set; }

        [JsonProperty("nameIndex")]
        public List<string>? NameIndex { get; set; }

        [JsonProperty("noHurt")]
        public bool NoHurt { get; set; }

        [JsonProperty("onEveryLevelUp")]
        public StatModifierJsonModelv0_3 OnEveryLevelUp { get; set; }

        [JsonProperty("portraitName")]
        public string? PortraitName { get; set; }

        [JsonProperty("prefix")]
        public string? Prefix { get; set; }

        [JsonProperty("price")]
        public float Price { get; set; }

        [JsonProperty("racingOffsets")]
        public List<RacingOffsetData> RacingOffsets { get; set; }

        //[JsonProperty("requiresRelic")]
        //[JsonConverter(typeof(StringEnumConverter))]
        //public ItemType RequiresRelic { get; set; }

        [JsonProperty("showcase")]
        public List<WeaponType> Showcase { get; set; }

        [JsonProperty("skinLangSheet")]
        public string? SkinLangSheet { get; set; }

        [JsonProperty("skins")]
        public List<SkinObjectModelv0_3> Skins { get; set; }

        [JsonProperty("spriteName")]
        public string? SpriteName { get; set; }

        [JsonProperty("startFrameCount")]
        public int StartFrameCount { get; set; }

        [JsonProperty("startingWeapon")]
        [JsonConverter(typeof(StringEnumConverter))]
        public WeaponType StartingWeapon { get; set; }

        [JsonProperty("suffix")]
        public string? Suffix { get; set; }

        [JsonProperty("surname")]
        public string? Surname { get; set; }

        [JsonProperty("textureName")]
        public string? TextureName { get; set; }

        [JsonProperty("walkFrameRate")]
        public int WalkFrameRate { get; set; }

        [JsonProperty("walkingFrames")]
        public int WalkingFrames { get; set; }

        public CharacterDataModelWrapper toCharacterDataModel()
        {
            CharacterDataModelWrapper modelWrapper = new();
            CharacterDataModel c = new();
            modelWrapper.CharacterSettings.Add(c);

            StatModifierJsonModelv0_3 stats = StatModifiers[0];

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
                    foreach (StatModifierJsonModelv0_3 statMod in StatModifiers.Skip(1))
                        modelWrapper.CharacterSettings.Add(statMod.toCharacterDataModel());
                }
                else if (prop.Name == "OnEveryLevelUp" && OnEveryLevelUp != null)
                {
                    c.OnEveryLevelUp = OnEveryLevelUp.toModifierStat();
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
    public class StatModifierJsonModelv0_3
    {
        [JsonProperty("amount")]
        public float Amount { get; set; }

        [JsonProperty("area")]
        public float Area { get; set; }

        [JsonProperty("armor")]
        public float Armor { get; set; }

        [JsonProperty("banish")]
        public float Banish { get; set; }

        [JsonProperty("charm")]
        public int Charm { get; set; }

        [JsonProperty("cooldown")]
        public float Cooldown { get; set; }

        [JsonProperty("curse")]
        public float Curse { get; set; }

        [JsonProperty("defang")]
        public float Defang { get; set; }

        [JsonProperty("duration")]
        public float Duration { get; set; }

        [JsonProperty("fever")]
        public float Fever { get; set; }

        [JsonProperty("greed")]
        public float Greed { get; set; }

        [JsonProperty("growth")]
        public float Growth { get; set; }

        [JsonProperty("invulTimeBonus")]
        public float InvulTimeBonus { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("luck")]
        public float Luck { get; set; }

        [JsonProperty("magnet")]
        public float Magnet { get; set; }

        [JsonProperty("maxHp")]
        public float MaxHp { get; set; }

        [JsonProperty("moveSpeed")]
        public float MoveSpeed { get; set; }

        [JsonProperty("power")]
        public double Power { get; set; }

        [JsonProperty("regen")]
        public float Regen { get; set; }

        [JsonProperty("rerolls")]
        public float Rerolls { get; set; }

        [JsonProperty("revivals")]
        public double Revivals { get; set; }

        [JsonProperty("shields")]
        public float Shields { get; set; }

        /*[JsonProperty("sineArea")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SineBonusData SineArea { get; set; }

        [JsonProperty("sineCooldown")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SineBonusData SineCooldown { get; set; }

        [JsonProperty("sineDuration")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SineBonusData SineDuration { get; set; }

        [JsonProperty("sineMight")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SineBonusData SineMight { get; set; }

        [JsonProperty("sineSpeed")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SineBonusData SineSpeed { get; set; }*/

        [JsonProperty("shroud")]
        public float Shroud { get; set; }

        [JsonProperty("skips")]
        public float Skips { get; set; }

        [JsonProperty("speed")]
        public float Speed { get; set; }

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

    public class SkinObjectModelv0_3
    {
        [JsonProperty("charInternalName")]
        [DefaultValue("")]
        public string CharInternalName { get; set; }

        [DefaultValue(false)]
        [JsonProperty("alwaysAnimated")]
        public bool AlwaysAnimated { get; set; }

        [JsonProperty("alwaysHidden")]
        public bool AlwaysHidden { get; set; }

        [JsonProperty("amount")]
        public float Amount { get; set; }

        [JsonProperty("area")]
        public float Area { get; set; }

        [JsonProperty("armor")]
        public float Armor { get; set; }

        [JsonProperty("banish")]
        public float Banish { get; set; }

        [JsonProperty("charSelFrame")]
        public string CharSelFrame { get; set; }

        [JsonProperty("charSelTexture")]
        public string CharSelTexture { get; set; }

        [JsonProperty("cooldown")]
        public float Cooldown { get; set; }

        [JsonProperty("curse")]
        public float Curse { get; set; }

        [JsonProperty("customStartingWeapon")]
        public string CustomStartingWeapon { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("duration")]
        public float Duration { get; set; }

        [JsonProperty("exAccesories")]
        public List<string> ExAccessories { get; set; } = new List<string>();

        [JsonProperty("exWeapons")]
        public List<string> ExWeapons { get; set; } = new List<string>();

        [JsonProperty("greed")]
        public float Greed { get; set; }

        [JsonProperty("growth")]
        public float Growth { get; set; }

        [JsonProperty("headOffsets")]
        [DefaultValue(null)]
        public Il2CppSystem.Collections.Generic.List<Vector2> HeadOffsets { get; set; }

        [JsonProperty("hidden")]
        public bool Hidden { get; set; }

        [JsonProperty("hiddenWeapons")]
        [DefaultValue(null)]
        public Il2CppSystem.Collections.Generic.List<string> HiddenWeapons { get; set; }

        [JsonProperty("luck")]
        public float Luck { get; set; }

        [JsonProperty("magnet")]
        public float Magnet { get; set; }

        [JsonProperty("maxHp")]
        public float MaxHp { get; set; }

        [JsonProperty("moveSpeed")]
        public float MoveSpeed { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("onEveryLevelUp")]
        public ModifierStats OnEveryLevelUp { get; set; }

        [JsonProperty("power")]
        public double Power { get; set; }

        [JsonProperty("prefix")]
        public string Prefix { get; set; }

        [JsonProperty("price")]
        public float Price { get; set; }

        [JsonProperty("regen")]
        public float Regen { get; set; }

        [JsonProperty("rerolls")]
        public float Rerolls { get; set; }

        [JsonProperty("revivals")]
        public float Revivals { get; set; }

        [JsonProperty("secret")]
        public bool Secret { get; set; }

        [JsonProperty("shields")]
        public float Shields { get; set; }

        [JsonProperty("skinType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SkinType SkinType { get; set; }

        [JsonProperty("skips")]
        public float Skips { get; set; }

        [JsonProperty("speed")]
        public float Speed { get; set; }

        [JsonProperty("spriteAnims")]
        public SpriteAnims SpriteAnims { get; set; }

        [JsonProperty("spriteName")]
        public string SpriteName { get; set; }

        [JsonProperty("startingWeapon")]
        [JsonConverter(typeof(StringEnumConverter))]
        [DefaultValue(null)]
        public Il2CppSystem.Nullable<WeaponType> StartingWeapon { get; set; }

        [JsonProperty("suffix")]
        public string Suffix { get; set; }

        [JsonProperty("textureName")]
        public string TextureName { get; set; }

        [JsonProperty("unlocked")]
        public bool Unlocked { get; set; }

        [JsonProperty("walkingFrames")]
        public int WalkingFrames { get; set; }

        public static implicit operator Skin(SkinObjectModelv0_3 model)
        {
            Skin skin = new();
            Il2CppSystem.Collections.Generic.List<string> exAccessories = new Il2CppSystem.Collections.Generic.List<string>();
            Il2CppSystem.Collections.Generic.List<string> exWeapons = new Il2CppSystem.Collections.Generic.List<string>();
            foreach (var a in model.ExAccessories) { exAccessories.Add(a); }
            foreach (var a in model.ExWeapons) { exWeapons.Add(a); }

            skin.alwaysHidden = model.AlwaysHidden;
            skin.amount = model.Amount;
            skin.area = model.Area;
            skin.armor = model.Armor;
            skin.banish = model.Banish;
            skin.charSelFrame = model.CharSelFrame;
            skin.charSelTexture = model.CharSelTexture;
            skin.cooldown = model.Cooldown;
            skin.curse = model.Curse;
            skin.description = model.Description;
            skin.duration = model.Duration;
            skin.exAccessories = exAccessories;
            skin.exWeapons = exWeapons;
            skin.greed = model.Greed;
            skin.growth = model.Growth;
            skin.headOffsets = model.HeadOffsets;
            skin.hidden = model.Hidden;
            skin.hiddenWeapons = model.HiddenWeapons;
            skin.luck = model.Luck;
            skin.magnet = model.Magnet;
            skin.maxHp = model.MaxHp;
            skin.moveSpeed = model.MoveSpeed;
            skin.name = model.Name;
            skin.onEveryLevelUp = model.OnEveryLevelUp;
            skin.power = model.Power;
            skin.prefix = model.Prefix;
            skin.price = model.Price;
            skin.regen = model.Regen;
            skin.reRolls = model.Rerolls;
            skin.revivals = model.Revivals;
            skin.secret = model.Secret;
            skin.shields = model.Shields;
            skin.skinType = model.SkinType;
            skin.skips = model.Skips;
            skin.speed = model.Speed;
            skin.spriteAnims = model.SpriteAnims;
            skin.spriteName = model.SpriteName;
            skin.startingWeapon = model.StartingWeapon;
            skin.suffix = model.Suffix;
            skin.textureName = model.TextureName;
            skin.unlocked = model.Unlocked;
            skin.walkingFrames = model.WalkingFrames;

            return skin;
        }

        public static implicit operator SkinObjectModelv0_3(Skin skin)
        {
            SkinObjectModelv0_3 model = new();
            List<string> exAccessories = new List<string>();
            List<string> exWeapons = new List<string>();
            foreach (var a in skin.exAccessories) { exAccessories.Add(a); }
            foreach (var a in skin.exWeapons) { exWeapons.Add(a); }

            model.AlwaysHidden = skin.alwaysHidden;
            model.Amount = skin.amount;
            model.Area = skin.area;
            model.Armor = skin.armor;
            model.Banish = skin.banish;
            model.CharSelFrame = skin.charSelFrame;
            model.CharSelTexture = skin.charSelTexture;
            model.Cooldown = skin.cooldown;
            model.Curse = skin.curse;
            model.Description = skin.description;
            model.Duration = skin.duration;
            model.ExAccessories = exAccessories;
            model.ExWeapons = exWeapons;
            model.Greed = skin.greed;
            model.Growth = skin.growth;
            model.HeadOffsets = skin.headOffsets;
            model.Hidden = skin.hidden;
            model.HiddenWeapons = skin.hiddenWeapons;
            model.Luck = skin.luck;
            model.Magnet = skin.magnet;
            model.MaxHp = skin.maxHp;
            model.MoveSpeed = skin.moveSpeed;
            model.Name = skin.name;
            model.OnEveryLevelUp = skin.onEveryLevelUp;
            model.Power = skin.power;
            model.Prefix = skin.prefix;
            model.Price = skin.price;
            model.Regen = skin.regen;
            model.Rerolls = skin.reRolls;
            model.Revivals = skin.revivals;
            model.Secret = skin.secret;
            model.Shields = skin.shields;
            model.SkinType = skin.skinType;
            model.Skips = skin.skips;
            model.Speed = skin.speed;
            model.SpriteAnims = skin.spriteAnims;
            model.SpriteName = skin.spriteName;
            model.StartingWeapon = skin.startingWeapon;
            model.Suffix = skin.suffix;
            model.TextureName = skin.textureName;
            model.Unlocked = skin.unlocked;
            model.WalkingFrames = skin.walkingFrames;

            return model;
        }
    }
}
using Bloodlines.src.JsonModels;
using Il2CppVampireSurvivors.Data;
using Il2CppVampireSurvivors.Data.Characters;
using Il2CppVampireSurvivors.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Bloodlines.src.DataModels
{
    // This is meant to be a json serializable copy of Vampire Survivors's Character data.
    // VampireSurvivors CharacterData has nullables, which make it hard to do anything. Copy the class into this.

    public class CharacterDataModelWrapper
    {
        [JsonIgnore]
        public CharacterType characterType { get; set; }

        public List<CharacterDataModel> CharacterSettings { get; set; } = new();

        [JsonIgnore]
        public string BaseDirectory { get; set; }

        public string SpritePath => Path.Combine(BaseDirectory, Character.SpriteName);

        public string PortraitPath => Path.Combine(BaseDirectory, Character.PortraitName);

        public string SkinPath(int skinId) => Path.Combine(BaseDirectory, Character.Skins[skinId].SpriteName);
        public string SkinPath(SkinType type) => Path.Combine(BaseDirectory, Character.Skins[SkinTypeInt(type)].SpriteName);

        public int SkinTypeInt(SkinType type)
        {
            if (type == SkinType.LEGACY) return 1;
            return 0;
        }

        public SkinObjectModelv0_3 Skin(SkinType type) => Character.Skins[SkinTypeInt(type)];

        [JsonIgnore]
        public CharacterDataModel Character
        {
            get
            {
                if (CharacterSettings.Any())
                {
                    return CharacterSettings[0];
                }
                else
                {
                    throw new System.Exception("Characters hasn't been set yet.");
                }
            }
        }
    }

    public class CharacterDataModel
    {
        [JsonProperty("charInternalName")]
        [DefaultValue("")]
        public string CharInternalName { get; set; }

        [JsonIgnore]
        public CharacterType CharacterType { get; set; }

        [JsonProperty("isBought")]
        public bool IsBought { get; set; }

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

        [JsonProperty("bgm")]
        public BgmType BGM { get; set; }

        [JsonProperty("bodyOffset")]
        public Vector2 BodyOffset { get; set; }

        [JsonProperty("characterLangSheet")]
        public string CharacterLangSheet { get; set; }

        [JsonProperty("charm")]
        public int Charm { get; set; }

        [JsonProperty("charName")]
        public string CharName { get; set; }

        [JsonProperty("charSelFrame")]
        public string CharSelFrame { get; set; }

        [JsonProperty("charSelTexture")]
        public string CharSelTexture { get; set; }

        [JsonProperty("cooldown")]
        public float Cooldown { get; set; }

        [JsonProperty("currentSkin")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SkinType CurrentSkin { get; set; }

        [JsonProperty("curse")]
        public float Curse { get; set; }

        //[JsonIgnore]
        //public string CustomStartingWeapon { get; set; }

        [JsonProperty("debugEnemies")]
        public float DebugEnemies { get; set; }

        [JsonProperty("debugTime")]
        public float DebugTime { get; set; }

        [JsonProperty("defang")]
        public float Defang { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("duration")]
        public float Duration { get; set; }

        [JsonProperty("exLevels")]
        public int ExLevels { get; set; }

        [JsonProperty("exWeapons")]
        [DefaultValue(null)]
        public List<string> ExWeapons { get; set; }

        [JsonProperty("fever")]
        public float Fever { get; set; }

        [JsonProperty("frameRate")]
        public int FrameRate { get; set; }

        [JsonProperty("greed")]
        public float Greed { get; set; }

        [JsonProperty("growth")]
        public float Growth { get; set; }

        [JsonProperty("headOffsets")]
        [DefaultValue(null)]
        public List<Vector2> HeadOffsets { get; set; }

        [JsonProperty("hidden")]
        public bool Hidden { get; set; }

        [JsonProperty("hiddenWeapons")]
        [DefaultValue(null)]
        public List<string> HiddenWeapons { get; set; }

        [JsonProperty("hideWeaponIcon")]
        [DefaultValue(false)]
        public bool HidenWeaponIcon { get; set; }

        [JsonProperty("invulTimeBonus")]
        public int InvulTimeBonus { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("levelUpPresets")]
        [DefaultValue(null)]
        public List<Loadout> LevelUpPresets { get; set; }

        [JsonProperty("luck")]
        public float Luck { get; set; }

        [JsonProperty("magnet")]
        public float Magnet { get; set; }

        [JsonProperty("maxHp")]
        public float MaxHp { get; set; }

        [JsonProperty("moveSpeed")]
        public float MoveSpeed { get; set; }

        [JsonProperty("nameIndex")]
        public int NameIndex { get; set; }

        [JsonProperty("noHurt")]
        public bool NoHurt { get; set; }

        [JsonProperty("onEveryLevelUp")]
        public ModifierStats OnEveryLevelUp { get; set; }

        [JsonProperty("portraitName")]
        public string PortraitName { get; set; }

        [JsonProperty("power")]
        public double Power { get; set; }

        [JsonProperty("prefix")]
        public string Prefix { get; set; }

        [JsonProperty("price")]
        public float Price { get; set; }

        [JsonProperty("racingOffsets")]
        [DefaultValue(null)]
        public List<RacingOffsetData> RacingOffsets { get; set; }

        [JsonProperty("regen")]
        public float Regen { get; set; }

        //[JsonProperty("requiresRelic")]
        //[JsonConverter(typeof(StringEnumConverter))]
        //public ItemType RequiresRelic { get; set; }

        [JsonProperty("rerolls")]
        public float Rerolls { get; set; }

        [JsonProperty("revivals")]
        public double Revivals { get; set; }

        [JsonProperty("secret")]
        public bool Secret { get; set; }

        [JsonProperty("shields")]
        public float Shields { get; set; }

        [JsonProperty("sineArea")]
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
        public SineBonusData SineSpeed { get; set; }

        [JsonProperty("showcase")]
        public List<WeaponType> Showcase { get; set; }

        [JsonProperty("shroud")]
        public float Shroud { get; set; }

        [JsonProperty("skinLangSheet")]
        public string SkinLangSheet { get; set; }

        [JsonProperty("skins")]
        public List<SkinObjectModelv0_3> Skins { get; set; }

        [JsonProperty("skips")]
        public float Skips { get; set; }

        [JsonProperty("speed")]
        public float Speed { get; set; }

        [JsonProperty("spriteAnims")]
        public SpriteAnims SpriteAnims { get; set; }

        [JsonProperty("spriteName")]
        public string SpriteName { get; set; }

        [JsonProperty("startFrameCount")]
        public int StartFrameCount { get; set; }

        [JsonProperty("startingWeapon")]
        [JsonConverter(typeof(StringEnumConverter))]
        public WeaponType StartingWeapon { get; set; }

        [JsonProperty("suffix")]
        public string Suffix { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("textureName")]
        public string TextureName { get; set; }

        [JsonProperty("walkFrameRate")]
        public int WalkFrameRate { get; set; }

        [JsonProperty("walkingFrames")]
        public int WalkingFrames { get; set; }

        [JsonProperty("zeroPad")]
        public int ZeroPad { get; set; }
    }
}
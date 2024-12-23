﻿using Bloodlines.src.DataModels;
using Bloodlines.src.JsonModels;
using MelonLoader;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bloodlines
{
    // The Deserialized version of the Character json files.
    public abstract class BaseCharacterFileModel
    {
        [JsonProperty("version")]
        [JsonConverter(typeof(VersionConverter))]
        abstract public Version Version { get; set; }

        public abstract Type CharacterFileVersion();

        public abstract List<CharacterDataModelWrapper> GetCharacterList();

        public abstract List<SpriteDataModelWrapper> GetSpriteList();
    }

    [Obsolete("CharacterFileModelV0_1 is deprecated, useCharacterFileModelV0_2 instead.")]
    public class CharacterFileModelV0_1 : BaseCharacterFileModel
    {
        [JsonIgnore]
        public const string _version = "0.1";

        public override Version Version { get; set; } = new Version("0.1");

        [JsonProperty("character")]
        public List<CharacterJsonModelv0_1> Character { get; set; }

        [JsonProperty("spriteData")]
        public List<SpriteJsonModelv0_3> SpriteData { get; set; }

        public CharacterFileModelV0_1() : base() { }

        public override Type CharacterFileVersion() => typeof(CharacterFileModelV0_1);

        public override List<CharacterDataModelWrapper> GetCharacterList()
        {
            List<CharacterDataModelWrapper> characterDatas = new();
            bool first = true;

            CharacterDataModelWrapper wrapper = new();

            foreach (CharacterJsonModelv0_1 old in Character)
            {
                CharacterDataModel model = new();

                PropertyInfo[] myProps = old.GetType().GetProperties();

                foreach (PropertyInfo prop in myProps)
                {
                    if (model.GetType().GetProperty(prop.Name) == null)
                    {
#if DEBUG
                        Melon<BloodlinesMod>.Logger.Msg($"No match for {prop.Name}");
#endif // DEBUG
                        continue;
                    }

                    try
                    {
                        var value = prop.GetValue(old, null);

                        if (prop.Name == "Skins" && old.Skins != null)
                        {
                            model.Skins = new();

                            foreach (SkinObjectModel os in old.Skins)
                            {
                                SkinObjectModelv0_3 ns = new()
                                {
                                    //Id = (Il2CppVampireSurvivors.Data.SkinType)os.Id,
                                    Name = os.Name,
                                    SpriteName = os.SpriteName,
                                    TextureName = os.TextureName,
                                    Unlocked = os.Unlocked,
                                    //frames = new()
                                };
                            }
                        }
                        else
                        {
                            model.GetType().GetProperty(prop.Name).SetValue(model, value);
                        }
                    }
                    catch (Exception e)
                    {
                        Melon<BloodlinesMod>.Logger
                            .Msg($"Failed to convert: {prop.Name} From {prop.PropertyType.FullName} to {model.GetType().GetProperty(prop.Name).PropertyType.FullName} on character: {old.CharName}");

                        Melon<BloodlinesMod>.Logger.Msg($"{e}");
                    }
                }

                if (first)
                {
                    model.PortraitName ??= model.SpriteName;
                    model.WalkingFrames = 1;

                    if (!model.Skins.Any())
                    {
                        SkinObjectModelv0_3 skin = new();
                        //skin.Id = 0;
                        skin.Name = "Default";
                        skin.SpriteName = model.SpriteName;
                        skin.TextureName = "characters";
                        skin.Unlocked = true;
                        //skin.frames = new();

                        model.Skins.Add(skin);
                    }

                    first = false;
                }

                wrapper.CharacterSettings.Add(model);
            }

            characterDatas.Add(wrapper);

            return characterDatas;
        }

        public override List<SpriteDataModelWrapper> GetSpriteList()
        {
            List<SpriteDataModelWrapper> spriteDatas = new();

            SpriteData.ForEach((c) => spriteDatas.Add(c.toSpriteDataModel()));

            return spriteDatas;
        }
    }

    [Obsolete("CharacterFileModelV0_2 is deprecated, use CharacterFileV0_3 instead.")]
    public class CharacterFileModelV0_2 : BaseCharacterFileModel
    {
        [JsonIgnore]
        public const string _version = "0.2";

        public override Version Version { get; set; } = new Version("0.2");

        [JsonProperty("characters")]
        public List<CharacterJsonModelv0_2> Characters { get; set; }

        [JsonProperty("spriteData")]
        public List<SpriteJsonModelv0_3> SpriteData { get; set; }

        [JsonIgnore]
        public string CharacterBaseDir { get; set; }

        public CharacterFileModelV0_2() : base() { }

        public override Type CharacterFileVersion() => typeof(CharacterFileModelV0_2);

        public override List<CharacterDataModelWrapper> GetCharacterList()
        {
            List<CharacterDataModelWrapper> characterDatas = new();

            Characters.ForEach((c) => characterDatas.Add(c.toCharacterDataModel()));

            return characterDatas;
        }

        public override List<SpriteDataModelWrapper> GetSpriteList()
        {
            List<SpriteDataModelWrapper> spriteDatas = new();

            SpriteData.ForEach((c) => spriteDatas.Add(c.toSpriteDataModel()));

            return spriteDatas;
        }
    }

    // Mark with [Obsolete("CharacterFileModelV0_3 is deprecated, use CharacterFileV* instead.")] when I add a new version.
    public class CharacterFileModelV0_3 : BaseCharacterFileModel
    {
        [JsonIgnore]
        public const string _version = "0.3";

        public override Version Version { get; set; } = new Version("0.3");

        [JsonProperty("characters")]
        public List<CharacterJsonModelv0_3> Characters { get; set; }

        [JsonProperty("spriteData")]
        public List<SpriteJsonModelv0_3> SpriteData { get; set; }

        [JsonIgnore]
        public string CharacterBaseDir { get; set; }

        public CharacterFileModelV0_3() : base() { }

        public override Type CharacterFileVersion() => typeof(CharacterFileModelV0_3);

        public override List<CharacterDataModelWrapper> GetCharacterList()
        {
            List<CharacterDataModelWrapper> characterDatas = new();

            Characters.ForEach((c) => characterDatas.Add(c.toCharacterDataModel()));

            return characterDatas;
        }

        public override List<SpriteDataModelWrapper> GetSpriteList()
        {
            List<SpriteDataModelWrapper> spriteDatas = new();

            SpriteData.ForEach((c) => spriteDatas.Add(c.toSpriteDataModel()));

            return spriteDatas;
        }
    }
}
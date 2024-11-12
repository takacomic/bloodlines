using Bloodlines.src.DataModels;
using Bloodlines.src.json;
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
    public class AnimJsonModelv0_3
    {
        [JsonProperty("walk")]
        [DefaultValue(null)]
        public AnimObjectModel Walk { get; set; }

        [JsonProperty("idle")]
        [DefaultValue(null)]
        public AnimObjectModel Idle { get; set; }

        [JsonProperty("melee")]
        [DefaultValue(null)]
        public AnimObjectModel Melee { get; set; }

        [JsonProperty("melee2")]
        [DefaultValue(null)]
        public AnimObjectModel Melee2 { get; set; }

        [JsonProperty("ranged")]
        [DefaultValue(null)]
        public AnimObjectModel Ranged { get; set; }

        [JsonProperty("magic")]
        [DefaultValue(null)]
        public AnimObjectModel Magic { get; set; }

        [JsonProperty("special")]
        [DefaultValue(null)]
        public AnimObjectModel Special { get; set; }

        public AnimDataModelWrapper toAnimDataModel()
        {
            AnimDataModelWrapper modelWrapper = new();
            AnimDataModel c = new();
            modelWrapper.AnimSettings.Add(c);


            PropertyInfo[] myProps = GetType().GetProperties();

            foreach (PropertyInfo prop in myProps)
            {
                if (c.GetType().GetProperty(prop.Name) == null)
                {
#if DEBUG
                    Melon<BloodlinesMod>.Logger.Msg($"No match for {prop.Name}");
#endif // DEBUG

                    continue;
                }

                var value = prop.GetValue(this, null);
                c.GetType().GetProperty(prop.Name).SetValue(c, value);
            }

            return modelWrapper;
        }
    }

    public class AnimObjectModel
    {
        [JsonProperty("sprites")]
        public List<string> Sprites { get; set; }

        [JsonProperty("fps")]
        public int Fps { get; set; }

        [JsonProperty("loops")]
        public bool Loops { get; set; }
    }
}
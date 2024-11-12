using Bloodlines.src.JsonModels;
using Il2CppVampireSurvivors.Data;
using Il2CppVampireSurvivors.Data.Characters;
using Il2CppVampireSurvivors.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Bloodlines.src.DataModels
{
    // This is meant to be a json serializable copy of Vampire Survivors's Character data.
    // VampireSurvivors CharacterData has nullables, which make it hard to do anything. Copy the class into this.

    public class AnimDataModelWrapper
    {

        public List<AnimDataModel> AnimSettings { get; set; } = new();

        [JsonIgnore]
        public string BaseDirectory { get; set; }

        [JsonIgnore]
        public AnimDataModel Anim
        {
            get
            {
                if (AnimSettings.Any())
                {
                    return AnimSettings[0];
                }
                else
                {
                    throw new System.Exception("Anims hasn't been set yet.");
                }
            }
        }
    }

    public class AnimDataModel
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
    }
}
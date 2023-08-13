﻿using Il2CppVampireSurvivors.Data;
using Il2CppVampireSurvivors.Data.Characters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyAddCharacter
{
    internal class ChracterJson
    {
        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("startingWeapon")]
        public string StartingWeapon { get; set; }

        [JsonProperty("cooldown")]
        public int Cooldown { get; set; }

        [JsonProperty("charName")]
        public string CharName { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("textureName")]
        public string TextureName { get; set; }

        [JsonProperty("spriteName")]
        public string SpriteName { get; set; }

        [JsonProperty("skins")]
        public List<Skin> Skins { get; set; }

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

        [JsonProperty("completedStages")]
        public List<object> CompletedStages { get; set; }

        [JsonProperty("survivedMinutes")]
        public int SurvivedMinutes { get; set; }

        [JsonProperty("enemiesKilled")]
        public int EnemiesKilled { get; set; }

        [JsonProperty("stageData")]
        public List<object> StageData { get; set; }

        [JsonProperty("maxHp")]
        public int MaxHp { get; set; }

        [JsonProperty("armor")]
        public int Armor { get; set; }

        [JsonProperty("regen")]
        public int Regen { get; set; }

        [JsonProperty("moveSpeed")]
        public int MoveSpeed { get; set; }

        [JsonProperty("power")]
        public double Power { get; set; }

        [JsonProperty("area")]
        public int Area { get; set; }

        [JsonProperty("speed")]
        public int Speed { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("luck")]
        public int Luck { get; set; }

        [JsonProperty("growth")]
        public int Growth { get; set; }

        [JsonProperty("greed")]
        public int Greed { get; set; }

        [JsonProperty("curse")]
        public int Curse { get; set; }

        [JsonProperty("magnet")]
        public int Magnet { get; set; }

        [JsonProperty("revivals")]
        public int Revivals { get; set; }

        [JsonProperty("rerolls")]
        public int Rerolls { get; set; }

        [JsonProperty("skips")]
        public int Skips { get; set; }

        [JsonProperty("banish")]
        public int Banish { get; set; }

        [JsonProperty("showcase")]
        public List<string> Showcase { get; set; }
    }
    public class Skin
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("textureName")]
        public string TextureName { get; set; }

        [JsonProperty("spriteName")]
        public string SpriteName { get; set; }

        [JsonProperty("walkingFrames")]
        public int WalkingFrames { get; set; }

        [JsonProperty("unlocked")]
        public bool Unlocked { get; set; }
    }
}

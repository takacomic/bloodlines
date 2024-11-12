using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Bloodlines.src.DataModels
{
   public class SpriteDataModelWrapper
    {

        public List<SpriteDataModel> SpriteSettings { get; set; } = new();

        [JsonIgnore]
        public string BaseDirectory { get; set; }

        public string TexturePath => Path.Combine(BaseDirectory, Sprite.TextureName);

        public string TextureNameWithoutExtension => Sprite.TextureName.Split(".")[0];

        public string SpriteNameWithoutExtension => Sprite.SpriteName.Split(".")[0];

        [JsonIgnore]
        public SpriteDataModel Sprite
        {
            get
            {
                if (SpriteSettings.Any())
                {
                    return SpriteSettings[0];
                }
                else
                {
                    throw new System.Exception("Sprites hasn't been set yet.");
                }
            }
        }
    }

    public class SpriteDataModel
    {
        [JsonProperty("rect")]
        public Rect Rect { get; set; }

        [JsonProperty("pivot")]
        public Vector2 Pivot { get; set; }

        [JsonProperty("spriteName")]
        public string SpriteName { get; set; }

        [JsonProperty("textureName")]
        public string TextureName { get; set; }
    }
}
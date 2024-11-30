using Bloodlines.src.DataModels;
using MelonLoader;
using Newtonsoft.Json;
using System.Reflection;
using System.Text.Json;
using UnityEngine;

namespace Bloodlines.src.JsonModels
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SpriteJsonModelv0_3
    {
        [JsonProperty("rect")]
        public Rect Rect { get; set; }

        /*[JsonProperty("pivot")]
        public Vector2 Pivot { get; set; }*/

        [JsonProperty("spriteName")]
        public string SpriteName { get; set; }

        [JsonProperty("textureName")]
        public string TextureName { get; set; }

        public SpriteDataModelWrapper toSpriteDataModel()
        {
            SpriteDataModelWrapper modelWrapper = new();
            SpriteDataModel c = new();
            modelWrapper.SpriteSettings.Add(c);


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
}
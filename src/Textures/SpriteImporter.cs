using MelonLoader;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Bloodlines
{
    public class SpriteImporter : MonoBehaviour
    {
        internal static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        public static Texture2D LoadTexture(string FilePath)
        {
            Texture2D texture;

            if (!File.Exists(FilePath))
            {
                throw new ArgumentException($"FilePath does not exist. <{FilePath}>");
            }
            using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            {
                byte[] imageBytes = new byte[fs.Length];
                fs.Read(imageBytes, 0, imageBytes.Length);
                texture = new Texture2D(2, 2);

                if (!ImageConversion.LoadImage(texture, imageBytes))
                {
                    throw new Exception("ImageConversion.LoadImage failed");
                }

                // Point makes the pixels come out much clearer.
                texture.filterMode = FilterMode.Point;
                texture.name = Path.GetFileNameWithoutExtension(FilePath);
                Melon<BloodlinesMod>.Logger.Msg($"{texture.name}, {texture.width}, {texture.height}");

                textures.Add(texture.name, texture);

                return texture;
            }
        }

        public static Sprite LoadSprite(Texture2D texture, Rect rect)
        {
            Sprite sprite = Sprite.Create(texture, new Rect((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height), new Vector2(0.5f, 0.5f));
            sprite.name = texture.name;
            return sprite;
        }
    }
}
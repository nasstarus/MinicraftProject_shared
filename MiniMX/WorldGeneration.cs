using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Content;

namespace MiniMX;

public class WorldGeneration : Sprite
{
    private Texture2D tileTextures;
    private Rectangle[] tileSourceRect = new Rectangle[9];
    private Dictionary<Vector2, int> tileMap = LoadMap("../../../Data/tilemap.csv");

    public void LoadContent(ContentManager content)
    {
        tileTextures = content.Load<Texture2D>("Textures/TileSet");
        tileSourceRect = Utils.GetSpriteSheetSourceRects(tileTextures, 16, 16, 3, 3, 9);
    }

    // From csv to array
    private static Dictionary<Vector2, int> LoadMap(string filePath)
    {
        Dictionary<Vector2, int> result = new();

        StreamReader reader = new(filePath);

        string line;
        int y = 0;
        while ((line = reader.ReadLine()) != null)
        {
            string[] items = line.Split(',');

            for (int x = 0; x < items.Length; x++)
            {
                if (int.TryParse(items[x], out int value))
                {
                    result[new Vector2(x, y)] = value;
                }
            }

            y++;
        }

        return result;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var item in tileMap)
        {
            Rectangle dest = new(
                (int)item.Key.X * 64,
                (int)item.Key.Y * 64,
                64, 64
            );
            Rectangle src = tileSourceRect[item.Value];
            spriteBatch.Draw(tileTextures, dest, src, Color.White);
        }
    }
}
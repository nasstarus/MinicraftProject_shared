using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace MiniMX;
public class WorldGeneration : Sprite
{
    private Texture2D tileSet;
    private int tileWidth = 16;
    private int tileHeight = 16;
    private Dictionary<int, Texture2D> tilesDictionary = new Dictionary<int, Texture2D>();
    
    int[,] Tiles =
    {
        {0,1,1,1,1,1,1,1,1,1,1,1,2},
        {3,4,4,4,4,4,4,4,4,4,4,4,5},
    };
    public void LoadTileSet(ContentManager Content, GraphicsDevice graphicsDevice, string fileName)
    {
        tileSet = Content.Load<Texture2D>(fileName);
        
        int tileIndex = 0;
        for (int y = 0; y != tileSet.Height; y += tileHeight)
        {
            for (int x = 0; x != tileSet.Width; x += tileWidth)
            {
                Texture2D tileTexture = new Texture2D(graphicsDevice, tileWidth, tileHeight);
                Color[] tileData = new Color[tileWidth * tileHeight];

                tileSet.GetData(0, new Rectangle(x, y, tileWidth, tileHeight), tileData, 0,
                    tileData.Length);
                tileTexture.SetData(tileData);
                tilesDictionary.Add(tileIndex, tileTexture);
                ++tileIndex;
            }
        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        for (int row = 0; row < Tiles.GetLength(0); row++)
        {
            for (int col = 0; col < Tiles.GetLength(1); col++)
            {
                int tileIndex = Tiles[row, col];
                spriteBatch.Draw(tilesDictionary[tileIndex], new Vector2(col * 16, row * 16), Color.White);
            }
        }
    }
    
}
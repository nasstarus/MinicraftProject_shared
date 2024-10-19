using System;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MiniMX;

public static class Utils
{
    /// <summary>
    /// Returns the rectangles where are sprites located in spritesheet. basically importer for spritesheets and animations
    /// </summary>
    /// <param name="spriteSizeX">the width of each sprite in pixels</param>
    public static Rectangle[] GetSpriteSheetSourceRects(Texture2D spriteSheet, int spriteSizeX, int spriteSizeY, int rows, int columns, int numberOfSprites)
    {
        Rectangle[] sprites = new Rectangle[numberOfSprites]; //initiate an array of sourceRects
        
        int index = 0;
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                sprites[index] = new Rectangle(c * spriteSizeX, r * spriteSizeY, spriteSizeX, spriteSizeY);
                    
                index++;
                if (index >= numberOfSprites) // if the number of sprites has been reached, wont output empty rectangles
                {
                    return sprites;
                } 
            }
        }

        return sprites;
    }

    public static void Log(SpriteFont font, SpriteBatch _spriteBatch, string text)
    {
        _spriteBatch.DrawString(font, text, new Vector2(250, 1000), Color.MediumVioletRed);
    }
}
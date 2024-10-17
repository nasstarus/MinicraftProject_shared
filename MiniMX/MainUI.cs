using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = System.Numerics.Vector2;

namespace MiniMX;

public class MainUI : Sprite
{
    private int offset = 64;
    private int numberOfSlots = 8;
    private int sizeOfSlot = 128;
    private int sizeOfSelectedSlot = 150;
    private Vector2 firstPosition;
    public int selectedSlot = 0;

    public void Initialize(Viewport _viewport)
    {
        firstPosition = new Vector2(offset, _viewport.Height/2 - sizeOfSlot/2 * numberOfSlots);
    }
    public void LoadContent(ContentManager Content) //import spritesheet
    {
        texture = Content.Load<Texture2D>(@"inventoryTexture");
        sourceRects = Utils.GetSpriteSheetSourceRects(texture, 16, 16, 1, 2, 2);
    }

    public void Update(GameTime gameTime)
    {
        
    }
    public void Draw(SpriteBatch _spriteBatch)
    {
        for (int i = 0; i < numberOfSlots; i++)
        {
            if (i == selectedSlot) // Draw the hotbar vertically with the selected bar being highlighted
            {
                _spriteBatch.Draw(
                    texture,
                    new Rectangle((int)firstPosition.X - (sizeOfSelectedSlot - sizeOfSlot)/2,
                        (int)firstPosition.Y + i * sizeOfSlot - (sizeOfSelectedSlot - sizeOfSlot)/2,
                        sizeOfSelectedSlot, sizeOfSelectedSlot),
                    sourceRects[1],
                    Color.White);
            }
            else
            {
                _spriteBatch.Draw(texture, new Rectangle((int)firstPosition.X, (int)firstPosition.Y + i * sizeOfSlot, sizeOfSlot, sizeOfSlot), sourceRects[0], Color.White);
            }
        }
    }
}
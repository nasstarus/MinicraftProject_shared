using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MiniMX;

public class Player
{
    private int scale = 10;
    private int speed = 5;
    private float rotation = 0;
    private Texture2D texture;
    private Vector2 position = Vector2.Zero;
    public Vector2 trueSize
    {
        get { return new Vector2(texture.Width*scale, texture.Height*scale); }
        set { trueSize = value; }
    }

    public void LoadContent(ContentManager Content)
    {
        texture = Content.Load<Texture2D>("player");
    }

    public void Update(GameTime gameTime)
    {
        Vector2 movement = Vector2.Zero;
        
        if (Keyboard.GetState().IsKeyDown(Keys.W)) //check for input WASD
        {
            movement += -Vector2.UnitY;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.S))
        {
            movement += Vector2.UnitY;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.A))
        {
            movement += -Vector2.UnitX;
            flipSprite = SpriteEffects.None;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.D))
        {
            movement += Vector2.UnitX;
            flipSprite = SpriteEffects.FlipHorizontally;
        }

        if (movement != Vector2.Zero) // normalization and speed scaling
        {
            movement.Normalize();
            movement *= speed;
        }
        position += movement;
    }

    private SpriteEffects flipSprite = SpriteEffects.None;

    public void Draw(SpriteBatch _spriteBatch)
    {
        _spriteBatch.Draw(texture,
            position,
            null,
            Color.White,
            rotation,
            Vector2.Zero,
            new Vector2(scale, scale),
            flipSprite,
            1f);
    }
}
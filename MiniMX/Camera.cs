using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniMX;

public class Camera
{
    private Vector2 offset = Vector2.Zero;
    
    public List<Sprite> sprites = new List<Sprite>();
    public Vector2 position = Vector2.Zero;

    public Camera(Vector2 position)
    {
        this.position = position;
    }

    public void Draw(SpriteBatch _spriteBatch)
    {
        foreach (var sprite in sprites)
        {
            _spriteBatch.Draw(
                sprite.texture,
                sprite.position - position,
                sprite.sourceRectangle,
                sprite.color,
                sprite.rotation,
                sprite.origin,
                sprite.scale,
                sprite.SpriteEffects,
                sprite.layer
                );
        }
    }
}
using System.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MiniMX;

public class Sprite
{
    public Texture2D texture;
    public Vector2 position = Vector2.Zero;
    public Rectangle? sourceRectangle = null;
    public Color color = Color.White;
    public float rotation = 0f;
    public Vector2 origin = Vector2.Zero;
    public Vector2 scale = Vector2.One;
    public SpriteEffects SpriteEffects = SpriteEffects.None;
    public float layer = 0f;

    public Sprite(Texture2D? texture = null,
        Vector2? position = null,
        Rectangle? sourceRectangle = null,
        Color? color = null,
        float rotation = 0f,
        Vector2? origin = null,
        Vector2? scale = null,
        SpriteEffects spriteEffects = SpriteEffects.None,
        float layer = 0
        )
    {
        this.texture = texture;
        this.position = position ?? Vector2.Zero;
        this.color = color ?? Color.White;
        this.origin = origin ?? Vector2.Zero;
        this.scale = scale ?? Vector2.One;
        
    }
    
    public virtual void Update(GameTime gameTime)
    {}
    
    public virtual void Initialize(ContentManager Content)
    {}
}
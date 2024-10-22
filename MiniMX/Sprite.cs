using System.Collections.Generic;
using System.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MiniMX;

public class Sprite
{
    public static List<Sprite> spritesToDraw = new List<Sprite>(); //draw all the not moving sprites here
    public static List<GroundItem> itemsOnGround = new List<GroundItem>(); // for the sprites on ground
    
    public Texture2D texture;
    public Vector2 position;
    public Rectangle rectangle;
    public Rectangle? sourceRectangle;
    public Color color;
    public float rotation;
    public Vector2 origin;
    public Vector2 scale;
    public SpriteEffects SpriteEffects;
    public float layer;

    public Rectangle[] sourceRects;
    public int textureID;

    public Sprite(Texture2D? texture = null,
        Vector2? position = null,
        Rectangle? rectangle = null,
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
        this.rectangle = rectangle ?? Rectangle.Empty;
        this.sourceRectangle = sourceRectangle;
        this.color = color ?? Color.White;
        this.rotation = rotation;
        this.origin = origin ?? Vector2.Zero;
        this.scale = scale ?? Vector2.One;
        this.SpriteEffects = spriteEffects;
        this.layer = layer;

    }
    
    public virtual void Update(GameTime gameTime)
    {}
    
    public virtual void Initialize(ContentManager Content)
    {}
}
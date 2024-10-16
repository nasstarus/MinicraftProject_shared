using Microsoft.Xna.Framework;
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
}
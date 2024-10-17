using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Camera
{
    private Vector2 position;
    private float zoom;
    private float rotation;
    private Viewport viewport;

    public Camera(Viewport viewport)
    {
        this.viewport = viewport;
        this.position = Vector2.Zero; // Camera at origin
        this.zoom = 1f;               // Default zoom (no scaling)
        this.rotation = 0f;           // No rotation
    }

    public Vector2 Position
    {
        get { return position; }
        set { position = value; }
    }

    public float Zoom
    {
        get { return zoom; }
        set { zoom = MathHelper.Clamp(value, 0.1f, 10f); } // Clamped to avoid extreme zooms
    }

    public float Rotation
    {
        get { return rotation; }
        set { rotation = value; }
    }

    // Create the transformation matrix
    public Matrix GetTransformationMatrix()
    {
        return
            Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) * // Translate world based on camera position
            Matrix.CreateRotationZ(rotation) * // Apply rotation
            Matrix.CreateScale(zoom, zoom, 1f) * // Apply zoom
            Matrix.CreateTranslation(new Vector3(viewport.Width / 2f, viewport.Height / 2f, 0)); // Center on screen
    }

    public Vector2 ScreenToWorld(Vector2 screenPosition)
    {
        // Get the inverse of the camera transformation matrix
        Matrix inverseTransform = Matrix.Invert(GetTransformationMatrix());

        return Vector2.Transform(screenPosition, inverseTransform);
    }

    public Vector2 WorldToScreen(Vector2 worldPosition)
    {
        return Vector2.Transform(worldPosition, GetTransformationMatrix());
    }

    /*
    public void Draw(SpriteBatch _spriteBatch)
    {
        foreach (var sprite in sprites) //draw each sprite in the list sprites
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
        
        sprites.Clear(); //clear the list so we can update it each frame
    }
    
    */
}
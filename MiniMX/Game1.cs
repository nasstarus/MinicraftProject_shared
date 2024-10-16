using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MiniMX;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    private WorldGeneration worldGeneration = new WorldGeneration();
    private Player player = new Player();
    private Camera camera;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }
    
    //TODO: delete after test
    private Texture2D texture;
    protected override void Initialize()
    {
        camera = new Camera(Vector2.Zero); // initialize camera to player position

        texture = new Texture2D(GraphicsDevice, 50, 50);
        Color[] colorData = new Color[50 * 50];
        for (int i = 0; i < colorData.Length; i++)
        {
            colorData[i] = Color.White;
        }
        texture.SetData(colorData);
        for (int i = 0; i < 10; i++)
        {
            camera.sprites.Add(new Sprite(texture, new Vector2(i*50, i*50)));
        }
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        worldGeneration.LoadTileSet(Content, GraphicsDevice, "TileSet");
        player.LoadContent(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        player.Update(gameTime);
        camera.position = player.position;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkSlateGray);
        
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        
        worldGeneration.Draw(_spriteBatch);
        player.Draw(_spriteBatch, _graphics);
        camera.Draw(_spriteBatch);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
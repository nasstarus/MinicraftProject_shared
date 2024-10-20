using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MiniMX;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Viewport _viewport;
    
    private WorldGeneration worldGeneration = new WorldGeneration();
    private Player player = new Player();
    private Camera camera;
    private MainUI mainUI = new MainUI();
    
    public static SpriteFont defaultFont;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }
    
    protected override void Initialize()
    {
        _graphics.PreferredBackBufferHeight = 1080; //Initialize _graphics
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.IsFullScreen = true;
        _graphics.ApplyChanges();
        
        _viewport = GraphicsDevice.Viewport;
        
        camera = new Camera(_viewport); // initialize camera to player position
        mainUI.Initialize(_viewport);
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        defaultFont = Content.Load<SpriteFont>("defaultFont"); //TODO: use when wanna debug (utils class)
        
        worldGeneration.LoadTileSet(Content, GraphicsDevice, "Textures/TileSet");
        player.LoadContent(Content);
        mainUI.LoadContent(Content);
        
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        player.Update(gameTime);
        
        camera.Position = player.centerPosition;
        mainUI.Update(gameTime);
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkSlateGray);
        
        
        //the draw that is affected by camera
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera.GetTransformationMatrix());
        
        worldGeneration.Draw(_spriteBatch);
        Utils.DrawSpriteList(_spriteBatch); // draws the static sprite list of all sprites in game
        player.Draw(_spriteBatch, _graphics);
        
        _spriteBatch.End();
        
        
        //the draw that is rendered only on screenPos
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        
        mainUI.Draw(_spriteBatch);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
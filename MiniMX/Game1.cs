using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MiniMX;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    public static SpriteBatch _spriteBatch;
    private Viewport _viewport;
    
    private WorldGeneration worldGeneration = new WorldGeneration();
    private Player player = new Player();
    private Camera camera;
    private Inventory inventory = new Inventory();
    
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
        inventory.Initialize(_viewport);
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        defaultFont = Content.Load<SpriteFont>("defaultFont");
        GroundItem.underglow = Content.Load<Texture2D>("Textures/Underglow");
        
        worldGeneration.LoadTileSet(Content, GraphicsDevice, "Textures/TileSet");
        player.LoadContent(Content);
        inventory.LoadContent(Content);
        
        
        //todo: delete after testiong
        for (int i = 0; i < 3; i++)
        {
            Sprite.itemsOnGround.Add(new GroundItem(Inventory.itemList[i], new Vector2(100 * i , 200), 5));
        }
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        player.Update(gameTime);
        
        camera.Position = player.centerPosition;
        inventory.Update(gameTime);
        foreach (var item in Sprite.itemsOnGround)
        {
            item.Update(gameTime);
        }
        GroundItem.DeleteItemsToRemove(); //must be called after updating items on ground
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkSlateGray);
        
        
        //the draw that is affected by camera
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera.GetTransformationMatrix());
        
        worldGeneration.Draw(_spriteBatch);
        Utils.DrawSpriteList(_spriteBatch); // draws the static sprite list of all sprites in game
        foreach (var item in Sprite.itemsOnGround) //draw all the dropped items
        {
            item.Draw(_spriteBatch);
        }
        player.Draw(_spriteBatch, _graphics);
        
        _spriteBatch.End();
        
        
        //the draw that is rendered only on screenPos
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        
        inventory.Draw(_spriteBatch);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
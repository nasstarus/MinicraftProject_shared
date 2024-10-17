﻿using System;
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
        
        camera.Position = player.centerPosition;
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkSlateGray);
        
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera.GetTransformationMatrix());
        
        worldGeneration.Draw(_spriteBatch);
        player.Draw(_spriteBatch, _graphics);
        
        _spriteBatch.End();
        

        base.Draw(gameTime);
    }
}
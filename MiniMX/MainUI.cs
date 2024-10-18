using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector2 = System.Numerics.Vector2;

namespace MiniMX;

public class MainUI : Sprite
{
    private int offset = 16;
    private int numberOfSlots = 10;
    private int sizeOfSlot = 80;
    private int sizeOfSelectedSlot = 100;
    private Vector2 firstPosition;
    
    public int selectedSlot = 0;
    public InventorySlot[,] inventory = new InventorySlot[10,5]; //represents the inventory + hotbar
    
    //TODO: make showing inventory
    //TODO: load items with ID
    //TODO: put items in inventory and draw them
    //TODO: select items with mouse and put them into other inv slots
    
    public class InventorySlot
    {
        public bool empty = true;
        public Item? item = null;
    }

        
    
    public void Initialize(Viewport _viewport)
    {
        //firstPosition = new Vector2(offset, _viewport.Height/2 - sizeOfSlot/2 * numberOfSlots);
        firstPosition = new Vector2(offset, offset); 
    }
    public void LoadContent(ContentManager Content) //import spritesheet
    {
        texture = Content.Load<Texture2D>(@"Textures/inventoryTexture");
        sourceRects = Utils.GetSpriteSheetSourceRects(texture, 16, 16, 1, 2, 2);
    }
    
    

    private Keys[] keys;
    private int lastMousPos = 0;
    private int mouseScrollDelta;
    public void Update(GameTime gameTime)
    {
        // set by keys (1, 2, ...)
        keys = Keyboard.GetState().GetPressedKeys();
        if(keys.Contains(Keys.D1)) {selectedSlot = 0;}
        else if(keys.Contains(Keys.D2)) {selectedSlot = 1;}
        else if(keys.Contains(Keys.D3)) {selectedSlot = 2;}
        else if(keys.Contains(Keys.D4)) {selectedSlot = 3;}
        else if(keys.Contains(Keys.D5)) {selectedSlot = 4;}
        else if(keys.Contains(Keys.D6)) {selectedSlot = 5;}
        else if(keys.Contains(Keys.D7)) {selectedSlot = 6;}
        else if(keys.Contains(Keys.D8)) {selectedSlot = 7;}
        else if(keys.Contains(Keys.D9)) {selectedSlot = 8;}
        else if(keys.Contains(Keys.D0)) {selectedSlot = 9;}
        
        //set by scroll Wheel
        mouseScrollDelta = Mouse.GetState().ScrollWheelValue - lastMousPos;
        if (mouseScrollDelta != 0)
        {
            Console.WriteLine(mouseScrollDelta);
            selectedSlot = MathHelper.Clamp(selectedSlot - mouseScrollDelta / 120, 0, numberOfSlots - 1);
        }
        lastMousPos = Mouse.GetState().ScrollWheelValue;
    }
    
    
    
    public void Draw(SpriteBatch _spriteBatch)
    {
        for (int i = 0; i < numberOfSlots; i++)
        {
            if (i == selectedSlot) // Draw the hotbar horizontaly with the selected bar being highlighted
            {
                _spriteBatch.Draw(
                    texture,
                    new Rectangle((int)firstPosition.X + i * sizeOfSlot - (sizeOfSelectedSlot - sizeOfSlot)/2,
                        (int)firstPosition.Y - (sizeOfSelectedSlot - sizeOfSlot)/2,
                        sizeOfSelectedSlot, sizeOfSelectedSlot),
                    sourceRects[1],
                    Color.White);
            }
            else
            {
                _spriteBatch.Draw(
                    texture,
                    new Rectangle((int)firstPosition.X + i * sizeOfSlot,
                        (int)firstPosition.Y,
                        sizeOfSlot, sizeOfSlot),
                    sourceRects[0],
                    Color.White);
            }
        }
    }
}




public class Item //TODO: deserialize JSON into this class, then set texture by GetSpriteSheetSourceRects
{
    public Texture2D SpriteSheet;
    public Rectangle sourceRectangle;
    
    public int ID;
    public string name;
    public Color rarity;
    public string description;
}
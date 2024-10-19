using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text.Json;

namespace MiniMX;

public class MainUI
{
    readonly int offset = 16;
    readonly int numberOfSlots = 10;
    readonly int sizeOfSlot = 64;
    readonly int sizeOfSelectedSlot = 75;
    private Vector2 firstPosition;
    private Texture2D inventoryTextures;
    private Rectangle[] sourceRects;
    
    public int selectedSlot = 0;
    public InventorySlot[,] inventory = new InventorySlot[10,5]; //represents the inventory with hot bar

    public List<Item> itemList = new List<Item>();
    
    //TODO: select items with mouse and put them into other inv slots
    
    public class InventorySlot
    {
        public bool empty = true;

        private Item? _item; // Backing field for the property

        public Item? item
        {
            get { return _item; } // Return the backing field
            set
            {
                _item = value; // Set backing field to the new value
                empty = (value == null); // Update empty based on value
            }
        }
    }
    
    public MainUI()
    {
        // Initialize InventorySlots in the constructor
        for (int i = 0; i < inventory.GetLength(0); i++)
        {
            for (int j = 0; j < inventory.GetLength(1); j++)
            {
                inventory[i, j] = new InventorySlot(); // Properly initialize each InventorySlot
            }
        }
    }

        
    
    public void Initialize(Viewport viewport)
    {
        firstPosition = new Vector2(offset, offset); 
    }
    public void LoadContent(ContentManager Content) //import spritesheet
    {
        inventoryTextures = Content.Load<Texture2D>(@"Textures/inventoryTexture");
        sourceRects = Utils.GetSpriteSheetSourceRects(inventoryTextures, 16, 16, 3, 1, 3);

        Texture2D items1 = Content.Load<Texture2D>("Textures/Items/items1");
        
        DeserializeJSON();
        SetTexturesAndRectangles(new []{1, 2, 3}, items1, 32, 32, 1, 3, 3);
        
        //TODO: delete
        inventory[0, 4].item = itemList[0];
        inventory[1, 4].item = itemList[1];
        inventory[5, 2].item = itemList[2];
    }
    
    

    private Keys[] keys;
    private int lastMousPos = 0;
    private int mouseScrollDelta;
    private bool inventoryOpen = false;
    private bool changedInventoryState = false;
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

        // open inventory when I is pressed
        if (keys.Contains(Keys.I))
        {
            if (inventoryOpen && !changedInventoryState)
            {
                firstPosition = new Vector2(offset, offset); 
                
                inventoryOpen = false;
                changedInventoryState = true;
            }
            else if (!inventoryOpen && !changedInventoryState)
            {
                firstPosition = new Vector2(offset, offset + sizeOfSlot * (inventory.GetLength(1)-1)); 
                
                inventoryOpen = true;
                changedInventoryState = true;
            }
        }
        else
        {
            changedInventoryState = false;
        }
        
        //set by scroll Wheel
        mouseScrollDelta = Mouse.GetState().ScrollWheelValue - lastMousPos;
        if (mouseScrollDelta != 0)
        {
            selectedSlot = MathHelper.Clamp(selectedSlot - mouseScrollDelta / 120, 0, numberOfSlots - 1);
        }
        lastMousPos = Mouse.GetState().ScrollWheelValue;
    }
    
    
    
    public void Draw(SpriteBatch _spriteBatch)
    {
        // Draw the hotbar horizontaly with the selected bar being highlighted
        for (int i = 0; i < numberOfSlots; i++)
        {
            if (i == selectedSlot) 
            {
                _spriteBatch.Draw(
                    inventoryTextures,
                    new Rectangle((int)firstPosition.X + i * sizeOfSlot - (sizeOfSelectedSlot - sizeOfSlot)/2,
                        (int)firstPosition.Y - (sizeOfSelectedSlot - sizeOfSlot)/2,
                        sizeOfSelectedSlot, sizeOfSelectedSlot),
                    sourceRects[1],
                    Color.White);
            }
            else
            {
                _spriteBatch.Draw(
                    inventoryTextures,
                    new Rectangle((int)firstPosition.X + i * sizeOfSlot,
                        (int)firstPosition.Y,
                        sizeOfSlot, sizeOfSlot),
                    sourceRects[0],
                    Color.White);
            }

            if (!inventory[i, 4].empty) // if something is in hot bar, draw it
            {
                _spriteBatch.Draw(
                    inventory[i, 4].item.spriteSheet,
                    new Rectangle((int)firstPosition.X + i * sizeOfSlot,
                        (int)firstPosition.Y,
                        sizeOfSlot, sizeOfSlot),
                    inventory[i, 4].item.sourceRectangle,
                    Color.White);
            }
        }
        
        // draw the inventory when opened
        if (inventoryOpen)
        {
            for (int y = 0; y < inventory.GetLength(1) - 1; y++)
            {
                for (int x = 0; x < inventory.GetLength(0); x++)
                {
                    _spriteBatch.Draw(
                        inventoryTextures,
                        new Rectangle(offset + sizeOfSlot * x, offset + sizeOfSlot * y, sizeOfSlot, sizeOfSlot),
                        sourceRects[2],
                        Color.White
                        );

                    if (!inventory[x, y].empty)
                    {
                        _spriteBatch.Draw(
                            inventory[x, y].item.spriteSheet,
                            new Rectangle(offset + sizeOfSlot * x, offset + sizeOfSlot * y, sizeOfSlot, sizeOfSlot),
                            inventory[x, y].item.sourceRectangle,
                            Color.White);
                    }
                }
            }
        }
    }

    
    
    
    void DeserializeJSON()
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ItemList.json");
        string jsonData = File.ReadAllText(filePath);
        
        itemsWrapper wrapper = JsonSerializer.Deserialize<itemsWrapper>(jsonData);
        itemList = wrapper.items;
    }

    void SetTexturesAndRectangles(int[] idsOfItems, Texture2D spritesheet, int sizeSpriteX, int sizeSpriteY, int rows, int columns, int numberOfItems)
    {
        Rectangle[] sourceRectangles =
            Utils.GetSpriteSheetSourceRects(spritesheet, sizeSpriteX, sizeSpriteY, rows, columns, numberOfItems);
        
        for (int i = 0; i < idsOfItems.Length; i++)
        {
            // fucking cool functionality lol
            itemList.Find(item => item.id == idsOfItems[i]).spriteSheet = spritesheet;
            itemList.Find(item => item.id == idsOfItems[i]).sourceRectangle = sourceRectangles[i];
        }
    }
}

//deserialize JSON into this class, then set texture by GetSpriteSheetSourceRects
public class Item 
{
    public Texture2D? spriteSheet { get; set; }
    public Rectangle? sourceRectangle { get; set; }
    
    public int id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string type { get; set; }
    public bool stackable { get; set; }
    public bool isMaterial { get; set; }
    public string? rarity { get; set; }
    public Stats? stats { get; set; }
}
public class Stats
{
    public int? axePower { get; set; }
    public int? pickaxePower { get; set; }
}

public class itemsWrapper
{
    public List<Item> items { get; set; }
}
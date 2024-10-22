using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniMX;

public class GroundItem
{
    public static Texture2D underglow;
    
    public Vector2 position;
    public float pickupDelay = 3;
    
    public Item item;
    public int count;

    private Color underglowColor;

    public GroundItem(Item item, Vector2 position, int count = 1)
    {
        this.item = item;
        this.position = position;
        this.count = count;
        
        // very cool switch expression to set underglow color by rarity
        underglowColor = item.rarity switch
        {
            "common"    => Color.Transparent,
            "uncommon"  => Color.GreenYellow,
            "rare"      => Color.DarkOrange,
            "epic"      => Color.DeepPink,
            _           => Color.Transparent
        };
    }

    private float distanceToPlayer = 0;
    private Vector2 legibleInvSlotPos = Vector2.Zero;
    private static List<GroundItem> itemsToRemove = new List<GroundItem>();
    public void Update(GameTime gameTime)
    {
        // if the player is close enough, find empty or same slot
        distanceToPlayer = Vector2.Distance(Player.position, position);
        if (distanceToPlayer <= Player.pickupDistance)
        {
            //if the slot is not legible, find new one
            if (legibleInvSlotPos == new Vector2(-1) ||
                !Inventory.inventory[(int)legibleInvSlotPos.X, (int)legibleInvSlotPos.Y].empty)
            {
                legibleInvSlotPos = FindLegibleItemslot();
            }
            
            //if there is an empty slot in inventory
            if(legibleInvSlotPos != new Vector2(-1))
            {
                // Move the object toward the player at a consistent speed
                position = Vector2.Lerp(position, Player.position, 0.1f);

                if (distanceToPlayer <= 5)
                { 
                    Inventory.inventory[(int)legibleInvSlotPos.X, (int)legibleInvSlotPos.Y].item = item;
                    if (item.stackable)
                    {
                        Inventory.inventory[(int)legibleInvSlotPos.X, (int)legibleInvSlotPos.Y].count += count;
                    }
                    
                    itemsToRemove.Add(this);
                }
            }
        } 
    }

    public static void DeleteItemsToRemove()
    {
        foreach (var item in itemsToRemove)
        {
            Sprite.itemsOnGround.Remove(item);
        }
        itemsToRemove.Clear();
    }

    private Vector2 FindLegibleItemslot()
    {
        // find slot with the same item
        for (var y = 0; y < Inventory.inventory.GetLength(1); y++)
        {
            for (var x = 0; x < Inventory.inventory.GetLength(0); x++)
            {
                if (Inventory.inventory[x, y].item == item && item.stackable)
                {
                    return new Vector2(x, y);
                }
            }
        }
        
        // if there is no same stackable item
        for (var y = 0; y < Inventory.inventory.GetLength(1); y++)
        {
            for (var x = 0; x < Inventory.inventory.GetLength(0); x++)
            {
                if (Inventory.inventory[x, y].empty)
                {
                    return new Vector2(x, y);
                }
            }
        }
        
        // if no legible space found, return (-1, -1)
        return new Vector2(-1);
    }

    public void Draw(SpriteBatch _spriteBatch)
    {
        Utils.Log(_spriteBatch, distanceToPlayer.ToString());
        
        _spriteBatch.Draw(underglow,
            new Rectangle( (int)position.X, (int)position.Y, 64, 64),
            null,
            underglowColor
        );
        
        _spriteBatch.Draw(item.spriteSheet,
        new Rectangle( (int)position.X, (int)position.Y, 64, 64),
            item.sourceRectangle,
            Color.White
        );
    }
}
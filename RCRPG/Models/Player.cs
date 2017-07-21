using RCRPG.Context;
using System;
using System.Collections.Generic;
using System.Text;
using static RCRPG.Models.Item;

namespace RCRPG.Models
{
    public class Player
    {
        public Guid PlayerId { get; set; }
        public string Name { get; set; }
        public DateTime LastPlayed { get; set; }

        public Guid PlayerItemId { get; set; }
        public PlayerItem PlayerItem { get; set; }
        public Guid RoomId { get; set; }
        public Room Room { get; set; }
        public Guid InventoryId { get; set; }
        public Inventory Inventory { get; set; }

        public Player()
        {
            Init();
            Inventory = new Inventory();
        }
        public Player(string name, Room room, Inventory inventory)
        {
            InitAdvanced(name, room, inventory);
            Item hand = new Item(ItemType.None, "Hand");

            PlayerItem = new PlayerItem
            {
                PlayerItemId = Guid.NewGuid(),
                Player = this,
                Item = hand
            };
        }

        public Player(string name, Room room, Inventory inventory, PlayerItem playerItem)
        {
            InitAdvanced(name, room, inventory);
            PlayerItem = playerItem;
        }

        private void Init()
        {
            PlayerId = Guid.NewGuid();
        }

        private void InitAdvanced(string name, Room room, Inventory inventory)
        {
            Init();
            Name = name;
            Room = room;
            Inventory = inventory;
            LastPlayed = DateTime.Now;
        }

        public bool Unequip()
        {
            bool success = false;

            using (RPGContext context = new RPGContext())
            {
                if (this.PlayerItem.Item.Type == ItemType.None)
                {
                    //you cannot unequip your hand
                }
                else
                {
                    context.PlayerItems.Remove(PlayerItem);
                    Inventory.AddItem(PlayerItem.Item);
                    context.Update(Inventory);
                }
                context.SaveChanges();
            }

            return success;
        }

        public bool Equip(Item item)
        {
            bool success = false;

            using (RPGContext context = new RPGContext())
            {
                if(this.PlayerItem.Item.Type != ItemType.None)
                {
                    Unequip();                    
                }
                context.PlayerItems.Remove(this.PlayerItem);
                context.Items.Remove(this.PlayerItem.Item);

                
                bool inRoom = this.Room.Inventory.Items.Exists(x => x.ItemId == item.ItemId);
                bool inPlayerInventory = this.Inventory.Items.Exists(x => x.ItemId == item.ItemId);

                if (inRoom || inPlayerInventory)
                {
                    Inventory inventory;

                    if(inRoom)
                    {
                        inventory = this.Room.Inventory;
                    }
                    else
                    {
                        inventory = this.Inventory;
                    }
                    InventoryItem inventoryItem = inventory.Items.Find(x => x.ItemId == item.ItemId);

                    inventory.Items.Remove(inventoryItem);
                    context.Remove(inventoryItem);
                    context.Update(inventory);
                }
                else
                {
                    //the item is not somewhere that can be picked up and/or equipped
                }

                PlayerItem playerItem = new PlayerItem
                {
                    PlayerItemId = Guid.NewGuid(),
                    Player = this,
                    Item = item
                };

                context.PlayerItems.Add(playerItem);

                context.SaveChanges();
            }

            return success;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace RCRPG.Models
{
    public class Inventory
    {
        public Guid InventoryId { get; set; }

        public List<InventoryItem> Items { get; set; }

        public Inventory()
        {
            Init();
            Items = new List<InventoryItem>();
        }

        public Inventory(List<InventoryItem> items)
        {
            Init();
            Items = items;
        }

        private void Init()
        {
            InventoryId = Guid.NewGuid();
        }

        public bool AddItem(Item item)
        {
            bool success = false;

            InventoryItem inventoryItem = new InventoryItem(item, this);

            Items.Add(inventoryItem);

            return success;
        }

        public bool RemoveItem(Item item)
        {
            bool success = false;

            return success;
        }
    }
}

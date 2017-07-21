using System;
using System.Collections.Generic;
using System.Text;

namespace RCRPG.Models
{
    public class InventoryItem
    {
        public Guid InventoryItemId { get; set; }

        public Guid InventoryId { get; set; }
        public Inventory Inventory { get; set; }
        public Guid ItemId { get; set; }
        public Item Item { get; set; }

        public InventoryItem(Item item, Inventory inventory)
        {
            InventoryItemId = Guid.NewGuid();
            Item = item;
            Inventory = inventory;
        }
    }
}

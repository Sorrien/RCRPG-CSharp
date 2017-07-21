using System;
using System.Collections.Generic;
using System.Text;

namespace RCRPG.Models
{
    public class Item
    {
        public enum ItemType
        {
            Sledge = 1000,
            Ladder = 2000,
            Gold = 3000,
            Misc = 4000,
            None = 20000
        }
        public Guid ItemId { get; set; }
        public ItemType Type { get; set; }
        public string Name { get; set; }

        public Item()
        {
            Init();
            Type = ItemType.None;
            Name = Type.ToString();
        }
        public Item(ItemType type)
        {
            Init();
            Type = type;
            Name = Type.ToString();
        }
        public Item(ItemType type, string name)
        {
            Init();
            Type = type;
            Name = name;
        }

        private void Init()
        {
            ItemId = Guid.NewGuid();
        }
    }
}

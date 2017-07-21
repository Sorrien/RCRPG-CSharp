using System;
using System.Collections.Generic;
using System.Text;

namespace RCRPG.Models
{
    public class PlayerItem
    {
        public Guid PlayerItemId { get; set; }

        public Guid PlayerId { get; set; }
        public Player Player { get; set; }
        public Guid ItemId { get; set; }
        public Item Item { get; set; }
    }
}

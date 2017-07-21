using System;
using System.Collections.Generic;
using System.Text;

namespace RCRPG.Models
{
    public class Room
    {
        public Guid RoomId { get; set; }

        public Guid InventoryId { get; set; }
        public Inventory Inventory { get; set; }
        public Guid VectorId { get; set; }
        public Vector Vector { get; set; }

        public Room()
        {
            Init();
            Vector = new Vector(0, 0, 0);
        }

        public Room(Vector vector, Inventory inventory)
        {
            InitAdvanced(vector, inventory);
        }
        public Room(int x, int y, int z, Inventory inventory)
        {
            InitAdvanced(new Vector(x, y, z), inventory);
        }

        private void Init()
        {
            RoomId = Guid.NewGuid();
        }

        private void InitAdvanced(Vector vector, Inventory inventory)
        {
            Init();
        }
    }
}

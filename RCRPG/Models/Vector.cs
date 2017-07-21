using System;
using System.Collections.Generic;
using System.Text;

namespace RCRPG
{
    public class Vector
    {
        public Guid VectorId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Vector(int x, int y, int z)
        {
            VectorId = Guid.NewGuid();
            X = x;
            Y = y;
            Z = z;
        }
    }
}

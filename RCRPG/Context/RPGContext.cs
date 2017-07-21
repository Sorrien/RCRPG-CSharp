using Microsoft.EntityFrameworkCore;
using RCRPG.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCRPG.Context
{
    public class RPGContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerItem> PlayerItems { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<GameState> GameStates { get; set; }
        public DbSet<GameStateRoom> GameStateRooms { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Vector> Vectors { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=rpg.db");
        }
    }
}

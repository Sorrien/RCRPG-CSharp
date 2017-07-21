using RCRPG.Context;
using RCRPG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCRPG
{
    public class GameState
    {
        public Guid GameStateId { get; set; }

        public Player Player { get; set; }
        
        public List<GameStateRoom> Rooms { get; set; }
    }
}

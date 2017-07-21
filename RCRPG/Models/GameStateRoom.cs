using System;
using System.Collections.Generic;
using System.Text;

namespace RCRPG.Models
{
    public class GameStateRoom
    {
        public Guid GameStateRoomId { get; set; }

        public Guid GameStateId { get; set; }
        public GameState GameState { get; set; }
        public Guid RoomId { get; set; }
        public Room Room { get; set; }
    }
}

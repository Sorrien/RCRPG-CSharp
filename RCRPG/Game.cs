using System;
using System.Collections.Generic;
using System.Text;

namespace RCRPG
{
    class Game
    {
        public GameState GameState;

        public Game()
        {

        }

        public Game(GameState state)
        {
            GameState = state;
        }

        public string RunCommand(string command)
        {

            return "";
        }
    }
}

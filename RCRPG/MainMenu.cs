using RCRPG.Context;
using RCRPG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCRPG
{
    class MainMenu
    {
        private enum MenuOption
        {
            NewGame,
            Continue,
            Load,
            Exit,
            None
        }
        public MainMenu()
        {

        }

        public GameState Start()
        {
            GameState gameState = new GameState();

            bool optionPicked = false;
            string input = "";

            MenuOption[] options = Options().ToArray();

            while (!optionPicked)
            {
                Console.WriteLine(OptionsString());
                input = Console.ReadLine();
                int optionNum = -1;

                int.TryParse(input, out optionNum);


                MenuOption selected = MenuOption.None;

                if (optionNum != -1)
                {
                    selected = options[optionNum];
                }
                else
                {
                    foreach (MenuOption option in options)
                    {
                        if (option.ToString().ToLower().Contains(input.Replace(" ", "").ToLower()))
                        {
                            selected = option;
                        }
                    }
                }
                optionPicked = true;

                switch (selected)
                {
                    case MenuOption.NewGame:
                        gameState = NewGame();
                        break;
                    case MenuOption.Continue:
                        gameState = Continue();
                        break;
                    case MenuOption.Load:

                        break;
                    case MenuOption.Exit:

                        break;
                    case MenuOption.None:
                        Console.WriteLine("Not an option.");
                        optionPicked = false;
                        break;
                }

            }

            return gameState;
        }

        private List<MenuOption> Options()
        {
            List<MenuOption> values = new List<MenuOption>
            {
                MenuOption.NewGame
            };
            using (RPGContext context = new RPGContext())
            {
                if (context.Players.Count() > 0)
                {
                    values.Add(MenuOption.Continue);
                    values.Add(MenuOption.Load);
                }
            }
            values.Add(MenuOption.Exit);

            return values;
        }

        public string OptionsString()
        {
            List<MenuOption> values = Options();

            string optionsString = "";

            for (int i = 0; i < values.Count(); i++)
            {
                optionsString += (i + 1) + ". " + values[i].ToString() + "\n";
            }

            return optionsString;
        }

        private GameState NewGame()
        {
            GameState gameState = new GameState();
            bool nameEntered = false;
            string input = "";
            while (!nameEntered)
            {
                Console.WriteLine("Please enter a name:");
                input = Console.ReadLine();

                using (RPGContext context = new RPGContext())
                {
                    Player player = context.Players.FirstOrDefault(x => x.Name == input);
                    if (player == null)
                    {
                        nameEntered = true;
                    }
                    else
                    {
                        Console.Write("I'm sorry but that player name is taken. ");
                    }
                }
            }

            Room startRoom = new Room
            {
                RoomId = Guid.NewGuid(),
                //TODO:initialize the inventory with a sledgehammer
                Inventory = new Inventory(),
                Vector = new Vector(0,0,0)
            };

            Player newPlayer = new Player
            {
                PlayerId = Guid.NewGuid(),
                Name = input,
                PlayerItem = null,
                Inventory = new Inventory(),
                LastPlayed = DateTime.Now,
                Room = startRoom
            };

            gameState = new GameState
            {
                GameStateId = Guid.NewGuid(),
                Player = newPlayer,
                Rooms = new List<GameStateRoom>()
            };

            GameStateRoom gameStateStartRoom = new GameStateRoom
            {
                GameStateRoomId = Guid.NewGuid(),
                GameState = gameState,
                Room = startRoom
            };

            gameState.Rooms.Add(gameStateStartRoom);

            int prizeMinDistance = 5;
            int prizeMaxDistance = 20;
            Random rand = new Random();

            Room prizeRoom = new Room
            {
                RoomId = Guid.NewGuid(),
                //TODO:initialize the inventory with a TON of gold
                Inventory = new Inventory(),
                Vector = new Vector(rand.Next(prizeMinDistance, prizeMaxDistance), rand.Next(prizeMinDistance, prizeMaxDistance), rand.Next(prizeMinDistance, prizeMaxDistance))
            };

            GameStateRoom gameStatePrizeRoom = new GameStateRoom
            {
                GameStateRoomId = Guid.NewGuid(),
                GameState = gameState,
                Room = prizeRoom
            };

            gameState.Rooms.Add(gameStatePrizeRoom);

            using (RPGContext context = new RPGContext())
            {
                context.GameStates.Add(gameState);
                context.SaveChanges();
            }
            return gameState;
        }

        private GameState Continue()
        {
            GameState gameState = new GameState();

            using (RPGContext context = new RPGContext())
            {
                Player player = context.Players.OrderBy(x => x.LastPlayed).First();
                player.LastPlayed = DateTime.Now;
                context.Update(player);
                context.SaveChanges();
                gameState = context.GameStates.FirstOrDefault(x => x.Player == player);
            }

            return gameState;
        }
        private GameState Load()
        {
            GameState gameState = new GameState();
            using (RPGContext context = new RPGContext())
            {
                List<Player> players = context.Players.OrderBy(x => x.LastPlayed).ToList();
                foreach (Player player in players)
                {
                    Console.WriteLine(player.Name);
                }

                string input = "";
                bool savePicked = false;
                while (!savePicked)
                {
                    input = Console.ReadLine();
                    Player player = context.Players.FirstOrDefault(x => x.Name.ToLower() == input.ToLower());
                    if (player == null)
                    {
                        Console.WriteLine("Please enter a valid player name.");
                    }
                    else
                    {
                        Console.WriteLine("Loading player " + player.Name + "...");
                        gameState = context.GameStates.FirstOrDefault(x => x.Player == player);
                        savePicked = true;
                    }
                }
            }

            return gameState;
        }
    }
}
using System;

namespace RCRPG
{
    class Program
    {
        Game game;
        static void Main(string[] args)
        {            


            string input = "";
            bool exit = false;
            while (!exit)
            {
                input = Console.ReadLine();

                switch (input)
                {
                    case "exit":
                        Console.WriteLine("Are you sure you want to exit? (y/n)");
                        input = Console.ReadLine();
                        if (input.Contains("y"))
                        {
                            Console.WriteLine("Goodbye");
                            exit = true;
                        }
                        else
                        {
                            Console.WriteLine("Then we shall continue the game.");
                        }
                        break;
                    default:
                        Console.WriteLine();
                        break;
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using WordWhipperServer.Game;

namespace WordWhipperServer
{
    /// <summary>
    /// Tests the program for basic game functions
    /// </summary>
    class Test_GameProgram
    {
        /// <summary>
        /// Runs tests
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Default Game Board:");
            
            GameInstance game = new GameInstance();
            game.PrintBoardToConsole();

            Console.WriteLine("\nRandom Game Board:");

            List<GameFlags> flags = new List<GameFlags>();
            flags.Add(GameFlags.RANDOM_BOARD_MULTIPLIERS);

            game = new GameInstance(flags, GameLanguages.ENGLISH);
            game.PrintBoardToConsole();

            Console.WriteLine("Tile Bag: " + game.GetTileBag().ToString());
        }
    }
}

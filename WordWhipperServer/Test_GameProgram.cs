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

            /*Console.WriteLine("\nRandom Game Board:");

            List<GameFlags> flags = new List<GameFlags>();
            flags.Add(GameFlags.RANDOM_BOARD_MULTIPLIERS);

            game = new GameInstance(flags, GameLanguages.ENGLISH);
            game.PrintBoardToConsole();

            Console.WriteLine("\nTile Bag: " + game.GetTileBag().ToString());

            Console.WriteLine("\nIs valid word test for english `dog`: " + GameUtils.IsValidWord(GameLanguages.ENGLISH, "dog"));*/

            Guid playerOneID = Guid.NewGuid();
            Guid playerTwoID = Guid.NewGuid();

            game.AddPlayer(playerOneID);
            game.AddPlayer(playerTwoID);

            Dictionary<BoardPosition, int> playedTiles = new Dictionary<BoardPosition, int>();
            playedTiles.Add(new BoardPosition(7, 7), (int) LettersForEnglish.H);
            playedTiles.Add(new BoardPosition(8, 7), (int)LettersForEnglish.I);

            try
            {
                GameLogicHandler.ExecuteMove(game, playerOneID, playedTiles, new List<BoardPosition>());
                Console.WriteLine("Executed Move!");
                game.PrintBoardToConsole();

                playedTiles.Clear();
                playedTiles.Add(new BoardPosition(7, 6), (int)LettersForEnglish.O);
                playedTiles.Add(new BoardPosition(6, 6), (int)LettersForEnglish.Y);

                GameLogicHandler.ExecuteMove(game, playerTwoID, playedTiles, new List<BoardPosition>());
                Console.WriteLine("Executed Move!");
                game.PrintBoardToConsole();
            }

            catch(Exception ex)
            {
                Console.WriteLine("Error! Reason: " + ex.Message);
            }
           
        }
    }
}

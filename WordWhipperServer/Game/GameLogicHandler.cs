using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordWhipperServer.Util;

namespace WordWhipperServer.Game
{
    /// <summary>
    /// Used to do all logic in a game
    /// </summary>
    public static class GameLogicHandler
    {
        /// <summary>
        /// Checks if a player can make a move
        /// </summary>
        /// <param name="Game">The game in which the move is happening</param>
        /// <param name="playerID">the player trying to move</param>
        /// <param name="m_pieces">the pieces they used</param>
        /// <param name="start">start pos of word</param>
        /// <param name="end">end pos of word</param>
        /// <returns>true or exception</returns>
        public static bool CanPlayMove(GameInstance game, Guid playerID, List<int> m_pieces, List<int> m_blankReplacements, BoardPosition start, BoardPosition end)
        {
            if (!game.CanPlayMoveNow(playerID))
                throw new Exception("This player can't make a move right now!");

            if (game.IsFirstTurn()
                && (!Enumerable.Range(start.GetX(), end.GetX() - start.GetX() + 1).Contains(GameBoard.BOARD_CENTER_X)
                || !Enumerable.Range(start.GetY(), end.GetY() - start.GetY() + 1).Contains(GameBoard.BOARD_CENTER_Y)))
                throw new Exception("First move must be placed in middle!");

            if(!game.GetCurrentPlayerTiles().ContainsAllItems(m_pieces))
                throw new Exception("The player tried to make a move with a piece they don't have!");

            if (start.GetX() != end.GetX() && start.GetY() != end.GetY())
                throw new Exception("Tiles weren't put in a line!");

            if (start.GetX() < 0 || start.GetY() < 0 || end.GetX() > GameBoard.BOARD_WIDTH || end.GetY() > GameBoard.BOARD_HEIGHT)
                throw new Exception("Tiles weren't on board!");

            if(start.GetX() <= end.GetX() || start.GetY() <= end.GetY())
                throw new Exception("End was equal to or before start!");



            //if (!GameUtils.IsValidWord(game.GetLanguage(), word))
                //throw new Exception("Played word isn't valid!");

            return true;
        }

    }
}

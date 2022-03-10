using System;
using System.Collections.Generic;
using System.Text;

namespace WordWhipperServer
{
    /// <summary>
    /// A board associated with a game
    /// </summary>
    class GameBoard
    {
        private static int BOARD_WIDTH = 15;
        private static int BOARD_HEIGHT = 15;

        private BoardSpace[,] m_spaces = new BoardSpace[BOARD_WIDTH, BOARD_HEIGHT];

        void CreateBoard()
        {
            //m_spaces.ForEach(x => x.);
        }
    }
}

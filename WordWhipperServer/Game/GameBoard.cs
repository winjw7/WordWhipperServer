namespace WordWhipperServer.Game
{
    /// <summary>
    /// A board associated with a game
    /// </summary>
    class GameBoard
    {
        private static int BOARD_WIDTH = 15;
        private static int BOARD_HEIGHT = 15;

        private BoardSpace[,] m_spaces = new BoardSpace[BOARD_WIDTH, BOARD_HEIGHT];

        /// <summary>
        /// Constructor for a game board
        /// </summary>
        public GameBoard()
        {
            CreateBoard();
        }

        /// <summary>
        /// Fills all spots on the board with empty spaces
        /// </summary>
        private void CreateBoard()
        {
            for(int x = 0; x < BOARD_WIDTH; x++)
            {
                for(int y = 0; y < BOARD_HEIGHT; y++)
                {
                    m_spaces[x,y] = new BoardSpace();
                }
            }
        }
    }
}

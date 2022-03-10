using System;

namespace WordWhipperServer.Game
{
    /// <summary>
    /// A board associated with a game
    /// </summary>
    public class GameBoard
    {
        private static int BOARD_WIDTH = 15;
        private static int BOARD_HEIGHT = 15;

        private BoardSpace[,] m_spaces = new BoardSpace[BOARD_WIDTH, BOARD_HEIGHT];

        /// <summary>
        /// Constructor for a default game board
        /// </summary>
        public GameBoard()
        {
            CreateBoard();
        }

        /// <summary>
        /// Constructor for a random game board
        /// </summary>
        /// <param name="id">game id used to generate randomness</param>
        public GameBoard(Guid id)
        {
            CreateBoard(true);
            FillWithRandomMultipliers(id);
        }

        /// <summary>
        /// Fills all spots on the board with empty spaces
        /// </summary>
        /// <param name="random">multipliers are random?</param>
        private void CreateBoard(bool random = false)
        {
            for(int x = 0; x < BOARD_WIDTH; x++)
            {
                for(int y = 0; y < BOARD_HEIGHT; y++)
                {
                    BoardSpaceMultipliers mult = random ? BoardSpaceMultipliers.NONE : GameBoardUtils.GetDefaultMultiplierAt(x, y);
                    m_spaces[x,y] = new BoardSpace(mult);
                }
            }
        }

        /// <summary>
        /// Gets a space on a board
        /// </summary>
        /// <param name="x">x coord</param>
        /// <param name="y">y coord</param>
        /// <returns></returns>
        private BoardSpace GetBoardSpace(int x, int y)
        {
            return m_spaces[x, y];
        }

        /// <summary>
        /// Fills a board with multipliers at random locations
        /// </summary>
        /// <param name="id">the game id</param>
        private void FillWithRandomMultipliers(Guid id) {
            
            Random r = new Random(id.ToString().GetHashCode());

            FillMultiplier(r, GameBoardUtils.GetDoubleLetterSpotAmounts(), BoardSpaceMultipliers.DOUBLE_LETTER);
            FillMultiplier(r, GameBoardUtils.GetDoubleWordSpotAmounts(), BoardSpaceMultipliers.DOUBLE_WORD);
            FillMultiplier(r, GameBoardUtils.GetTripleLetterSpotAmounts(), BoardSpaceMultipliers.TRIPLE_LETTER);
            FillMultiplier(r, GameBoardUtils.GetTripleLetterSpotAmounts(), BoardSpaceMultipliers.TRIPLE_WORD);
        }

        /// <summary>
        /// Helper method to fill the board with a specific multiplier
        /// </summary>
        /// <param name="r">The random to use to make same based on game id</param>
        /// <param name="amount">amount of this type to fill</param>
        /// <param name="type">type to fill</param>
        private void FillMultiplier(Random r, int amount, BoardSpaceMultipliers type)
        {
            if (type == BoardSpaceMultipliers.NONE)
                throw new Exception("None is not a multiplier!");

            for (int i = 0; i < amount; i++)
            {
                while (true)
                {
                    int x = r.Next(0, BOARD_WIDTH);
                    int y = r.Next(0, BOARD_HEIGHT);

                    BoardSpace space = GetBoardSpace(x, y);

                    if (space.GetMultiplier() == BoardSpaceMultipliers.NONE)
                    {
                        space.UpdateMultiplier(type);
                        break;
                    }
                }
            }
        }
    }
}

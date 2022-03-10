using System;
using WordWhipperServer.Util;

namespace WordWhipperServer.Game
{
    /// <summary>
    /// A board associated with a game
    /// </summary>
    public class GameBoard
    {
        public const int BOARD_WIDTH = 15;
        public const int BOARD_HEIGHT = 15;

        public const int BOARD_CENTER_X = 7;
        public const int BOARD_CENTER_Y = 7;

        private BoardSpace[,] m_spaces = new BoardSpace[BOARD_WIDTH, BOARD_HEIGHT];

        private bool m_centerDoubled;

        /// <summary>
        /// Constructor for a default game board
        /// </summary>
        public GameBoard(bool centerDouble)
        {
            CreateBoard(centerDouble);
        }

        /// <summary>
        /// Constructor for a random game board
        /// </summary>
        /// <param name="id">game id used to generate randomness</param>
        public GameBoard(Guid id, bool centerDouble)
        {
            CreateBoard(centerDouble, true);
            FillWithRandomMultipliers(id);
        }

        /// <summary>
        /// Creates a deep copy of the board
        /// </summary>
        /// <param name="id">id of the game</param>
        /// <returns>deep copy</returns>
        public GameBoard DeepCopy(Guid id)
        {
            GameBoard board = new GameBoard(id, m_centerDoubled);

            BoardSpace[,] toCopy = GetBoardSpaces();

            for (int x = 0; x < toCopy.GetLength(0); x++)
            {
                for (int y = 0; y < toCopy.GetLength(0); y++)
                {
                    BoardSpace space = toCopy[x, y];

                    board.GetBoardSpace(x, y).SetLetter(space.GetLetter());
                    space.GetZingers().ForEach(z => board.GetBoardSpace(x, y).AddZinger(z));
                }
            }

            return board;
        }

        /// <summary>
        /// Gets the board spaces on the board
        /// </summary>
        /// <returns>board spaces</returns>
        public BoardSpace[,] GetBoardSpaces()
        {
            return m_spaces;
        }

        /// <summary>
        /// Fills all spots on the board with empty spaces
        /// </summary>
        /// <param name="random">multipliers are random?</param>
        private void CreateBoard(bool centerDouble, bool random = false)
        {
            for(int x = 0; x < BOARD_WIDTH; x++)
            {
                for(int y = 0; y < BOARD_HEIGHT; y++)
                {
                    BoardSpaceMultipliers mult = random ? BoardSpaceMultipliers.NONE : GameBoardUtils.GetDefaultMultiplierAt(x, y);
                    m_spaces[x,y] = new BoardSpace(mult);
                }
            }

            if (!centerDouble && !random)
                m_spaces[BOARD_CENTER_X, BOARD_CENTER_Y].UpdateMultiplier(BoardSpaceMultipliers.NONE);
        }

        /// <summary>
        /// Gets a space on a board
        /// </summary>
        /// <param name="x">x coord</param>
        /// <param name="y">y coord</param>
        /// <returns></returns>
        public BoardSpace GetBoardSpace(int x, int y)
        {
            if (x < 0 || x >= BOARD_WIDTH || y < 0 || y >= BOARD_HEIGHT)
                throw new Exception("This position is out of bounds!");

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

        /// <summary>
        /// Prints the board to console
        /// </summary>
        public void PrintToConsole(GameLanguages lang)
        {
            for (int x = 0; x < BOARD_WIDTH; x++)
            {
                for (int y = 0; y < BOARD_HEIGHT; y++)
                {
                    BoardSpace space = GetBoardSpace(x, y);

                    switch (space.GetMultiplier()) {
                        case BoardSpaceMultipliers.NONE:
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;
                        case BoardSpaceMultipliers.DOUBLE_LETTER:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            break;
                        case BoardSpaceMultipliers.DOUBLE_WORD:
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        case BoardSpaceMultipliers.TRIPLE_LETTER:
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            break;
                        case BoardSpaceMultipliers.TRIPLE_WORD:
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            break;
                    }


                    string letter = !space.HasLetter() ? "." : Enum.GetName(lang.GetAttribute<LanguageAttribute>().GetLangEnum().GetType(), space.GetLetter());
                    Console.Write("[" + letter + "] ");
                }

                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}

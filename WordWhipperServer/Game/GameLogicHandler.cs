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
        public static int BLANK_TILE_NUMBER = 1;

        /// <summary>
        /// Checks if a player can make a move
        /// </summary>
        /// <param name="Game">The game in which the move is happening</param>
        /// <param name="playerID">the player trying to move</param>
        /// <param name="m_pieces">the pieces they used</param>
        /// <param name="start">start pos of word</param>
        /// <param name="end">end pos of word</param>
        /// <returns>true or exception</returns>
        private static bool CanPlayMove(GameInstance game, Guid playerID, Dictionary<BoardPosition, int> m_pieces, List<BoardPosition> m_blankSpots)
        {
            if (!game.CanPlayMoveNow(playerID))
                throw new Exception("This player can't make a move right now!");

            if (game.IsFirstTurn() && m_pieces.Keys.Where(x => x.GetX() == GameBoard.BOARD_CENTER_X && x.GetY() == GameBoard.BOARD_CENTER_Y).Count() == 0)
                throw new Exception("First move must be placed in middle!");

            //if(!game.GetCurrentPlayerTiles().ContainsAllItems(m_pieces.Values.ToList()))
                //throw new Exception("The player tried to make a move with a piece they don't have!");

            int matchX = m_pieces.Keys.First().GetX();
            int matchY = m_pieces.Keys.First().GetY();

            bool horizontalWord = m_pieces.Keys.Where(x => x.GetX() == matchX).Count() == m_pieces.Keys.Count();
            bool verticalWord = m_pieces.Keys.Where(x => x.GetY() == matchY).Count() == m_pieces.Keys.Count();

            if (!horizontalWord && !verticalWord)
                throw new Exception("Tiles weren't put in a line!");

            int horzLength = m_pieces.Keys.Max(x => x.GetY()) - m_pieces.Keys.Min(x => x.GetY()) + 1;

            if (horizontalWord && (horzLength != m_pieces.Count()))
                throw new Exception("There is a space in the line!");

            int vertWidth = m_pieces.Keys.Max(x => x.GetX()) - m_pieces.Keys.Min(x => x.GetX()) + 1;

            if (verticalWord && (vertWidth != m_pieces.Count()))
                throw new Exception("There is a space in the line!");

            GameBoard clonedBoard = game.GetBoard().DeepCopy(game.GetID());

            foreach(BoardPosition pos in m_pieces.Keys)
            {
                clonedBoard.GetBoardSpace(pos.GetX(), pos.GetY()).SetLetter(m_pieces[pos], m_blankSpots.Contains(pos));
            }

            List<PlayedWord> playedWords = GetPlayedWords(clonedBoard, m_pieces, game.GetLanguage());

            if (playedWords.Count == 0)
                throw new Exception("An invalid word or words were played!");

            return true;
        }

        /// <summary>
        /// Executes a move
        /// </summary>
        /// <param name="Game">The game in which the move is happening</param>
        /// <param name="playerID">the player trying to move</param>
        /// <param name="m_pieces">the pieces they used</param>
        /// <param name="start">start pos of word</param>
        /// <param name="end">end pos of word</param>
        /// <returns>true or exception</returns>
        public static void ExecuteMove(GameInstance game, Guid playerID, Dictionary<BoardPosition, int> m_pieces, List<BoardPosition> m_blankSpots)
        {
            if (!CanPlayMove(game, playerID, m_pieces, m_blankSpots)) //will throw exception
                return;

            foreach (BoardPosition pos in m_pieces.Keys)
            {
                game.GetBoard().GetBoardSpace(pos.GetX(), pos.GetY()).SetLetter(m_pieces[pos], m_blankSpots.Contains(pos));
            }

            List<PlayedWord> playedWords = GetPlayedWords(game.GetBoard(), m_pieces, game.GetLanguage());

            int scoreToAdd = 0;
            playedWords.ForEach(x => scoreToAdd += x.GetMultiplierScore());

            foreach (BoardPosition pos in m_pieces.Keys)
            {
                game.GetBoard().GetBoardSpace(pos.GetX(), pos.GetY()).SetLocked(true);
            }

            game.PlayerDidTurn(m_pieces.Values.ToList(), scoreToAdd);
        }

        /// <summary>
        /// Skips a turn
        /// </summary>
        /// <param name="game">game</param>
        /// <param name="playerID">player id</param>
        public static void SkipTurn(GameInstance game, Guid playerID)
        {
            if (!game.CanPlayMoveNow(playerID))
                throw new Exception("This player can't make a move right now!");

            game.PlayerPassTurn();
        }

        /// <summary>
        /// Trades in tiles for a player
        /// </summary>
        /// <param name="game">game</param>
        /// <param name="playerID">player id</param>
        /// <param name="tiles">tiles to trade</param>
        public static void TradeTiles(GameInstance game, Guid playerID, List<int> tiles)
        {
            if (!game.CanPlayMoveNow(playerID))
                throw new Exception("This player can't make a move right now!");

            //May throw exception if tried to trade in tiles they don't have
            game.PlayerTradeTiles(playerID, tiles);
        }

        /// <summary>
        /// Makes a player pass their turn
        /// </summary>
        /// <param name="game">game</param>
        /// <param name="playerID">player id</param>
        public static void PassTurn(GameInstance game, Guid playerID)
        {
            if (!game.CanPlayMoveNow(playerID))
                throw new Exception("This player can't make a move right now!");

            game.PlayerPassTurn();
        }

        /// <summary>
        /// Gets played words from a dictionary of locations and pieces placed
        /// </summary>
        /// <param name="board"></param>
        /// <param name="m_pieces"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        private static List<PlayedWord> GetPlayedWords(GameBoard board, Dictionary<BoardPosition, int> m_pieces, GameLanguages lang)
        {
            Enum langEnum = lang.GetAttribute<LanguageAttribute>().GetLangEnum();

            List<PlayedWord> m_playedWords = new List<PlayedWord>();
            
            foreach(BoardPosition playedPos in m_pieces.Keys)
            {
                List<List<BoardPosition>> looped = loopForWords(board, playedPos);
                
                //words found from this single tile
                foreach(List<BoardPosition> words in looped)
                {
                    int rawScore = 0;
                    int multiplierScore = 0;

                    List<int> wordMultipliers = new List<int>();
                    List<Zinger> zingers = new List<Zinger>();

                    List<int> rawLetters = new List<int>();

                    //spots that make up word
                    foreach(BoardPosition pos in words)
                    {
                        BoardSpace space = board.GetBoardSpace(pos.GetX(), pos.GetY());

                        int letterNum = space.GetLetter();

                        rawLetters.Add(letterNum);

                        Enum letterEnum = (Enum) Enum.ToObject(langEnum.GetType(), letterNum);
                        LetterDataAttribute data = letterEnum.GetAttribute<LetterDataAttribute>();

                        int value = space.IsBlankTile() ? 0 : data.Value;

                        rawScore += value;

                        switch (space.GetMultiplier()) {
                            case BoardSpaceMultipliers.NONE:
                                multiplierScore += value;
                                break;
                            case BoardSpaceMultipliers.DOUBLE_LETTER:
                                multiplierScore += (value * 2);
                                break;
                            case BoardSpaceMultipliers.TRIPLE_LETTER:
                                multiplierScore += (value * 3);
                                break;
                            case BoardSpaceMultipliers.DOUBLE_WORD:
                                multiplierScore += value;
                                wordMultipliers.Add(2);
                                break;
                            case BoardSpaceMultipliers.TRIPLE_WORD:
                                multiplierScore += value;
                                wordMultipliers.Add(3);
                                break;
                        }

                        zingers.AddRange(space.GetZingers());
                    }

                    foreach(int i in wordMultipliers)
                    {
                        multiplierScore *= i;
                    }

                    PlayedWord word = new PlayedWord(words, rawScore, multiplierScore, zingers);
                    String wordString = GameUtils.StringFromIntList(lang, rawLetters);

                    bool validWord = GameUtils.IsValidWord(lang, wordString);

                    if (validWord && !m_playedWords.Contains(word))
                        m_playedWords.Add(word);
                }
            }

            return m_playedWords;
        }

        /// <summary>
        /// loops for words
        /// </summary>
        /// <param name="board">the board to check</param>
        /// <param name="playedPos">played pos of letter</param>
        /// <returns>words from played letter</returns>
        private static List<List<BoardPosition>> loopForWords(GameBoard board, BoardPosition playedPos)
        {
            List<List<BoardPosition>> words = new List<List<BoardPosition>>();

            for(int x = 0; x < 2; x++)
            {
                List<BoardPosition> word = new List<BoardPosition>();

                for (int i = 0; i < 2; i++)
                {
                    
                    BoardPosition current = playedPos;
                    int add = 0;

                    while (true)
                    {
                        try
                        {
                            if (x == 0)
                                current = new BoardPosition(playedPos.GetX() + add, playedPos.GetY());
                            else
                                current = new BoardPosition(playedPos.GetX(), playedPos.GetY() + add);

                            if (!board.GetBoardSpace(current.GetX(), current.GetY()).HasLetter())
                                break;


                            if (i == 0 && !word.Contains(current))
                                word.Add(current);
                            else if (!word.Contains(current))
                                word.Insert(0, current);
                        }

                        //space probably doesnt exist
                        catch (Exception)
                        {
                            break;
                        }

                        add += (i == 0 ? 1 : -1);
                    }

                    if (word.Count > 1 && !words.Contains(word))
                        words.Add(word);
                }
            }

  
            return words;
        }
    }
}

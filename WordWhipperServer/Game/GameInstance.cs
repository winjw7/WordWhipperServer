using System;
using System.Collections.Generic;
using System.Linq;
using WordWhipperServer.Util;

namespace WordWhipperServer.Game
{
    /// <summary>
    /// An instance of a game
    /// </summary>
    public class GameInstance
    {
        private Guid m_id;

        private const int MIN_PLAYERS = 2;
        private const int MAX_PLAYERS = 4;

        private GameBoard m_board;
        private List<GamePlayer> m_players;
        private int m_maxPlayers;

        private GameLanguages m_language;

        private List<GameFlags> m_gameFlags;
        private TileBag m_tileBag;

        private GameStatus m_status;

        private List<ChatMessage> m_messages;

        private int m_whichPlayerTurn;
        private int m_turnNumber;

        private int m_turnsPassed;

        private List<Guid> m_winners;

        /// <summary>
        /// Initializes all of the lists and variables
        /// </summary>
        private void InitialSetup()
        {
            m_maxPlayers = MIN_PLAYERS;
            m_players = new List<GamePlayer>();
            m_gameFlags = new List<GameFlags>();
            m_players = new List<GamePlayer>();
            m_id = Guid.NewGuid();
            m_language = GameLanguages.ENGLISH;
            m_status = GameStatus.WAITING_FOR_PLAYERS_TO_ACCEPT;
            m_messages = new List<ChatMessage>();
            m_whichPlayerTurn = 0;
            m_turnNumber = 0;
            m_turnsPassed = 0;
        }

        /// <summary>
        /// Creates a game with default settings
        /// </summary>
        public GameInstance()
        {
            InitialSetup();
            m_board = new GameBoard(true);
            m_tileBag = new TileBag(GameLanguages.ENGLISH);
        }

        /// <summary>
        /// Creates a new game with settings
        /// </summary>
        /// <param name="playerCount">the amount of players</param>
        public GameInstance(List<GameFlags> flags, GameLanguages lang, int playerCount = 2)
        {
            if (playerCount < MIN_PLAYERS)
                throw new Exception($"There must be at least {MIN_PLAYERS} players in a game!");

            if(playerCount > MAX_PLAYERS)
                throw new Exception($"There must be at max {MAX_PLAYERS} players in a game!");

            InitialSetup();

            m_maxPlayers = playerCount;
            m_gameFlags = flags;
            m_language = lang;

            bool doubleCenter = flags.Contains(GameFlags.NO_DOUBLE_FIRST_WORD);

            if (flags.Contains(GameFlags.RANDOM_BOARD_MULTIPLIERS))
                m_board = new GameBoard(GetID(), doubleCenter);
            else
                m_board = new GameBoard(doubleCenter);

            if (flags.Contains(GameFlags.RANDOM_START))
                m_whichPlayerTurn = new Random(m_id.ToString().GetHashCode()).Next(0, MAX_PLAYERS);


            m_tileBag = new TileBag(lang);
        }

        /// <summary>
        /// Prints the board to console
        /// </summary>
        public void PrintBoardToConsole()
        {
            m_board.PrintToConsole(m_language);
        }

        /// <summary>
        /// Adds a player to a game
        /// </summary>
        /// <param name="id"></param>
        public void AddPlayer(Guid id)
        {
            if (!IsJoinable())
                throw new Exception("This game is already full!");

            m_players.Add(new GamePlayer(id));
            DrawTilesForPlayer(id, GamePlayer.MAX_TILES);

            if (IsFull())
                m_status = GameStatus.PENDING_PLAYER_MOVE;
        }

        /// <summary>
        /// Gets the max players allowed in this game
        /// </summary>
        /// <returns>max players</returns>
        public int GetMaxPlayers()
        {
            return m_maxPlayers;
        }

        /// <summary>
        /// Checks whether or not a game is full
        /// </summary>
        /// <returns>players == max players</returns>
        public bool IsFull()
        {
            return GetPlayerCount() == GetMaxPlayers();
        }

        /// <summary>
        /// Returns whether this game is joinable or not
        /// </summary>
        /// <returns>joinable</returns>
        public bool IsJoinable()
        {
            return !IsFull() && m_status == GameStatus.WAITING_FOR_PLAYERS_TO_ACCEPT;
        }

        /// <summary>
        /// Gets the id of the game
        /// </summary>
        /// <returns>id</returns>
        public Guid GetID()
        {
            return m_id;
        }

        /// <summary>
        /// Gets how many players are in this game
        /// </summary>
        /// <returns>player count</returns>
        public int GetPlayerCount()
        {
            return m_players.Count;
        }

        /// <summary>
        /// Gets all messages sent during a game
        /// </summary>
        /// <returns>message list</returns>
        public List<ChatMessage> GetChatMessages()
        {
            return m_messages;
        }

        /// <summary>
        /// Checks whether or not this game has custom rules
        /// </summary>
        /// <returns>has custom rules</returns>
        public bool HasCustomRules()
        {
            return m_gameFlags.Count != 0;
        }

        /// <summary>
        /// Checks whether a game has a custom flag
        /// </summary>
        /// <param name="flag">the flag to check</param>
        /// <returns>true or false</returns>
        public bool ContainsGameRule(GameFlags flag)
        {
            if (HasCustomRules())
                return false;

            return m_gameFlags.Contains(flag);
        }

        /// <summary>
        /// Gets which language is being used for this game
        /// </summary>
        /// <returns>language</returns>
        public GameLanguages GetLanguage()
        {
            return m_language;
        }

        /// <summary>
        /// Gets the tile bag for this game
        /// </summary>
        /// <returns>tile bag</returns>
        public TileBag GetTileBag()
        {
            return m_tileBag;
        }

        /// <summary>
        /// Gets the status of the game
        /// </summary>
        /// <returns>status</returns>
        public GameStatus GetStatus()
        {
            return m_status;
        }

        /// <summary>
        /// Gets the id of the player who is up
        /// </summary>
        /// <returns></returns>
        private Guid GetPlayersTurnID()
        {
            if (m_players.Count <= m_whichPlayerTurn - 1)
                throw new Exception("The player who is starting isn't in the game yet!");

            return m_players[m_whichPlayerTurn].GetID();
        }

        /// <summary>
        /// Gets if this is the first turn
        /// </summary>
        /// <returns>first turn</returns>
        public bool IsFirstTurn()
        {
            return m_turnNumber == 0;
        }

        /// <summary>
        /// Checks if the game contains a player
        /// </summary>
        /// <param name="id">the id of the player</param>
        /// <returns>contains player</returns>
        public bool ContainsPlayer(Guid id)
        {
            return m_players.Where(x => x.GetID() == id).Count() == 1;
        }

        /// <summary>
        /// Gets the tiles of whos turn it is
        /// </summary>
        /// <returns>tile list</returns>
        public List<int> GetPlayerTiles(Guid id) {
            return GetPlayer(id).GetLetters();
        }

        /// <summary>
        /// Gets a player from the game
        /// </summary>
        /// <param name="id">id of the player</param>
        /// <returns>the player</returns>
        public GamePlayer GetPlayer(Guid id)
        {
            if (!ContainsPlayer(id))
                throw new Exception("This player isn't in this game!");

            return m_players.Where(x => x.GetID() == id).First();
        }

        /// <summary>
        /// Checks if a player can play a move at this time
        /// </summary>
        /// <param name="id">id of the player</param>
        /// <returns>can play move</returns>
        public bool CanPlayMoveNow(Guid id)
        {
            return GetStatus() == GameStatus.PENDING_PLAYER_MOVE && GetPlayersTurnID() == id;
        }

        /// <summary>
        /// Gets the game board
        /// </summary>
        /// <returns>game board</returns>
        public GameBoard GetBoard()
        {
            return m_board;
        }

        /// <summary>
        /// Sets the turn to the next player
        /// </summary>
        private void NextPlayersTurn()
        {
            m_whichPlayerTurn = ((m_whichPlayerTurn + 1) % GetMaxPlayers());
            m_turnNumber++;

            CheckEndGame();
        }

        /// <summary>
        /// Trades tiles in for a player
        /// </summary>
        /// <param name="tiles">tiles to trade in</param>
        public void PlayerTradeTiles(Guid player, List<int> tiles)
        {
            GamePlayer p = m_players[m_whichPlayerTurn];

            if (!p.GetLetters().ContainsAllItems(tiles))
                throw new Exception("This player doesn't have all the tiles they want to trade in!");

            tiles.ForEach(x => p.RemoveLetter(x));
            m_tileBag.AddTiles(tiles);
            DrawTilesForPlayer(player, tiles.Count);
            NextPlayersTurn();
        }

        /// <summary>
        /// Draws tiles for a player from the bag
        /// </summary>
        /// <param name="count">amount of tiles to draw</param>
        public void DrawTilesForPlayer(Guid id, int count)
        {
            if (!ContainsPlayer(id))
                throw new Exception("This player isn't in this game!");

            GamePlayer p = m_players.Where(x => x.GetID() == id).First();

            for (int i = 0; i < count; i++)
            {
                if (GetTileBag().IsEmpty())
                    break;

                p.AddLetter(GetTileBag().DrawLetter());
            }
        }

        /// <summary>
        /// A player passes their turn
        /// </summary>
        public void PlayerPassTurn()
        {
            m_turnsPassed++;

            NextPlayersTurn();
        }

        /// <summary>
        /// A player did a turn
        /// </summary>
        public void PlayerDidTurn(List<int> tiles, int scoreToAdd)
        {
            GamePlayer p = m_players[m_whichPlayerTurn];
            tiles.ForEach(x => p.RemoveLetter(x));

            DrawTilesForPlayer(p.GetID(), tiles.Count);

            p.AddScore(scoreToAdd);
            m_turnsPassed = 0;

            NextPlayersTurn();
        }

        /// <summary>
        /// Checks to see if the game is over because can make no moves / both players pass
        /// </summary>
        private void CheckEndGame()
        {
            //every player has passed, end
            if(m_turnsPassed == GetMaxPlayers())
            {
                EndGame();
            }
        }

        /// <summary>
        /// Ends a game and returns winners
        /// </summary>
        /// <returns>winners</returns>
        public void EndGame()
        {
            if (m_status == GameStatus.COMPLETED)
                throw new Exception("The game is already over!");

            m_status = GameStatus.COMPLETED;

            m_winners = new List<Guid>();
            int winningScore = -1;

            foreach(GamePlayer player in m_players)
            {
                int playerScore = player.GetScore();

                if (playerScore > winningScore)
                {
                    m_winners.Clear();
                    m_winners.Add(player.GetID());
                    winningScore = player.GetScore();
                }

                else if (playerScore == winningScore)
                    m_winners.Add(player.GetID());
            }
        }

        public int GetWinningScore()
        {
            if (m_status != GameStatus.COMPLETED)
                throw new Exception("The game is not over!");

            return GetPlayer(m_winners.First()).GetScore();
        }

        public List<Guid> GetWinners()
        {
            if (m_status != GameStatus.COMPLETED)
                throw new Exception("The game is not over!");

            return m_winners;
        }
    }
}

using System;
using System.Collections.Generic;

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
            if (IsFull())
                throw new Exception("This game is already full!");

            m_players.Add(new GamePlayer(id));
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
        /// Gets the tiles of whos turn it is
        /// </summary>
        /// <returns>tile list</returns>
        public List<int> GetCurrentPlayerTiles() {
            return m_players[m_whichPlayerTurn].GetLetters();
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
    }
}

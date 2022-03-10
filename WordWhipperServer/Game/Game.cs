using System;
using System.Collections.Generic;

namespace WordWhipperServer.Game
{
    /// <summary>
    /// An instance of a game
    /// </summary>
    class Game
    {
        private Guid m_id;

        private const byte MIN_PLAYERS = 2;
        private const byte MAX_PLAYERS = 4;

        private GameBoard m_board;
        private List<GamePlayer> m_players;
        private int m_maxPlayers;

        private GameLanguages m_language;

        private List<GameFlags> m_gameFlags;
        private List<int> m_tileBag;

        /// <summary>
        /// Initializes all of the lists and variables
        /// </summary>
        private void InitialSetup()
        {
            m_maxPlayers = MIN_PLAYERS;
            m_board = new GameBoard();
            m_players = new List<GamePlayer>();
            m_gameFlags = new List<GameFlags>();
            m_players = new List<GamePlayer>();
            m_tileBag = new List<int>();
            m_id = Guid.NewGuid();
            m_language = GameLanguages.ENGLISH;
        }

        /// <summary>
        /// Creates a game with default settings
        /// </summary>
        public Game()
        {
            InitialSetup();
        }

        /// <summary>
        /// Creates a new game with settings
        /// </summary>
        /// <param name="playerCount">the amount of players</param>
        public Game(List<GameFlags> flags, GameLanguages lang, int playerCount = 2)
        {
            if (playerCount < MIN_PLAYERS)
                throw new Exception($"There must be at least {MIN_PLAYERS} players in a game!");

            if(playerCount > MAX_PLAYERS)
                throw new Exception($"There must be at max {MAX_PLAYERS} players in a game!");

            InitialSetup();

            m_maxPlayers = playerCount;
            m_gameFlags = flags;
            m_language = lang;
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
    }
}

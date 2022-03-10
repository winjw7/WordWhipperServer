using System;
using System.Collections.Generic;
using System.Text;

namespace WordWhipperServer.Game
{
    /// <summary>
    /// An instance of a game
    /// </summary>
    class Game
    {
        private Guid m_id;

        private const int MIN_PLAYERS = 2;
        private const int MAX_PLAYERS = 4;

        private GameBoard m_board;
        private List<GamePlayer> m_players = new List<GamePlayer>();
        private int m_maxPlayers;

        /// <summary>
        /// Creates a new game with a variable player amount
        /// </summary>
        /// <param name="playerCount">the amount of players</param>
        public Game(int playerCount = 2)
        {
            if (playerCount < MIN_PLAYERS)
                throw new Exception($"There must be at least {MIN_PLAYERS} players in a game!");

            if(playerCount > MAX_PLAYERS)
                throw new Exception($"There must be at max {MAX_PLAYERS} players in a game!");

            m_maxPlayers = playerCount;

            m_board = new GameBoard();
            m_players = new List<GamePlayer>();

            m_id = Guid.NewGuid();
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
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace WordWhipperServer.Game
{
    /// <summary>
    /// The player of a game
    /// </summary>
    class GamePlayer
    {
        private Guid m_id;
        private List<char> m_letters;
        private int m_score;

        /// <summary>
        /// Creates a game player
        /// </summary>
        /// <param name="id">the id of the player</param>
        public GamePlayer(Guid id)
        {
            m_id = id;
            m_letters = new List<char>();
            m_score = 0;
        }

        /// <summary>
        /// Gets the score of the player
        /// </summary>
        /// <returns>score</returns>
        public int GetScore()
        {
            return m_score;
        }

        /// <summary>
        /// Gets the id of the player
        /// </summary>
        /// <returns>id</returns>
        public Guid GetID()
        {
            return m_id;
        }

        /// <summary>
        /// Gets how many letters the player has
        /// </summary>
        /// <returns>letter count</returns>
        public int GetLetterCount()
        {
            return m_letters.Count;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordWhipperServer.Util;

namespace WordWhipperServer.Game
{
    /// <summary>
    /// The player of a game
    /// </summary>
    public class GamePlayer
    {
        public const int MAX_TILES = 7;

        private Guid m_id;
        private List<int> m_letters;
        private List<ZingerTypes> m_zingers;
        private int m_score;

        /// <summary>
        /// Creates a game player
        /// </summary>
        /// <param name="id">the id of the player</param>
        public GamePlayer(Guid id)
        {
            m_id = id;
            m_letters = new List<int>();
            m_zingers = new List<ZingerTypes>();
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
        /// Adds a letter to a player
        /// </summary>
        /// <param name="letter"></param>
        public void AddLetter(int letter)
        {
            if (GetLetterCount() == MAX_TILES)
                throw new Exception("This player can't have any more tiles!");

            m_letters.Add(letter);
        }

        /// <summary>
        /// Adds a zinger to this player
        /// </summary>
        /// <param name="type">type to add</param>
        public void AddZinger(ZingerTypes type)
        {
            ZingerDataAttribute data = type.GetAttribute<ZingerDataAttribute>();

            if (GetZingerCountByType(type) == data.MaxAllowed)
                throw new Exception("This player can't have any more of this zinger type!");

            m_zingers.Add(type);
        }

        /// <summary>
        /// Gets which zingers the player can use
        /// </summary>
        /// <returns>list of zinger types</returns>
        public List<ZingerTypes> GetAvaliableZingers()
        {
            return m_zingers;
        }

        /// <summary>
        /// Gets how many zingers they have of a specific type
        /// </summary>
        /// <param name="type">the type of zinger</param>
        /// <returns>count</returns>
        private int GetZingerCountByType(ZingerTypes type)
        {
            if (m_zingers.Count == 0)
                return 0;

            return m_zingers.Where(x => x == type).Count();
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

        /// <summary>
        /// Gets all letters a player has
        /// </summary>
        /// <returns>letter list</returns>
        public List<int> GetLetters()
        {
            return m_letters;
        }
    }
}

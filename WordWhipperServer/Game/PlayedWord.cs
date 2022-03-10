using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordWhipperServer.Game
{
    /// <summary>
    /// A word that was played
    /// </summary>
    class PlayedWord
    {
        private List<BoardPosition> m_tiles;
        private int m_rawScore;
        private int m_multiplierScore;
        private List<Zinger> m_activatedZingers;

        /// <summary>
        /// Constructor for a played word
        /// </summary>
        /// <param name="raw">the raw score of the word</param>
        /// <param name="multiScore">the score with board space multipliers</param>
        /// <param name="zings">the zingers activated</param>
        public PlayedWord(List<BoardPosition> dict, int raw, int multiScore, List<Zinger> zings)
        {
            m_tiles = dict;
            m_rawScore = raw;
            m_multiplierScore = multiScore;
            m_activatedZingers = zings;
        }

        /// <summary>
        /// Gets the tiles in the word
        /// </summary>
        /// <returns>tile pos</returns>
        public List<BoardPosition> GetTiles()
        {
            return m_tiles;
        }

        /// <summary>
        /// The raw score of the word
        /// </summary>
        /// <returns>raw score</returns>
        public int GetRawScore()
        {
            return m_rawScore;
        }

        /// <summary>
        /// The multiplier score
        /// </summary>
        /// <returns>multi score</returns>
        public int GetMultiplierScore()
        {
            return m_multiplierScore;
        }

        /// <summary>
        /// Gets the activated zingers
        /// </summary>
        /// <returns>zingers</returns>
        public List<Zinger> GetZingers()
        {
            return m_activatedZingers;
        }

        public override int GetHashCode()
        {
            return (m_tiles.First().ToString() + "to" + m_tiles.Last().ToString()).GetHashCode();
        }

        public override bool Equals(object other)
        {
            if (other.GetType() != typeof(PlayedWord))
                return false;

            PlayedWord played = (PlayedWord)other;

            return played.GetHashCode().Equals(this.GetHashCode());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordWhipperServer.Game
{
    /// <summary>
    /// A spot on a game board
    /// </summary>
    class BoardSpace
    {
        private List<Zinger> m_zingers;
        private char m_letter;
        private bool m_isLocked;

        /// <summary>
        /// Constructor for a board space
        /// </summary>
        public BoardSpace()
        {
            m_zingers = new List<Zinger>();
            m_letter = '\0';
            m_isLocked = false;
        }

        /// <summary>
        /// Locks the board space for letters
        /// </summary>
        public void LockSpace()
        {
            m_isLocked = true;
        }

        /// <summary>
        /// Adds a zinger to the board space
        /// </summary>
        /// <param name="z">zinger</param>
        public void AddZinger(Zinger z)
        {
            //Can only have 1 zinger on a space at a time
            if (ContainsZingerFromPlayer(z.GetOwner()))
                throw new Exception("This player already has a zinger on this space!");

            m_zingers.Add(z);
        }

        public bool ContainsZingerFromPlayer(Guid id)
        {
            if (!ContainsZingers())
                return false;

            return m_zingers.Where(x => x.GetOwner() == id).Count() >= 1;
        }

        /// <summary>
        /// Checks if this spot has any zingers at all
        /// </summary>
        /// <returns>has zinger</returns>
        public bool ContainsZingers()
        {
            return m_zingers.Count >= 1;
        }

        /// <summary>
        /// Checks whether the board space is locked or not
        /// </summary>
        /// <returns>is locked</returns>
        public bool IsLocked()
        {
            return m_isLocked;
        }

        /// <summary>
        /// Sets the letter in the space
        /// </summary>
        /// <param name="c">letter to set to</param>
        public void SetLetter(char c)
        {
            if (IsLocked())
                throw new Exception("This tile is locked!");

            m_letter = c;
        }

        /// <summary>
        /// Gets the letter in the board space
        /// </summary>
        /// <returns>letter</returns>
        public char GetLetter()
        {
            return m_letter;
        }

        /// <summary>
        /// Checks if this space has a letter
        /// </summary>
        /// <returns>letter</returns>
        public bool HasLetter()
        {
            return m_letter != '\0';
        }
    }
}

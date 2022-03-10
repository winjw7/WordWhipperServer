using System;
using System.Collections.Generic;
using System.Text;

namespace WordWhipperServer.Game
{
    /// <summary>
    /// A position in the board (x,y)
    /// </summary>
    public class BoardPosition
    {
        private int m_x;
        private int m_y;

        /// <summary>
        /// Creates a board position
        /// </summary>
        /// <param name="x">x coord</param>
        /// <param name="y">y coord</param>
        public BoardPosition(int x, int y)
        {
            m_x = x;
            m_y = y;
        }

        /// <summary>
        /// Gets the x coord of the space
        /// </summary>
        /// <returns></returns>
        public int GetX() {
            return m_x;
        }

        /// <summary>
        /// Gets the y coord of the space
        /// </summary>
        /// <returns></returns>
        public int GetY()
        {
            return m_y;
        }

        public override string ToString()
        {
            return (GetX() + ":" + GetY());
        }

        public override bool Equals(object other)
        {
            if (other.GetType() != typeof(BoardPosition))
                return false;

            BoardPosition otherPos = (BoardPosition)other;

            return otherPos.GetHashCode().Equals(this.GetHashCode());
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace WordWhipperServer.Game
{
    /// <summary>
    /// A position in the board (x,y)
    /// </summary>
    class BoardPosition
    {
        private int m_x;
        private int m_y;

        public BoardPosition(int x, int y)
        {
            m_x = x;
            m_y = y;
        }

        public int GetX() {
            return m_x;
        }

        public int GetY()
        {
            return m_y;
        }
    }
}

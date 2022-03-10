using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace WordWhipperServer.Game
{
    public class GameBoardUtils
    {
        private static int m_tripleWordSpots;
        private static int m_doubleWordSpots;
        private static int m_doubleLetterSpots;
        private static int m_tripleLetterSpots;

        private static Dictionary<BoardPosition, BoardSpaceMultipliers> m_defaultMultipliers;

        /// <summary>
        /// Sets the default spots and setup for space multipliers
        /// </summary>
        static GameBoardUtils()
        {
            m_defaultMultipliers = new Dictionary<BoardPosition, BoardSpaceMultipliers>();

            List<BoardPosition> tripleWordSpots = new List<BoardPosition>
            {
                new BoardPosition(0, 0),
                new BoardPosition(0, 7),
                new BoardPosition(0, 14),

                new BoardPosition(7, 0),
                new BoardPosition(7, 7),
                new BoardPosition(7, 14),

                new BoardPosition(14, 0),
                new BoardPosition(14, 7),
                new BoardPosition(14, 14),
            };

            List<BoardPosition> doubleWordSpots = new List<BoardPosition>
            {
                new BoardPosition(1, 1),
                new BoardPosition(1, 13),

                new BoardPosition(2, 2),
                new BoardPosition(2, 12),

                new BoardPosition(3, 3),
                new BoardPosition(3, 11),

                new BoardPosition(4, 4),
                new BoardPosition(4, 10),

                new BoardPosition(10, 4),
                new BoardPosition(10, 10),

                new BoardPosition(11, 3),
                new BoardPosition(11, 11),

                new BoardPosition(12, 2),
                new BoardPosition(12, 12),

                new BoardPosition(13, 1),
                new BoardPosition(13, 13),
            };

            List<BoardPosition> doubleLetterSpots = new List<BoardPosition>
            {
                new BoardPosition(0, 3),
                new BoardPosition(0, 11),

                new BoardPosition(2, 6),
                new BoardPosition(2, 8),

                new BoardPosition(3, 0),
                new BoardPosition(3, 7),
                new BoardPosition(3, 14),

                new BoardPosition(6, 2),
                new BoardPosition(6, 6),
                new BoardPosition(6, 8),
                new BoardPosition(6, 12),

                new BoardPosition(7, 3),
                new BoardPosition(7, 11),

                new BoardPosition(8, 2),
                new BoardPosition(8, 6),
                new BoardPosition(8, 8),
                new BoardPosition(8, 12),

                new BoardPosition(11, 0),
                new BoardPosition(11, 7),
                new BoardPosition(11, 14),

                new BoardPosition(12, 6),
                new BoardPosition(12, 8),

                new BoardPosition(14, 3),
                new BoardPosition(14, 11),
            };

            List<BoardPosition> tripleLetterSpots = new List<BoardPosition>
            {
                    new BoardPosition(1, 5),
                    new BoardPosition(1, 9),

                    new BoardPosition(5, 1),
                    new BoardPosition(5, 5),
                    new BoardPosition(5, 9),
                    new BoardPosition(5, 13),

                    new BoardPosition(9, 1),
                    new BoardPosition(9, 5),
                    new BoardPosition(9, 9),
                    new BoardPosition(9, 13),

                    new BoardPosition(13, 5),
                    new BoardPosition(13, 9),
            };

            m_tripleLetterSpots = tripleLetterSpots.Count;
            m_tripleWordSpots = tripleWordSpots.Count;
            m_doubleLetterSpots = doubleLetterSpots.Count;
            m_doubleWordSpots = doubleWordSpots.Count;

            doubleLetterSpots.ForEach(x => m_defaultMultipliers.Add(x, BoardSpaceMultipliers.DOUBLE_LETTER));
            doubleWordSpots.ForEach(x => m_defaultMultipliers.Add(x, BoardSpaceMultipliers.DOUBLE_WORD));
            tripleLetterSpots.ForEach(x => m_defaultMultipliers.Add(x, BoardSpaceMultipliers.TRIPLE_LETTER));
            tripleWordSpots.ForEach(x => m_defaultMultipliers.Add(x, BoardSpaceMultipliers.TRIPLE_WORD));
        }

        /// <summary>
        /// Gets what multiplier is at a position by default
        /// </summary>
        /// <param name="x">x coord</param>
        /// <param name="y">y coord</param>
        /// <returns>multiplier</returns>
        public static BoardSpaceMultipliers GetDefaultMultiplierAt(int x, int y)
        {
            BoardPosition pos = new BoardPosition(x, y);

            if (!m_defaultMultipliers.ContainsKey(pos))
                return BoardSpaceMultipliers.NONE;

            return m_defaultMultipliers[pos];
        }

        /// <summary>
        /// Gets how many double letter spots there are
        /// </summary>
        /// <returns>int</returns>
        public static int GetDoubleLetterSpotAmounts()
        {
            return m_doubleLetterSpots;
        }

        /// <summary>
        /// Gets how many double word spots there are
        /// </summary>
        /// <returns>int</returns>
        public static int GetDoubleWordSpotAmounts()
        {
            return m_doubleWordSpots;
        }

        /// <summary>
        /// Gets how many triple letter spots there are
        /// </summary>
        /// <returns>int</returns>
        public static int GetTripleLetterSpotAmounts()
        {
            return m_tripleLetterSpots;
        }

        /// <summary>
        /// Gets how many triple word spots there are
        /// </summary>
        /// <returns>int</returns>
        public static int GetTripleWordSpotAmounts()
        {
            return m_tripleWordSpots;
        }
    }
}

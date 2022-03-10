using System;
using System.Collections.Generic;
using System.Text;
using WordWhipperServer.Util;

namespace WordWhipperServer.Game
{
    /// <summary>
    /// a bag that is used for a game
    /// </summary>
    class TileBag
    {
        private List<int> m_tiles;

        /// <summary>
        /// Constructor for the class
        /// </summary>
        public TileBag(GameLanguages type)
        {
            m_tiles = new List<int>();

            switch (type) {
                case GameLanguages.ENGLISH:
                    Fill(new EnglishLetters());
                    break;
                default:
                    throw new Exception("This language isn't setup properly! Create an enum for it and add to tile bag!");
            }
        }

        /// <summary>
        /// Fills the tile bag
        /// </summary>
        private void Fill(Enum lang)
        {
            foreach (int letter in Enum.GetValues(lang.GetType()))
            {
                LetterDataAttribute letterData = EnumExtensions.GetAttribute<LetterDataAttribute>(lang);

                for (int i = 0; i < letterData.TileBagAmount; i++)
                {
                    m_tiles.Add(letter);
                }
            }

            m_tiles.Shuffle();
        }

        /// <summary>
        /// Gets how many tiles are in the bag left
        /// </summary>
        /// <returns>tile count</returns>
        public int GetRemainingTileCount()
        {
            return m_tiles.Count;
        }

        /// <summary>
        /// Gets whether the tile bag is empty
        /// </summary>
        /// <returns>is empty</returns>
        public bool IsEmpty()
        {
            return GetRemainingTileCount() == 0;
        }
    }
}

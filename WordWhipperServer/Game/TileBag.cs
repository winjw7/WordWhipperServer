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
        private Queue<int> m_tiles;

        /// <summary>
        /// Constructor for the class
        /// </summary>
        public TileBag(GameLanguages type)
        {
            switch (type) {
                case GameLanguages.ENGLISH:
                    Fill(new EnglishLetters());
                    break;
                default:
                    throw new Exception("This language isn't setup properly! Create an enum for it and add to tile bag!");
            }
        }

        /// <summary>
        /// Removes a letter from the tile bag and returns it
        /// </summary>
        /// <returns></returns>
        public int DrawLetter()
        {
            return m_tiles.Dequeue();
        }

        /// <summary>
        /// Fills the tile bag
        /// </summary>
        private void Fill(Enum lang)
        {
            List<int> tiles = new List<int>();

            foreach (int letter in Enum.GetValues(lang.GetType()))
            {
                LetterDataAttribute letterData = EnumExtensions.GetAttribute<LetterDataAttribute>(lang);

                for (int i = 0; i < letterData.TileBagAmount; i++)
                {
                    tiles.Add(letter);
                }
            }

            tiles.Shuffle();
            m_tiles = new Queue<int>(tiles);
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

using System;
using System.Collections.Generic;
using System.Text;
using WordWhipperServer.Util;

namespace WordWhipperServer.Game
{
    /// <summary>
    /// a bag that is used for a game
    /// </summary>
    public class TileBag
    {
        private Queue<int> m_tiles;

        /// <summary>
        /// Constructor for the class
        /// </summary>
        public TileBag(GameLanguages type)
        {
            Fill(type.GetAttribute<LanguageAttribute>().GetLangEnum());
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

            foreach (Enum letter in Enum.GetValues(lang.GetType()))
            {
                LetterDataAttribute letterData = letter.GetAttribute<LetterDataAttribute>();

                for (int i = 0; i < letterData.TileBagAmount; i++)
                {
                    tiles.Add((int) Enum.Parse(letter.GetType(), letter.ToString()));
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

        public override string ToString()
        {
            string str = "[";
            int[] tileArray = m_tiles.ToArray();

            for (int i = 0; i < GetRemainingTileCount(); i++)
            {
                str += Enum.GetName(typeof(EnglishLetters), tileArray[i]) + ",";
            }

            str = str.Remove(str.Length - 1);
            str += "]";

            return str;
        }
    }
}

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
        private GameLanguages m_lang;

        /// <summary>
        /// Constructor for the class
        /// </summary>
        public TileBag(GameLanguages type)
        {
            m_lang = type;
            Fill(type.GetAttribute<LanguageAttribute>().GetLangEnum());
        }

        /// <summary>
        /// Constructor for a tile bag with a tile set already created
        /// </summary>
        /// <param name="q">tiles</param>
        /// <param name="lang">language</param>
        public TileBag(Queue<int> q, GameLanguages lang)
        {
            m_lang = lang;
            m_tiles = q;
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
        /// Shuffles the tile bag, call if put tiles back in
        /// </summary>
        private void Shuffle()
        {
            List<int> tiles = new List<int>(m_tiles);
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

            Type langEnumType = m_lang.GetAttribute<LanguageAttribute>().GetLangEnum().GetType();

            for (int i = 0; i < GetRemainingTileCount(); i++)
            {
                str += Enum.GetName(langEnumType, tileArray[i]) + ",";
            }

            str = str.Remove(str.Length - 1);
            str += "]";

            return str;
        }
    }
}

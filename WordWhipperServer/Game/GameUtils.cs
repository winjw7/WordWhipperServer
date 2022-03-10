using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WordWhipperServer.Util;

namespace WordWhipperServer.Game
{
    /// <summary>
    /// Utils that the game can use
    /// </summary>
    public static class GameUtils
    {
        /// <summary>
        /// Checks if a word is valid for a language
        /// </summary>
        /// <param name="lang">The language to check</param>
        /// <param name="potentialWord">the word to check</param>
        /// <returns>is valid</returns>
        public static bool IsValidWord(GameLanguages lang, string potentialWord)
        {
            var linesRead = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory(), ".\\Data\\" + lang.GetAttribute<LanguageAttribute>().GetDictionaryPath() + ".txt"));
            
            foreach(var line in linesRead)
            {
                if (line.Equals(potentialWord.ToUpper()))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Converts a list of ints to a string
        /// </summary>
        /// <param name="lang">Language to use</param>
        /// <param name="word">word list</param>
        /// <returns>string</returns>
        public static string StringFromIntList(GameLanguages lang, List<int> word)
        {
            string str = "";
            
            Enum convert = lang.GetAttribute<LanguageAttribute>().GetLangEnum();

            foreach(int i in word)
            {
                str += Enum.GetName(convert.GetType(), i);
            }

            return str;
        }
    }
}

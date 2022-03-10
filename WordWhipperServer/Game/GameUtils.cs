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
    }
}

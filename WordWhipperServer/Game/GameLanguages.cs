using System;
using System.Collections.Generic;
using System.Text;

namespace WordWhipperServer.Game
{
    /// <summary>
    /// Languages that the game can be played in
    /// </summary>
    public enum GameLanguages
    {
        [Language(new EnglishLetters())]
        ENGLISH
    }
}

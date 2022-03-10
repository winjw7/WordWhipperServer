using System;
using System.Collections.Generic;
using System.Text;

namespace WordWhipperServer.Game
{
    /// <summary>
    /// A list of letters that can be played in the english language
    /// </summary>
    enum EnglishLetters
    {
        [LetterData(TileBagAmount = 0, Value = 0)]
        NULL,
        [LetterData(TileBagAmount = 2, Value = 0)]
        BLANK,
        [LetterData(TileBagAmount = 9, Value = 1)]
        A,
        [LetterData(TileBagAmount = 2, Value = 3)]
        B,
        [LetterData(TileBagAmount = 2, Value = 3)]
        C,
        [LetterData(TileBagAmount = 4, Value = 2)]
        D,
        [LetterData(TileBagAmount = 12, Value = 1)]
        E,
        [LetterData(TileBagAmount = 2, Value = 4)]
        F,
        [LetterData(TileBagAmount = 3, Value = 2)]
        G,
        [LetterData(TileBagAmount = 2, Value = 4)]
        H,
        [LetterData(TileBagAmount = 9, Value = 1)]
        I,
        [LetterData(TileBagAmount = 1, Value = 8)]
        J,
        [LetterData(TileBagAmount = 1, Value = 5)]
        K,
        [LetterData(TileBagAmount = 4, Value = 1)]
        L,
        [LetterData(TileBagAmount = 2, Value = 3)]
        M,
        [LetterData(TileBagAmount = 6, Value = 1)]
        N,
        [LetterData(TileBagAmount = 8, Value = 1)]
        O,
        [LetterData(TileBagAmount = 2, Value = 3)]
        P,
        [LetterData(TileBagAmount = 1, Value = 10)]
        Q,
        [LetterData(TileBagAmount = 6, Value = 1)]
        R,
        [LetterData(TileBagAmount = 4, Value = 1)]
        S,
        [LetterData(TileBagAmount = 6, Value = 1)]
        T,
        [LetterData(TileBagAmount = 4, Value = 1)]
        U,
        [LetterData(TileBagAmount = 2, Value = 4)]
        V,
        [LetterData(TileBagAmount = 2, Value = 4)]
        W,
        [LetterData(TileBagAmount = 1, Value = 8)]
        X,
        [LetterData(TileBagAmount = 2, Value = 4)]
        Y,
        [LetterData(TileBagAmount = 1, Value = 10)]
        Z,
    }
}

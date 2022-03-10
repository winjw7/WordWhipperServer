using System;
using System.Collections.Generic;
using System.Text;

namespace WordWhipperServer.Game
{
    /// <summary>
    /// Data associated with a letter
    /// </summary>
    public class LetterDataAttribute : Attribute
    {
        public int Value { get; set; }
        public int TileBagAmount { get; set; }
    }
}

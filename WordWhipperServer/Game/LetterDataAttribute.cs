using System;
using System.Collections.Generic;
using System.Text;

namespace WordWhipperServer.Game
{
    class LetterDataAttribute : Attribute
    {
        public int Value { get; set; }
        public int TileBagAmount { get; set; }
    }
}

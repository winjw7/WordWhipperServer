using System;
using System.Collections.Generic;
using System.Text;

namespace WordWhipperServer.Game
{
    /// <summary>
    /// Data associated with a zinger
    /// </summary>
    class ZingerDataAttribute : Attribute
    {
        public int MaxAllowed { get; set; }

        public bool PlayedOnTile { get; set; }
    }
}

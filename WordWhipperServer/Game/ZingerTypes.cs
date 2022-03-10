using System;
using System.Collections.Generic;
using System.Text;

namespace WordWhipperServer.Game
{
    /// <summary>
    /// A list of zinger types
    /// </summary>
    public enum ZingerTypes
    {
        [ZingerData(MaxAllowed = 1)]
        EXPLODE_LETTER,

        [ZingerData(MaxAllowed = 1)]
        EXPLODE_RANDOM_LETTER,
    }
}

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
        [ZingerData(MaxAllowed = 1, PlayedOnTile = true)]
        EXPLODE_LETTER,

        [ZingerData(MaxAllowed = 1, PlayedOnTile = true)]
        EXPLODE_RANDOM_LETTER,

        [ZingerData(MaxAllowed = 1, PlayedOnTile = true)]
        EXPLODE_WORD,

        [ZingerData(MaxAllowed = 1, PlayedOnTile = false)]
        EXPLODE_LETTER_EMPTY,

        [ZingerData(MaxAllowed = 1, PlayedOnTile = false)]
        EXPLODE_TILE_EMPTY,
    }
}

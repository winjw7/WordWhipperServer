using System;
using System.Collections.Generic;
using System.Text;

namespace WordWhipperServer.Game
{
    /// <summary>
    /// Flags that can be toggled on a game
    /// </summary>
    enum GameFlags
    {
        /// <summary>
        /// Whether or not the board has the double word, tripple letter, etc randomly placed around
        /// </summary>
        RANDOM_BOARD_MULTIPLIERS,

        /// <summary>
        /// Whether or not a random player is chosen to start, other wise host starts
        /// </summary>
        RANDOM_START,
    }
}

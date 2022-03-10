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
        /// Double word, tripple letter, etc are randomly placed around
        /// </summary>
        RANDOM_BOARD_MULTIPLIERS,

        /// <summary>
        /// A random player is chosen to start, other wise host starts
        /// </summary>
        RANDOM_START,

        /// <summary>
        /// Disables zingers
        /// </summary>
        DISABLE_ZINGERS
    }
}

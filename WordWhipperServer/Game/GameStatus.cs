using System;
using System.Collections.Generic;
using System.Text;

namespace WordWhipperServer.Game
{
    /// <summary>
    /// Different states a game can be in
    /// </summary>
    public enum GameStatus
    {
        WAITING_FOR_PLAYERS_TO_ACCEPT,
        PENDING_PLAYER_MOVE,
        COMPLETED
    }
}

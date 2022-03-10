using System;
using System.Collections.Generic;
using System.Text;

namespace WordWhipperServer.Game
{
    class ZingerDataAttribute : Attribute
    {
        public int MaxAllowed { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace WordWhipperServer
{
    /// <summary>
    /// An effect that can be played on a word
    /// </summary>
    abstract class Zinger
    {
        private Guid owner;

        /// <summary>
        /// Constructor for a zinger
        /// </summary>
        /// <param name="owner">Who owns this zinger</param>
        public Zinger(Guid owner)
        {
            this.owner = owner;
        }

        /// <summary>
        /// Gets the type of Zinger it is
        /// </summary>
        /// <returns>Zinger Type</returns>
        public abstract Zingers GetZingerType();
    }
}

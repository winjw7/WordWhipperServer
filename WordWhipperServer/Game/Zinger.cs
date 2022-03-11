using System;
using System.Collections.Generic;
using System.Text;

namespace WordWhipperServer.Game
{
    /// <summary>
    /// An effect that can be played on a word
    /// </summary>
    [Serializable]
    public abstract class Zinger
    {
        private Guid m_owner;
        private bool m_activated;

        /// <summary>
        /// Constructor for a zinger
        /// </summary>
        /// <param name="owner">Who owns this zinger</param>
        public Zinger(Guid owner)
        {
            m_owner = owner;
            m_activated = false;
        }

        /// <summary>
        /// Checks whether this zinger has been activated
        /// </summary>
        /// <returns>activated</returns>
        public bool IsActivated()
        {
            return m_activated;
        }

        /// <summary>
        /// Gets the owner of the zinger
        /// </summary>
        /// <returns>owner</returns>
        public Guid GetOwner()
        {
            return m_owner;
        }

        /// <summary>
        /// Gets the type of Zinger it is
        /// </summary>
        /// <returns>Zinger Type</returns>
        public abstract ZingerTypes GetZingerType();
    }
}

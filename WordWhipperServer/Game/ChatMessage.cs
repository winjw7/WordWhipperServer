using System;
using System.Collections.Generic;
using System.Text;

namespace WordWhipperServer.Game
{
    /// <summary>
    /// A message sent in a game
    /// </summary>
    [Serializable]
    public class ChatMessage
    {
        private Guid m_sender;
        private string m_message;
        private DateTime m_sent;

        /// <summary>
        /// Constructor for a chat message
        /// </summary>
        /// <param name="guid">the id of the sender</param>
        /// <param name="m">the actual message</param>
        /// <param name="s">when it was sent</param>
        public ChatMessage(Guid guid, string m, DateTime s)
        {
            m_sender = guid;
            m_message = m;
            m_sent = s;
        }

        /// <summary>
        /// gets the sender of the message
        /// </summary>
        /// <returns>sender</returns>
        public Guid GetSender()
        {
            return m_sender;
        }

        /// <summary>
        /// Gets the message
        /// </summary>
        /// <returns>message</returns>
        public string GetMessage()
        {
            return m_message;
        }

        /// <summary>
        /// Gets when the message was sent
        /// </summary>
        /// <returns>datetime</returns>
        public DateTime GetWhenSent()
        {
            return m_sent;
        }
    }
}

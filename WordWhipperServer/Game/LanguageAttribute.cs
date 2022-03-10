using System;
using System.Collections.Generic;
using System.Text;

namespace WordWhipperServer.Game
{
    /// <summary>
    /// What enum this language is associated with
    /// </summary>
    public class LanguageAttribute : Attribute
    {
        private Enum m_langEnum;
        private String m_dictionaryPath;

        public LanguageAttribute(object e, string path)
        {
            m_langEnum = e as Enum;
            m_dictionaryPath = path;
        }

        public Enum GetLangEnum()
        {
            return m_langEnum;
        }

        public String GetDictionaryPath()
        {
            return m_dictionaryPath;
        }
    }
}

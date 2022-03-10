using System;
using System.Collections.Generic;
using System.Text;

namespace WordWhipperServer.Game
{
    public class LanguageAttribute : Attribute
    {
        private Enum langEnum;

        public LanguageAttribute(object e)
        {
            langEnum = e as Enum;
        }

        public Enum GetLangEnum()
        {
            return langEnum;
        }
    }
}

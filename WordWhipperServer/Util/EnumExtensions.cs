using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordWhipperServer.Util
{
    /// <summary>
    /// Helper class for enums
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets an attribute of an enum
        /// </summary>
        /// <typeparam name="TAttribute">Attribute to get</typeparam>
        /// <param name="value">the enum to check</param>
        /// <returns>attribute</returns>
        public static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            return type.GetField(name) 
                .GetCustomAttributes(false)
                .OfType<TAttribute>()
                .SingleOrDefault();
        }
    }
}

using IvanStoychev.StringExtensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Backend.Utility
{
    /// <summary>
    /// Works with strings, performing various actions on them.
    /// </summary>
    static class StringOperator
    {
        /// <summary>
        /// Removes <paramref name="trimString"/> from the start of each member of the <paramref name="strings"/> collection.
        /// </summary>
        /// <param name="strings">Collection of strings to operate on.</param>
        /// <param name="trimString">String to remove from the start of each <paramref name="strings"/> member.</param>
        /// <param name="ignoreCase">Whether to ignore case when looking for <paramref name="trimString"/>.</param>
        /// <returns>Array of strings, representing the trimmed members of the <paramref name="strings"/> collection.</returns>
        internal static string[] TrimStartStrings(IEnumerable<string> strings, string trimString, bool ignoreCase = true)
        {
            string[] stringArray = strings.ToArray();
            for (int i = 0; i < stringArray.Length; i++)
            {
                string tempString = stringArray[i].TrimStart(trimString, ignoreCase, CultureInfo.InvariantCulture);
                stringArray[i] = tempString;
            }

            return stringArray;
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The string extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.Utils
{
    using System;

    /// <summary>
    /// The string extensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// The stf format string.
        /// </summary>
        /// <param name="theString">
        /// The the string.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string StfFormatString(this string theString, params object[] args)
        {
            var retVal = string.Empty;

            if (string.IsNullOrEmpty(theString))
            {
                return retVal;
            }

            if (args == null || args.Length <= 0)
            {
                return theString;
            }

            retVal = theString;

            try
            {
                retVal = string.Format(theString, args);
            }
            catch (Exception)
            {
                // just don't break
            }

            return retVal;
        }
    }
}

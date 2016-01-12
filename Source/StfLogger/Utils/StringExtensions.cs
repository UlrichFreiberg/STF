﻿// --------------------------------------------------------------------------------------------------------------------
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
            return string.IsNullOrEmpty(theString) ? string.Empty : string.Format(theString, args);
        }
    }
}
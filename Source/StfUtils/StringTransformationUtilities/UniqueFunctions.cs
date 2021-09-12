// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UniqueFunctions.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the UniqueFunctions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Text.RegularExpressions;

namespace Mir.Stf.Utilities.StringTransformationUtilities
{
    using System;

    /// <summary>
    /// The unique functions.
    /// </summary>
    public class UniqueFunctions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueFunctions"/> class.
        /// </summary>
        public UniqueFunctions()
        {
            GuidFormatString = "D";
        }

        /// <summary>
        /// Gets or sets the guid format string.
        /// </summary>
        public string GuidFormatString { get; set; }

        /// <summary>
        /// The stu uuid.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        [StringTransformationUtilFunction("GUID")]
        public string StuGuid(string arg)
        {
            var format = string.IsNullOrEmpty(arg) ? GuidFormatString : arg;

            if (!Regex.IsMatch(format, "^[NDBPX]$"))
            {
                return null;
            }

            var guid = Guid.NewGuid();
            var retVal = guid.ToString(format);

            return retVal;
        }
    }
}

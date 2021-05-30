// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleFunctions.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the SimpleFunctions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.StringTransformationUtilities
{
    using System;

    /// <summary>
    /// The simple functions.
    /// </summary>
    public class SimpleFunctions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleFunctions"/> class.
        /// </summary>
        public SimpleFunctions()
        {
            DateFormatString = "yyyy-MM-dd";
            TimeFormatString = "HH:mm";
        }

        /// <summary>
        /// Gets or sets the time format string.
        /// </summary>
        public string TimeFormatString { get; set; }

        /// <summary>
        /// Gets or sets the date format string.
        /// </summary>
        public string DateFormatString { get; set; }

        /// <summary>
        /// The stu today.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        [StringTransformationUtilFunction("TODAY")]
        public string StuToday(string arg)
        {
            var format = string.IsNullOrEmpty(arg) ? DateFormatString : arg;
            var retVal = DateTime.Now.ToString(format);

            return retVal;
        }

        /// <summary>
        /// The stu now.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        [StringTransformationUtilFunction("NOW")]
        public string StuNow(string arg)
        {
            var format = string.IsNullOrEmpty(arg) ? TimeFormatString : arg;
            var retVal = DateTime.Now.ToString(format);

            return retVal;
        }

        /// <summary>
        /// The stu space.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        [StringTransformationUtilFunction("SPACE")]
        public string StuSpace(string arg)
        {
            var retVal = " ";

            if (int.TryParse(arg, out var numberOfSpaces))
            {
                retVal = retVal.PadLeft(numberOfSpaces);
            }
            
            return retVal;
        }

        /// <summary>
        /// The stu empty.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        [StringTransformationUtilFunction("EMPTY")]
        public string StuEmpty(string arg)
        {
            var retVal = string.Empty;

            return retVal;
        }
    }
}

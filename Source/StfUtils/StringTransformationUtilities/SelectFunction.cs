// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectFunction.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the SelectFunction type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.StringTransformationUtilities
{
    using System;

    /// <summary>
    /// The select function.
    /// </summary>
    public class SelectFunction
    {
        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// The stu select.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        [StringTransformationUtilFunction("SELECT")]
        public string StuSelect(string arg)
        {
            if (string.IsNullOrEmpty(arg))
            {
                ErrorMessage = $"arg [{arg}] is null or empty";
                return null;
            }

            var valuesArr = arg.Trim().Split(';');

            if (valuesArr.Length == 0)
            {
                ErrorMessage = $"arg [{arg}] contains en empty values set";
                return null;
            }

            // Random is seeded by time - so lets wait a mili-sec - or so
            System.Threading.Thread.Sleep(3);

            var random = new Random();

            var theChosenOne = random.Next(0, valuesArr.Length);
            var retVal = valuesArr[theChosenOne];

            return retVal;
        }
    }
}

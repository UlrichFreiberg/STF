// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapValuesFunction.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the MapValuesFunction type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.StringTransformationUtilities
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// The map values function.
    /// </summary>
    public class MapValuesFunction
    {
        /// <summary>
        /// The reg exp.
        /// .
        /// {MAPVALUES 23 [23 ; 43] [twentyThree ; fourtyThree]}
        /// {MAPVALUES 23 [23 ; 43] [; fourtyThree]} # 23 is mapped to string.Empty
        /// </summary>
        public const string RegExp = @"((?<ValueToMap>""?[^\[""]+""?)\s*)\[(?<From>[^]]+)\]\s*\[(?<To>[^]]+)\]";

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// The stu map values.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        [StringTransformationUtilFunction("MAPVALUES")]
        public string StuMapValues(string arg)
        {
            if (string.IsNullOrEmpty(arg))
            {
                ErrorMessage = $"arg [{arg}] is null or empty";
                return null;
            }

            var selectRegexp = new Regex(RegExp);
            var match = selectRegexp.Match(arg);

            if (!match.Success)
            {
                ErrorMessage = $"arg [{arg}] couldn't be parsed";
                return null;
            }

            var valueToMap = match.Groups["ValueToMap"].Value.Trim();
            var setFrom = match.Groups["From"].Value.Trim();
            var setTo = match.Groups["To"].Value.Trim();
            var setFromArr = setFrom.Split(';');
            var setToArr = setTo.Split(';');
            var numberOfChoicesFrom = setFromArr.Length;
            var numberOfChoicesTo = setToArr.Length;

            if (numberOfChoicesFrom == 0)
            {
                ErrorMessage = $"arg [{arg}] contains en empty numberOfChoicesFrom set";
                return null;
            }

            if (numberOfChoicesTo == 0)
            {
                ErrorMessage = $"arg [{arg}] contains en empty numberOfChoicesTo set";
                return null;
            }

            int index;

            for (index = 0; index < numberOfChoicesFrom; index++)
            {
                var fromValue = setFromArr[index].Trim();

                if (valueToMap.Equals(fromValue))
                {
                    break;
                }
            }

            if (index == numberOfChoicesFrom)
            {
                ErrorMessage = $"Couldn't map [{valueToMap}] as it is not found in the FROM set";

                return null;
            }

            if (index >= numberOfChoicesTo)
            {
                ErrorMessage = $"Couldn't map [{valueToMap}] as it is not found in the TO set";

                return null;
            }

            var retVal = setToArr[index];

            return retVal;
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringFunction.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the StringFunction type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.StringTransformationUtilities
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// The string function. Wrapper for C# string functions
    /// </summary>
    public class StringFunction : StfUtilsBase
    {
        /// <summary>
        /// The stu string.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        [StringTransformationUtilFunction("STRING")]
        public string StuString(string arg)
        {
            const string RegExp = @"""(?<StringFunction>[^""]+)""(?<Arguments>.*)";
            var match = Regex.Match(arg, RegExp);

            if (!match.Success)
            {
                return null;
            }

            var stuStringFunction = match.Groups["StringFunction"].Value.Trim();
            var stuStringFunctionArgument = match.Groups["Arguments"].Value.Trim();
            string retVal = null;

            switch (stuStringFunction)
            {
                case "IndexOf":
                    retVal = StuFunctionIndexOf(stuStringFunctionArgument);
                    break;
                case "Substring":
                    retVal = StuFunctionSubstring(stuStringFunctionArgument);
                    break;
                case "Format":
                    retVal = StuFunctionFormat(stuStringFunctionArgument);
                    break;
                case "Compare":
                    retVal = StuFunctionCompare(stuStringFunctionArgument);
                    break;
                case "Length":
                    retVal = StuFunctionLength(stuStringFunctionArgument);
                    break;
                case "Trim":
                    retVal = StuFunctionTrim(stuStringFunctionArgument);
                    break;
                case "TrimEnd":
                    retVal = StuFunctionTrimEnd(stuStringFunctionArgument);
                    break;
                case "TrimStart":
                    retVal = StuFunctionTrimStart(stuStringFunctionArgument);
                    break;
                case "EndsWith":
                    retVal = StuFunctionEndsWith(stuStringFunctionArgument);
                    break;
                case "StartsWith":
                    retVal = StuFunctionStartsWith(stuStringFunctionArgument);
                    break;
                case "Insert":
                    retVal = StuFunctionInsert(stuStringFunctionArgument);
                    break;
                case "PADLEFT":
                    retVal = StuFunctionPadLeft(stuStringFunctionArgument);
                    break;
                case "PADRIGHT":
                    retVal = StuFunctionPadRight(stuStringFunctionArgument);
                    break;
                case "Remove":
                    retVal = StuFunctionRemove(stuStringFunctionArgument);
                    break;
                case "Replace":
                    retVal = StuFunctionReplace(stuStringFunctionArgument);
                    break;
                case "ToLower":
                    retVal = StuFunctionToLower(stuStringFunctionArgument);
                    break;
                case "ToUpper":
                    retVal = StuFunctionToUpper(stuStringFunctionArgument);
                    break;
            }

            return retVal;
        }

        /// <summary>
        /// The stu function index of.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string StuFunctionIndexOf(string arg)
        {
            return "[IndexOf]" + arg;
        }

        /// <summary>
        /// The stu function substring.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string StuFunctionSubstring(string arg)
        {
            return "[Substring]" + arg;
        }

        /// <summary>
        /// The stu function format.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string StuFunctionFormat(string arg)
        {
            return "[Format]" + arg;
        }

        /// <summary>
        /// The stu function compare.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string StuFunctionCompare(string arg)
        {
            return "[Compare]" + arg;
        }

        /// <summary>
        /// The stu function length.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string StuFunctionLength(string arg)
        {
            return "[Length]" + arg;
        }

        /// <summary>
        /// The stu function trim.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string StuFunctionTrim(string arg)
        {
            return "[Trim]" + arg;
        }

        /// <summary>
        /// The stu function trim end.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string StuFunctionTrimEnd(string arg)
        {
            return "[TrimEnd]" + arg;
        }

        /// <summary>
        /// The stu function trim start.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string StuFunctionTrimStart(string arg)
        {
            return "[TrimStart]" + arg;
        }

        /// <summary>
        /// The stu function ends with.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string StuFunctionEndsWith(string arg)
        {
            return "[EndsWith]" + arg;
        }

        /// <summary>
        /// The stu function starts with.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string StuFunctionStartsWith(string arg)
        {
            return "[StartsWith]" + arg;
        }

        /// <summary>
        /// The stu function insert.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string StuFunctionInsert(string arg)
        {
            return "[Insert]" + arg;
        }

        /// <summary>
        /// The stu function pad left.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string StuFunctionPadLeft(string arg)
        {
            // "PadLeft" "Source" "TotalWidth" "PaddingChar"
            // "PadLeft" "Source" "TotalWidth" "PaddingChar"
            // "PadLeft" "Bo oB" "8" "x" --> "xxxBo oB"
            // In a config.txt looks like:
            //     "{STRING "PadLeft" "Bo oB" "8" "x"}
            const string RegExp = @"""(?<Source>[^""]*)""\s+""(?<TotalWidth>[^""]*)""(\s+""(?<PaddingChar>[^""]*)"")?";
            var match = Regex.Match(arg, RegExp);

            if (!match.Success)
            {
                return null;
            }

            var argSource = match.Groups["Source"].Value.Trim();
            var argTotalWidth = match.Groups["TotalWidth"].Value.Trim();
            var argPaddingChar = match.Groups["PaddingChar"].Value.Trim();

            if (string.IsNullOrEmpty(argSource))
            {
                LogError("PadLeft: Source cannot be null or empty");
                return null;
            }

            if (!int.TryParse(argTotalWidth, out var totalWidth))
            {
                LogError("PadLeft: totalWidth must be an int");
                return null;
            }

            if (totalWidth < argSource.Length)
            {
                LogError("PadLeft: totalWidth must not be less than length of source");
                return null;
            }


            var retVal = argPaddingChar.Length == 0
                ? argSource.PadLeft(totalWidth)
                : argSource.PadLeft(totalWidth, argPaddingChar[0]);

            return retVal;
        }

        /// <summary>
        /// The stu function pad right.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string StuFunctionPadRight(string arg)
        {
            // "PadRight" "Source" "TotalWidth" "PaddingChar"
            // "PadRight" "Source" "TotalWidth" "PaddingChar"
            // "PadRight" "Bo oB" "8" "x" --> "Bo oBxxx"
            // In a config.txt looks like:
            //     "{STRING "PadRight" "Bo oB" "8" "x"}
            const string RegExp = @"""(?<Source>[^""]*)""\s+""(?<TotalWidth>[^""]*)""(\s+""(?<PaddingChar>[^""]*)"")?";
            var match = Regex.Match(arg, RegExp);

            if (!match.Success)
            {
                return null;
            }

            var argSource = match.Groups["Source"].Value.Trim();
            var argTotalWidth = match.Groups["TotalWidth"].Value.Trim();
            var argPaddingChar = match.Groups["PaddingChar"].Value.Trim();

            if (string.IsNullOrEmpty(argSource))
            {
                LogError("PadRight: Source cannot be null or empty");
                return null;
            }

            if (!int.TryParse(argTotalWidth, out var totalWidth))
            {
                LogError("PadRight: totalWidth must be an int");
                return null;
            }

            if (totalWidth < argSource.Length)
            {
                LogError("PadRight: totalWidth must not be less than length of source");
                return null;
            }

            var retVal = argPaddingChar.Length == 0
                       ? argSource.PadRight(totalWidth)
                       : argSource.PadRight(totalWidth, argPaddingChar[0]);

            return retVal;
        }

        /// <summary>
        /// The stu function remove.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string StuFunctionRemove(string arg)
        {
            // "Remove" "Source" "StringToRemove"
            // "Remove" "BooB" "o" --> "BB"
            return "[Remove]" + arg;
        }

        /// <summary>
        /// The stu function replace.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string StuFunctionReplace(string arg)
        {
            // "Replace" "Source" "Search" "Replace" "IgnoreCase=false/true"
            // "Replace" "BooB" "o" "Z" --> "BZZB"
            return "[Replace]" + arg;
        }

        /// <summary>
        /// The stu function to lower.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string StuFunctionToLower(string arg)
        {
            var retVal = arg.ToLower();

            return retVal;
        }

        /// <summary>
        /// The stu function to upper.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string StuFunctionToUpper(string arg)
        {
            return "[ToUpper]" + arg;
        }
    }
}

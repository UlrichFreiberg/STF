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
    using System;
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
                case "ENDSWITH":
                    retVal = StuFunctionEndsWith(stuStringFunctionArgument);
                    break;
                case "STARTSWITH":
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
                case "TOLOWER":
                    retVal = StuFunctionToLower(stuStringFunctionArgument);
                    break;
                case "TOUPPER":
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
        /// returns the source string if it does end with the testString
        /// </returns>
        private string StuFunctionEndsWith(string arg)
        {
            // "EndsWith" "Source" "TestString" "CaseSensitive"(optional, default is CaseSensitive)
            // "EndsWith" "Bo oB" "ob" "CS" --> string.empty
            // In a config.txt looks like:
            //     "{STRING "StartsWith" "Bo oB" "ob" "CS"}   returns string.empty
            //     "{STRING "StartsWith" "Bo oB" "ob" "CI"}   returns "Bo oB"
            const string RegExp = @"""(?<Source>[^""]*)""\s+""(?<TestString>[^""]*)""(\s+""(?<StringComparison>[^""]*)"")?";
            var match = Regex.Match(arg, RegExp);

            if (!match.Success)
            {
                return null;
            }

            var argSource = match.Groups["Source"].Value.Trim();
            var argTestString = match.Groups["TestString"].Value.Trim();
            var argStringComparison = match.Groups["StringComparison"].Value.Trim();

            if (string.IsNullOrEmpty(argSource))
            {
                LogError("EndsWith: Source cannot be null or empty");
                return null;
            }

            if (string.IsNullOrEmpty(argTestString))
            {
                LogError("EndsWith: TestString cannot be null or empty");
                return null;
            }

            var stringComparison = StringComparison.CurrentCultureIgnoreCase;
            if (string.IsNullOrEmpty(argStringComparison) || argStringComparison == "CS")
            {
                stringComparison = StringComparison.CurrentCulture;
            }
            else if (argStringComparison != "CS" && argStringComparison != "CI")
            {
                LogError("EndsWith: stringComparison must be CS (Case Sensitive) or IC (Ignore Case)");
                return null;
            }

            var isEndingWith = argSource.EndsWith(argTestString, stringComparison);
            var retVal = isEndingWith ? argSource : string.Empty;

            return retVal;
        }

        /// <summary>
        /// The stu function starts with.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// returns the source string if it does start with the testString
        /// </returns>
        private string StuFunctionStartsWith(string arg)
        {
            // "StartsWith" "Source" "TestString" "CaseSensitive"(optional, default is CaseSensitive)
            // "StartsWith" "Bo oB" "bo" "CS" --> string.empty
            // In a config.txt looks like:
            //     "{STRING "StartsWith" "Bo oB" "bo" "CS"}   returns string.empty
            //     "{STRING "StartsWith" "Bo oB" "bo" "CI"}   returns "Bo oB"
            const string RegExp = @"""(?<Source>[^""]*)""\s+""(?<TestString>[^""]*)""(\s+""(?<StringComparison>[^""]*)"")?";
            var match = Regex.Match(arg, RegExp);

            if (!match.Success)
            {
                return null;
            }

            var argSource = match.Groups["Source"].Value.Trim();
            var argTestString = match.Groups["TestString"].Value.Trim();
            var argStringComparison = match.Groups["StringComparison"].Value.Trim();

            if (string.IsNullOrEmpty(argSource))
            {
                LogError("StartsWith: Source cannot be null or empty");
                return null;
            }

            if (string.IsNullOrEmpty(argTestString))
            {
                LogError("StartsWith: TestString cannot be null or empty");
                return null;
            }

            var stringComparison = StringComparison.CurrentCultureIgnoreCase;
            if (string.IsNullOrEmpty(argStringComparison) || argStringComparison == "CS")
            {
                stringComparison = StringComparison.CurrentCulture;
            }
            else if (argStringComparison != "CS" && argStringComparison != "CI")
            {
                LogError("StartsWith: stringComparison must be CS (Case Sensitive) or IC (Ignore Case)");
                return null;
            }

            var isStartingWith = argSource.StartsWith(argTestString, stringComparison);
            var retVal = isStartingWith ? argSource : string.Empty;

            return retVal;
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
            // "ToLower" "Source"
            // "TOLOWER" "Bo 12 ?!asoB" "bo 12 ?!asob"
            // In a config.txt looks like:
            //     "{STRING "TOLOWER" "Bo 12 ?!asoB"}
            const string RegExp = @"""(?<Source>[^""]*)""";
            var match = Regex.Match(arg, RegExp);

            if (!match.Success)
            {
                return null;
            }

            var argSource = match.Groups["Source"].Value.Trim();

            var retVal = argSource.ToLower();

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
            // "ToUpper" "Source"
            // "TOUPPER" "Bo 12 ?!asoB" "Bo 12 ?!ASOB"
            // In a config.txt looks like:
            //     "{STRING "TOUPPER" "Bo 12 ?!asoB"}
            const string RegExp = @"""(?<Source>[^""]*)""";
            var match = Regex.Match(arg, RegExp);

            if (!match.Success)
            {
                return null;
            }

            var argSource = match.Groups["Source"].Value.Trim();

            var retVal = argSource.ToUpper();

            return retVal;
        }
    }
}

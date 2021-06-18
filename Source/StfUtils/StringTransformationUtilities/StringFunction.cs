﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringFunction.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the StringFunction type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable RedundantAssignment
// ReSharper disable ExpressionIsAlwaysNull
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
                case "EndsWith":
                    retVal = StuFunctionEndsWith(stuStringFunctionArgument);
                    break;
                case "StartsWith":
                    retVal = StuFunctionStartsWith(stuStringFunctionArgument);
                    break;
                case "Insert":
                    retVal = StuFunctionInsert(stuStringFunctionArgument);
                    break;
                case "PadLeft":
                    retVal = StuFunctionPadLeft(stuStringFunctionArgument);
                    break;
                case "PadRight":
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
            // "IndexOf" "Source" "TestString" "startIndex" ""CaseSensitive"(optional, default is CaseSensitive)
            // no optional count parameter as in c# IndexOf
            // "IndexOf" "Bo oB Bo" "ob" "2" "CS" --> "3"
            // In a config.txt looks like:
            //     "{STRING "IndexOf" "Bo oB Bo" "ob" "2" "CS"}   returns "-1"
            //     "{STRING "IndexOf" "Bo oB Bo" "ob" "2" "CI"}   returns "3"
            //     "{STRING "IndexOf" "Bo oB Bo" "ob" "4" "CI"}   returns "-1"
            var retVal = "-1";
            try
            {
                const string RegExp =
                    @"""(?<Source>[^""]*)""\s+""(?<Value>[^""]*)""\s+""(?<StartIndex>[^""]*)""(\s+""(?<StringComparison>[^""]*)"")?";
                var match = Regex.Match(arg, RegExp);

                var argSource = match.Groups["Source"].Value.Trim();
                var argValue = match.Groups["Value"].Value.Trim();
                var argStartIndex = match.Groups["StartIndex"].Value.Trim();
                var argStringComparison = match.Groups["StringComparison"].Value.Trim();

                if (string.IsNullOrEmpty(argSource))
                {
                    argSource = string.Empty;
                }

                if (!int.TryParse(argStartIndex, out var startIndex)
                    || 
                    startIndex < 0 
                    ||
                    startIndex >= argSource.Length)
                {
                    LogError("IndexOf: startIndex must be a positive int less than length of Source");
                    retVal = "-1";
                    return retVal;
                }

                var stringComparison = StringComparison.CurrentCulture;

                switch (argStringComparison)
                {
                    case "CS":
                        stringComparison = StringComparison.CurrentCulture;
                        break;
                    case "CI":
                        stringComparison = StringComparison.CurrentCultureIgnoreCase;
                        break;
                    case "":
                        stringComparison = StringComparison.CurrentCulture;
                        break;
                    default:
                        LogError($"EndsWith: invalid value for Case Sensitivity (CS or CI)");
                        retVal = StuBoolean.False.ToString();
                        return retVal;
                }

                retVal = argValue.Length == 0
                             ? startIndex.ToString()
                             : argSource.IndexOf(argValue, startIndex, stringComparison).ToString();

                return retVal;
            }
            catch (Exception ex)
            {
                LogError($"IndexOf: Exception {ex.Message} ");
                retVal = "-1";
                return retVal;
            }
            finally
            {
                LogInfo($"IndexOf: Finally retVal {retVal} ");
            }
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
            // "Substring" "Source" "startIndex" "length" 
            // "Substring" "Bo oB Bo" "2" "2" --> " o"
            // "Substring" "Bo oB Bo" "3" "4" --> "oB B"
            // In a config.txt looks like:
            //     "{STRING "IndexOf" "Bo oB Bo" "2" "2"}   returns " o"
            //     "{STRING "IndexOf" "Bo oB Bo" "3" "4"}   returns "oB B"
            string retVal = null;
            try
            {
                const string RegExp = @"""(?<Source>[^""]*)""\s+""(?<StartIndex>[^""]*)""(\s+""(?<Length>[^""]*)"")?";
                var match = Regex.Match(arg, RegExp);

                var argSource = match.Groups["Source"].Value;
                var argStartIndex = match.Groups["StartIndex"].Value.Trim();
                var argLength = match.Groups["Length"].Value.Trim();

                if (string.IsNullOrEmpty(argSource))
                {
                    argSource = string.Empty;
                }

                if (!int.TryParse(argStartIndex, out var startIndex)
                    ||
                    startIndex < 0
                    ||
                    startIndex > argSource.Length)
                {
                    LogError("Substring: startIndex must be a positive int less than length of Source");
                    retVal = null;
                    return retVal;
                }

                var length = 0;
                if (string.IsNullOrEmpty(argLength))
                {
                    argLength = string.Empty;
                }
                else if (!int.TryParse(argLength, out length)
                         ||
                         length < 0)
                {
                    LogError("Substring: length (optional) must be a positive int less than length of Source");
                    retVal = null;
                    return retVal;
                }

                retVal = argLength.Length == 0
                             ? argSource.Substring(startIndex)
                             : argSource.Substring(startIndex, length);

                return retVal;
            }
            catch (Exception ex)
            {
                LogError($"Substring: Exception {ex.Message} ");
                retVal = null;
                return retVal;
            }
            finally
            {
                LogInfo($"Substring: Finally retVal {retVal ?? "null"} ");
            }
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
            //     "{STRING "EndsWith" "Bo oB" "ob" "CS"}   returns string.empty
            //     "{STRING "EndsWith" "Bo oB" "ob" "CI"}   returns "Bo oB"
            var retVal = StuBoolean.False.ToString();
            try
            {
                const string RegExp = @"""(?<Source>[^""]*)""\s+""(?<TestString>[^""]*)""(\s+""(?<StringComparison>[^""]*)"")?";
                var match = Regex.Match(arg, RegExp);

                if (!match.Success)
                {
                    return StuBoolean.False.ToString();
                }

                var argSource = match.Groups["Source"].Value.Trim();
                var argTestString = match.Groups["TestString"].Value.Trim();
                var argStringComparison = match.Groups["StringComparison"].Value.Trim();

                if (string.IsNullOrEmpty(argSource))
                {
                    LogError("EndsWith: Source cannot be null or empty");
                    retVal = StuBoolean.False.ToString();
                    return retVal;
                }

                if (string.IsNullOrEmpty(argTestString))
                {
                    LogError("EndsWith: TestString cannot be null or empty");
                    retVal = StuBoolean.False.ToString();
                    return retVal;
                }

                var stringComparison = StringComparison.CurrentCulture;

                switch (argStringComparison)
                {
                    case "CS":
                        stringComparison = StringComparison.CurrentCulture;
                        break;
                    case "CI":
                        stringComparison = StringComparison.CurrentCultureIgnoreCase;
                        break;
                    case "":
                        stringComparison = StringComparison.CurrentCulture;
                        break;
                    default:
                        LogError($"EndsWith: invalid value for Case Sensitivity (CS or CI)");
                        retVal = StuBoolean.False.ToString();
                        return retVal;
                }

                var isEndingWith = argSource.EndsWith(argTestString, stringComparison);
                retVal = isEndingWith ? StuBoolean.True.ToString() : StuBoolean.False.ToString();

                return retVal;
            }
            catch (Exception ex)
            {
                LogError($"EndsWith: Exception {ex.Message} ");
                retVal = StuBoolean.False.ToString();
                return retVal;
            }
            finally
            {
                LogInfo($"EndsWith: Finally retVal {retVal} ");
            }
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
            var retVal = StuBoolean.False.ToString();
            try
            {
                const string RegExp = @"""(?<Source>[^""]*)""\s+""(?<TestString>[^""]*)""(\s+""(?<StringComparison>[^""]*)"")?";
                var match = Regex.Match(arg, RegExp);

                if (!match.Success)
                {
                    return StuBoolean.False.ToString();
                }

                var argSource = match.Groups["Source"].Value.Trim();
                var argTestString = match.Groups["TestString"].Value.Trim();
                var argStringComparison = match.Groups["StringComparison"].Value.Trim();

                if (string.IsNullOrEmpty(argSource))
                {
                    LogError("StartsWith: Source cannot be null or empty");
                    retVal = StuBoolean.False.ToString();
                    return retVal;
                }

                if (string.IsNullOrEmpty(argTestString))
                {
                    LogError("StartsWith: TestString cannot be null or empty");
                    retVal = StuBoolean.False.ToString();
                    return retVal;
                }

                var stringComparison = StringComparison.CurrentCulture;

                switch (argStringComparison)
                {
                    case "CS":
                        stringComparison = StringComparison.CurrentCulture;
                        break;
                    case "CI":
                        stringComparison = StringComparison.CurrentCultureIgnoreCase;
                        break;
                    case "":
                        stringComparison = StringComparison.CurrentCulture;
                        break;
                    default:
                        LogError($"StartsWith: invalid value for Case Sensitivity (CS or CI)");
                        retVal = StuBoolean.False.ToString();
                        return retVal;
                }

                var isStartingWith = argSource.StartsWith(argTestString, stringComparison);
                retVal = isStartingWith ? StuBoolean.True.ToString() : StuBoolean.False.ToString();

                return retVal;
            }
            catch (Exception ex)
            {
                LogError($"StartsWith: Exception {ex.Message} ");
                retVal = StuBoolean.False.ToString();
                return retVal;
            }
            finally
            {
                LogInfo($"StartsWith: Finally retVal {retVal} ");
            }
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
            string retVal = null;

            try
            {
                const string RegExp = @"""(?<Source>[^""]*)""\s+""(?<TotalWidth>[^""]*)""(\s+""(?<PaddingChar>[^""]*)"")?";
                var match = Regex.Match(arg, RegExp);

                if (!match.Success)
                {
                    retVal = null;
                    return retVal;
                }

                var argSource = match.Groups["Source"].Value;
                var argTotalWidth = match.Groups["TotalWidth"].Value;
                var argPaddingChar = match.Groups["PaddingChar"].Value;

                if (string.IsNullOrEmpty(argSource))
                {
                    argSource = string.Empty;
                }

                if (!int.TryParse(argTotalWidth, out var totalWidth))
                {
                    LogError("PadLeft: totalWidth must be an int");
                    retVal = null;
                    return retVal;
                }

                retVal = argPaddingChar.Length == 0
                             ? argSource.PadLeft(totalWidth)
                             : argSource.PadLeft(totalWidth, argPaddingChar[0]);

                return retVal;
            }
            catch (Exception ex)
            {
                LogError($"PadLeft: Exception {ex.Message} ");
                retVal = null;
                return retVal;
            }
            finally
            {
                LogInfo($"PadLeft: Finally retVal {retVal ?? "null"} ");
            }
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
            string retVal = null; 

            try
            {
                const string RegExp = @"""(?<Source>[^""]*)""\s+""(?<TotalWidth>[^""]*)""(\s+""(?<PaddingChar>[^""]*)"")?";
                var match = Regex.Match(arg, RegExp);

                if (!match.Success)
                {
                    retVal = null;
                    return retVal;
                }

                var argSource = match.Groups["Source"].Value;
                var argTotalWidth = match.Groups["TotalWidth"].Value;
                var argPaddingChar = match.Groups["PaddingChar"].Value;

                if (string.IsNullOrEmpty(argSource))
                {
                    argSource = string.Empty;
                }

                if (!int.TryParse(argTotalWidth, out var totalWidth))
                {
                    LogError("PadRight: totalWidth must be an int");
                    retVal = null;
                    return retVal;
                }

                retVal = argPaddingChar.Length == 0
                                 ? argSource.PadRight(totalWidth)
                                 : argSource.PadRight(totalWidth, argPaddingChar[0]);

                return retVal;
            }
            catch (Exception ex)
            {
                LogError($"PadRight: Exception {ex.Message} ");
                retVal = null;
                return retVal;
            }
            finally
            {
                LogInfo($"PadRight: Finally retVal {retVal ?? "null"} ");
            }
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
            // "ToLower" "Bo 12 ?!asoB" "bo 12 ?!asob"
            // In a config.txt looks like:
            //     "{STRING "ToLower" "Bo 12 ?!asoB"}
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
            // "ToUpper" "Bo 12 ?!asoB" "Bo 12 ?!ASOB"
            // In a config.txt looks like:
            //     "{STRING "ToUpper" "Bo 12 ?!asoB"}
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

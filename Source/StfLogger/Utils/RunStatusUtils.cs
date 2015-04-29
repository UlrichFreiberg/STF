// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RunStatusUtils.cs" company="Foobar">
//   2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
namespace Stf.Utilities.Utils
{
    /// <summary>
    /// The run status utils.
    /// </summary>
    public class RunStatusUtils
    {
        /// <summary>
        /// The get logline stat regexp.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetLoglineStatRegexp()
        {
            // One runstats logline looks like:
            // <div class="line runstats" passed="0" failed="0" Errors="0" Warnings="0">
            const string Regexp =
                "div class=\"line runstats\" passed=\"(?<pass>[0-9]+)\" failed=\"(?<fail>[0-9]+)\" Errors=\"(?<error>[0-9]+)\" Warnings=\"(?<warning>[0-9]+)\"";

            return Regexp;
        }

        public static Dictionary<StfLogLevel, int> GetRunStatus(string logFilename)
        {
            var retVal = new Dictionary<StfLogLevel, int>();
            var everything = File.ReadAllText(logFilename, Encoding.UTF8);
            var matches = Regex.Matches(
                                    everything,
                                    GetLoglineStatRegexp(),
                                    RegexOptions.IgnoreCase | RegexOptions.Singleline);

            retVal.Add(StfLogLevel.Pass, 0);
            retVal.Add(StfLogLevel.Fail, 0);
            retVal.Add(StfLogLevel.Error, 0);
            retVal.Add(StfLogLevel.Warning, 0);

            if (matches.Count > 0)
            {
                retVal[StfLogLevel.Pass] = int.Parse(matches[0].Groups["pass"].Value);
                retVal[StfLogLevel.Fail] = int.Parse(matches[0].Groups["fail"].Value);
                retVal[StfLogLevel.Error] = int.Parse(matches[0].Groups["error"].Value);
                retVal[StfLogLevel.Warning] = int.Parse(matches[0].Groups["warning"].Value);
            }

            return retVal;
        }
    }
}

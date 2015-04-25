// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SummeryLogger.cs" company="Foobar">
//   2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Stf.Utilities
{
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    using Stf.Utilities.Properties;
    using Stf.Utilities.Utils;

    /// <summary>
    /// The summery logger.
    /// </summary>
    public class StfSummeryLogger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StfSummeryLogger"/> class.
        /// </summary>
        /// <param name="nameOfSummeryfile">
        /// The name of summeryfile.
        /// </param>
        /// <param name="logDir">
        /// The log dir.
        /// </param>
        /// <param name="filePattern">
        /// The file pattern.
        /// </param>
        public StfSummeryLogger(string nameOfSummeryfile, string logDir, string filePattern)
        {
            CreateSummeryLog(nameOfSummeryfile, logDir, filePattern);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StfSummeryLogger"/> class.
        /// </summary>
        public StfSummeryLogger()
        {
        }

        /// <summary>
        /// The create summery log.
        /// </summary>
        /// <param name="nameOfSummeryfile">
        /// The name of summeryfile.
        /// </param>
        /// <param name="logDir">
        /// The log dir.
        /// </param>
        /// <param name="filePattern">
        /// The file pattern.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool CreateSummeryLog(string nameOfSummeryfile, string logDir, string filePattern)
        {
            var summaryLogFile = new LogfileWriter { LogFileName = nameOfSummeryfile, OverwriteLogFile = true };
            var loglineStatRegexp = GetLoglineStatRegexp();

            summaryLogFile.Open();
            summaryLogFile.Write(GetTextReource("SummaryLoggerHeader"));

            foreach (var logfile in Directory.GetFiles(logDir, filePattern))
            {
                var everything = File.ReadAllText(logfile, Encoding.UTF8);
                var matches = Regex.Matches(
                                        everything,
                                        loglineStatRegexp,
                                        RegexOptions.IgnoreCase | RegexOptions.Singleline);
                var tableFormatString = this.GetTableFormatString();
                string tableRowId;

                foreach (Match match in matches)
                {
                    tableRowId = GetTableRowId(match);
                    var tableRow = string.Format(
                        tableFormatString,
                        tableRowId,
                        logfile,
                        Path.GetFileName(logfile),
                        match.Groups["pass"].Value,
                        match.Groups["fail"].Value,
                        match.Groups["error"].Value,
                        match.Groups["warning"].Value);
                    summaryLogFile.Write(tableRow);
                }
            }

            summaryLogFile.Write(GetTextReource("SummaryLoggerFooter"));
            return summaryLogFile.Close();
        }

        /// <summary>
        /// reads in the HTML that constitutes the top LogHeader
        /// </summary>
        /// <param name="resourceName">
        /// The Resource Name.
        /// </param>
        /// <returns>
        /// A Html string representing the start of the Body for the logger
        /// </returns>
        private string GetTextReource(string resourceName)
        {
            string retVal;
            var resourceObject = Resources.ResourceManager.GetObject(resourceName);

            if (resourceObject == null)
            {
                retVal = string.Format("<error>No {0} section file found</error>", resourceName);
            }
            else
            {
                retVal = resourceObject.ToString();
            }

            return retVal;
        }

        /// <summary>
        /// The get table format string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetTableFormatString()
        {
            int index = 0;
            var retVal = "<tr id=\"{" + index++ + "}\">\n";

            retVal += "  <td>\n";
            retVal += "    <a href=\"{" + index++ + "}\">{" + index++ + "}</a>\n";
            retVal += "  </td>\n";
            for (var i = 2; i < 6; i++)
            {
                retVal += "  <td>{" + index++ + "}</td>\n";
            }

            retVal += "</tr>\n";

            return retVal;
        }

        /// <summary>
        /// The get logline stat regexp.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetLoglineStatRegexp()
        {
            // One runstats logline looks like:
            // <div class="line runstats" passed="0" failed="0" Errors="0" Warnings="0">
            const string Regexp =
                "div class=\"line runstats\" passed=\"(?<pass>[0-9]+)\" failed=\"(?<fail>[0-9]+)\" Errors=\"(?<error>[0-9]+)\" Warnings=\"(?<warning>[0-9]+)\"";

            return Regexp;
        }

        private string GetTableRowId(Match match)
        {
            if (int.Parse(match.Groups["fail"].Value) > 0)
            {
                return "testresultfail";
            }

            if (int.Parse(match.Groups["error"].Value) > 0)
            {
                return "testresulterror";
            }

            if (int.Parse(match.Groups["warning"].Value) > 0)
            {
                return "testresultwarning";
            }

            if (int.Parse(match.Groups["pass"].Value) > 0)
            {
                return "testresultpass";
            }

            return "not";
        }
    }
}

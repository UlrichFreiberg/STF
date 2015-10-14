// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfSummeryLogger.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using Mir.Stf.Utilities.Properties;
using Mir.Stf.Utilities.Utils;

namespace Mir.Stf.Utilities
{
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

            if (!InitializeSummeryLogFile(nameOfSummeryfile, summaryLogFile))
            {
                return false;
            }

            foreach (var logFilename in Directory.GetFiles(logDir, filePattern))
            {
                var runStatus = RunStatusUtils.GetRunStatus(logFilename);
                var tableFormatString = this.GetTableFormatString();
                var tableRowId = GetTableRowId(runStatus);

                var tableRow = string.Format(
                    tableFormatString,
                    tableRowId,
                    logFilename,
                    Path.GetFileName(logFilename),
                    runStatus[StfLogLevel.Pass],
                    runStatus[StfLogLevel.Fail],
                    runStatus[StfLogLevel.Error],
                    runStatus[StfLogLevel.Warning]);

                summaryLogFile.Write(tableRow);
            }

            summaryLogFile.Write(this.GetTextResource("SummaryLoggerFooter"));
            return summaryLogFile.Close();
        }

        /// <summary>
        /// The initialize summery log file.
        /// </summary>
        /// <param name="nameOfSummeryfile">
        /// The name of summeryfile.
        /// </param>
        /// <param name="summaryLogFile">
        /// The summary log file.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool InitializeSummeryLogFile(string nameOfSummeryfile, LogfileWriter summaryLogFile)
        {
            var logHeader = this.GetTextResource("SummaryLoggerHeader");

            if (!summaryLogFile.Open())
            {
                return false;
            }

            var logfileTitle = string.Format("SummaryLogger for {0}", Path.GetFileNameWithoutExtension(nameOfSummeryfile));
            logHeader = logHeader.Replace("LOGFILETITLE", logfileTitle);
            summaryLogFile.Write(logHeader);

            return true;
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
        private string GetTextResource(string resourceName)
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
        /// The get table row id.
        /// </summary>
        /// <param name="runResultStatus">
        /// A collection of number of entries for each StfLogLevels
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetTableRowId(Dictionary<StfLogLevel, int> runResultStatus)
        {
            if (runResultStatus[StfLogLevel.Fail] > 0)
            {
                return "testresultfail";
            }

            if (runResultStatus[StfLogLevel.Error] > 0)
            {
                return "testresulterror";
            }

            if (runResultStatus[StfLogLevel.Warning] > 0)
            {
                return "testresultwarning";
            }

            if (runResultStatus[StfLogLevel.Pass] > 0)
            {
                return "testresultpass";
            }

            return "testresultUnknown";
        }
    }
}

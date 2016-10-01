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
    using System;
    using System.Collections.Specialized;
    using System.Text.RegularExpressions;

    /// <summary>
    /// The summery logger.
    /// </summary>
    public class StfSummeryLogger
    {
        /// <summary>
        /// The summary log file writer.
        /// </summary>
        private LogfileWriter summaryLogFile;

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
            var logFiles = Directory.GetFiles(logDir, filePattern);

            summaryLogFile = new LogfileWriter { LogFileName = nameOfSummeryfile, OverwriteLogFile = true };

            if (logFiles.Length == 0)
            {
                return true;
            }

            // use the first logfile, to figure out how to setup the header for datadriven parameters
            var dataDrivenParameters = GetDataDrivenParameter(logFiles[0]);

            if (!OpenSummeryLogFile(nameOfSummeryfile, dataDrivenParameters))
            {
                return false;
            }

            // we use the value for the first logfile, to control the column for the rest of the logfiles.
            // In that way we dont need to set it for all data rows
            var iterationDescriptionColumnToUse = GetIterationDescriptionColumn(dataDrivenParameters);
            foreach (var logfile in logFiles)
            {
                var runStatus = RunStatusUtils.GetRunStatus(logfile);

                dataDrivenParameters = GetDataDrivenParameter(logfile);

                var iterationDescription = GetIterationDescription(iterationDescriptionColumnToUse, logfile, dataDrivenParameters);

                LogSummeryForOneLogfile(logfile, runStatus, dataDrivenParameters, iterationDescription);
            }

            return CloseSummeryLogFile();
        }

        /// <summary>
        /// The get iteration description.
        /// </summary>
        /// <param name="columnToUse">
        /// The column to use.
        /// </param>
        /// <param name="logfile">
        /// The logfile.
        /// </param>
        /// <param name="dataDrivenParameters">
        /// The data driven parameters.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetIterationDescription(string columnToUse, string logfile, OrderedDictionary dataDrivenParameters)
        {
            string columnValue;
            string retVal;

            // are we using a column redirect?
            if (!string.IsNullOrEmpty(columnToUse))
            {
                columnValue = dataDrivenParameters[columnToUse].ToString();
                retVal = !string.IsNullOrEmpty(columnValue) ? columnValue : Path.GetFileName(logfile);

                return retVal;
            }

            if (!dataDrivenParameters.Contains("StfIterationDescription"))
            {
                return Path.GetFileName(logfile);
            }

            columnValue = dataDrivenParameters["StfIterationDescription"].ToString();
            retVal = !string.IsNullOrEmpty(columnValue) ? columnValue : Path.GetFileName(logfile);

            return retVal;
        }

        /// <summary>
        /// The get iteration description column.
        /// </summary>
        /// <param name="dataDrivenParameters">
        /// The data driven parameters.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetIterationDescriptionColumn(OrderedDictionary dataDrivenParameters)
        {
            if (dataDrivenParameters == null || !dataDrivenParameters.Contains("StfIterationDescription"))
            {
                return null;
            }

            var column = dataDrivenParameters["StfIterationDescription"].ToString();

            // if the column contains something that does not exists as an column, then it isn't a column redirect
            var retVal = dataDrivenParameters.Contains(column) ? column : null;

            return retVal;
        }

        /// <summary>
        /// The close summery log file.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool CloseSummeryLogFile()
        {
            summaryLogFile.Write(GetTextResource("SummaryLoggerFooter"));
            return summaryLogFile.Close();
        }

        /// <summary>
        /// The log summery for one logfile.
        /// </summary>
        /// <param name="logfile">
        /// The logfile.
        /// </param>
        /// <param name="runStatus">
        /// The run status.
        /// </param>
        /// <param name="dataDrivenParameters">
        /// The data driven parameters.
        /// </param>
        /// <param name="iterationDescription">
        /// The iteration Description.
        /// </param>
        private void LogSummeryForOneLogfile(
                        string logfile,
                        Dictionary<StfLogLevel, int> runStatus,
                        OrderedDictionary dataDrivenParameters,
                        string iterationDescription = null)
        {
            var tableRowTestResult = GetTableRowTestResult(runStatus);
            var tableFormatString = GetTableFormatString(dataDrivenParameters);
            var logFileName = Path.GetFileName(logfile);
            var tableRow = string.Format(
                tableFormatString,
                tableRowTestResult,
                iterationDescription ?? logFileName,
                logFileName,
                logFileName,
                runStatus[StfLogLevel.Pass],
                runStatus[StfLogLevel.Fail],
                runStatus[StfLogLevel.Error],
                runStatus[StfLogLevel.Warning]);

            summaryLogFile.Write(tableRow);
        }

        /// <summary>
        /// The get data driven parameter.
        /// </summary>
        /// <param name="logFilename">
        /// The log filename.
        /// </param>
        /// <returns>
        /// The <see cref="OrderedDictionary"/>.
        /// </returns>
        private OrderedDictionary GetDataDrivenParameter(string logFilename)
        {
            var retVal = new OrderedDictionary();
            var content = File.ReadAllText(logFilename);

            // <div class="el msg">Column[Message]=[Third and last iteration of datadriven test]</div>
            var parameterRegexp = "<div class=\"el msg\">Column[[](?<Key>[^]]*)[]]=[[](?<Value>[^]]*)";
            var match = Regex.Match(content, parameterRegexp, RegexOptions.Multiline);

            while (match.Success)
            {
                var key = match.Groups["Key"].Value;
                var value = match.Groups["Value"].Value;

                retVal.Add(key, value);
                match = match.NextMatch();
            }

            return retVal;
        }

        /// <summary>
        /// The initialize summery log file.
        /// </summary>
        /// <param name="nameOfSummeryfile">
        /// The name of summeryfile.
        /// </param>
        /// <param name="dataDrivenParameters">
        /// The data Driven Parameters.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool OpenSummeryLogFile(string nameOfSummeryfile, OrderedDictionary dataDrivenParameters)
        {
            var logHeader = GetTextResource("SummaryLoggerHeader");

            if (!summaryLogFile.Open())
            {
                return false;
            }

            var logfileTitle = string.Format("SummaryLogger for {0}", Path.GetFileNameWithoutExtension(nameOfSummeryfile));
            logHeader = logHeader.Replace("LOGFILETITLE", logfileTitle);

            var headers = string.Empty;
            var dataDrivenParametersKeys = new string[dataDrivenParameters.Count];

            dataDrivenParameters.Keys.CopyTo(dataDrivenParametersKeys, 0);

            for (var i = 0; i < dataDrivenParameters.Count; i++)
            {
                headers += string.Format("<th>{0}</th>{1}", dataDrivenParametersKeys[i], Environment.NewLine);
            }

            logHeader = logHeader.Replace("DATADRIVENPARAMETERS", headers);
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
            var resourceObject = Resources.ResourceManager.GetObject(resourceName);

            var retVal = resourceObject == null
                ? string.Format("<error>No {0} section file found</error>", resourceName)
                : resourceObject.ToString();

            return retVal;
        }

        /// <summary>
        /// The get table format string.
        /// </summary>
        /// <param name="dataDrivenParameters">
        /// The data Driven Parameters.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetTableFormatString(OrderedDictionary dataDrivenParameters)
        {
            var index = 0;
            var retVal = "<tr id=\"{" + index++ + "}\" description=\"{" + index++ + "}\">" + Environment.NewLine;

            retVal += "  <td>" + Environment.NewLine;
            retVal += "    <a href=\"{" + index++ + "}\">{" + index++ + "}</a>" + Environment.NewLine;
            retVal += "  </td>" + Environment.NewLine;
            for (var i = 2; i < 6; i++)
            {
                retVal += "  <td>{" + index++ + "}</td>" + Environment.NewLine;
            }

            var dataDrivenParametersValues = new string[dataDrivenParameters.Count];

            dataDrivenParameters.Values.CopyTo(dataDrivenParametersValues, 0);
            for (var i = 0; i < dataDrivenParameters.Count; i++)
            {
                var value = dataDrivenParametersValues[i];

                if (!string.IsNullOrEmpty(value))
                {
                    value = value.Replace("{", "{{").Replace("}", "}}");
                }

                retVal += string.Format("<td>{0}</td>{1}", value, Environment.NewLine);
            }

            retVal += string.Format("</tr>{0}", Environment.NewLine);

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
        private string GetTableRowTestResult(Dictionary<StfLogLevel, int> runResultStatus)
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

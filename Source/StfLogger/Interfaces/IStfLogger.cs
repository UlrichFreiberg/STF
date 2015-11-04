// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStfLogger.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Mir.Stf.Utilities.Interfaces
{
    /// <summary>
    /// An interface for the StfLogger
    /// </summary>
    public interface IStfLogger : IStfLoggerAutomationIdentification,
                                  IStfLoggerCallStackManagement,
                                  IStfLoggerLogfileManagement,
                                  IStfLoggerLoggingFunctions,
                                  IStfLoggerPerformanceManagement,
                                  IStfLoggerScreenshots,
                                  IStfLoggerTestScriptHeaders
    {
        /// <summary>
        /// Gets the number of loglevel messages.
        /// </summary>
        Dictionary<StfLogLevel, int> NumberOfLoglevelMessages { get; }

        /// <summary>
        /// Gets the configuration for the logfile.
        /// </summary>
        LogConfiguration Configuration { get; }

        /// <summary>
        /// Gets Details about Environment, Test Agent (the current machine - OS, versions of Software), Date
        /// </summary>
        Dictionary<string, string> LogInfoDetails { get; }

        /// <summary>
        /// Gets or sets the current log level.
        /// </summary>
        int CurrentLogLevel { get; set; }

        /// <summary>
        /// Gets or sets the build archive log file path.
        /// </summary>
        int BuildArchiveLogFilePath { get; set; }

        /// <summary>
        /// Gets or sets the Title used in the header of the logfile
        /// </summary>
        string LogTitle { get; set; }

        /// <summary>
        /// Gets or sets the path to the resulting logfile
        /// </summary>
        string FileName { get; set; }

        /// <summary>
        /// Gets or sets the log level.
        /// </summary>
        StfLogLevel LogLevel { get; set; }
    }
}

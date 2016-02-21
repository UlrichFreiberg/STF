// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStfLogger.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Mir.Stf.Utilities.Interfaces
{
    using Mir.Stf.Utilities.Configuration;

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
        /// Gets or sets the configuration.
        /// </summary>
        IStfLoggerConfiguration Configuration { get; set; }

        /// <summary>
        /// Gets the number of loglevel messages.
        /// </summary>
        Dictionary<StfLogLevel, int> NumberOfLoglevelMessages { get; }

        /// <summary>
        /// Gets Details about Environment, Test Agent (the current machine - OS, versions of Software), Date
        /// </summary>
        Dictionary<string, string> LogInfoDetails { get; }

        /// <summary>
        /// Gets or sets the current log level.
        /// </summary>
        int CurrentLogLevel { get; set; }

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

﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStfLoggerConfiguration.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.Configuration
{
    /// <summary>
    /// Interface for the StfLogger configuration
    /// </summary>
    public interface IStfLoggerConfiguration
    {
        /// <summary>
        /// Gets or sets a value indicating whether overwrite log file.
        /// </summary>
        bool OverwriteLogFile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether log to file.
        /// </summary>
        bool LogToFile { get; set; }

        /// <summary>
        /// Gets or sets the log title.
        /// </summary>
        string LogTitle { get; set; }

        /// <summary>
        /// Gets or sets the log file name.
        /// </summary>
        string LogFileName { get; set; }

        /// <summary>
        /// Gets or sets the alert interval. How many seconds is acceptable between two log entries.
        /// </summary>
        int AlertLongInterval { get; set; }

        /// <summary>
        /// Gets or sets the log level.
        /// </summary>
        StfLogLevel LogLevel { get; set; }

        /// <summary>
        /// Gets or sets the path to logo image file.
        /// </summary>
        string PathToLogoImageFile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to log a screenshot when calling log fail.
        /// </summary>
        bool ScreenshotOnLogFail { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether log entries with newlines will be mapped to html tag BR 
        /// </summary>
        bool MapNewlinesToBr { get; set; }

        /// <summary>
        /// Gets or sets the keep alive interval.
        /// </summary>
        int KeepAliveInterval { get; set; }
    }
}

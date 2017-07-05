// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebAdapterDriverLogging.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the WebAdapter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WrapTrack.Stf.Adapters.WebAdapter
{
    using System;
    using System.IO;

    using OpenQA.Selenium.IE;

    /// <summary>
    /// The web adapter.
    /// </summary>
    public partial class WebAdapter
    {
        /// <summary>
        /// The driver log file path.
        /// </summary>
        private const string DefaultLogPath = @"C:\Temp\Stf\Logs\WebDriverLogs";

        /// <summary>
        /// Gets or sets the driver log file.
        /// </summary>
        private string DriverLogFile { get; set; }

        /// <summary>
        /// The log unsafe driver settings.
        /// </summary>
        /// <param name="driverOptions">
        /// The driver options.
        /// </param>
        private void LogUnsafeDriverSettings(InternetExplorerOptions driverOptions)
        {
            if (driverOptions.IntroduceInstabilityByIgnoringProtectedModeSettings)
            {
                StfLogger.LogWarning("Driver is configured to ignore protected mode settings. This could cause instabillity!");
            }
        }

        /// <summary>
        /// The get driver log file path.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetDriverLogFilePath()
        {
            if (!CheckPath(DefaultLogPath))
            {
                return string.Empty;
            }

            var retVal = string.Format(@"{0}\Ie_{1}.log", DefaultLogPath, DateTime.Now.ToString("ddMMyyyy_hhmmssff"));

            if (File.Exists(retVal))
            {
                StfLogger.LogDebug("IE log file already exists! File path: [{0}]", retVal);
            }

            return retVal;
        }

        /// <summary>
        /// The get driver logging level.
        /// </summary>
        /// <returns>
        /// The <see cref="InternetExplorerDriverLogLevel"/>.
        /// </returns>
        private InternetExplorerDriverLogLevel GetDriverLoggingLevel()
        {
            InternetExplorerDriverLogLevel logLevel;

            if (Enum.TryParse(Configuration.DriverLogLevel, true, out logLevel))
            {
                return logLevel;
            }

            StfLogger.LogDebug("Unable to parse loglevel for IE driver. Tried parsing: [{0}]", Configuration.DriverLogLevel);

            return InternetExplorerDriverLogLevel.Trace;
        }
   }
}
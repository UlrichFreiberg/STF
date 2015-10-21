﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogConfiguration.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Configuration;
using System.Globalization;
using System.Linq;

namespace Mir.Stf.Utilities
{
    /// <summary>
    /// The log configuration.
    /// </summary>
    public class LogConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogConfiguration"/> class. 
        /// TODO:Later stfConfiguration kicks in..
        /// </summary>
        public LogConfiguration()
        {
            OverwriteLogFile = Settings.Setting("OverwriteLogFile", true);
            LogToFile = Settings.Setting("LogToFile", false);
            LogTitle = Settings.Setting("LogTitle", "Ovid LogTitle");
            LogFileName = Settings.Setting("LogFileName", @"c:\temp\Ovid_defaultlog.html");
            AlertLongInterval = Settings.Setting("AlertLongInterval", 30000);
            PathToLogoImageFile = Settings.Setting<string>("PathToLogoImageFile", null);
            ScreenshotOnLogFail = Settings.Setting("ScreenshotOnLogFail", true);
            MapNewlinesToBr = Settings.Setting("MapNewlinesToBr", true);

            const StfLogLevel DefaultLoglevel = StfLogLevel.Internal;
            var logLevelString = Settings.Setting("LogLevel", DefaultLoglevel.ToString());
            var convertedLoglevel = StringToLogLevel(logLevelString);

            if (convertedLoglevel == null)
            {
                this.LogLevel = DefaultLoglevel;
            }
            else
            {
                this.LogLevel = (StfLogLevel)convertedLoglevel;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether overwrite log file.
        /// </summary>
        public bool OverwriteLogFile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether log to file.
        /// </summary>
        public bool LogToFile { get; set; }

        /// <summary>
        /// Gets or sets the log title.
        /// </summary>
        public string LogTitle { get; set; }

        /// <summary>
        /// Gets or sets the log file name.
        /// </summary>
        public string LogFileName { get; set; }

        /// <summary>
        /// Gets or sets the alert interval. How many seconds is acceptable between two log entries.
        /// </summary>
        public int AlertLongInterval { get; set; }

        /// <summary>
        /// Gets or sets the log level.
        /// </summary>
        public StfLogLevel LogLevel { get; set; }

        /// <summary>
        /// Gets or sets the path to logo image file.
        /// </summary>
        public string PathToLogoImageFile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to log a screenshot when calling log fail.
        /// </summary>
        public bool ScreenshotOnLogFail { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether log entries with newlines will be mapped to html tag BR 
        /// </summary>
        public bool MapNewlinesToBr { get; set; }

        /// <summary>
        /// The string to log level.
        /// </summary>
        /// <param name="loglevelString">
        /// The loglevel string.
        /// </param>
        /// <returns>
        /// The <see cref="LogLevel"/>.
        /// </returns>
        internal StfLogLevel? StringToLogLevel(string loglevelString)
        {
            StfLogLevel? retVal = null;

            if (string.IsNullOrEmpty(loglevelString))
            {
                return null;
            }

            // make sure the first letter is upper case and the rest is lower case
            loglevelString = loglevelString.First().ToString().ToUpper() + loglevelString.Substring(1).ToLower();

            try
            {
                var logLevelValue = (StfLogLevel)Enum.Parse(typeof(StfLogLevel), loglevelString);
                if (Enum.IsDefined(typeof(StfLogLevel), logLevelValue) | logLevelValue.ToString().Contains(","))
                {
                    retVal = logLevelValue;
                }
                else
                {
                    Console.WriteLine(@"{0} is not an underlying value of the LogLevel enumeration.", loglevelString);
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine(@"{0} is not an underlying value of the LogLevel enumeration.", loglevelString);
            }

            return retVal;
        }

        /// <summary>
        /// The settings.
        /// </summary>
        private static class Settings
        {
            /// <summary>
            /// The configuration file for <c>this</c> <c>assemnbly</c>.
            /// </summary>
            private static readonly Configuration MyDllConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            /// <summary>
            /// The application settings.
            /// </summary>
            private static readonly AppSettingsSection AppSettings = (AppSettingsSection)MyDllConfig.GetSection("StfConfiguration");

            /// <summary>
            /// The nfi.
            /// </summary>
            private static readonly NumberFormatInfo Nfi = new NumberFormatInfo 
            {
                NumberGroupSeparator = string.Empty, 
                CurrencyDecimalSeparator = "."
            };

            /// <summary>
            /// The setting.
            /// </summary>
            /// <typeparam name="T">
            /// For now only Strings are supported
            /// </typeparam>
            /// <param name="name">
            /// The name of the configuration file entry.
            /// </param>
            /// <param name="defaultValue">
            /// The default Value.
            /// </param>
            /// <returns>
            /// The <see cref="T"/>.
            /// </returns>
            public static T Setting<T>(string name, T defaultValue)
            {
                T retVal;

                if (AppSettings != null && AppSettings.Settings.AllKeys.Contains(name))
                {
                    retVal = (T)Convert.ChangeType(AppSettings.Settings[name].Value, typeof(T), Nfi);
                }
                else
                {
                    retVal = defaultValue;
                }

                return retVal;
            }
        }
    }
}

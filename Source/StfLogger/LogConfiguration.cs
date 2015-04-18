// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogConfiguration.cs" company="Foobar">
//   2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Stf.Utilities
{
    using System;
    using System.Configuration;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// The log configuration.
    /// </summary>
    public class LogConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogConfiguration"/> class. 
        /// TODO:Later YACF kicks in..
        /// </summary>
        public LogConfiguration()
        {
            OverwriteLogFile = Settings.Setting("OverwriteLogFile", true);
            LogToFile = Settings.Setting("LogToFile", false);
            LogTitle = Settings.Setting("LogTitle", "Ovid LogTitle");
            LogFileName = Settings.Setting("LogFileName", @"c:\temp\Ovid_defaultlog.html");
            AlertLongInterval = Settings.Setting("AlertLongInterval", 30000);
            PathToLogoImageFile = Settings.Setting<string>("PathToLogoImageFile", null);

            const StfLogLevel DefaultLoglevel = StfLogLevel.Info;
            var logLevelString = Settings.Setting("StfLogLevel", DefaultLoglevel.ToString());
            var convertedLoglevel = StringToLogLevel(logLevelString);

            if (convertedLoglevel == null)
            {
                this.StfLogLevel = DefaultLoglevel;
            }
            else
            {
                this.StfLogLevel = (StfLogLevel)convertedLoglevel;
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
        public StfLogLevel StfLogLevel { get; set; }

        /// <summary>
        /// Gets or sets the path to logo image file.
        /// </summary>
        public string PathToLogoImageFile { get; set; }

        /// <summary>
        /// The string to log level.
        /// </summary>
        /// <param name="loglevelString">
        /// The loglevel string.
        /// </param>
        /// <returns>
        /// The <see cref="StfLogLevel"/>.
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
                    Console.WriteLine(@"{0} is not an underlying value of the StfLogLevel enumeration.", loglevelString);
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine(@"{0} is not an underlying value of the StfLogLevel enumeration.", loglevelString);
            }

            return retVal;
        }

        /// <summary>
        /// The settings.
        /// </summary>
        private static class Settings
        {
            /// <summary>
            /// The path to the assembly.
            /// </summary>
            private static readonly UriBuilder Uri = new UriBuilder(Assembly.GetExecutingAssembly().CodeBase);

            /// <summary>
            /// The configuration file for <c>this</c> <c>assemnbly</c>.
            /// </summary>
            private static readonly Configuration MyDllConfig = ConfigurationManager.OpenExeConfiguration(Uri.Path);

            /// <summary>
            /// The application settings.
            /// </summary>
            private static readonly AppSettingsSection AppSettings = (AppSettingsSection)MyDllConfig.GetSection("appSettings");

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

                if (AppSettings.Settings.AllKeys.Contains(name))
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

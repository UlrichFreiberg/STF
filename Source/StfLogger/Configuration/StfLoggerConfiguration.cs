// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfLoggerConfiguration.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.Configuration
{
    using System.ComponentModel;
    using System.Reflection;

    /// <summary>
    /// The stf logger configuration object
    /// </summary>
    public class StfLoggerConfiguration : IStfLoggerConfiguration
    {
        /// <summary>
        /// Gets or sets a value indicating whether overwrite log file.
        /// </summary>
        [StfConfigurationAttribute("Configuration.StfKernel.StfLogger.OverwriteLogFile", DefaultValue = "false")]
        public bool OverwriteLogFile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether log to file.
        /// </summary>
        [StfConfigurationAttribute("Configuration.StfKernel.StfLogger.LogToFile", DefaultValue = "true")]
        public bool LogToFile { get; set; }

        /// <summary>
        /// Gets or sets the log title.
        /// </summary>
        [StfConfigurationAttribute("Configuration.StfKernel.StfLogger.LogTitle", DefaultValue = "LogTitle")]
        public string LogTitle { get; set; }

        /// <summary>
        /// Gets or sets the log file name.
        /// </summary>
        [StfConfigurationAttribute("Configuration.StfKernel.StfLogger.LogFileName", DefaultValue = @"c:\temp\Ovid_defaultlog.html")]
        public string LogFileName { get; set; }

        /// <summary>
        /// Gets or sets the alert long interval.
        /// </summary>
        [StfConfigurationAttribute("Configuration.StfKernel.StfLogger.AlertLongInterval", DefaultValue = "30000")]
        public int AlertLongInterval { get; set; }

        /// <summary>
        /// Gets or sets the log level.
        /// </summary>
        [StfConfigurationAttribute("Configuration.StfKernel.StfLogger.LogLevel", DefaultValue = "Info")]
        public StfLogLevel LogLevel { get; set; }

        /// <summary>
        /// Gets or sets the path to logo image file.
        /// </summary>
        [StfConfigurationAttribute("Configuration.StfKernel.StfLogger.PathToLogoImageFile")]
        public string PathToLogoImageFile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether screenshot on log fail.
        /// </summary>
        [StfConfigurationAttribute("Configuration.StfKernel.StfLogger.ScreenshotOnLogFail", DefaultValue = "true")]
        public bool ScreenshotOnLogFail { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether map newlines to br.
        /// </summary>
        [StfConfigurationAttribute("Configuration.StfKernel.StfLogger.MapNewlinesToBr", DefaultValue = "false")]
        public bool MapNewlinesToBr { get; set; }

        /// <summary>
        /// Gets or sets the keep alive interval.
        /// </summary>
        [StfConfigurationAttribute("Configuration.StfKernel.StfLogger.KeepAliveInterval", DefaultValue = "5")]
        public int KeepAliveInterval { get; set; }

        public StfLoggerConfiguration()
        {
            this.InitializeWithDefaultValues();
        }

        internal void InitializeWithDefaultValues()
        {
            var properties = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var property in properties)
            {
                var propertyDefaultValue = this.GetDefaultValue(property.Name);

                if (property.PropertyType == typeof(string))
                {
                    property.SetValue(this, propertyDefaultValue);
                    continue;
                }

                // TODO: should be in tryCatch
                var castedNewValue = TypeDescriptor.GetConverter(property.PropertyType).ConvertFromInvariantString(propertyDefaultValue);
                property.SetValue(this, castedNewValue);
            }
        }
    }
}
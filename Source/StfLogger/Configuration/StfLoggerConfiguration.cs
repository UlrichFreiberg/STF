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
        /// Initializes a new instance of the <see cref="StfLoggerConfiguration"/> class.
        /// </summary>
        public StfLoggerConfiguration()
        {
            InitializeWithDefaultValues();
        }

        /// <summary>
        /// Gets or sets a value indicating whether overwrite log file.
        /// </summary>
        [StfConfigurationAttribute("Configuration.StfKernel.StfLogger.OverwriteLogFile", DefaultValue = "true")]
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
        [StfConfigurationAttribute("Configuration.StfKernel.StfLogger.LogFileName", DefaultValue = @"C:\Temp\Stf\KernelLogger.html")]
        public string LogFileName { get; set; }

        /// <summary>
        /// Gets or sets the alert long interval.
        /// </summary>
        [StfConfigurationAttribute("Configuration.StfKernel.StfLogger.AlertLongInterval", DefaultValue = "30000")]
        public int AlertLongInterval { get; set; }

        /// <summary>
        /// Gets or sets the log level.
        /// </summary>
        [StfConfigurationAttribute("Configuration.StfKernel.StfLogger.LogLevel", DefaultValue = "Internal")]
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
        [StfConfigurationAttribute("Configuration.StfKernel.StfLogger.MapNewlinesToBr", DefaultValue = "true")]
        public bool MapNewlinesToBr { get; set; }

        /// <summary>
        /// Gets or sets the keep alive interval.
        /// </summary>
        [StfConfigurationAttribute("Configuration.StfKernel.StfLogger.KeepAliveInterval", DefaultValue = "5")]
        public int KeepAliveInterval { get; set; }

        /// <summary>
        /// Gets or sets the body background color.
        /// </summary>
        [StfConfigurationAttribute("Configuration.StfKernel.StfLogger.BodyBackgroundColor", DefaultValue = "white")]
        public string BodyBackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the body foreground color.
        /// </summary>
        [StfConfigurationAttribute("Configuration.StfKernel.StfLogger.BodyForegroundColor", DefaultValue = "black")]
        public string BodyForegroundColor { get; set; }

        /// <summary>
        /// Gets or sets the header background color.
        /// </summary>
        [StfConfigurationAttribute("Configuration.StfKernel.StfLogger.HeaderBackgroundColor", DefaultValue = "#EFEBEF")]
        public string HeaderBackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the header foreground color.
        /// </summary>
        [StfConfigurationAttribute("Configuration.StfKernel.StfLogger.HeaderForegroundColor", DefaultValue = "rgb(152, 45, 58)")]
        public string HeaderForegroundColor { get; set; }

        /// <summary>
        /// The initialize with default values.
        /// </summary>
        private void InitializeWithDefaultValues()
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
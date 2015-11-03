// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfArchiverConfiguration.cs" company="Mir Software">
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
    /// The stf logger configuration object
    /// </summary>
    public class StfLoggerConfiguration : IStfLoggerConfiguration
    {
        [StfConfigurationAttribute("Configuration.StfKernel.StfLogger.OverwriteLogFile", DefaultValue = "false")]
        public bool OverwriteLogFile { get; set; }

        [StfConfigurationAttribute("Configuration.StfKernel.StfLogger.LogToFile", DefaultValue = "true")]
        public bool LogToFile { get; set; }

        [StfConfigurationAttribute("Configuration.StfKernel.StfLogger.LogTitle", DefaultValue = "LogTitle")]
        public string LogTitle { get; set; }

        [StfConfigurationAttribute("Configuration.StfKernel.StfLogger.LogFileName", DefaultValue = @"c:\temp\Ovid_defaultlog.html")]
        public string LogFileName { get; set; }

        [StfConfigurationAttribute("Configuration.StfKernel.StfLogger.AlertLongInterval", DefaultValue = "30000")]
        public int AlertLongInterval { get; set; }

        [StfConfigurationAttribute("Configuration.StfKernel.StfLogger.LogLevel", DefaultValue = "Info")]
        public StfLogLevel LogLevel { get; set; }

        [StfConfigurationAttribute("Configuration.StfKernel.StfLogger.PathToLogoImageFile")]
        public string PathToLogoImageFile { get; set; }

        [StfConfigurationAttribute("Configuration.StfKernel.StfLogger.ScreenshotOnLogFail", DefaultValue = "true")]
        public bool ScreenshotOnLogFail { get; set; }

        [StfConfigurationAttribute("Configuration.StfKernel.StfLogger.MapNewlinesToBr", DefaultValue = "false")]
        public bool MapNewlinesToBr { get; set; }
    }
}
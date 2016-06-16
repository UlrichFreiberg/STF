// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebAdapterConfiguration.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the WebAdapterConfiguration type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DemoCorp.Stf.Adapters.WebAdapter.Configuration
{
    using System.Diagnostics.CodeAnalysis;

    using Mir.Stf.Utilities;

    /// <summary>
    /// The web adapter configuration.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global", Justification = "Setter used when reflecting")]
    public class WebAdapterConfiguration
    {
        /// <summary>
        /// Gets or sets the browser timeout.
        /// </summary>
        [StfConfiguration("Adapters.WebAdapter.Browsers.InternetExplorer.BrowserTimeout", DefaultValue = "180")]
        public int BrowserTimeout { get; set; }

        /// <summary>
        /// Gets or sets the wait for control ready timeout.
        /// </summary>
        [StfConfiguration("Adapters.WebAdapter.Browsers.InternetExplorer.WaitForControlReadyTimeout", DefaultValue = "15")]
        public int WaitForControlReadyTimeout { get; set; }

        /// <summary>
        /// Gets or sets the driver server path.
        /// </summary>
        [StfConfiguration("Adapters.WebAdapter.Browsers.InternetExplorer.DriverServerPath", DefaultValue = @"C:\Temp\Stf\Selenium\IEDriverServer_Win32_2.53.1")]
        public string DriverServerPath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to kill selenium before init.
        /// </summary>
        [StfConfiguration("Adapters.WebAdapter.Browsers.InternetExplorer.KillAllSeleniumBeforeInit", DefaultValue = "true")]
        public bool KillAllSeleniumBeforeInit { get; set; }

        /// <summary>
        /// Gets or sets the wait time for process exit.
        /// </summary>
        [StfConfiguration("Adapters.WebAdapter.Browsers.InternetExplorer.WaitTimeForProcessExit", DefaultValue = "500")]
        public int WaitTimeForProcessExit { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to ignore protected zone settings in IE exit.
        /// </summary>
        [StfConfiguration("Adapters.WebAdapter.Browsers.InternetExplorer.IgnoreProtectedModeSettings", DefaultValue = "false")]
        public bool IgnoreProtectedModeSettings { get; set; }

        /// <summary>
        /// Gets or sets the driver log level. Can be one of the following values Fatal, Error, Warn, Info, Debug, Trace
        /// </summary>
        [StfConfiguration("Adapters.WebAdapter.Browsers.InternetExplorer.DriverLogLevel", DefaultValue = "Trace")]
        public string DriverLogLevel { get; set; }
    }
}

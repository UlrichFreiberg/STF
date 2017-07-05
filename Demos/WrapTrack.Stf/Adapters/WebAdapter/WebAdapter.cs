// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebAdapter.cs" company="Mir Software">
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
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading;

    using Mir.Stf.Utilities;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.IE;

    /// <summary>
    /// The web adapter.
    /// </summary>
    public partial class WebAdapter : StfAdapterBase, IWebAdapter
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name => this.ToString();

        /// <summary>
        /// Gets the version info.
        /// </summary>
        public Version VersionInfo => new Version(1, 0, 0, 0);

        /// <summary>
        /// Gets or sets the driver for Browser.
        /// </summary>
        private IWebDriver WebDriver { get; set; }

        /// <summary>
        /// The open url.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool OpenUrl(string url)
        {
            bool retVal = true;

            try
            {
                WebDriver.Navigate().GoToUrl(url);
            }
            catch (Exception ex)
            {
                var msg = string.Format("Couldn't OpenUrl [{0}] - got error [{1}]", url, ex.Message);

                // this might not be an error - if we are looking for something to become rendered in a loop....
                StfLogger.LogInternal(msg);
                StfLogger.LogScreenshot(StfLogLevel.Internal, msg);
                retVal = false;
            }

            return retVal;
        }

        /// <summary>
        /// The get text by by. Returns the text - if element not found string.empty is returned
        /// </summary>
        /// <param name="by">
        /// The by.
        /// </param>
        /// <param name="secondsToWait">
        /// The seconds To Wait.
        /// </param>
        /// <param name="depth">
        /// Only used internally to stir out of endless recursion
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetText(By by, int secondsToWait = 1, int depth = 0)
        {
            var element = FindElement(by, secondsToWait);
            var retVal = string.Empty;

            if (element == null || depth >= 5)
            {
                return retVal;
            }

            try
            {
                retVal = element.Text;
            }
            catch (Exception ex)
            {
                StfLogger.LogInternal("Reading text failed - Have to reRead - got exception [{0}]", ex.Message);

                // if we cant get to the text, then the UI has changed (like we did press search, but the page wasn't rendered fully yet)
                retVal = GetText(by, secondsToWait, depth + 1);
            }

            return retVal;
        }

        /// <summary>
        /// The go back one page.
        /// </summary>
        public void GoBackOnePage()
        {
            WebDriver.Navigate().Back();
        }

        /// <summary>
        /// The init.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Init()
        {
            var retVal = true;

            StfLogger.LogDebug("Starting to initialize [{0}]", GetType().Name);
            Configuration = GetConfiguration();
            DriverLogFile = GetDriverLogFilePath();

            if (Configuration.KillAllSeleniumBeforeInit)
            {
                if (!KillProcesses())
                {
                    return false;
                }
            }

            switch (Configuration.BrowserName.ToLower())
            {
                case "chrome":
                    retVal = InitializeWebDriverChrome();
                    break;
                case "internetexplorer":
                    retVal = InitializeWebDriverInternetExplorer();
                    break;
            }

            StfLogger.LogDebug("Done initializing {0}. Successful: {1}", GetType().Name, retVal.ToString());

            return retVal;
        }
        
        private bool InitializeWebDriverChrome()
        {
            var retVal = true;
            var driverOptions = GetChromeOptionsOptions();

            try
            {
                var driverService = ChromeDriverService.CreateDefaultService(Configuration.DriverServerPath);

                driverService.LogPath = DriverLogFile;
                driverService.EnableVerboseLogging = true; //TODO get this right

                WebDriver = new ChromeDriver(
                                    driverService,
                                    driverOptions,
                                    TimeSpan.FromSeconds(Configuration.BrowserTimeout));
                WebDriver.Manage().Window.Maximize();
                ResetImplicitlyWait();
            }
            catch (Exception ex)
            {
                StfLogger.LogInternal($"Couldn't Initialize WebAdapter - got error [{ex.Message}]");
                retVal = false;
            }

            return retVal;
        }

        /// <summary>
        /// The initialize web driver internet explorer.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool InitializeWebDriverInternetExplorer()
        {
            var retVal = true;
            var driverOptions = GetInternetExplorerOptions();

            try
            {
                var driverService = InternetExplorerDriverService.CreateDefaultService(Configuration.DriverServerPath);

                driverService.LogFile = DriverLogFile;
                driverService.LoggingLevel = GetDriverLoggingLevel();

                WebDriver = new InternetExplorerDriver(
                                    driverService,
                                    driverOptions,
                                    TimeSpan.FromSeconds(Configuration.BrowserTimeout));
                WebDriver.Manage().Window.Maximize();
                ResetImplicitlyWait();
            }
            catch (Exception ex)
            {
                StfLogger.LogInternal($"Couldn't Initialize WebAdapter - got error [{ex.Message}]");
                retVal = false;
            }

            StfLogger.LogDebug("Done initializing {0}. Successful: {1}", GetType().Name, retVal.ToString());

            return retVal;
        }

        /// <summary>
        /// The close down.
        /// </summary>
        public void CloseDown()
        {
            WebDriver.Quit();
        }

        /// <summary>
        /// The kill process.
        /// </summary>
        /// <param name="processName">
        /// The process name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool KillProcesses(string processName = "IEDriverServer")
        {
            StfLogger.LogDebug("Starting to kill all processes with name [{0}]", processName);

            var seleniumDriverProcesses = Process.GetProcesses().Where(pr => pr.ProcessName == processName);
            var retVal = true;

            foreach (var process in seleniumDriverProcesses)
            {
                try
                {
                    StfLogger.LogDebug("Killing process with ID [{0}]", process.Id);
                    process.Kill();
                }
                catch (Exception ex)
                {
                    StfLogger.LogWarning("Challenges killing one process [{0}]", ex.Message);
                    retVal = false;
                }
            }

            // Process does not leave the process list immediately, stays in there as hasexited = true
            Thread.Sleep(Configuration.WaitTimeForProcessExit);

            // note if we did indeed kill all the processes...
            seleniumDriverProcesses = Process.GetProcesses().Where(pr => pr.ProcessName == processName);
            var allProcessKilled = !seleniumDriverProcesses.Any();
            retVal = retVal && allProcessKilled;

            if (!retVal)
            {
                StfLogger.LogError("Challenges killing processes{0}", !allProcessKilled ? " - Still some processes left to kill" : string.Empty);
            }

            return retVal;
        }

        /// <summary>
        /// The check path.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool CheckPath(string path)
        {
            if (Directory.Exists(path))
            {
                return true;
            }

            var retVal = false;

            try
            {
                Directory.CreateDirectory(path);
                retVal = true;
            }
            catch (Exception ex)
            {
                StfLogger.LogInternal("Unable to create directory [{0}]. Got error: [{1}]", path, ex.Message);
            }

            return retVal;
        }
   }
}
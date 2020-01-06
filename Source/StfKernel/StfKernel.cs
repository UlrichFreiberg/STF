// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfKernel.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;

using Mir.Stf.KernelUtils;
using Mir.Stf.Utilities;
using Mir.Stf.Utilities.Interfaces;

namespace Mir.Stf
{
    using Utilities.Configuration;

    /// <summary>
    /// The stf kernel.
    /// </summary>
    public class StfKernel : IDisposable
    {
        /// <summary>
        /// The disposed.
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="StfKernel"/> class.
        /// </summary>
        public StfKernel()
        {
            StfTextUtils = new StfTextUtils();

            // setup needed kernel directories
            KernelSetupKernelDirectories();

            // lets get a logger and a configuration
            var uniqueKernelLoggerFilename = StfTextUtils.AppendUniquePartToFileName("KernelLogger.html");
            var kernelLoggerFilename = Path.Combine(StfKernelLogDir, uniqueKernelLoggerFilename);
            var kernelLoggerConfiguration = new StfLoggerConfiguration
            {
                LogTitle = "KernelLog",
                LogFileName = kernelLoggerFilename,
                LogLevel = StfLogLevel.Internal
            };

            KernelLogger = new StfLogger(kernelLoggerConfiguration);

            // get the initial configuration together - at this point in time: Only the kernel configuration
            AssembleStfConfigurationBeforePlugins();

            // Any plugins for us?
            PluginLoader = new StfPluginLoader(KernelLogger, StfConfiguration);
            PluginLoader.RegisterInstance(typeof(StfConfiguration), StfConfiguration);

            if (StfConfiguration.TryGetKeyValue("Configuration.StfKernel.PluginPath", out var pluginPath))
            {
                // TODO: Check for existing config file. If file not found then create default template configuration
                PluginLoader.LoadStfPlugins(pluginPath);
            }

            AssembleStfConfigurationAfterPlugins();

            // now all configurations are loaded, we can set the Environment.
            StfConfiguration.Environment = StfConfiguration.DefaultEnvironment;

            LoadConfigurationForStfTypes();
            DumpStfConfiguration();

            KernelLogger.LogKeyValue("Stf Root", StfRoot, "The Stf Root directory");
        }

        /// <summary>
        /// The configuration file type.
        /// </summary>
        private enum ConfigurationFileType
        {
            /// <summary>
            /// The machine.
            /// </summary>
            Machine,

            /// <summary>
            /// The user.
            /// </summary>
            User,

            /// <summary>
            /// The test case.
            /// </summary>
            TestCase,

            /// <summary>
            /// The test suite.
            /// </summary>
            TestSuite
        }

        /// <summary>
        /// Gets or sets the stf text utils - where string stuff to remember is stored.
        /// </summary>
        public StfTextUtils StfTextUtils { get; set; }

        /// <summary>
        /// Gets or sets the stf root.
        /// </summary>
        public string StfRoot { get; set; }

        /// <summary>
        /// Gets or sets the stf log dir.
        /// </summary>
        public string StfLogDir { get; set; }

        /// <summary>
        /// Gets or sets the stf kernel log dir.
        /// </summary>
        public string StfKernelLogDir { get; set; }

        /// <summary>
        /// Gets or sets the stf configuration directory
        /// </summary>
        public string StfConfigDir { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether use archiver.
        /// </summary>
        protected bool UseArchiver { get; set; }

        /// <summary>
        /// Gets the Stf logger.
        /// </summary>
        private IStfLogger KernelLogger { get; }

        /// <summary>
        /// Gets the stf container.
        /// </summary>
        private StfPluginLoader PluginLoader { get; }

        /// <summary>
        /// Gets or sets the stf configuration.
        /// </summary>
        private StfConfiguration StfConfiguration { get; set; }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// The get.
        /// </summary>
        /// <typeparam name="T">
        /// The type to get
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        protected T Get<T>()
        {
            return PluginLoader.Get<T>();
        }

        /// <summary>
        /// Load Additional Stf Plugins - if someone has "found" some extra one somewhere (like the unit test:-))
        /// </summary>
        /// <param name="directoryName">
        /// The directory name
        /// </param>
        /// <param name="pluginPatterns">
        /// The plugin patterns
        /// </param>
        /// <returns>
        /// A boolean indicating whether successful
        /// </returns>
        protected bool LoadAdditionalStfPlugins(string directoryName, string pluginPatterns)
        {
            PluginLoader.LoadStfPlugins(directoryName, pluginPatterns);
            return true;
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (KernelLogger != null)
                    {
                        KernelLogger.LogDebug("Closing log files and disposing container");
                        KernelLogger.CloseLogFile();
                    }

                    PluginLoader?.Dispose();
                }

                disposed = true;
            }
        }

        /// <summary>
        /// The dump stf configuration.
        /// </summary>
        /// <param name="configurationLogFilename">
        /// The configuration log filename.
        /// </param>
        protected void DumpStfConfiguration(string configurationLogFilename = null)
        {
            if (string.IsNullOrEmpty(configurationLogFilename))
            {
                configurationLogFilename = Path.Combine(StfLogDir, @"StfConfigurationDump");
            }

            StfConfiguration.DumpStfConfiguration(configurationLogFilename);
        }

        /// <summary>
        /// Set a configuration value in the configuration
        /// </summary>
        /// <param name="configKeyPath">
        /// The path the configuration key
        /// </param>
        /// <param name="value">
        /// The value to set
        /// </param>
        /// <returns>
        /// A bool indicating whether it was possible to set the configuration value
        /// </returns>
        protected bool SetConfigurationValue(string configKeyPath, string value)
        {
            bool retVal;

            try
            {
                retVal = StfConfiguration.SetConfigValue(configKeyPath, value);
            }
            catch (Exception ex)
            {
                KernelLogger.LogDebug("Failed to set configuration. Got exception: [{0}]", ex.Message);
                retVal = false;
            }

            return retVal;
        }

        /// <summary>
        /// The kernel setup kernel directories.
        /// </summary>
        private void KernelSetupKernelDirectories()
        {
            var stfKernelDirectories = new StfKernelDirectories(StfTextUtils);

            StfRoot = stfKernelDirectories.GetStfRoot();

            // Check or create the needed sub directories
            StfLogDir = stfKernelDirectories.CheckForNeededKernelDirectory(@"Stf_LogDir", Path.Combine(StfRoot, @"Logs"));
            StfKernelLogDir = stfKernelDirectories.CheckForNeededKernelDirectory(@"Stf_KernelLogDir", Path.Combine(StfLogDir, "KernelLog"));
            StfConfigDir = stfKernelDirectories.CheckForNeededKernelDirectory(@"Stf_ConfigDir", Path.Combine(StfRoot, @"Config"));
        }

        /// <summary>
        /// The assembly stf configuration.
        /// </summary>
        private void AssembleStfConfigurationBeforePlugins()
        {
            var stfConfigurationFile = Path.Combine(StfConfigDir, @"StfConfiguration.xml");

            if (!File.Exists(stfConfigurationFile))
            {
                KernelLogger.LogInfo($"StfConfiguration file not found - creating default [{stfConfigurationFile}]");

                if (!CreateDefaultStfConfigurationFile(stfConfigurationFile))
                {
                    KernelLogger.LogInfo($"Couldn't create Default Configuration file not found - name [{stfConfigurationFile}]");
                }
            }

            if (File.Exists(stfConfigurationFile))
            {
                KernelLogger.LogInfo($"StfConfiguration created using [{stfConfigurationFile}]");
                StfConfiguration = new StfConfiguration(stfConfigurationFile);
            }
            else
            {
                StfConfiguration = new StfConfiguration();
                KernelLogger.LogInfo($"StfConfiguration created using no file as [{stfConfigurationFile}] doesn't exist");
            }

            // need to be able to control something for plugins - like plugin path:-)
            OverlayStfConfigurationForOneSettingType(StfConfigDir, ConfigurationFileType.Machine);
        }

        /// <summary>
        /// The create default stf configuration file.
        /// </summary>
        /// <param name="stfConfigurationFile">
        /// The stf configuration file.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool CreateDefaultStfConfigurationFile(string stfConfigurationFile)
        {
            var defaultConfigurationTemplate = @"<?xml version=""1.0""?>
<configuration xmlns=""http://www.testautomation.dk/stfConfiguration"" version=""2013.04.21.0"">
  <section name=""StfKernel"">
    <key name=""PluginPath"" value=""%STF_ROOT%\Plugins"" />
  </section>
  <section name=""Environments"" defaultsection=""TESTENVIRONMENT1"">
    <section name=""TESTENVIRONMENT1"" />
  </section>
</configuration>";

            try
            {
                File.WriteAllText(stfConfigurationFile, defaultConfigurationTemplate);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// The assembly stf configuration.
        /// </summary>
        private void AssembleStfConfigurationAfterPlugins()
        {
            OverlayStfConfigurationForOneSettingType(StfConfigDir, ConfigurationFileType.Machine);
            OverlayStfConfiguration(Environment.CurrentDirectory);
        }

        /// <summary>
        /// The set configuration for stf types. Loads configuration for 
        /// the stf types that were created by the kernel from the resolved configuration
        /// </summary>
        private void LoadConfigurationForStfTypes()
        {
            StfConfiguration.LoadUserConfiguration(KernelLogger.Configuration);
        }

        /// <summary>
        /// The overlay stf configuration.
        /// </summary>
        /// <param name="directoryName">
        /// The directory name.
        /// </param>
        private void OverlayStfConfiguration(string directoryName)
        {
            if (!Directory.Exists(directoryName))
            {
                return;
            }

            OverlayStfConfigurationForOneSettingType(directoryName, ConfigurationFileType.TestSuite);
            OverlayStfConfigurationForOneSettingType(directoryName, ConfigurationFileType.TestCase);
            OverlayStfConfigurationForOneSettingType(directoryName, ConfigurationFileType.User);
        }

        /// <summary>
        /// The overlay stf configuration for one setting type.
        /// </summary>
        /// <param name="directoryName">
        /// The directory name.
        /// </param>
        /// <param name="configurationFileType">
        /// The configuration file type.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Exception if configuration file type is unrecognized
        /// </exception>
        private void OverlayStfConfigurationForOneSettingType(string directoryName, ConfigurationFileType configurationFileType)
        {
            string configFilename;

            if (!Directory.Exists(directoryName))
            {
                return;
            }

            switch (configurationFileType)
            {
                case ConfigurationFileType.Machine:
                    configFilename = "StfConfiguration_Machine.xml";
                    break;
                case ConfigurationFileType.User:
                    configFilename = "StfConfiguration_User.xml";
                    break;
                case ConfigurationFileType.TestCase:
                    configFilename = "StfConfiguration_TestCase.xml";
                    break;
                case ConfigurationFileType.TestSuite:
                    configFilename = "StfConfiguration_TestSuite.xml";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(configurationFileType), configurationFileType, null);
            }

            var fileLocation = Path.Combine(directoryName, configFilename);

            if (!File.Exists(fileLocation))
            {
                KernelLogger.LogInternal($"Skipping Configuration file [{fileLocation}]: It does not exist, so not overlaying");
                return;
            }

            KernelLogger.LogInternal($"Applying configuration found at [{fileLocation}]");

            StfConfiguration.OverLay(fileLocation);
        }
    }
}

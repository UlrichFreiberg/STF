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
            var kernelLoggerFilename = Path.Combine(StfLogDir, @"KernelLogger.html");
            var kernelLoggerConfiguration = new StfLoggerConfiguration { LogFileName = kernelLoggerFilename };
            KernelLogger = new StfLogger(kernelLoggerConfiguration);

            // get the configuration together
            AssembleStfConfiguration();

            // Any plugins for us?
            PluginLoader = new StfPluginLoader(KernelLogger, StfConfiguration);
            PluginLoader.RegisterInstance(typeof(StfConfiguration), StfConfiguration);
            PluginLoader.LoadStfPlugins(StfConfiguration.GetKeyValue("StfKernel.PluginPath"));

            // now all configurations are loaded, we can set the Environment.
            StfConfiguration.Environment = StfConfiguration.DefaultEnvironment;
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
            /// The testcase.
            /// </summary>
            Testcase,

            /// <summary>
            /// The testsuite.
            /// </summary>
            Testsuite
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
        /// Gets or sets the stf configuration directory
        /// </summary>
        public string StfConfigDir { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether use archiver.
        /// </summary>
        protected bool UseArchiver { get; set; }

        /// <summary>
        /// Gets or sets the Stf logger.
        /// </summary>
        private IStfLogger KernelLogger { get; set; }

        /// <summary>
        /// Gets or sets the stf container.
        /// </summary>
        private StfPluginLoader PluginLoader { get; set; }

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
        /// A boolean indicating whether successfull
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

                    if (PluginLoader != null)
                    {
                        PluginLoader.Dispose();
                    }
                }

                disposed = true;
            }
        }

        /// <summary>
        /// The assembly stf configuration.
        /// </summary>
        private void AssembleStfConfiguration()
        {
            var stfConfigurationFile = Path.Combine(StfConfigDir, @"StfConfiguration.xml");

            if (File.Exists(stfConfigurationFile))
            {
                StfConfiguration = new StfConfiguration(stfConfigurationFile);
            }
            else
            {
                StfConfiguration = new StfConfiguration();
            }

            OverlayStfConfigurationForOneSettingType(StfConfigDir, ConfigurationFileType.Machine);
            OverlayStfConfiguration(Environment.CurrentDirectory);
        }

        /// <summary>
        /// Check For Needed Kernel Directory - like configDir
        /// </summary>
        /// <param name="dirVariableName">
        /// What STF name should we remember this directory as
        /// </param>
        /// <param name="defaultValue">
        /// default Value
        /// </param>
        /// <returns>
        /// The path to the diretory
        /// </returns>
        private string CheckForNeededKernelDirectory(string dirVariableName, string defaultValue = null)
        {
            var directoryNameVariable = string.Format("%{0}%", dirVariableName);
            var directoryName = StfTextUtils.ExpandVariables(directoryNameVariable);

            // if unknown directory (the variable didn't got expanded), 
            // then use the default value...
            if (directoryName == directoryNameVariable)
            {
                if (string.IsNullOrEmpty(defaultValue))
                {
                    return null;
                }

                // The default value might be using variables 
                defaultValue = StfTextUtils.ExpandVariables(defaultValue);
                directoryName = StfTextUtils.GetVariableOrSetDefault(dirVariableName, defaultValue);
            }

            // We need the directory - try to create it
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            return Directory.Exists(directoryName) ? directoryName : null;
        }

        /// <summary>
        /// The kernel setup kernel directories.
        /// </summary>
        private void KernelSetupKernelDirectories()
        {
            StfRoot = CheckForNeededKernelDirectory(@"Stf_Root", @"C:\temp\Stf");
            StfLogDir = CheckForNeededKernelDirectory(@"Stf_LogDir", Path.Combine(StfRoot, @"Logs"));
            StfConfigDir = CheckForNeededKernelDirectory(@"Stf_ConfigDir", Path.Combine(StfRoot, @"Config"));
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

            OverlayStfConfigurationForOneSettingType(directoryName, ConfigurationFileType.Machine);
            OverlayStfConfigurationForOneSettingType(directoryName, ConfigurationFileType.Testsuite);
            OverlayStfConfigurationForOneSettingType(directoryName, ConfigurationFileType.Testcase);
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
        /// Exception if configurationfiletype unrecognized
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
                case ConfigurationFileType.Testcase:
                    configFilename = "StfConfiguration_TestCase.xml";
                    break;
                case ConfigurationFileType.Testsuite:
                    configFilename = "StfConfiguration_TestSuite.xml";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("configurationFileType", configurationFileType, null);
            }

            var fileLocation = Path.Combine(directoryName, configFilename);
            if (!File.Exists(fileLocation))
            {
                return;
            }

            KernelLogger.LogInternal(string.Format("Applying configuration found at [{0}]", fileLocation));

            StfConfiguration.OverLay(fileLocation);
        }
    }
}

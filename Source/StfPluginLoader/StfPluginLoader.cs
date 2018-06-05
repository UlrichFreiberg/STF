// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfPluginLoader.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The stf plugin loader.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Mir.Stf.Utilities.Extensions;
using Mir.Stf.Utilities.Interfaces;

namespace Mir.Stf.Utilities
{
    /// <summary>
    /// The stf plugin loader.
    /// </summary>
    public class StfPluginLoader
    {
        /// <summary>
        /// The container.
        /// </summary>
        private readonly IUnityContainer container;

        /// <summary>
        /// The plugin assembly list.
        /// </summary>
        private IList<Assembly> pluginAssemblyList; 

        /// <summary>
        /// Initializes a new instance of the <see cref="StfPluginLoader"/> class.
        /// </summary>
        /// <param name="stfLogger">
        /// The stf Logger.
        /// </param>
        /// <param name="stfConfiguration">
        /// The stf Configuration.
        /// </param>
        public StfPluginLoader(IStfLogger stfLogger, StfConfiguration stfConfiguration)
        {
            PluginLogger = stfLogger;
            StfConfiguration = stfConfiguration;
            container = new UnityContainer();
            RegisterInternalTypes();
        }

        /// <summary>
        /// Gets the plugin logger.
        /// </summary>
        private IStfLogger PluginLogger { get; }

        /// <summary>
        /// Gets the stf configuration.
        /// </summary>
        private StfConfiguration StfConfiguration { get; }

        /// <summary>
        /// Gets the plugin assemblies.
        /// </summary>
        private IList<Assembly> PluginAssemblies => pluginAssemblyList ?? (pluginAssemblyList = new List<Assembly>());

        /// <summary>
        /// The load stf plugins.
        /// </summary>
        /// <param name="stfPluginPath">
        /// The stf plugin path.
        /// </param>
        /// <param name="pluginPatterns">
        /// The plugin Patterns.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LoadStfPlugins(string stfPluginPath, string pluginPatterns = "*stf.*.dll")
        {
            if (!Directory.Exists(stfPluginPath))
            {
                return 0;
            }

            PluginLogger.LogHeader("looking for plugins at [{0}]", stfPluginPath);

            var patterns = pluginPatterns.Split(';');
            foreach (var pattern in patterns)
            {
                var stfPluginDllFileNames = Directory.GetFiles(stfPluginPath, pattern);
                var assemblies = new List<Assembly>(stfPluginDllFileNames.Length);

                foreach (var stfPluginDllFileName in stfPluginDllFileNames)
                {
                    var stfPluginDllAssemblyName = AssemblyName.GetAssemblyName(stfPluginDllFileName);
                    var assembly = Assembly.Load(stfPluginDllAssemblyName);
                    assemblies.Add(assembly);
                }

                foreach (var assembly in assemblies)
                {
                    if (assembly == null)
                    {
                        continue;
                    }

                    var types = assembly.GetTypes();

                    foreach (var type in types)
                    {
                        if (type.IsInterface || type.IsAbstract)
                        {
                            continue;
                        }

                        if (type.GetInterface(typeof(IStfPlugin).FullName) != null)
                        {
                            RegisterPlugin(type);
                            PluginAssemblies.Add(assembly);
                        }
                    }
                }
            }

            OverlayPluginSettings(stfPluginPath);

            PluginLogger.LogInfo("Done looking for plugins");

            return container.Registrations.Count();
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
        public T Get<T>()
        {
            return container.ResolveType<T>();
        }

        /// <summary>
        /// The register instance.
        /// </summary>
        /// <param name="typeToRegister">
        /// The type to register.
        /// </param>
        /// <param name="instanceToRegister">
        /// The instance to register.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool RegisterInstance(Type typeToRegister, object instanceToRegister)
        {
            bool success;
            try
            {
                container.RegisterInstance(typeToRegister, instanceToRegister);
                success = true;
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            container.Dispose();
        }

        /// <summary>
        /// The register type.
        /// </summary>
        /// <param name="typeToRegister">
        /// The type to register.
        /// </param>
        private void RegisterPlugin(Type typeToRegister)
        {
            var interfaceName = $"I{typeToRegister.Name}";
            var mainInterface = typeToRegister.GetInterface(interfaceName);

            if (mainInterface == null)
            {
                PluginLogger.LogWarning("Registering type [{0}] with no matching interface", typeToRegister.Name);

                container.RegisterType(
                    typeToRegister, 
                    new InjectionProperty("StfContainer"),
                    new InjectionProperty("StfLogger"));
                
                return;
            }

            PluginLogger.LogInfo("Registering type [{0}] with matching interface [{1}]", typeToRegister.Name, interfaceName);
            
            container.RegisterMyType(mainInterface, typeToRegister);
        }

        /// <summary>
        /// Register internal plugin loader types.
        /// </summary>
        /// <remarks>
        /// Currently interception is only about logging, so we can't register
        /// the interception extension unless we also have a logger
        /// </remarks>
        private void RegisterInternalTypes()
        {
            container.RegisterType<IStfContainer, StfContainer>();

            if (PluginLogger == null)
            {
                return;
            }

            container.RegisterInstance(PluginLogger);
            container.AddNewExtension<Interception>();
        }

        /// <summary>
        /// The overlay plugin settings.
        /// </summary>
        /// <param name="pluginPath">
        /// The plugin Path.
        /// </param>
        private void OverlayPluginSettings(string pluginPath)
        {
            if (PluginAssemblies.Count == 0)
            {
                PluginLogger.LogInfo("No plugins detected so no pluginsettings to overlay");
            }

            foreach (var assembly in PluginAssemblies)
            {
                var locations = GetPluginSettingsPaths(pluginPath, assembly);

                foreach (var settingsFile in locations)
                {
                    if (!File.Exists(settingsFile))
                    {
                        PluginLogger.LogDebug($"Skipping plugin settings file [{settingsFile}]: It does not exist");
                        continue;
                    }

                    if (!PerformOverlay(settingsFile))
                    {
                        PluginLogger.LogFail("OverlayPluginSettings", $"Failed to overlay [{settingsFile}]. Check config file");
                    }
                }
            }
        }

        /// <summary>
        /// The perform overlay.
        /// </summary>
        /// <param name="settingsFilePath">
        /// The settings file path.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool PerformOverlay(string settingsFilePath)
        {
            PluginLogger.LogInfo($"Applying plugin settings: [{settingsFilePath}]");

            try
            {
                StfConfiguration.OverLay(settingsFilePath);
            }
            catch (Exception ex)
            {
                PluginLogger.LogError($"Error when overlaying plugin settings: {ex.Message}");

                return false;
            }

            return true;
        }

        /// <summary>
        /// The get plugin settings paths.
        /// </summary>
        /// <param name="pluginPath">
        /// The plugin path.
        /// </param>
        /// <param name="assembly">
        /// The assembly.
        /// </param>
        /// <returns>
        /// The <see cref="IList{T}"/>.
        /// </returns>
        private IList<string> GetPluginSettingsPaths(string pluginPath, Assembly assembly)
        {
            if (Regex.Match(pluginPath, @"^\.$").Success)
            {
                pluginPath = Directory.GetCurrentDirectory();
            }

            const string FileNameFormat = "{0}.PluginSettings.Xml";
            var assemblyLocation = assembly.Location;
            var pluginPathFileName = Path.GetFileName(assemblyLocation);

            if (string.IsNullOrEmpty(pluginPathFileName))
            {
                PluginLogger.LogError($"Couldn't get filename() of {assemblyLocation}");

                return new List<string>();
            }

            var pluginPathFileNameFullPath = Path.Combine(pluginPath, pluginPathFileName);
            var settingsPaths = new List<string> { string.Format(FileNameFormat, pluginPathFileNameFullPath) };

            if (!pluginPathFileNameFullPath.Equals(assemblyLocation, StringComparison.CurrentCultureIgnoreCase))
            {
                settingsPaths.Add(string.Format(FileNameFormat, assembly.Location));
            }

            return settingsPaths;
        }
    }
}

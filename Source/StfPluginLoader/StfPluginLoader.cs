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
        /// Gets or sets the plugin logger.
        /// </summary>
        private IStfLogger PluginLogger { get; set; }

        /// <summary>
        /// Gets or sets the stf configuration.
        /// </summary>
        private StfConfiguration StfConfiguration { get; set; }

        /// <summary>
        /// Gets the plugin assemblies.
        /// </summary>
        private IList<Assembly> PluginAssemblies
        {
            get { return pluginAssemblyList ?? (pluginAssemblyList = new List<Assembly>()); }
        }

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

            OverlayPluginSettings();

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
            var interfaceName = string.Format("I{0}", typeToRegister.Name);
            var mainInterface = typeToRegister.GetInterface(interfaceName);
            if (mainInterface == null)
            {
                container.RegisterType(
                    typeToRegister, 
                    new InjectionProperty("StfContainer"),
                    new InjectionProperty("MyLogger"));
                
                return;
            }
            
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
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool OverlayPluginSettings()
        {
            if (PluginAssemblies.Count == 0)
            {
                return false;
            }

            var success = true;
            var fileNameFormat = "{0}.PluginSettings.Xml";
            foreach (var assembly in PluginAssemblies)
            {
                var settingsFile = string.Format(fileNameFormat, assembly.Location);
                if (!File.Exists(settingsFile))
                {
                    continue;
                }

                if (!PerformOverlay(settingsFile))
                {
                    success = false;
                }
            }

            return success;
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
            try
            {
                StfConfiguration.OverLay(settingsFilePath);
            }
            catch (Exception ex)
            {
                PluginLogger.LogInternal(string.Format("Error when overlaying plugin settings: {0}", ex.Message));
                return false;
            }

            return true;
        }
    }
}

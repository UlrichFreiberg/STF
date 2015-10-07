// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Foobar" file="StfPluginLoader.cs">
//   2015
// </copyright>
// <summary>
//   Defines the GenericPluginLoader type.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity;
using Mir.Stf.Utilities.Extension;

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
        /// Initializes a new instance of the <see cref="StfPluginLoader"/> class.
        /// </summary>
        /// <param name="stfLogger">
        /// The stf Logger.
        /// </param>
        public StfPluginLoader(StfLogger stfLogger)
        {
            PluginLogger = stfLogger;
            container = new UnityContainer();
            RegisterInternalTypes();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StfPluginLoader"/> class.
        /// </summary>
        public StfPluginLoader()
        {
            container = new UnityContainer();
            RegisterInternalTypes();
        }

        /// <summary>
        /// Gets or sets the plugin logger.
        /// </summary>
        private StfLogger PluginLogger { get; set; }

        /// <summary>
        /// The load stf plugins.
        /// </summary>
        /// <param name="stfPluginPath">
        /// The stf plugin path.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LoadStfPlugins(string stfPluginPath)
        {
            if (!Directory.Exists(stfPluginPath))
            {
                return 0;
            }

            var stfPluginDllFileNames = Directory.GetFiles(stfPluginPath, "Stf.*.dll");
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
                    }
                }
            }

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
                    new InjectionProperty("StfContainer"));
                
                return;
            }
            
            container.RegisterType(
                mainInterface, 
                typeToRegister, 
                new InjectionProperty("StfContainer"));
        }

        /// <summary>
        /// Register internal plugin loader types.
        /// </summary>
        private void RegisterInternalTypes()
        {
            container.RegisterType<IStfContainer, StfContainer>();

            if (PluginLogger != null)
            {
                container.RegisterInstance(PluginLogger);
            }
        }
    }
}

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
using System.Reflection;

namespace Stf.Utilities
{
    /// <summary>
    /// The stf plugin loader.
    /// </summary>
    public class StfPluginLoader
    {
        /// <summary>
        /// The load plugins.
        /// </summary>
        /// <param name="stfPluginPath">
        /// The StfPluginPath.
        /// </param>
        /// <returns>
        /// The <see cref="ICollection"/>.
        /// </returns>
        public ICollection<IStfPlugin> LoadStfPlugins(string stfPluginPath)
        {
            if (!Directory.Exists(stfPluginPath))
            {
                return null;
            }

            var stfPluginDllFileNames = Directory.GetFiles(stfPluginPath, "stf.*.dll");
            var assemblies = new List<Assembly>(stfPluginDllFileNames.Length);

            foreach (var stfPluginDllFileName in stfPluginDllFileNames)
            {
                var stfPluginDllAssemblyName = AssemblyName.GetAssemblyName(stfPluginDllFileName);
                var assembly = Assembly.Load(stfPluginDllAssemblyName);
                assemblies.Add(assembly);
            }

            var pluginTypes = new List<Type>();
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
                        pluginTypes.Add(type);
                    }
                }
            }

            var stfPlugins = new List<IStfPlugin>(pluginTypes.Count);
            foreach (var type in pluginTypes)
            {
                var plugin = (IStfPlugin)Activator.CreateInstance(type);
                stfPlugins.Add(plugin);
            }

            return stfPlugins;
        }
    }
}

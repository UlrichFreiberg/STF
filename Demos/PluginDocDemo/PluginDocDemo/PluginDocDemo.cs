// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PluginDocDemo.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

using Mir.Stf.Utilities;

namespace PluginDocDemo
{
    /// <summary>
    /// Demo for a Stf Plugin
    /// </summary>
    public class PluginDocDemo : IStfPlugin
    {
        /// <summary>
        /// Gets the name of the plugin - Standard Stf IStfPlugin property
        /// </summary>
        public string Name
        {
            get { return "PluginDocDemo"; }
        }

        /// <summary>
        /// Gets the VersionInfo - Standard Stf IStfPlugin property
        /// </summary>
        public Version VersionInfo
        {
            get
            {
                return new Version(1, 0, 0, 51030);
            }
        }

        /// <summary>
        /// Gets or sets the StfContainer - Standard Stf IStfPlugin property
        /// </summary>
        public IStfContainer StfContainer { get; set; }

        /// <summary>
        /// Gets or sets the Stf Logger - Standard Stf IStfPlugin property
        /// </summary>
        public StfLogger MyLogger { get; set; }

        /// <summary>
        /// Standard Stf IStfPlugin function
        /// </summary>
        /// <returns>
        /// Success if init succeeded
        /// </returns>
        public bool Init()
        {
            return true;
        }
    }
}

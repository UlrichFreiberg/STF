// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PluginDocDemoConfiguration.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DemoCorp.Stf.PluginDocDemo.Configuration
{
    using Mir.Stf.Utilities;

    /// <summary>
    /// Interface for the PluginDocDemo Configuration
    /// </summary>
    public class PluginDocDemoConfiguration : IPluginDocDemoConfiguration
    {
        /// <summary>
        /// Gets or sets the KeyOne
        /// </summary>
        [ConfigAttributes.ConfigInfo("Environments.PluginDocDemo.KeyOne")]
        public string KeyOne { get; set; }

        /// <summary>
        /// Gets or sets the KeyTwo
        /// </summary>
        [ConfigAttributes.ConfigInfo("Environments.PluginDocDemo.KeyTwo")]
        public string KeyTwo { get; set; }
    }
}

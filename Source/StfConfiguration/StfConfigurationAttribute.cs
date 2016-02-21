// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfConfigurationAttribute.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the ConfigAttributes type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Mir.Stf.Utilities
{
    /// <summary>
    /// The config info.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class StfConfigurationAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StfConfigurationAttribute"/> class.
        /// </summary>
        /// <param name="configKeyPath">
        /// The config key path.
        /// </param>
        public StfConfigurationAttribute(string configKeyPath)
        {
            ConfigKeyPath = configKeyPath;
            Version = "1.0";
            DefaultValue = string.Empty;
        }

        /// <summary>
        /// Gets or sets the config key path.
        /// </summary>
        public string ConfigKeyPath { get; set; }

        /// <summary>
        /// Gets or sets the default value.
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        public string Version { get; set; }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigAttributes.cs" company="Mir Software">
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
    /// The config attributes - used to fine the link into the configuration tree
    /// </summary>
    public class ConfigAttributes
    {
        /// <summary>
        /// The config info.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class ConfigInfo : Attribute
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ConfigInfo"/> class.
            /// </summary>
            /// <param name="configKeyPath">
            /// The config key path.
            /// </param>
            public ConfigInfo(string configKeyPath)
            {
                this.ConfigKeyPath = configKeyPath;
                this.Version = "1.0";
                this.DefaultValue = string.Empty;
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
}

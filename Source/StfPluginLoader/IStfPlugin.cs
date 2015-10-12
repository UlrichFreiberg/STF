// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStfPlugin.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the IPlugin type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Mir.Stf.Utilities
{
    /// <summary>
    /// The Plugin interface.
    /// </summary>
    public interface IStfPlugin : IStfGettable, IStfLoggable
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the version info.
        /// </summary>
        Version VersionInfo { get; }

        /// <summary>
        /// The init.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool Init();
    }
}

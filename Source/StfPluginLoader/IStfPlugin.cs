// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStfPlugin.cs" company="Foobar">
//   2015
// </copyright>
// <summary>
//   Defines the IPlugin type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Stf.Utilities
{
    /// <summary>
    /// The Plugin interface.
    /// </summary>
    public interface IStfPlugin
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

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStfSingletonPluginType.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the IStfSingletonPluginType type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Stf.Unittests
{
    using System;

    using Mir.Stf.Utilities;
    using Mir.Stf.Utilities.Attributes;

    /// <summary>
    /// The StfSingletonPluginType interface.
    /// </summary>
    [StfSingleton]
    [StfInterfaceLogLevel(StfLogLevel.Internal)]
    public interface IStfSingletonPluginType : IEquatable<IStfSingletonPluginType>
    {
        /// <summary>
        /// Gets or sets the singleton integer.
        /// </summary>
        int SingletonInteger { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether singleton bool.
        /// </summary>
        [StfMemberLogLevel(StfLogLevel.Trace)]
        bool SingletonBool { get; set; }
    }
}

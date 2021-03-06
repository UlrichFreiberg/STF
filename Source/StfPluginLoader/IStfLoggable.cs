﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStfLoggable.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the IStfLoggable type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Mir.Stf.Utilities.Interfaces;

namespace Mir.Stf.Utilities
{
    /// <summary>
    /// The StfLoggable interface.
    /// </summary>
    public interface IStfLoggable
    {
        /// <summary>
        /// Gets or sets the my logger.
        /// </summary>
        IStfLogger StfLogger { get; set; }
    }
}

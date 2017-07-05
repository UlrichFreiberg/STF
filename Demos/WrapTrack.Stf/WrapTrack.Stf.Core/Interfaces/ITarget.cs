// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITarget.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the ITarget type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WrapTrack.Stf.Core.Interfaces
{
    /// <summary>
    /// The Target interface.
    /// </summary>
    public interface ITarget
    {
        /// <summary>
        /// Gets the current environment.
        /// </summary>
        string CurrentEnvironment { get; }
    }
}
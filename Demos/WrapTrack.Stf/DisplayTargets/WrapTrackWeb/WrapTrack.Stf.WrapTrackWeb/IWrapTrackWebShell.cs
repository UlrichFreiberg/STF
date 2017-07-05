// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWrapTrackWebShell.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the IWrapTrackWebShell type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WrapTrack.Stf.WrapTrackWeb
{
    using Mir.Stf.Utilities;

    /// <summary>
    /// The WrapTrackWebShell interface.
    /// </summary>
    public interface IWrapTrackWebShell : IStfPlugin
    {
        /// <summary>
        /// The learn more.
        /// </summary>
        /// <returns>
        /// The <see cref="IMinSamling"/>.
        /// </returns>
        IMinSamling LearnMore();
    }
}
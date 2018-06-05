// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWrapTrackWebShell.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the IWrapTrackWebShell type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WrapTrack.Stf.WrapTrackWeb.Interfaces
{
    using Mir.Stf.Utilities;

    using WrapTrack.Stf.WrapTrackWeb.Interfaces.Me;

    /// <summary>
    /// The WrapTrackWebShell interface.
    /// </summary>
    public interface IWrapTrackWebShell : IStfPlugin
    {
        /// <summary>
        /// The learn more.
        /// </summary>
        /// <returns>
        /// The <see cref="ICollection"/>.
        /// </returns>
        ICollection Collection();

        /// <summary>
        /// The me.
        /// </summary>
        /// <returns>
        /// The <see cref="IMe"/>.
        /// </returns>
        IMe Me();

        /// <summary>
        /// The explorer.
        /// </summary>
        /// <returns>
        /// The <see cref="IExplorer"/>.
        /// </returns>
        IExplorer Explorer();

        /// <summary>
        /// The market.
        /// </summary>
        /// <returns>
        /// The <see cref="IMarket"/>.
        /// </returns>
        IMarket Market();

        /// <summary>
        /// The faq.
        /// </summary>
        /// <returns>
        /// The <see cref="IFaq"/>.
        /// </returns>
        IFaq Faq();

        /// <summary>
        /// The logout.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool Logout();
    }
}
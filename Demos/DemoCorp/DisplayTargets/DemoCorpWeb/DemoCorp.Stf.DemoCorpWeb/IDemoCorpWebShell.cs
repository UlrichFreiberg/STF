// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDemoCorpWebShell.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the IDemoCorpWebShell type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DemoCorp.Stf.DemoCorpWeb
{
    using Mir.Stf.Utilities;

    /// <summary>
    /// The DemoCorpWebShell interface.
    /// </summary>
    public interface IDemoCorpWebShell : IStfPlugin
    {
        /// <summary>
        /// The learn more.
        /// </summary>
        /// <returns>
        /// The <see cref="ILearnMore"/>.
        /// </returns>
        ILearnMore LearnMore();
    }
}
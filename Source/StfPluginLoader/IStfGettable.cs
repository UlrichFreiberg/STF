// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStfGettable.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The StfGettable interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities
{
    /// <summary>
    /// The StfGettable interface.
    /// </summary>
    public interface IStfGettable
    {
        /// <summary>
        /// Gets or sets the stf container.
        /// </summary>
        IStfContainer StfContainer { get; set; }
    }
}

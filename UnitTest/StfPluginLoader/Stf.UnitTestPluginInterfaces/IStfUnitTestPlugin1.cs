// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStfUnitTestPlugin1.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The StfUnitTestPlugin1 interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Mir.Stf.Utilities;

namespace Stf.Unittests
{
    /// <summary>
    /// The StfUnitTestPlugin1 interface.
    /// </summary>
    public interface IStfUnitTestPlugin1 : IStfPlugin
    {
        /// <summary>
        /// Gets or sets a value indicating whether is initialized.
        /// </summary>
        bool IsInitialized { get; set; }

        /// <summary>
        /// The stf unit test plugin 1 func.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int StfUnitTestPlugin1Func();
    }
}

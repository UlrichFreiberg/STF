// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStfUnitTestPlugin2.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The StfUnitTestPlugin2 interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Mir.Stf.Utilities;

namespace Stf.Unittests
{
    /// <summary>
    /// The StfUnitTestPlugin2 interface.
    /// </summary>
    public interface IStfUnitTestPlugin2 : IStfPlugin
    {
        /// <summary>
        /// The stf unit test plugin 2 func.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int StfUnitTestPlugin2Func();
    }
}

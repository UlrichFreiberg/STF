// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStfUnitTestPlugin1.cs" company="Foobar">
//   2015
// </copyright>
// <summary>
//   Defines the IStfUnitTestPlugin1 type.
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
        /// The stf unit test plugin 1 func.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int StfUnitTestPlugin1Func();
    }
}

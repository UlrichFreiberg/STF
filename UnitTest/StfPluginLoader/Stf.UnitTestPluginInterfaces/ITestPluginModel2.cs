// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITestPluginModel2.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The TestPlugin2Model interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Stf.Unittests
{
    using System;

    /// <summary>
    /// The TestPlugin2Model interface.
    /// </summary>
    public interface ITestPluginModel2 : IEquatable<ITestPluginModel2>
    {
        /// <summary>
        /// Gets or sets the test prop.
        /// </summary>
        string TestProp { get; set; }

        /// <summary>
        /// The plugin 2 type func.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int TestPlugin2Func();

        /// <summary>
        /// The test plugin 2 func with params.
        /// </summary>
        /// <param name="param1">
        /// The param 1.
        /// </param>
        /// <param name="param2">
        /// The param 2.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string TestPlugin2FuncWithParams(string param1, int param2);
    }
}

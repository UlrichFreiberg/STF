// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITestPluginAdapter.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the ITestPluginAdapter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Stf.Unittests
{
    /// <summary>
    /// The TestPluginAdapter interface.
    /// </summary>
    public interface ITestPluginAdapter
    {
        /// <summary>
        /// The test plugin adapter func.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int TestPluginAdapterFunc();

        /// <summary>
        /// The can use adapter base internally.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool CanUseAdapterBaseInternally();
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITestPluginModel.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the ITestPluginModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Stf.Unittests
{
    /// <summary>
    /// The Plugin2Type interface.
    /// </summary>
    public interface ITestPluginModel
    {
        /// <summary>
        /// The plugin 2 type func.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int TestPluginFunc();

        /// <summary>
        /// The can use model base internally.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool CanUseModelBaseInternally();
    }
}

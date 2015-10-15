// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestPluginModel2.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The test plugin 2 model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Mir.Stf.Utilities;

namespace Stf.Unittests.UnitTestPluginTypes
{
    /// <summary>
    /// The test plugin 2 model.
    /// </summary>
    public class TestPluginModel2 : StfModelBase, ITestPluginModel2
    {
        /// <summary>
        /// The test plugin 2 func.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int TestPlugin2Func()
        {
            return 203;
        }
    }
}

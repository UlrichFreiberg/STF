// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestPluginAdapter.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The test plugin adapter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Mir.Stf.Utilities;

namespace Stf.Unittests.UnitTestPluginTypes
{
    /// <summary>
    /// The test plugin adapter.
    /// </summary>
    public class TestPluginAdapter : StfAdapterBase, ITestPluginAdapter
    {
        /// <summary>
        /// The test plugin adapter func.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int TestPluginAdapterFunc()
        {
            return 42;
        }
    }
}

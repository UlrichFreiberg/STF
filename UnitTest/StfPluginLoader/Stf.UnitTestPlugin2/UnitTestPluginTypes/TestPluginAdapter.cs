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

        /// <summary>
        /// The can use model base internally.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool CanUseAdapterBaseInternally()
        {
            try
            {
                Log("Log message from Test Plugin Model");
                Log("Formatted log message with args [{0}], [{1}] and [{2}]", 1, 2, 3);
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestPluginModel.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The plugin 2 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Mir.Stf.Utilities;

namespace Stf.Unittests.UnitTestPluginTypes
{
    /// <summary>
    /// The plugin 2 type.
    /// </summary>
    public class TestPluginModel : StfModelBase, ITestPluginModel
    {
        /// <summary>
        /// The plugin 2 type func.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int TestPluginFunc()
        {
            return 202;
        }

        /// <summary>
        /// The can use model base internally.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool CanUseModelBaseInternally()
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

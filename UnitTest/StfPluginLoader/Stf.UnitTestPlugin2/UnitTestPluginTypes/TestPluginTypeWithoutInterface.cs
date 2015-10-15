// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestPluginTypeWithoutInterface.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The test plugin type without interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Mir.Stf.Utilities;

namespace Stf.Unittests.UnitTestPluginTypes
{
    /// <summary>
    /// The test plugin type without interface.
    /// </summary>
    public class TestPluginTypeWithoutInterface : StfModelBase
    {
        /// <summary>
        /// The test plugin with no interface.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int TestPluginWithNoInterface()
        {
            return 84;
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestAdapterWithoutInterface.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The test adapter without interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Runtime.Remoting.Messaging;
using Mir.Stf.Utilities;

namespace Stf.Unittests.UnitTestPluginTypes
{
    /// <summary>
    /// The test adapter without interface.
    /// </summary>
    public class TestAdapterWithoutInterface : StfAdapterBase
    {
        /// <summary>
        /// The adapter func.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int AdapterFunc()
        {
            return 168;
        }
    }
}

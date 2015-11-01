// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTest1.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    using DemoCorp.Stf.PluginDocDemo.Interfaces;

    using Mir.Stf;

    /// <summary>
    /// Unittests for the Plugin Demo
    /// </summary>
    [TestClass]
    public class UnitTest1 : StfTestScriptBase
    {
        /// <summary>
        /// Does it fly at all?
        /// </summary>
        [TestMethod]
        public void TestCanInstantiate()
        {
            var pluginDocDemo = this.Get<IPluginDocDemo>();

            pluginDocDemo.PrintValues();
        }
    }
}

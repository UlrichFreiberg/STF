﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestStfPluginLoader.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Mir.Stf;

namespace UnitTest
{
    using System.Runtime.InteropServices;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Mir.Stf.Utilities;

    using Stf.Unittests;

    /// <summary>
    /// The unit test stf asserts.
    /// </summary>
    [TestClass]
    public class UnitTestStfPluginLoader : StfTestScriptBase
    {
        /// <summary>
        /// The test method plugin test get.
        /// </summary>
        [TestMethod]
        public void TestMethodPluginTestGet()
        {
            // need to load the UnitTest plug-ins
            LoadAdditionalStfPlugins(".", "Stf.UnitTestPlugin*.dll");

            var sp1 = Get<IStfUnitTestPlugin1>();
            MyAssert.AssertNotNull("Get<IStfUnitTestPlugin1>", sp1);
            MyAssert.AssertEquals("sp1.StfUnitTestPlugin1Func", 101, sp1.StfUnitTestPlugin1Func());

            var sp2 = Get<IStfUnitTestPlugin2>();
            MyAssert.AssertNotNull("Get<IStfUnitTestPlugin2>", sp2);
            MyAssert.AssertEquals("sp2.StfUnitTestPlugin2Func", 102, sp2.StfUnitTestPlugin2Func());
        }

        /// <summary>
        /// The test method get plugin type test.
        /// </summary>
        [TestMethod]
        public void TestMethodGetPluginTypeTest()
        {
            // need to load the UnitTest plug-ins
            LoadAdditionalStfPlugins(".", "Stf.UnitTestPlugin*.dll");

            var sp2 = Get<IStfUnitTestPlugin2>();
            MyAssert.AssertNotNull("Get<IStfUnitTestPlugin2>", sp2);
            MyAssert.AssertEquals("sp2.StfUnitTestPlugin2Func", 102, sp2.StfUnitTestPlugin2Func());

            var plugin2Type = Get<IPlugin2Type>();
            MyAssert.AssertNotNull("Get<IPlugin2Type>", plugin2Type);
            MyAssert.AssertEquals("plugin2Type.Plugin2TypeFunc", 202, plugin2Type.Plugin2TypeFunc());
        }

        /// <summary>
        /// The test method getter on stf container.
        /// </summary>
        [TestMethod]
        public void TestMethodGetterOnStfContainer()
        {
            // need to load the UnitTest plug-ins
            LoadAdditionalStfPlugins(".", "Stf.UnitTestPlugin*.dll");

            var sp2 = Get<IStfUnitTestPlugin2>();
            var pluginType = sp2.StfContainer.Get<IPlugin2Type>();
            MyAssert.AssertNotNull("Not null", pluginType);
        }

        /// <summary>
        /// The test method container extension initializes plugin.
        /// </summary>
        [TestMethod]
        public void TestMethodContainerExtensionInitializesPlugin()
        {
            // need to load the UnitTest plug-ins
            LoadAdditionalStfPlugins(".", "Stf.UnitTestPlugin*.dll");

            var sp2 = Get<IStfUnitTestPlugin2>();
            MyAssert.AssertNotNull("sp2.StfContainer != null", sp2.StfContainer);
            var sp1 = sp2.StfContainer.Get<IStfUnitTestPlugin1>();
            MyAssert.AssertTrue("sp1.IsInitialized", sp1.IsInitialized);
        }
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTest_StfPluginLoader.cs" company="Foobar">
//   2015
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace UnitTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Stf.Unittests;
    using Stf.Utilities;

    /// <summary>
    /// The unit test stf asserts.
    /// </summary>
    [TestClass]
    public class UnitTestStfPluginLoader : StfTestScriptBase
    {
        /// <summary>
        /// The test method plugin test.
        /// </summary>
        /// <remarks>
        /// Plugin count is 3 because the container holds a registration of itself :-)
        /// </remarks>
        [TestMethod]
        public void TestMethodPluginTest()
        {
            var stfLoader = new StfPluginLoader();
            var containerCount = stfLoader.LoadStfPlugins(".");

            MyAssert.AssertEquals("stfPlugins.Count", 4, containerCount);
        }

        /// <summary>
        /// The test method plugin test get.
        /// </summary>
        [TestMethod]
        public void TestMethodPluginTestGet()
        {
            var stfLoader = new StfPluginLoader();
            var containerCount = stfLoader.LoadStfPlugins(".");

            MyAssert.AssertEquals("stfPlugins.Count", 4, containerCount);

            var sp1 = stfLoader.Get<IStfUnitTestPlugin1>();
            MyAssert.AssertNotNull("Get<IStfUnitTestPlugin1>", sp1);
            MyAssert.AssertEquals("sp1.StfUnitTestPlugin1Func", 101, sp1.StfUnitTestPlugin1Func());

            var sp2 = stfLoader.Get<IStfUnitTestPlugin2>();
            MyAssert.AssertNotNull("Get<IStfUnitTestPlugin2>", sp2);
            MyAssert.AssertEquals("sp2.StfUnitTestPlugin2Func", 102, sp2.StfUnitTestPlugin2Func());
        }

        /// <summary>
        /// The test method get plugin type test.
        /// </summary>
        [TestMethod]
        public void TestMethodGetPluginTypeTest()
        {
            var stfLoader = new StfPluginLoader();
            var containerCount = stfLoader.LoadStfPlugins(".");

            MyAssert.AssertEquals("stfPlugins.Count", 4, containerCount);
            var sp2 = stfLoader.Get<IStfUnitTestPlugin2>();
            MyAssert.AssertNotNull("Get<IStfUnitTestPlugin2>", sp2);
            MyAssert.AssertEquals("sp2.StfUnitTestPlugin2Func", 102, sp2.StfUnitTestPlugin2Func());

            var plugin2Type = stfLoader.Get<IPlugin2Type>();
            MyAssert.AssertNotNull("Get<IPlugin2Type>", plugin2Type);
            MyAssert.AssertEquals("plugin2Type.Plugin2TypeFunc", 202, plugin2Type.Plugin2TypeFunc());
        }

        /// <summary>
        /// The test method getter on stf container.
        /// </summary>
        [TestMethod]
        public void TestMethodGetterOnStfContainer()
        {
            var sp2 = Get<IStfUnitTestPlugin2>();
            var pluginType = sp2.StfContainer.Get<IPlugin2Type>();
            MyAssert.AssertNotNull("Not null", pluginType);
        }

    }
}

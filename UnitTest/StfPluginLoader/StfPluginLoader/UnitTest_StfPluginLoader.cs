// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTest_StfAssert.cs" company="Foobar">
//   2015
// </copyright>
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
        [TestMethod]
        public void TestMethodPluginTest()
        {
            var stfLoader = new StfPluginLoader();
            var stfPlugins = stfLoader.LoadStfPlugins(".");

            MyAssert.AssertEquals("stfPlugins.Count", 2, stfPlugins.Count);
        }

        [TestMethod]
        public void TestMethodPluginTestGet()
        {
            var stfLoader = new StfPluginLoader();
            var stfPlugins = stfLoader.LoadStfPlugins(".");

            MyAssert.AssertEquals("stfPlugins.Count", 2, stfPlugins.Count);

            var sp1 = stfLoader.Get<IStfUnitTestPlugin1>();
            MyAssert.AssertNotNull("Get<IStfUnitTestPlugin1>", sp1);
            MyAssert.AssertEquals("sp1.StfUnitTestPlugin1Func", 101, sp1.StfUnitTestPlugin1Func());

            var sp2 = stfLoader.Get<IStfUnitTestPlugin2>();
            MyAssert.AssertNotNull("Get<IStfUnitTestPlugin1>", sp2);
            MyAssert.AssertEquals("sp1.StfUnitTestPlugin1Func", 102, sp2.StfUnitTestPlugin2Func());
        }

    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTest_StfAssert.cs" company="Foobar">
//   2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace UnitTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Stf.Utilities;

    /// <summary>
    /// The unit test stf asserts.
    /// </summary>
    [TestClass]
    public class UnitTestStfAsserts : StfTestScriptBase
    {
        [TestMethod]
        public void TestMethodPluginTest()
        {
            var stfLoader = new StfPluginLoader();
            var stfPlugins = stfLoader.LoadStfPlugins(".");

            MyAssert.AssertGreaterThan("stfPlugins.Count", stfPlugins.Count, 0);
        }
    }
}

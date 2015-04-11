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
        /// <summary>
        /// The test initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            MyAssert.EnableNegativeTesting = true;
            this.MyLogger.LogInfo("UnitTestStfAsserts TestInitialize");
        }

        /// <summary>
        /// The test method assert strings.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertTrue()
        {
            Assert.IsTrue(this.MyAssert.AssertTrue("true", true));
            Assert.IsFalse(this.MyAssert.AssertTrue("false", false));
        }

        /// <summary>
        /// The test method assert strings.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertFalse()
        {
            Assert.IsFalse(this.MyAssert.AssertFalse("true", true));
            Assert.IsTrue(this.MyAssert.AssertFalse("false", false));
        }

        /// <summary>
        /// The test cleanup.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            this.MyLogger.LogInfo("UnitTestStfAsserts TestCleanup");
        }
    }
}

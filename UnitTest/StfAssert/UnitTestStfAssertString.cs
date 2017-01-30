// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestStfAssertString.cs" company="Mir Software">
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
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The unit test stf asserts.
    /// </summary>
    [TestClass]
    public class UnitTestStfAssertString : StfTestScriptBase
    {
        /// <summary>
        /// The test initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            StfAssert.EnableNegativeTesting = true;
            StfLogger.Configuration.ScreenshotOnLogFail = false;
        }

        /// <summary>
        /// The test cleanup.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            // setting to true agains resets failure count
            StfAssert.ResetStatistics();
        }

        /// <summary>
        /// The test method assert strings.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertStrings()
        {
            Assert.IsTrue(StfAssert.StringContains("TestStepName 1", "Hejsa", "Hej"));
            Assert.IsFalse(StfAssert.StringContains("TestStepName 2", "Hejsa", "Bent"));

            Assert.IsTrue(StfAssert.StringDoesNotContain("TestStepName 3", "Hejsa", "Bent"));
            Assert.IsFalse(StfAssert.StringDoesNotContain("TestStepName 4", "Hejsa", "Hej"));

            Assert.IsTrue(StfAssert.StringMatches("TestStepName 5", "Hejsa", "^He.*"));
            Assert.IsFalse(StfAssert.StringMatches("TestStepName 6", "Hejsa", "Nix.*"));

            Assert.IsTrue(StfAssert.StringDoesNotMatch("TestStepName 7", "Hejsa", "Nix.*"));
            Assert.IsFalse(StfAssert.StringDoesNotMatch("TestStepName 8", "Hejsa", "^He.*"));

            Assert.IsTrue(StfAssert.StringStartsWith("TestStepName 9", "Hejsa", "He"));
            Assert.IsFalse(StfAssert.StringStartsWith("TestStepName 10", "Hejsa", "hej"));

            Assert.IsTrue(StfAssert.StringDoesNotStartWith("TestStepName 11", "Hejsa", "hej"));
            Assert.IsFalse(StfAssert.StringDoesNotStartWith("TestStepName 12", "Hejsa", "He"));

            Assert.IsTrue(StfAssert.StringEndsWith("TestStepName 13", "Hejsa", "jsa"));
            Assert.IsFalse(StfAssert.StringEndsWith("TestStepName 14", "Hejsa", "Hej"));

            Assert.IsTrue(StfAssert.StringDoesNotEndsWith("TestStepName 15", "Hejsa", "Bent"));
            Assert.IsFalse(StfAssert.StringDoesNotEndsWith("TestStepName 16", "Hejsa", "ejsa"));

            Assert.IsTrue(StfAssert.StringEquals("TestStepName 17", "Hejsa", "Hejsa"));
            Assert.IsFalse(StfAssert.StringEquals("TestStepName 18", "Hejsa", "hejsa"));

            Assert.IsTrue(StfAssert.StringNotEquals("TestStepName 19", "Hejsa", "hejsa"));
            Assert.IsFalse(StfAssert.StringNotEquals("TestStepName 20", "Hejsa", "Hejsa"));

            Assert.IsTrue(StfAssert.StringEqualsCi("TestStepName 21", "Hejsa", "hejsa"));
            Assert.IsFalse(StfAssert.StringEqualsCi("TestStepName 22", "Hejsa", "hej"));

            Assert.IsTrue(StfAssert.StringNotEqualsCi("TestStepName 23", "Hejsa", "hejs"));
            Assert.IsFalse(StfAssert.StringNotEqualsCi("TestStepName 24", "Hejsa", "hejsa"));

            Assert.IsTrue(StfAssert.StringEmpty("TestStepName 25", string.Empty));
            Assert.IsFalse(StfAssert.StringEmpty("TestStepName 26", "Hejsa"));

            Assert.IsTrue(StfAssert.StringNotEmpty("TestStepName 27", "Hejsa"));
            Assert.IsFalse(StfAssert.StringNotEmpty("TestStepName 28", string.Empty));
        }
    }
}

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
    public class UnitTestStfStringAsserts
    {
        /// <summary>
        /// The test method assert strings.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertStrings()
        {
            var myLogger = new Stf.Utilities.StfLogger { FileName = @"c:\temp\unittestlogger_TestMethodAssertStrings.html" };
            var myAsserts = new StfAssert(myLogger);

            Assert.IsTrue(myAsserts.StringContains("TestStepName 1", "Hejsa", "Hej"));
            Assert.IsFalse(myAsserts.StringContains("TestStepName 2", "Hejsa", "Bent"));

            Assert.IsTrue(myAsserts.StringDoesNotContain("TestStepName 3", "Hejsa", "Bent"));
            Assert.IsFalse(myAsserts.StringDoesNotContain("TestStepName 4", "Hejsa", "Hej"));

            Assert.IsTrue(myAsserts.StringMatches("TestStepName 5", "Hejsa", "^He.*"));
            Assert.IsFalse(myAsserts.StringMatches("TestStepName 6", "Hejsa", "Nix.*"));

            Assert.IsTrue(myAsserts.StringDoesNotMatch("TestStepName 7", "Hejsa", "Nix.*"));
            Assert.IsFalse(myAsserts.StringDoesNotMatch("TestStepName 8", "Hejsa", "^He.*"));

            Assert.IsTrue(myAsserts.StringStartsWith("TestStepName 9", "Hejsa", "He"));
            Assert.IsFalse(myAsserts.StringStartsWith("TestStepName 10", "Hejsa", "hej"));

            Assert.IsTrue(myAsserts.StringDoesNotStartWith("TestStepName 11", "Hejsa", "hej"));
            Assert.IsFalse(myAsserts.StringDoesNotStartWith("TestStepName 12", "Hejsa", "He"));

            Assert.IsTrue(myAsserts.StringEndsWith("TestStepName 13", "Hejsa", "jsa"));
            Assert.IsFalse(myAsserts.StringEndsWith("TestStepName 14", "Hejsa", "Hej"));

            Assert.IsTrue(myAsserts.StringDoesNotEndsWith("TestStepName 15", "Hejsa", "Bent"));
            Assert.IsFalse(myAsserts.StringDoesNotEndsWith("TestStepName 16", "Hejsa", "ejsa"));

            Assert.IsTrue(myAsserts.StringEquals("TestStepName 17", "Hejsa", "Hejsa"));
            Assert.IsFalse(myAsserts.StringEquals("TestStepName 18", "Hejsa", "hejsa"));

            Assert.IsTrue(myAsserts.StringNotEquals("TestStepName 19", "Hejsa", "hejsa"));
            Assert.IsFalse(myAsserts.StringNotEquals("TestStepName 20", "Hejsa", "Hejsa"));

            Assert.IsTrue(myAsserts.StringEqualsCi("TestStepName 21", "Hejsa", "hejsa"));
            Assert.IsFalse(myAsserts.StringEqualsCi("TestStepName 22", "Hejsa", "hej"));

            Assert.IsTrue(myAsserts.StringNotEqualsCi("TestStepName 23", "Hejsa", "hejs"));
            Assert.IsFalse(myAsserts.StringNotEqualsCi("TestStepName 24", "Hejsa", "hejsa"));

            Assert.IsTrue(myAsserts.StringEmpty("TestStepName 25", string.Empty));
            Assert.IsFalse(myAsserts.StringEmpty("TestStepName 26", "Hejsa"));

            Assert.IsTrue(myAsserts.StringNotEmpty("TestStepName 27", "Hejsa"));
            Assert.IsFalse(myAsserts.StringNotEmpty("TestStepName 28", string.Empty));
        }
    }
}

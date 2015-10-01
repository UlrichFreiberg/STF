// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTest_StfStringAssert.cs" company="Foobar">
//   2015
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace UnitTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Stf.Utilities;

    /// <summary>
    /// The unit test stf asserts.
    /// </summary>
    [TestClass]
    public class UnitTestStfStringAsserts : StfTestScriptBase
    {
        /// <summary>
        /// The test method assert strings.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertStrings()
        {
            Assert.IsTrue(MyAssert.StringContains("TestStepName 1", "Hejsa", "Hej"));
            Assert.IsFalse(MyAssert.StringContains("TestStepName 2", "Hejsa", "Bent"));

            Assert.IsTrue(MyAssert.StringDoesNotContain("TestStepName 3", "Hejsa", "Bent"));
            Assert.IsFalse(MyAssert.StringDoesNotContain("TestStepName 4", "Hejsa", "Hej"));

            Assert.IsTrue(MyAssert.StringMatches("TestStepName 5", "Hejsa", "^He.*"));
            Assert.IsFalse(MyAssert.StringMatches("TestStepName 6", "Hejsa", "Nix.*"));

            Assert.IsTrue(MyAssert.StringDoesNotMatch("TestStepName 7", "Hejsa", "Nix.*"));
            Assert.IsFalse(MyAssert.StringDoesNotMatch("TestStepName 8", "Hejsa", "^He.*"));

            Assert.IsTrue(MyAssert.StringStartsWith("TestStepName 9", "Hejsa", "He"));
            Assert.IsFalse(MyAssert.StringStartsWith("TestStepName 10", "Hejsa", "hej"));

            Assert.IsTrue(MyAssert.StringDoesNotStartWith("TestStepName 11", "Hejsa", "hej"));
            Assert.IsFalse(MyAssert.StringDoesNotStartWith("TestStepName 12", "Hejsa", "He"));

            Assert.IsTrue(MyAssert.StringEndsWith("TestStepName 13", "Hejsa", "jsa"));
            Assert.IsFalse(MyAssert.StringEndsWith("TestStepName 14", "Hejsa", "Hej"));

            Assert.IsTrue(MyAssert.StringDoesNotEndsWith("TestStepName 15", "Hejsa", "Bent"));
            Assert.IsFalse(MyAssert.StringDoesNotEndsWith("TestStepName 16", "Hejsa", "ejsa"));

            Assert.IsTrue(MyAssert.StringEquals("TestStepName 17", "Hejsa", "Hejsa"));
            Assert.IsFalse(MyAssert.StringEquals("TestStepName 18", "Hejsa", "hejsa"));

            Assert.IsTrue(MyAssert.StringNotEquals("TestStepName 19", "Hejsa", "hejsa"));
            Assert.IsFalse(MyAssert.StringNotEquals("TestStepName 20", "Hejsa", "Hejsa"));

            Assert.IsTrue(MyAssert.StringEqualsCi("TestStepName 21", "Hejsa", "hejsa"));
            Assert.IsFalse(MyAssert.StringEqualsCi("TestStepName 22", "Hejsa", "hej"));

            Assert.IsTrue(MyAssert.StringNotEqualsCi("TestStepName 23", "Hejsa", "hejs"));
            Assert.IsFalse(MyAssert.StringNotEqualsCi("TestStepName 24", "Hejsa", "hejsa"));

            Assert.IsTrue(MyAssert.StringEmpty("TestStepName 25", string.Empty));
            Assert.IsFalse(MyAssert.StringEmpty("TestStepName 26", "Hejsa"));

            Assert.IsTrue(MyAssert.StringNotEmpty("TestStepName 27", "Hejsa"));
            Assert.IsFalse(MyAssert.StringNotEmpty("TestStepName 28", string.Empty));
        }
    }
}

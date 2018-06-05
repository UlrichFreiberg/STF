// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestStfAssertString.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
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
        protected enum BinaryStringAssert
        {
            StringEquals,
            StringEqualsCi,
            StringNotEquals,
            StringNotEqualsCi,
            StringContains,
            StringNotContains,
            StringEndsWith,
            StringDoesNotEndsWith,
            StringMatches,
            StringStartsWith,
            StringDoesNotStartWith,
            StringDoesNotMatch
        }

        protected enum UnaryStringAssert
        {
            StringEmpty,
            StringNotEmpty
        }

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

        [TestMethod]
        public void TestStringEmpty()
        {
            // String Empty
            HelperUnaryAssert(UnaryStringAssert.StringEmpty, null, false);
            HelperUnaryAssert(UnaryStringAssert.StringEmpty, string.Empty, true);

            HelperUnaryAssert(UnaryStringAssert.StringEmpty, "Bob", false);

            // String Not Empty
            HelperUnaryAssert(UnaryStringAssert.StringNotEmpty, null, false);
            HelperUnaryAssert(UnaryStringAssert.StringNotEmpty, string.Empty, false);

            HelperUnaryAssert(UnaryStringAssert.StringNotEmpty, "Bob", true);
        }

        [TestMethod]
        public void TestStringEquals()
        {
            // Equals
            HelperBinaryAssert(BinaryStringAssert.StringEquals, null, null, true);
            HelperBinaryAssert(BinaryStringAssert.StringEquals, null, string.Empty, false);
            HelperBinaryAssert(BinaryStringAssert.StringEquals, string.Empty, null, false);
            HelperBinaryAssert(BinaryStringAssert.StringEquals, string.Empty, string.Empty, true);

            HelperBinaryAssert(BinaryStringAssert.StringEquals, "Hejsa", null, false);
            HelperBinaryAssert(BinaryStringAssert.StringEquals, "Hejsa", string.Empty, false);
            HelperBinaryAssert(BinaryStringAssert.StringEquals, null, "Hejsa", false);
            HelperBinaryAssert(BinaryStringAssert.StringEquals, string.Empty, "Hejsa", false);

            HelperBinaryAssert(BinaryStringAssert.StringEquals, "Hejsa", "Hejsa", true);
            HelperBinaryAssert(BinaryStringAssert.StringEquals, "Hejsa", "hejsa", false);

            // NotEquals
            HelperBinaryAssert(BinaryStringAssert.StringNotEquals, null, null, false);
            HelperBinaryAssert(BinaryStringAssert.StringNotEquals, null, string.Empty, true);
            HelperBinaryAssert(BinaryStringAssert.StringNotEquals, string.Empty, null, true);
            HelperBinaryAssert(BinaryStringAssert.StringNotEquals, string.Empty, string.Empty, false);

            HelperBinaryAssert(BinaryStringAssert.StringNotEquals, "Hejsa", null, true);
            HelperBinaryAssert(BinaryStringAssert.StringNotEquals, "Hejsa", string.Empty, true);
            HelperBinaryAssert(BinaryStringAssert.StringNotEquals, null, "Hejsa", true);
            HelperBinaryAssert(BinaryStringAssert.StringNotEquals, string.Empty, "Hejsa", true);

            HelperBinaryAssert(BinaryStringAssert.StringNotEquals, "Hejsa", "Hejsa", false);
            HelperBinaryAssert(BinaryStringAssert.StringNotEquals, "Hejsa", "hejsa", true);

            // EqualsCI
            HelperBinaryAssert(BinaryStringAssert.StringEqualsCi, null, null, true);
            HelperBinaryAssert(BinaryStringAssert.StringEqualsCi, null, string.Empty, false);
            HelperBinaryAssert(BinaryStringAssert.StringEqualsCi, string.Empty, null, false);
            HelperBinaryAssert(BinaryStringAssert.StringEqualsCi, string.Empty, string.Empty, true);

            HelperBinaryAssert(BinaryStringAssert.StringEqualsCi, "Hejsa", null, false);
            HelperBinaryAssert(BinaryStringAssert.StringEqualsCi, "Hejsa", string.Empty, false);
            HelperBinaryAssert(BinaryStringAssert.StringEqualsCi, null, "Hejsa", false);
            HelperBinaryAssert(BinaryStringAssert.StringEqualsCi, string.Empty, "Hejsa", false);

            HelperBinaryAssert(BinaryStringAssert.StringEqualsCi, "Hejsa", "Hejsa", true);
            HelperBinaryAssert(BinaryStringAssert.StringEqualsCi, "Hejsa", "hejsa", true);

            // NotEqualsCi
            HelperBinaryAssert(BinaryStringAssert.StringNotEqualsCi, null, null, false);
            HelperBinaryAssert(BinaryStringAssert.StringNotEqualsCi, null, string.Empty, true);
            HelperBinaryAssert(BinaryStringAssert.StringNotEqualsCi, string.Empty, null, true);
            HelperBinaryAssert(BinaryStringAssert.StringNotEqualsCi, string.Empty, string.Empty, false);

            HelperBinaryAssert(BinaryStringAssert.StringNotEqualsCi, "Hejsa", null, true);
            HelperBinaryAssert(BinaryStringAssert.StringNotEqualsCi, "Hejsa", string.Empty, true);
            HelperBinaryAssert(BinaryStringAssert.StringNotEqualsCi, null, "Hejsa", true);
            HelperBinaryAssert(BinaryStringAssert.StringNotEqualsCi, string.Empty, "Hejsa", true);

            HelperBinaryAssert(BinaryStringAssert.StringNotEqualsCi, "Hejsa", "Hejsa", false);
            HelperBinaryAssert(BinaryStringAssert.StringNotEqualsCi, "Hejsa", "hejsa", false);
        }

        [TestMethod]
        public void TestStringEndsWith()
        {
            // Ends With
            HelperBinaryAssert(BinaryStringAssert.StringEndsWith, null, null, false);
            HelperBinaryAssert(BinaryStringAssert.StringEndsWith, null, string.Empty, false);
            HelperBinaryAssert(BinaryStringAssert.StringEndsWith, string.Empty, null, false);
            HelperBinaryAssert(BinaryStringAssert.StringEndsWith, string.Empty, string.Empty, false);

            HelperBinaryAssert(BinaryStringAssert.StringEndsWith, "Hejsa", "jsa", true);
            HelperBinaryAssert(BinaryStringAssert.StringEndsWith, "Hejsa", "Hej", false);

            // Does Not Ends With
            HelperBinaryAssert(BinaryStringAssert.StringDoesNotEndsWith, null, null, false);
            HelperBinaryAssert(BinaryStringAssert.StringDoesNotEndsWith, null, string.Empty, false);
            HelperBinaryAssert(BinaryStringAssert.StringDoesNotEndsWith, string.Empty, null, false);
            HelperBinaryAssert(BinaryStringAssert.StringDoesNotEndsWith, string.Empty, string.Empty, false);
                                                  
            HelperBinaryAssert(BinaryStringAssert.StringDoesNotEndsWith, "Hejsa", "jsa", false);
            HelperBinaryAssert(BinaryStringAssert.StringDoesNotEndsWith, "Hejsa", "Hej", true);
        }

        [TestMethod]
        public void TestStartWith()
        {
            // Start With
            HelperBinaryAssert(BinaryStringAssert.StringStartsWith, null, null, false);
            HelperBinaryAssert(BinaryStringAssert.StringStartsWith, null, string.Empty, false);
            HelperBinaryAssert(BinaryStringAssert.StringStartsWith, string.Empty, null, false);
            HelperBinaryAssert(BinaryStringAssert.StringStartsWith, string.Empty, string.Empty, false);
                                                  
            HelperBinaryAssert(BinaryStringAssert.StringStartsWith, "Hejsa", "jsa", false);
            HelperBinaryAssert(BinaryStringAssert.StringStartsWith, "Hejsa", "Hej", true);

            // Does Not Start With
            HelperBinaryAssert(BinaryStringAssert.StringDoesNotStartWith, null, null, false);
            HelperBinaryAssert(BinaryStringAssert.StringDoesNotStartWith, null, string.Empty, false);
            HelperBinaryAssert(BinaryStringAssert.StringDoesNotStartWith, string.Empty, null, false);
            HelperBinaryAssert(BinaryStringAssert.StringDoesNotStartWith, string.Empty, string.Empty, false);
                                                  
            HelperBinaryAssert(BinaryStringAssert.StringDoesNotStartWith, "Hejsa", "jsa", true);
            HelperBinaryAssert(BinaryStringAssert.StringDoesNotStartWith, "Hejsa", "Hej", false);
        }

        [TestMethod]
        public void TestMatches()
        {
            HelperBinaryAssert(BinaryStringAssert.StringMatches, null, null, false);
            HelperBinaryAssert(BinaryStringAssert.StringMatches, null, string.Empty, false);
            HelperBinaryAssert(BinaryStringAssert.StringMatches, string.Empty, null, false);
            HelperBinaryAssert(BinaryStringAssert.StringMatches, string.Empty, string.Empty, false);

            HelperBinaryAssert(BinaryStringAssert.StringMatches, "Hejsa", "^He.*", true);
            HelperBinaryAssert(BinaryStringAssert.StringMatches, "Hejsa", "Nix.*", false);
        }

        [TestMethod]
        public void TestContains()
        {
            // StringContains
            HelperBinaryAssert(BinaryStringAssert.StringContains, null, null, false);
            HelperBinaryAssert(BinaryStringAssert.StringContains, null, string.Empty, false);
            HelperBinaryAssert(BinaryStringAssert.StringContains, string.Empty, null, false);
            HelperBinaryAssert(BinaryStringAssert.StringContains, string.Empty, string.Empty, false);
            HelperBinaryAssert(BinaryStringAssert.StringContains, "Hejsa", null, false);
            HelperBinaryAssert(BinaryStringAssert.StringContains, "Hejsa", string.Empty, false);
            HelperBinaryAssert(BinaryStringAssert.StringContains, null, "Hejsa", false);
            HelperBinaryAssert(BinaryStringAssert.StringContains, string.Empty, "Hejsa", false);

            HelperBinaryAssert(BinaryStringAssert.StringContains, "Hejsa", "Hej", true);
            HelperBinaryAssert(BinaryStringAssert.StringContains, "Hejsa", "ejs", true);
            HelperBinaryAssert(BinaryStringAssert.StringContains, "Hejsa", "Bent", false);

            // StringNotContains
            HelperBinaryAssert(BinaryStringAssert.StringNotContains, null, null, false);
            HelperBinaryAssert(BinaryStringAssert.StringNotContains, null, string.Empty, false);
            HelperBinaryAssert(BinaryStringAssert.StringNotContains, string.Empty, null, false);
            HelperBinaryAssert(BinaryStringAssert.StringNotContains, string.Empty, string.Empty, false);
            HelperBinaryAssert(BinaryStringAssert.StringNotContains, "Hejsa", null, false);
            HelperBinaryAssert(BinaryStringAssert.StringNotContains, "Hejsa", string.Empty, false);
            HelperBinaryAssert(BinaryStringAssert.StringNotContains, null, "Hejsa", false);
            HelperBinaryAssert(BinaryStringAssert.StringNotContains, string.Empty, "Hejsa", false);

            HelperBinaryAssert(BinaryStringAssert.StringNotContains, "Hejsa", "Hej", false);
            HelperBinaryAssert(BinaryStringAssert.StringNotContains, "Hejsa", "ejs", false);
            HelperBinaryAssert(BinaryStringAssert.StringNotContains, "Hejsa", "Bent", true);
        }

        private void HelperUnaryAssert(
            UnaryStringAssert unaryStringAssert,
            string argument,
            bool expected)
        {
            Func<string, string, bool> stringAssertFunction;

            switch (unaryStringAssert)
            {
                // Empty
                case UnaryStringAssert.StringEmpty:
                    stringAssertFunction = StfAssert.StringEmpty;
                    break;
                case UnaryStringAssert.StringNotEmpty:
                    stringAssertFunction = StfAssert.StringNotEmpty;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(unaryStringAssert), unaryStringAssert, null);
            }

            var argumentValue = GetStringValueUnfoldEmptyNull(argument);
            var testDescription = $"{unaryStringAssert}({argumentValue})";

            StfLogger.LogSubHeader($"Unit Testing: {testDescription} --> Expecting {expected}");

            var retVal = stringAssertFunction(testDescription, argument);

            if (expected != retVal
            )
            {
                StfLogger.LogSubHeader($"Unit Test Found an error for {testDescription}...");
            }

            if (expected)
            {
                Assert.IsTrue(retVal, "Unit Test Result");
            }
            else
            {
                Assert.IsFalse(retVal, "Unit Test Result");
            }
        }

        private void HelperBinaryAssert(
            BinaryStringAssert binaryStringAssert,
            string arg1,
            string arg2,
            bool expected)
        {
            Func<string, string, string, bool> stringAssertFunction;

            switch (
                binaryStringAssert
            )
            {
                // EQUALS
                case BinaryStringAssert.StringEquals:
                    stringAssertFunction = StfAssert.StringEquals;
                    break;
                case BinaryStringAssert.StringEqualsCi:
                    stringAssertFunction = StfAssert.StringEqualsCi;
                    break;
                case BinaryStringAssert.StringNotEquals:
                    stringAssertFunction = StfAssert.StringNotEquals;
                    break;
                case BinaryStringAssert.StringNotEqualsCi:
                    stringAssertFunction = StfAssert.StringNotEqualsCi;
                    break;

                // CONTAINS
                case BinaryStringAssert.StringContains:
                    stringAssertFunction = StfAssert.StringContains;
                    break;
                case BinaryStringAssert.StringNotContains:
                    stringAssertFunction = StfAssert.StringDoesNotContain;
                    break;

                // ENDS WITH
                case BinaryStringAssert.StringEndsWith:
                    stringAssertFunction = StfAssert.StringEndsWith;
                    break;
                case BinaryStringAssert.StringDoesNotEndsWith:
                    stringAssertFunction = StfAssert.StringDoesNotEndsWith;
                    break;

                // STARTS WITH
                case BinaryStringAssert.StringStartsWith:
                    stringAssertFunction = StfAssert.StringStartsWith;
                    break;
                case BinaryStringAssert.StringDoesNotStartWith:
                    stringAssertFunction = StfAssert.StringDoesNotStartWith;
                    break;

                // MATCHES
                case BinaryStringAssert.StringMatches:
                    stringAssertFunction = StfAssert.StringMatches;
                    break;
                case BinaryStringAssert.StringDoesNotMatch:
                    stringAssertFunction = StfAssert.StringDoesNotMatch;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(binaryStringAssert), binaryStringAssert, null);
            }

            var arg1Value = GetStringValueUnfoldEmptyNull(arg1);
            var arg2Value = GetStringValueUnfoldEmptyNull(arg2);
            var testDescription = $"{binaryStringAssert}({arg1Value}, {arg2Value})";

            StfLogger.LogSubHeader($"Unit Testing: {testDescription} --> Expecting {expected}");

            var retVal = stringAssertFunction(testDescription, arg1, arg2);

            if (expected != retVal)
            {
                StfLogger.LogSubHeader($"Unit Test Found an error for {testDescription}...");
            }

            if (expected)
            {
                Assert.IsTrue
            (
                retVal, "Unit Test Result");
            }
            else
            {
                Assert.IsFalse
            (
                retVal, "Unit Test Result");
            }
        }

        private string GetStringValueUnfoldEmptyNull(string argument)
        {
            if (argument == null)
            {
                return "null";
            }

            var retVal = string.IsNullOrEmpty(argument) ? "Empty" : argument;

            return retVal;
        }
    }
}

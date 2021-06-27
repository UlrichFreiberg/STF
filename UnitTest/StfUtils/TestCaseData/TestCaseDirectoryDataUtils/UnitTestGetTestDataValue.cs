﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestGetTestDataValue.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The unit test get test data value.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace UnitTest.TestCaseData.TestCaseDirectoryDataUtils
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Mir.Stf.Utilities.TestCaseData;

    /// <summary>
    /// The unit test get test data value.
    /// </summary>
    [TestClass]
    public class UnitTestGetTestDataValue : UnitTestScriptBase
    {
        /// <summary>
        /// The unit test test data root.
        /// </summary>
        private const string UnitTestTestDataRoot = @".\TestData\TestCaseData\TestCaseDirectoryDataUtils";

        /// <summary>
        /// The test get test data value simple.
        /// </summary>
        [TestMethod]
        public void TestGetTestDataValueSimple()
        {
            var testCaseDirectoryDataUtils = new TestCaseDirectoryDataUtils(4001, UnitTestTestDataRoot);
            var actual = testCaseDirectoryDataUtils.GetTestDataValue("FourtyTwo");
            var expected = "42";

            StfAssert.AreEqual("Simple FourtyTwo", expected, actual);
        }

        /// <summary>
        /// The test get test data value expr.
        /// </summary>
        [TestMethod]
        public void TestGetTestDataValueExpr()
        {
            var testCaseDirectoryDataUtils = new TestCaseDirectoryDataUtils(4001, UnitTestTestDataRoot);
            var actual = testCaseDirectoryDataUtils.GetTestDataValue("TwoPlusOne");
            var expected = "3";

            StfAssert.AreEqual("Expr 2 + 1", expected, actual);
        }
    }
}

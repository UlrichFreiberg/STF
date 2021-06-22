﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestTestDataManagement.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the UnitTest1 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.EndToEnd
{
    using Mir.Stf.Utilities.FileUtilities;
    using Mir.Stf.Utilities.TestCaseDirectoryUtilities;

    /// <summary>
    /// The unit test test data management.
    /// </summary>
    [TestClass]
    public class UnitTestTestDataManagement : UnitTestScriptBase
    {
        /// <summary>
        /// The test simple test data management.
        /// </summary>
        [TestMethod]
        public void TestSimpleTestDataManagement()
        {
            var testCaseFileAndFolderUtils = new TestCaseFileAndFolderUtils(4001, @".\TestData\EndToEnd");
            var testDataValuesFilePath = testCaseFileAndFolderUtils.GetTestCaseRootFilePath("TestDataValues.txt");
            var keyValuePairUtils = new KeyValuePairUtils();
            var testData = keyValuePairUtils.ReadKeyValuePairsFromFile(testDataValuesFilePath);

            foreach (var testDataKey in testData.Keys)
            {
                var value = testData[testDataKey];

                StfLogger.LogInfo($"[{testDataKey}] has the value [{value}]");
            }
        }
    }
}

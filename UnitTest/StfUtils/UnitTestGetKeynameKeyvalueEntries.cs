// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestGetKeynameKeyvalueEntries.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace UnitTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Mir.Stf;
    using Mir.Stf.Utilities;
    using Mir.Stf.Utilities.StfTestUtilities;

    /// <summary>
    /// The unit test get keyname keyvalue entries.
    /// </summary>
    [TestClass]
    public class UnitTestGetKeynameKeyvalueEntries : StfTestScriptBase
    {
        /// <summary>
        /// The stf test utils.
        /// </summary>
        private StfTestUtils stfTestUtils;

        /// <summary>
        /// The test read key value pairs from file.
        /// </summary>
        [TestMethod]
        public void TestReadKeyValuePairsFromFile()
        {
            StfLogger.Configuration.ScreenshotOnLogFail = false;
            stfTestUtils = new StfTestUtils(6001);
            stfTestUtils.TestCaseFileAndFolderUtils.SetupTempAndResultsFolders();

            // keyName Case Ignore Case
            HelperReadKeyValuePairsFromFile("File With two simple assignments", "Simple.txt");
            HelperReadKeyValuePairsFromFile("File With two duplicate key assignments", "DuplicateKeys - CaseSignificant.txt", keyNameIgnoreCase: false);

            // keyName Case Significant
            HelperReadKeyValuePairsFromFile("File With two simple assignments", "Simple.txt", keyNameIgnoreCase: false);
            HelperReadKeyValuePairsFromFile("File With two duplicate key assignments", "DuplicateKeys - IgnoreCase.txt");
        }

        /// <summary>
        /// The test read key value pairs from file with different assignment operator.
        /// </summary>
        [TestMethod]
        public void TestReadKeyValuePairsFromFileWithDifferentAssignmentOperator()
        {
            StfLogger.Configuration.ScreenshotOnLogFail = false;
            stfTestUtils = new StfTestUtils(6002);
            stfTestUtils.TestCaseFileAndFolderUtils.SetupTempAndResultsFolders();

            // AssignmentOperator BECOMES
            HelperReadKeyValuePairsFromFile("AssignmentOperator BECOMES", "SimpleAssignmentOperatorBECOMES.txt", "BECOMES");
        }

        /// <summary>
        /// The helper read key value pairs from file.
        /// </summary>
        /// <param name="testStep">
        /// The test step.
        /// </param>
        /// <param name="inputFilename">
        /// The input filename.
        /// </param>
        /// <param name="assignmentOperator">
        /// The assignment operator.
        /// </param>
        /// <param name="commentIndicator">
        /// The comment indicator.
        /// </param>
        /// <param name="keyNameIgnoreCase">
        /// The key name ignore case.
        /// </param>
        private void HelperReadKeyValuePairsFromFile(string testStep, string inputFilename, string assignmentOperator = "=", string commentIndicator = "//", bool keyNameIgnoreCase = true)
        {
            StfLogger.LogHeader(testStep);

            var absolutePathInput = stfTestUtils.GetTestCaseRootFilePath(inputFilename);
            var absolutePathExpected = stfTestUtils.GetTestCaseRootFilePath($@"Expected\{inputFilename}");
            var tempInputPath = stfTestUtils.GetTestCaseTempFilePath(inputFilename, false);
            var tempActualPath = stfTestUtils.GetTestCaseTempFilePath($@"{inputFilename}-Actual.txt", false);
            var tempExpectedPath = stfTestUtils.GetTestCaseTempFilePath($@"{inputFilename}-Expected.txt", false);
            var fileUtils = stfTestUtils.FileUtils;

            fileUtils.CopyFile(absolutePathInput, tempInputPath);
            fileUtils.CopyFile(absolutePathExpected, tempExpectedPath);

            // generate Actual
            var keyNameValueUtils = new KeyValuePairUtils(assignmentOperator, commentIndicator, keyNameIgnoreCase);
            var keyValuePairs = keyNameValueUtils.ReadKeyValuePairsFromFile(tempInputPath);

            keyNameValueUtils.SaveKeyValuePairsToFile(tempActualPath, keyValuePairs);

            StfAssert.FilesDoNotDiffer(testStep, tempExpectedPath, tempActualPath);
        }
    }
}

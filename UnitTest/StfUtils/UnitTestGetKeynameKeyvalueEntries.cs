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

    using Mir.Stf.Utilities;

    /// <summary>
    /// The unit test get keyname keyvalue entries.
    /// </summary>
    [TestClass]
    public class UnitTestGetKeynameKeyvalueEntries : UnitTestScriptBase
    {
        /// <summary>
        /// The test read key value pairs from file.
        /// </summary>
        [TestMethod]
        public void TestReadKeyValuePairsFromFile()
        {
            StfLogger.Configuration.ScreenshotOnLogFail = false;

            // keyName Case Ignore Case
            HelperReadKeyValuePairsFromFile("File With two simple assignments", "Simple.txt");
            HelperReadKeyValuePairsFromFile("File With two duplicate key assignments", "DuplicateKeys - CaseSignificant.txt", keyNameIgnoreCase: false);

            // keyName Case Significant
            HelperReadKeyValuePairsFromFile("File With two simple assignments", "Simple.txt", keyNameIgnoreCase: false);
            HelperReadKeyValuePairsFromFile("File With two duplicate key assignments", "DuplicateKeys - IgnoreCase.txt");
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
            const string DataDir = @".\TestData\KeyValuePairsUtils";

            StfLogger.LogHeader(testStep);
            FileUtils.SetupTempResultFolders(DataDir);

            var absolutePathInput = FileUtils.GetTestCaseLocalFilePath(inputFilename);
            var absolutePathExpected = FileUtils.GetTestCaseLocalFilePath($@"Expected\{inputFilename}");
            var tempInputPath = FileUtils.GetTestCaseTempDirFilePath(inputFilename);
            var tempActualPath = FileUtils.GetTestCaseTempDirFilePath($@"{inputFilename}-Actual.txt");
            var tempExpectedPath = FileUtils.GetTestCaseTempDirFilePath($@"{inputFilename}-Expected.txt");

            FileUtils.CopyFile(absolutePathInput, tempInputPath);
            FileUtils.CopyFile(absolutePathExpected, tempExpectedPath);

            // generate Actual
            var keyNameValueUtils = new KeyValuePairUtils(assignmentOperator, commentIndicator, keyNameIgnoreCase);
            var keyValuePairs = keyNameValueUtils.ReadKeyValuePairsFromFile(tempInputPath);

            keyNameValueUtils.SaveKeyValuePairsToFile(tempActualPath, keyValuePairs);

            StfAssert.FilesDoNotDiffer(testStep, tempExpectedPath, tempActualPath);
        }
    }
}

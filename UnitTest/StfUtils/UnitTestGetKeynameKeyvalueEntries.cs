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
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Mir.Stf;
    using Mir.Stf.Utilities.FileUtilities;

    /// <summary>
    /// The unit test get keyname keyvalue entries.
    /// </summary>
    [TestClass]
    public class UnitTestGetKeynameKeyvalueEntries : StfTestScriptBase
    {
        /// <summary>
        /// The test read key value pairs from file.
        /// </summary>
        [TestMethod]
        public void TestReadKeyValuePairsFromFile()
        {
            // keyName Case Ignore Case
            HelperReadKeyValuePairsFromFile("File With two simple assignments", "Simple.txt");
            HelperReadKeyValuePairsFromFile("File With two duplicate key assignments", "DuplicateKeys - CaseSignificant.txt");

            // keyName Case Significant
            HelperReadKeyValuePairsFromFile("File With two simple assignments", "Simple.txt", keyNameIgnoreCase: false);
            HelperReadKeyValuePairsFromFile("File With two duplicate key assignments", "DuplicateKeys - IgnoreCase.txt", keyNameIgnoreCase: false);
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
            const string DataDir = @"D:\Projects\STF\UnitTest\StfUtils\TestData\FileUtils\KeyVauePairsUtils";
            var dataDirTemp = Path.Combine(DataDir, "Temp");
            var fileUtils = new FileUtils();
            var absolutePathInput = Path.Combine(DataDir, inputFilename);
            var absolutePathExpected = Path.Combine(DataDir, $@"Expected\{inputFilename}");
            var tempInputPath = Path.Combine(dataDirTemp, inputFilename);
            var tempActualPath = Path.Combine(dataDirTemp, $@"{inputFilename}-Actual.txt");
            var tempExpectedPath = Path.Combine(dataDirTemp, $@"{inputFilename}-Expected.txt");

            // Setup Temp files
            if (!Directory.Exists(dataDirTemp))
            {
                Directory.CreateDirectory(dataDirTemp);
            }

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

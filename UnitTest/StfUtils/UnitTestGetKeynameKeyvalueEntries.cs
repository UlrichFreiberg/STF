// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestGetKeynameKeyvalueEntries.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace UnitTest.TextUtils
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mir.Stf;
    using Mir.Stf.Utilities.FileUtilities;
    using System.IO;

    [TestClass]
    public class UnitTestGetKeynameKeyvalueEntries : StfTestScriptBase
    {
        [TestMethod]
        public void TestReadKeyValuePairsFromFile()
        {
            // keyName Case Ignore Case
            Helper_ReadKeyValuePairsFromFile("File With two simple assignments", "Simple.txt");
            Helper_ReadKeyValuePairsFromFile("File With two duplicate key assignments", "DuplicateKeys - CaseSignificant.txt");

            // keyName Case Significant
            Helper_ReadKeyValuePairsFromFile("File With two simple assignments", "Simple.txt", keyNameIgnoreCase: false);
            Helper_ReadKeyValuePairsFromFile("File With two duplicate key assignments", "DuplicateKeys - IgnoreCase.txt", keyNameIgnoreCase: false);
        }

        private void Helper_ReadKeyValuePairsFromFile(string testStep, string inputFilename, string assignmentOperator = "=", string commentIndicator = "//", bool keyNameIgnoreCase = true)
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

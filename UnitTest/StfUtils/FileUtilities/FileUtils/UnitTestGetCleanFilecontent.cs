// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestGetCleanFilecontent.cs" company="Mir Software">
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
    public class UnitTestGetCleanFilecontent : StfTestScriptBase
    {
        [TestMethod]
        public void TestGetCleanFilecontent()
        {
            Helper_GetCleanFilecontent("File With Header", "FileWithComments.txt");
            Helper_GetCleanFilecontent("File With no Header", "FileWithCommentsNoHeader.txt");
        }

        private void Helper_GetCleanFilecontent(string testStep, string inputFilename)
        {
            const string DataDir = @"D:\Projects\STF\UnitTest\StfUtils\TestData\FileUtils\GetCleanFilecontent";
            var dataDirTemp = Path.Combine(DataDir, "Temp");
            var fileUtils = new FileUtils();
            var expectedFilename = $"{inputFilename}.Expected.txt";
            var absolutePathInput = Path.Combine(DataDir, inputFilename);
            var absolutePathExpected = Path.Combine(DataDir, expectedFilename);
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
            var cleaned = fileUtils.GetCleanFilecontent(tempInputPath);

            File.WriteAllText(tempActualPath, cleaned);
            StfAssert.FilesDoNotDiffer(testStep, tempExpectedPath, tempActualPath);
        }
    }
}

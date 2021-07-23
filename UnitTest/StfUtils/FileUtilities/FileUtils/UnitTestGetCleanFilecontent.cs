// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestGetCleanFilecontent.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The unit test get clean file content.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace UnitTest.FileUtilities.FileUtils
{
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The unit test get clean file content.
    /// </summary>
    [TestClass]
    public class UnitTestGetCleanFilecontent : UnitTestScriptBase
    {
        /// <summary>
        /// The test get clean file content.
        /// </summary>
        [TestMethod]
        public void TestGetCleanFilecontent()
        {
            HelperGetCleanFileContent(null, "path is null");
            HelperGetCleanFileContent(string.Empty, "path is empty");

            HelperGetCleanFileContent("File With Header", "FileWithComments.txt");
            HelperGetCleanFileContent("File With no Header", "FileWithCommentsNoHeader.txt");
        }

        /// <summary>
        /// The helper get clean file content.
        /// </summary>
        /// <param name="testStep">
        /// The test step.
        /// </param>
        /// <param name="inputFilename">
        /// The input filename.
        /// </param>
        private void HelperGetCleanFileContent(string testStep, string inputFilename)
        {
            StfLogger.LogHeader(testStep);
            StfAssert.IsInconclusive(testStep, "Missing Data Files to be submitted to GIT");

            FileUtils.SetupTempResultFolders(@".\TestData\FileUtils\GetCleanFileContent");

            var expectedFilename = $"{inputFilename}.Expected.txt";
            var absolutePathInput = FileUtils.GetTestCaseLocalFilePath(inputFilename);
            var absolutePathExpected = FileUtils.GetTestCaseLocalFilePath(expectedFilename);
            var tempInputPath = FileUtils.GetTestCaseTempDirFilePath(inputFilename);
            var tempActualPath = FileUtils.GetTestCaseTempDirFilePath($@"{inputFilename}-Actual.txt");
            var tempExpectedPath = FileUtils.GetTestCaseTempDirFilePath($@"{inputFilename}-Expected.txt");

            FileUtils.CopyFile(absolutePathInput, tempInputPath);
            FileUtils.CopyFile(absolutePathExpected, tempExpectedPath);

            // generate Actual
            var actual = FileUtils.GetCleanFilecontent(tempInputPath);

            File.WriteAllText(tempActualPath, actual);

            StfAssert.FilesDoNotDiffer(testStep, tempExpectedPath, tempActualPath);
        }
    }
}

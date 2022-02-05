// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestGetCleanFileContent.cs" company="Mir Software">
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

    using Mir.Stf;
    using Mir.Stf.Utilities.FileUtilities;
    using Mir.Stf.Utilities.StfTestUtilities;

    /// <summary>
    /// The unit test get clean file content.
    /// </summary>
    [TestClass]
    public class UnitTestGetCleanFileContent : StfTestScriptBase
    {
        /// <summary>
        /// The stf test utils.
        /// </summary>
        private StfTestUtils stfTestUtils;

        /// <summary>
        /// The test get clean file content.
        /// </summary>
        [TestMethod]
        public void TestGetCleanFileContent()
        {
            stfTestUtils = new StfTestUtils(4501);
            stfTestUtils.TestCaseFileAndFolderUtils.SetupTempAndResultsFolders();

            HelperGetCleanFileContentNoReferenceFiles("path is null", null);
            HelperGetCleanFileContentNoReferenceFiles("path is empty", string.Empty);

            HelperGetCleanFileContent("File With Header", "FileWithComments.txt");
            HelperGetCleanFileContent("File With no Header", "FileWithCommentsNoHeader.txt");
        }

        /// <summary>
        /// The helper get clean file content no reference files.
        /// </summary>
        /// <param name="testStep">
        /// The test step.
        /// </param>
        /// <param name="inputFilename">
        /// The input filename.
        /// </param>
        private void HelperGetCleanFileContentNoReferenceFiles(string testStep, string inputFilename)
        {
            var content = stfTestUtils.FileUtils.GetCleanFileContent(inputFilename);

            StfAssert.IsNull(testStep, content);
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

            var expectedFilename = $"{inputFilename}.Expected.txt";
            var absolutePathInput = stfTestUtils.GetTestCaseRootFilePath(inputFilename);
            var absolutePathExpected = stfTestUtils.GetTestCaseRootFilePath(expectedFilename);
            var tempInputPath = stfTestUtils.GetTestCaseTempFilePath(inputFilename, false);
            var tempActualPath = stfTestUtils.GetTestCaseTempFilePath($@"{inputFilename}-Actual.txt", false);
            var tempExpectedPath = stfTestUtils.GetTestCaseTempFilePath($@"{inputFilename}-Expected.txt", false);
            var fileUtils = stfTestUtils.FileUtils;

            fileUtils.CopyFile(absolutePathInput, tempInputPath);
            fileUtils.CopyFile(absolutePathExpected, tempExpectedPath);

            // generate Actual
            var actual = fileUtils.GetCleanFileContent(tempInputPath);

            File.WriteAllText(tempActualPath, actual);

            StfAssert.FilesDoNotDiffer(testStep, tempExpectedPath, tempActualPath);
        }
    }
}

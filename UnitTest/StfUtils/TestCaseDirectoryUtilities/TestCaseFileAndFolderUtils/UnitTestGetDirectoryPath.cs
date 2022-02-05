// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestGetDirectoryPath.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The unit test get directory path.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace UnitTest.TestCaseDirectoryUtilities.TestCaseFileAndFolderUtils
{
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Mir.Stf;
    using Mir.Stf.Utilities.TestCaseDirectoryUtilities;

    /// <summary>
    /// The unit test get directory path.
    /// </summary>
    [TestClass]
    public class UnitTestGetDirectoryPath : StfTestScriptBase
    {
        /// <summary>
        /// The unit test test data root.
        /// </summary>
        private const string UnitTestTestDataRoot = @".\TestData\TestCaseDirectoryUtilities";

        /// <summary>
        /// The test test case directory exists.
        /// </summary>
        [TestMethod]
        public void TestTestCaseDirectoriesExists()
        {
            var testCaseFileAndFolderUtils = new TestCaseFileAndFolderUtils(9001, UnitTestTestDataRoot);

            // root
            HelperTestCaseDirectories(testCaseFileAndFolderUtils.GetTestCaseRootFilePath(null), @"Tc9001");
            HelperTestCaseDirectories(testCaseFileAndFolderUtils.GetTestCaseRootFilePath(string.Empty), @"Tc9001");
            HelperTestCaseDirectories(testCaseFileAndFolderUtils.GetTestCaseRootFilePath("FileName.NotExists"), null);
            HelperTestCaseDirectories(testCaseFileAndFolderUtils.GetTestCaseRootFilePath("FileName.NotExists", false), @"Tc9001\FileName.NotExists");
            HelperTestCaseDirectories(testCaseFileAndFolderUtils.GetTestCaseRootFilePath("UnitTestRootFile01.txt"), @"Tc9001\UnitTestRootFile01.txt");
            HelperTestCaseDirectories(testCaseFileAndFolderUtils.GetTestCaseRootFilePath("UnitTestRootFile01.txt", false), @"Tc9001\UnitTestRootFile01.txt");

            // Results
            HelperTestCaseDirectories(testCaseFileAndFolderUtils.GetTestCaseResultsFilePath(null), @"Tc9001\Results");
            HelperTestCaseDirectories(testCaseFileAndFolderUtils.GetTestCaseResultsFilePath(string.Empty), @"Tc9001\Results");
            HelperTestCaseDirectories(testCaseFileAndFolderUtils.GetTestCaseResultsFilePath("FileName.NotExists"), null);
            HelperTestCaseDirectories(testCaseFileAndFolderUtils.GetTestCaseResultsFilePath("FileName.NotExists", false), @"Tc9001\Results\FileName.NotExists");
            HelperTestCaseDirectories(testCaseFileAndFolderUtils.GetTestCaseResultsFilePath("UnitTestResultFile01.txt"), @"Tc9001\Results\UnitTestResultFile01.txt");
            HelperTestCaseDirectories(testCaseFileAndFolderUtils.GetTestCaseResultsFilePath("UnitTestResultFile01.txt", false), @"Tc9001\Results\UnitTestResultFile01.txt");

            // Temp
            HelperTestCaseDirectories(testCaseFileAndFolderUtils.GetTestCaseTempFilePath(null), @"Tc9001\Temp");
            HelperTestCaseDirectories(testCaseFileAndFolderUtils.GetTestCaseTempFilePath(string.Empty), @"Tc9001\Temp");
            HelperTestCaseDirectories(testCaseFileAndFolderUtils.GetTestCaseTempFilePath("FileName.NotExists"), null);
            HelperTestCaseDirectories(testCaseFileAndFolderUtils.GetTestCaseTempFilePath("FileName.NotExists", false), @"Tc9001\Temp\FileName.NotExists");
            HelperTestCaseDirectories(testCaseFileAndFolderUtils.GetTestCaseTempFilePath("UnitTestTempFile01.txt"), @"Tc9001\Temp\UnitTestTempFile01.txt");
            HelperTestCaseDirectories(testCaseFileAndFolderUtils.GetTestCaseTempFilePath("UnitTestTempFile01.txt", false), @"Tc9001\Temp\UnitTestTempFile01.txt");
        }

        /// <summary>
        /// The test test case directory does not exists.
        /// </summary>
        [TestMethod]
        public void TestTestCaseDirectoryDoesNotExists()
        {
            var testCaseFileAndFolderUtils = new TestCaseFileAndFolderUtils(5001, UnitTestTestDataRoot);
            var actual = testCaseFileAndFolderUtils.TestCaseDirectory;

            StfAssert.IsNull("TestCaseDirectory does not exists", actual);
        }

        /// <summary>
        /// The helper test case directories.
        /// </summary>
        /// <param name="actual">
        /// The actual.
        /// </param>
        /// <param name="subDir">
        /// The sub dir.
        /// </param>
        private void HelperTestCaseDirectories(string actual, string subDir)
        {
            var expected = subDir == null 
                             ? null
                             : Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), UnitTestTestDataRoot, subDir));

            StfAssert.AreEqual($"TestCaseDirectory correct for [{subDir}]", expected, actual);
        }
    }
}

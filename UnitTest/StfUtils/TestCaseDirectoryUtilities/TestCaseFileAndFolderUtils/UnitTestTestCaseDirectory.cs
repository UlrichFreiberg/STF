// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestTestCaseDirectory.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace UnitTest.TestCaseDirectoryUtilities.TestCaseFileAndFolderUtils
{
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Mir.Stf.Utilities.TestCaseDirectoryUtilities;

    /// <summary>
    /// The unit test test case directory.
    /// </summary>
    [TestClass]
    public class UnitTestTestCaseDirectory : UnitTestScriptBase
    {
        /// <summary>
        /// The unit test test data root.
        /// </summary>
        private const string UnitTestTestDataRoot = @".\TestData\TestCaseDirectoryUtilities";

        /// <summary>
        /// The test test case directory exists.
        /// </summary>
        [TestMethod]
        public void TestTestCaseDirectoryExists()
        {
            var testCaseFileAndFolderUtils = new TestCaseFileAndFolderUtils(4001, UnitTestTestDataRoot);
            var actual = testCaseFileAndFolderUtils.TestCaseDirectory;
            var expected = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), UnitTestTestDataRoot, "Tc4001"));

            StfAssert.AreEqual("TestCaseDirectory exists", expected, actual);
            StfAssert.AreEqual("TestCaseID", 4001, testCaseFileAndFolderUtils.TestCaseId);
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
    }
}

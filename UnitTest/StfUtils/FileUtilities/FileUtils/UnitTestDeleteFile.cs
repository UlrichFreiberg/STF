// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestDeleteFile.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the UnitTestDeleteFile type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace UnitTest.FileUtilities.FileUtils
{
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Mir.Stf;
    using Mir.Stf.Utilities.StfTestUtilities;

    /// <summary>
    /// The unit test delete file.
    /// </summary>
    [TestClass]
    public class UnitTestDeleteFile : StfTestScriptBase
    {
        /// <summary>
        /// Gets or sets the stf test utils.
        /// </summary>
        private StfTestUtils StfTestUtils { get; set; }

        /// <summary>
        /// The test initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            StfTestUtils = new StfTestUtils(4502);
        }

        /// <summary>
        /// The test delete file.
        /// </summary>
        [TestMethod]
        public void TestDeleteFile()
        {
            HelperDeleteFile(@"temp\Nope.txt", "Not existing file - path correct");
            HelperDeleteFile(@"temp\FolderNotExist\Nope.txt", "Not existing file - path incorrect");
            HelperDeleteFile(@"QQQ:\temp\FolderNotExist\Nope.txt", "Not existing file - path incorrect");

            HelperDeleteFile(@"temp\CreateFirstDel.txt", "Existing file(create first) - path correct", true, true);

            // Usual test
            HelperDeleteFile(null, "path is null");
            HelperDeleteFile(string.Empty, "path is empty");
        }

        /// <summary>
        /// The helper delete file.
        /// </summary>
        /// <param name="filename">
        /// The filename.
        /// </param>
        /// <param name="testComment">
        /// The test comment.
        /// </param>
        /// <param name="expected">
        /// The expected.
        /// </param>
        /// <param name="createFileFirst">
        /// The create file first.
        /// </param>
        private void HelperDeleteFile(string filename, string testComment, bool expected = true, bool createFileFirst = false)
        {
            var rootedFileName = Path.IsPathRooted(filename)
                               ? filename
                               : StfTestUtils.GetTestCaseRootFilePath(filename, false);
            StfLogger.LogHeader(testComment);

            CreateFileUtilsTestFile(rootedFileName, createFileFirst);

            var actual = StfTestUtils.FileUtils.DeleteFile(rootedFileName);

            StfAssert.IsTrue(testComment, expected == actual);
        }

        /// <summary>
        /// The create file utils test file.
        /// </summary>
        /// <param name="filename">
        /// The filename.
        /// </param>
        /// <param name="createFile">
        /// The create File.
        /// </param>
        /// <param name="content">
        /// The intended content of the file.
        /// </param>
        private void CreateFileUtilsTestFile(string filename, bool createFile, string content = "UnitTestStuff")
        {
            if (!createFile)
            {
                return;
            }

            var ok = StfTestUtils.FileUtils.WriteAllTextFile(filename, content);

            StfAssert.IsTrue("Was able to create the test file", ok);
        }
    }
}

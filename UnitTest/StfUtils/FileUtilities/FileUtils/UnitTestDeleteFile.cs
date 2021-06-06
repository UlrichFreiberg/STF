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
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The unit test delete file.
    /// </summary>
    [TestClass]
    public class UnitTestDeleteFile : UnitTestScriptBase
    {
        /// <summary>
        /// The test delete file.
        /// </summary>
        [TestMethod]
        public void TestDeleteFile()
        {
            HelperDeleteFile(@"C:\temp\Nope.txt", "Not existing file - path correct");
            HelperDeleteFile(@"C:\temp\FolderNotExist\Nope.txt", "Not existing file - path incorrect");
            HelperDeleteFile(@"QQQ:\temp\FolderNotExist\Nope.txt", "Not existing file - path incorrect");

            HelperDeleteFile(@"C:\temp\CreateFirstDel.txt", "Existing file(create first) - path correct", true, true);

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
            StfLogger.LogHeader(testComment);

            CreateFileUtilsTestFile(filename, createFileFirst);

            var actual = FileUtils.DeleteFile(filename);

            StfAssert.IsTrue(testComment, expected == actual);
        }
    }
}

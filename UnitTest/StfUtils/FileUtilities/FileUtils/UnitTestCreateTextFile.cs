// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestCreateTextFile.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace UnitTest.FileUtilities.FileUtils
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The unit test create text file.
    /// </summary>
    [TestClass]
    public class UnitTestCreateTextFile : UnitTestScriptBase
    {
        /// <summary>
        /// The test create text file.
        /// </summary>
        [TestMethod]
        public void TestCreateTextFile()
        {
            HelperTestCreateTextFile(@"C:\temp\CreateTextFile.txt", "CreateTextFile - path correct", true);

            // Errors related to invalid file names
            HelperTestCreateTextFile(@"C:\temp\FolderNotExist\Nope.txt", "Folder not exist", false);
            HelperTestCreateTextFile(@"QQQ:\temp\FolderNotExist\Nope.txt", "path incorrect format", false);

            // File overwrite tests
            HelperTestCreateTextFile(@"C:\temp\CreateFirst.txt", "Overwrite Existing file(create first) - path correct", true, true);

            // Usual string weirdos
            HelperTestCreateTextFile(null, "path is null", false);
            HelperTestCreateTextFile(string.Empty, "path is empty", false);
        }

        /// <summary>
        /// The helper_ test create text file.
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
        private void HelperTestCreateTextFile(string filename, string testComment, bool expected, bool createFileFirst = false)
        {
            if (createFileFirst)
            {
                var ok = FileUtils.WriteAllTextFile(filename, "UnitTestStuff");

                StfAssert.IsTrue("Was able to create the test file", ok);
            }

            var streamWriter = FileUtils.CreateText(filename);
            var actual = streamWriter != null;

            StfAssert.IsTrue(testComment, expected == actual);
        }
    }
}

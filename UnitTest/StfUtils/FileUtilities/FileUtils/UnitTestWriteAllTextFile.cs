// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestWriteAllTextFile.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the UnitTestWriteAllTextFile type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace UnitTest.FileUtilities.FileUtils
{
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Mir.Stf;
    using Mir.Stf.Utilities.StfTestUtilities;

    /// <summary>
    /// The unit test write all text file.
    /// </summary>
    [TestClass]
    public class UnitTestWriteAllTextFile : StfTestScriptBase
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
            StfTestUtils = new StfTestUtils(4501);
        }

        /// <summary>
        /// The test write all text file.
        /// </summary>
        [TestMethod]
        public void TestWriteAllTextFile()
        {
            const string StandardTextToWrite = "Sfu text to write";

            HelperWriteAllTextFile(@"temp\wallText1.txt", StandardTextToWrite, true);
            HelperWriteAllTextFile(@"temp\emptyText1.txt", string.Empty, true);
            HelperWriteAllTextFile(@"temp\nullText1.txt", null, true);

            HelperWriteAllTextFile(@"QQ:\temp\baddriveText1.txt", StandardTextToWrite, false);
            HelperWriteAllTextFile(@"temp\folderNotExists\noFolderText1.txt", StandardTextToWrite, false);
            HelperWriteAllTextFile(@"temp\!\noFolderText1.txt", StandardTextToWrite, false);

            // Usual test
            HelperWriteAllTextFile(null, "path is null", false);
            HelperWriteAllTextFile(string.Empty, "path is empty", false);
        }

        /// <summary>
        /// The helper_ write all text file.
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
        private void HelperWriteAllTextFile(string filename, string testComment, bool expected)
        {
            var rootedFileName = Path.IsPathRooted(filename)
                                     ? filename
                                     : StfTestUtils.GetTestCaseRootFilePath(filename, false);
            var actual = StfTestUtils.FileUtils.WriteAllTextFile(rootedFileName, testComment);

            StfAssert.AreEqual(testComment, expected, actual);
        }
    }
}

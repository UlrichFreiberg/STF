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
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Mir.Stf;
    using Mir.Stf.Utilities.FileUtilities;
    using Mir.Stf.Utilities.StfTestUtilities;

    /// <summary>
    /// The unit test create text file.
    /// </summary>
    [TestClass]
    public class UnitTestCreateTextFile : StfTestScriptBase
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
        /// The test create text file.
        /// </summary>
        [TestMethod]
        public void TestCreateTextFile()
        {
            HelperTestCreateTextFile(@"temp\CreateTextFile.txt", "CreateTextFile - path correct", true);

            // Errors related to invalid file names
            HelperTestCreateTextFile(@"temp\FolderNotExist\Nope.txt", "Folder not exist", false);
            HelperTestCreateTextFile(@"QQQ:\temp\FolderNotExist\Nope.txt", "path incorrect format", false);

            // File overwrite tests
            HelperTestCreateTextFile(@"temp\CreateFirst.txt", "Overwrite Existing file(create first) - path correct", true, true);

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
            var rootedFileName = Path.IsPathRooted(filename)
                                     ? filename
                                     : StfTestUtils.GetTestCaseRootFilePath(filename, false);

            var fileUtils = new FileUtils();

            if (createFileFirst)
            {
                var ok = fileUtils.WriteAllTextFile(rootedFileName, "UnitTestStuff");

                StfAssert.IsTrue("Was able to create the test file", ok);
            }

            var streamWriter = fileUtils.CreateText(rootedFileName);
            var actual = streamWriter != null;

            StfAssert.IsTrue(testComment, expected == actual);
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestGetCleanFilecontent.cs" company="Mir Software">
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

    using Mir.Stf.Utilities.FileUtilities;

    /// <summary>
    /// The unit test get clean file content.
    /// </summary>
    [TestClass]
    public class UnitTestGetCleanFilecontent
    {
        /// <summary>
        /// The test get clean filecontent.
        /// </summary>
        [TestMethod]
        public void TestGetCleanFilecontent()
        {
            HelperGetCleanFilecontent(null, "path is null", true);
            HelperGetCleanFilecontent(string.Empty, "path is empty", true);

            HelperGetCleanFilecontent("File With Header", "FileWithComments.txt");
            HelperGetCleanFilecontent("File With no Header", "FileWithCommentsNoHeader.txt");
        }

        /// <summary>
        /// The helper get clean filecontent.
        /// </summary>
        /// <param name="testStep">
        /// The test step.
        /// </param>
        /// <param name="inputFilename">
        /// The input filename.
        /// </param>
        private void HelperGetCleanFilecontent(string testStep, string inputFilename)
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
            //// TODO: this.StfAssert.FilesDoNotDiffer(testStep, tempExpectedPath, tempActualPath);
        }
    }
}

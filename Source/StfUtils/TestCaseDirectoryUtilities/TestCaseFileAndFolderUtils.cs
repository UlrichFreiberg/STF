// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestCaseFileAndFolderUtils.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The test case file and folder utils.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.TestCaseDirectoryUtilities
{
    using System.IO;

    /// <summary>
    /// The test case file and folder utils.
    /// </summary>
    public class TestCaseFileAndFolderUtils
    {
        /// <summary>
        /// Backing field
        /// </summary>
        private string testCaseResultsDirectory;

        /// <summary>
        /// Backing field
        /// </summary>
        private string testCaseTempDirectory;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseFileAndFolderUtils"/> class.
        /// </summary>
        /// <param name="testCaseId">
        /// The test case id.
        /// </param>
        /// <param name="rootFolder">
        /// The root folder.
        /// </param>
        public TestCaseFileAndFolderUtils(int testCaseId, string rootFolder)
        {
            DirectoryUtils = new DirectoryUtils(rootFolder);
            TestCaseId = testCaseId;
            TestCaseDirectory = DirectoryUtils.GetTestCaseDirectoryPath(TestCaseId);
        }

        /// <summary>
        /// Gets the test case directory.
        /// </summary>
        public string TestCaseDirectory { get; }

        /// <summary>
        /// Gets the directory utils.
        /// </summary>
        public DirectoryUtils DirectoryUtils { get; }

        /// <summary>
        /// Gets the test case id.
        /// </summary>
        public int TestCaseId { get; }

        /// <summary>
        /// The test case result directory.
        /// </summary>
        public string TestCaseResultsDirectory => testCaseResultsDirectory ?? (testCaseResultsDirectory = string.IsNullOrEmpty(TestCaseDirectory) ? null : Path.Combine(TestCaseDirectory, "Results"));

        /// <summary>
        /// The test case temp directory.
        /// </summary>
        public string TestCaseTempDirectory => testCaseTempDirectory ?? (testCaseTempDirectory = string.IsNullOrEmpty(TestCaseDirectory) ? null : Path.Combine(TestCaseDirectory, "Temp"));

        /// <summary>
        /// The get test case root file path.
        /// </summary>
        /// <param name="relativeFilePath">
        /// The relative file path.
        /// </param>
        /// <param name="expectToExist">
        /// Indication of whether the file is expected to exist or not
        /// Needed when composing output file names (they do not exists before the test tools is run)
        /// </param>
        /// <returns>
        /// file path or null if expected to exist but not found
        /// </returns>
        public string GetTestCaseRootFilePath(string relativeFilePath, bool expectToExist = true)
        {
            var files = GetTestCaseRootFilePaths(relativeFilePath);
            var retVal = CheckTestCaseFilePath(files, TestCaseDirectory, relativeFilePath, expectToExist);

            return retVal;
        }

        /// <summary>
        /// The get test case root file paths.
        /// </summary>
        /// <param name="relativeFilePath">
        /// The relative file path.
        /// </param>
        /// <returns>
        /// String array of file paths
        /// </returns>
        public string[] GetTestCaseRootFilePaths(string relativeFilePath)
        {
            // This will also handle the expanding of wildcards
            var retVal = SafeDirectoryDotGetFiles(TestCaseDirectory, relativeFilePath);

            return retVal;
        }

        /// <summary>
        /// The get test case result file path.
        /// </summary>
        /// <param name="relativeFilePath">
        /// The relative file path.
        /// </param>
        /// <param name="expectToExist">
        /// Indication of whether the file is expected to exist or not
        /// Needed when composing output file names (they do not exists before the test tools is run)
        /// </param>
        /// <returns>
        /// file path or null if expected to exist but not found
        /// </returns>
        public string GetTestCaseResultsFilePath(string relativeFilePath, bool expectToExist = true)
        {
            var files = GetTestCaseResultsFilePaths(relativeFilePath);
            var retVal = CheckTestCaseFilePath(files, TestCaseResultsDirectory, relativeFilePath, expectToExist);

            return retVal;
        }

        /// <summary>
        /// The get all files in the test case result file directory.
        /// </summary>
        /// <param name="relativeFilePath">
        /// The relative file path.
        /// </param>
        /// <returns>
        /// String array of file paths
        /// </returns>
        public string[] GetTestCaseResultsFilePaths(string relativeFilePath)
        {
            // This will also handle the expanding of wildcards
            var retVal = SafeDirectoryDotGetFiles(TestCaseResultsDirectory, relativeFilePath);

            return retVal;
        }

        /// <summary>
        /// The get test case temp file path.
        /// </summary>
        /// <param name="relativeFilePath">
        /// The relative file path.
        /// </param>
        /// <param name="expectToExist">
        /// Indication of whether the file is expected to exist or not
        /// Needed when composing output file names (they do not exists before the test tools is run)
        /// </param>
        /// <returns>
        /// file path or null if expected to exist but not found
        /// </returns>
        public string GetTestCaseTempFilePath(string relativeFilePath, bool expectToExist = true)
        {
            var files = GetTestCaseTempFilePaths(relativeFilePath);
            var retVal = CheckTestCaseFilePath(files, TestCaseTempDirectory, relativeFilePath, expectToExist);

            return retVal;
        }

        /// <summary>
        /// The get test case temp file paths.
        /// </summary>
        /// <param name="relativeFilePath">
        /// The relative file path.
        /// </param>
        /// <returns>
        /// String array of file paths
        /// </returns>
        public string[] GetTestCaseTempFilePaths(string relativeFilePath)
        {
            // This will also handle the expanding of wildcards
            var retVal = SafeDirectoryDotGetFiles(TestCaseTempDirectory, relativeFilePath);

            return retVal;
        }

        /// <summary>
        /// The safe directory dot get files.
        /// </summary>
        /// <param name="baseDirectoryPath">
        /// The base directory path.
        /// </param>
        /// <param name="relativeFilePath">
        /// The relative file path.
        /// </param>
        /// <returns>
        /// Array of files found - Empty array if some error, or none found
        /// </returns>
        private string[] SafeDirectoryDotGetFiles(string baseDirectoryPath, string relativeFilePath)
        {
            if (string.IsNullOrEmpty(baseDirectoryPath))
            {
                return new string[0];
            }

            if (string.IsNullOrEmpty(relativeFilePath))
            {
                // is the relative path is zero - someone probably meant the baseDirectory
                return new[] { baseDirectoryPath };
            }

            var retVal = Directory.GetFiles(baseDirectoryPath, relativeFilePath);

            return retVal;
        }

        /// <summary>
        /// Check the outcome of finding files.
        /// </summary>
        /// <param name="files">
        /// The files.
        /// </param>
        /// <param name="testCaseDirectoryRoot">
        /// The test case directory root.
        /// </param>
        /// <param name="localPath">
        /// The local path.
        /// </param>
        /// <param name="expectToExist">
        /// Indication of whether the file is expected to exist or not
        /// Needed when composing output file names (they do not exists before the test tools is run)
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string CheckTestCaseFilePath(string[] files, string testCaseDirectoryRoot, string localPath, bool expectToExist)
        {
            switch (files.Length)
            {
                case 0 when expectToExist:
                    Logger.LogError($"Expected to only find one file - found none - for [{localPath}] ");
                    return null;

                case 0:
                    {
                        // if the file is not expected to exist then GetTestCase*FilePaths
                        // would not have found any - need to compose the correct file name
                        var absoluteLocalFilePath = Path.Combine(testCaseDirectoryRoot, localPath);

                        return absoluteLocalFilePath;
                    }

                case 1:
                    {
                        var retVal = files[0];

                        if (!expectToExist || File.Exists(retVal) || Directory.Exists(retVal))
                        {
                            return retVal;
                        }

                        Logger.LogError($"TestCaseFile [{retVal}] was expected to exists - but it does not");
                        return null;
                    }

                default:
                    Logger.LogError($"Expected to only find one file - found more than one - found {files.Length}) files for [{localPath}]");
                    return null;
            }
        }
    }
}

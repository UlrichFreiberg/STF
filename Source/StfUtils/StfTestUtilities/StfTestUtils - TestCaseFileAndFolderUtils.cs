// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfTestUtils - TestCaseFileAndFolderUtils.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The Stf Test utilities.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.StfTestUtilities
{
    using Mir.Stf.Utilities.TestCaseDirectoryUtilities;

    /// <summary>
    /// The stf test utilities.
    /// </summary>
    public partial class StfTestUtils
    {
        /// <summary>
        /// Backing field for TestCaseFileAndFolderUtils
        /// </summary>
        private TestCaseFileAndFolderUtils testCaseFileAndFolderUtils;

        /// <summary>
        /// Gets the TestCaseFileAndFolderUtils
        /// </summary>
        public TestCaseFileAndFolderUtils TestCaseFileAndFolderUtils
        {
            get
            {
                var retVal = testCaseFileAndFolderUtils ?? (testCaseFileAndFolderUtils = new TestCaseFileAndFolderUtils(TestCaseId, RootFolder));

                return retVal;
            }
        }

        /// <summary>
        /// The get test case root file path.
        /// </summary>
        /// <param name="relativeFilePath">
        /// The relative file path.
        /// </param>
        /// <param name="expectToExist">
        /// The expect to exist.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetTestCaseRootFilePath(string relativeFilePath, bool expectToExist = true)
        {
            var retVal = TestCaseFileAndFolderUtils.GetTestCaseRootFilePath(relativeFilePath, expectToExist);

            return retVal;
        }

        /// <summary>
        /// The get test case root file paths.
        /// </summary>
        /// <param name="relativeFilePath">
        /// The relative file path.
        /// </param>
        /// <returns>
        /// Array of file paths.
        /// </returns>
        public string[] GetTestCaseRootFilePaths(string relativeFilePath)
        {
            var retVal = TestCaseFileAndFolderUtils.GetTestCaseRootFilePaths(relativeFilePath);

            return retVal;
        }

        /// <summary>
        /// The get test case temp file path.
        /// </summary>
        /// <param name="relativeFilePath">
        /// The relative file path.
        /// </param>
        /// <param name="expectToExist">
        /// The expect to exist.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetTestCaseTempFilePath(string relativeFilePath, bool expectToExist = true)
        {
            var retVal = TestCaseFileAndFolderUtils.GetTestCaseTempFilePath(relativeFilePath, expectToExist);

            return retVal;
        }

        /// <summary>
        /// The get test case temp file paths.
        /// </summary>
        /// <param name="relativeFilePath">
        /// The relative file path.
        /// </param>
        /// <returns>
        /// Array of file paths.
        /// </returns>
        public string[] GetTestCaseTempFilePaths(string relativeFilePath)
        {
            var retVal = TestCaseFileAndFolderUtils.GetTestCaseTempFilePaths(relativeFilePath);

            return retVal;
        }

        /// <summary>
        /// The get test case result file path.
        /// </summary>
        /// <param name="relativeFilePath">
        /// The relative file path.
        /// </param>
        /// <param name="expectToExist">
        /// The expect to exist.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetTestCaseResultFilePath(string relativeFilePath, bool expectToExist = true)
        {
            var retVal = TestCaseFileAndFolderUtils.GetTestCaseResultsFilePath(relativeFilePath, expectToExist);

            return retVal;
        }

        /// <summary>
        /// The get test case result file paths.
        /// </summary>
        /// <param name="relativeFilePath">
        /// The relative file path.
        /// </param>
        /// <returns>
        /// Array of file paths.
        /// </returns>
        public string[] GetTestCaseResultFilePaths(string relativeFilePath)
        {
            var retVal = TestCaseFileAndFolderUtils.GetTestCaseResultsFilePaths(relativeFilePath);

            return retVal;
        }
    }
}

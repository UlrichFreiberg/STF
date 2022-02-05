// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfTestUtils.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The Stf Test utilities.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.StfTestUtilities
{
    using System.Collections.Generic;
    using System.IO;

    using Mir.Stf.Utilities.FileUtilities;
    using Mir.Stf.Utilities.RetryerUtilities;
    using Mir.Stf.Utilities.StringTransformationUtilities;
    using Mir.Stf.Utilities.TestCaseDirectoryUtilities;

    /// <summary>
    /// The stf test utilities.
    /// </summary>
    public partial class StfTestUtils
    {
        /// <summary>
        /// Backing field
        /// </summary>
        private FileUtils fileUtils;

        /// <summary>
        /// Backing field
        /// </summary>
        private RetryerUtils retryerUtils;

        /// <summary>
        /// Backing field
        /// </summary>
        private StringTransformationUtils stringTransformationUtils;

        /// <summary>
        /// Backing field
        /// </summary>
        private TestCaseDirectoryCacheUtils testCaseDirectoryCacheUtils;

        /// <summary>
        /// Backing field for TestCaseStepFilePathUtils
        /// </summary>
        private TestCaseStepFilePathUtils testCaseStepFilePathUtils;

        /// <summary>
        /// Initializes a new instance of the <see cref="StfTestUtils"/> class.
        /// </summary>
        /// <param name="testCaseId">
        /// The test Case Id.
        /// </param>
        /// <param name="rootFolder">
        /// The root folder.
        /// </param>
        public StfTestUtils(int testCaseId, string rootFolder = @".\TestData")
        {
            TestCaseId = testCaseId;
            RootFolder = Path.IsPathRooted(rootFolder)
                       ? rootFolder
                       : Path.GetFullPath(rootFolder);
        }

        /// <summary>
        /// Gets the root folder.
        /// </summary>
        public string RootFolder { get; }

        /// <summary>
        /// Gets the test case id.
        /// </summary>
        public int TestCaseId { get; }

        /// <summary>
        /// The test case directory.
        /// </summary>
        public string TestCaseDirectory => TestCaseFileAndFolderUtils.TestCaseDirectory;

        /// <summary>
        /// Gets the string transformation utils.
        /// </summary>
        public StringTransformationUtils StringTransformationUtils
        {
            get
            {
                var retVal = stringTransformationUtils ?? (stringTransformationUtils = new StringTransformationUtils());

                return retVal;
            }
        }

        /// <summary>
        /// Gets the file utils.
        /// </summary>
        public FileUtils FileUtils
        {
            get
            {
                var retVal = fileUtils ?? (fileUtils = new FileUtils());

                return retVal;
            }
        }

        /// <summary>
        /// Gets or sets the file utils.
        /// </summary>
        public RetryerUtils RetryerUtils
        {
            get
            {
                var retVal = retryerUtils ?? (retryerUtils = new RetryerUtils());

                return retVal;
            }

            set => retryerUtils = value;
        }

        /// <summary>
        /// Gets or sets the file utils.
        /// </summary>
        public TestCaseDirectoryCacheUtils TestCaseDirectoryCacheUtils
        {
            get
            {
                var retVal = testCaseDirectoryCacheUtils ?? (testCaseDirectoryCacheUtils = new TestCaseDirectoryCacheUtils(RootFolder));

                return retVal;
            }

            set => testCaseDirectoryCacheUtils = value;
        }

        /// <summary>
        /// Gets the TestCaseStepFilePathUtils
        /// </summary>
        /// <param name="fileNameFilters">
        /// The file Name Filters.
        /// </param>
        /// <param name="ignoreFileExtensions">
        /// The ignore File Extensions.
        /// </param>
        /// <returns>
        /// The <see cref="TestCaseStepFilePathUtils"/>.
        /// </returns>
        public TestCaseStepFilePathUtils TestCaseStepFilePathUtils(string[] fileNameFilters, bool ignoreFileExtensions = false)
        {
            if (testCaseStepFilePathUtils == null)
            {
                testCaseStepFilePathUtils = new TestCaseStepFilePathUtils(TestCaseDirectory, fileNameFilters, ignoreFileExtensions);
            }

            var retVal = testCaseStepFilePathUtils;

            return retVal;
        }

        /// <summary>
        /// The get test case folder paths from cache.
        /// Use this one for unit tests 
        /// </summary>
        /// <returns>
        /// The list of test cases folder paths from the cache>.
        /// </returns>
        public string[] GetTestCaseFolderPathsFromCache()
        {
            var retVal = TestCaseDirectoryCacheUtils.GetTestCaseFolderPaths();

            return retVal;
        }

        /// <summary>
        /// The get test case ids from cache.
        /// Use this one for unit tests 
        /// </summary>
        /// <returns>
        /// The list of test case Ids from the cache>.
        /// </returns>
        public List<int> GetTestCaseIdsFromCache()
        {
            var retVal = TestCaseDirectoryCacheUtils.GetTestCaseIdsFromCache();

            return retVal;
        }
    }
}

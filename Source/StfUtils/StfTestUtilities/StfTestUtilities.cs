// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfTestUtilities.cs" company="Mir Software">
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

    using Mir.Stf.Utilities.TestCaseDirectoryUtilities;

    /// <summary>
    /// The stf test utilities.
    /// </summary>
    public class StfTestUtilities : StfUtilsBase 
    {
        /// <summary>
        /// The default root folder.
        /// </summary>
        /// ToDO: should this somehow be initialized from a config file.
        private static readonly string DefaultRootFolder = @"C:\Temp\StfTestTool\TestData\StfTestUtilities";

        /// <summary>
        /// Initializes a new instance of the <see cref="StfTestUtilities"/> class.
        /// </summary>
        /// <param name="rootFolder">
        /// The root folder.
        /// </param>
        public StfTestUtilities(string rootFolder) : base()
        {
            RootFolder = rootFolder;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StfTestUtilities"/> class.
        /// </summary>
        public StfTestUtilities() : this(DefaultRootFolder)
        {
        }

        /// <summary>
        /// Gets the root folder.
        /// </summary>
        public string RootFolder { get; }

        /// <summary>
        /// The get test case folder paths from cache.
        /// Use this one for unit tests 
        /// </summary>
        /// <returns>
        /// The list of test cases folder paths from the cache>.
        /// </returns>
        public List<string> GetTestCaseFolderPathsFromCache()
        {
            var directoryUtils = new DirectoryUtils(RootFolder);
            var retVal = directoryUtils.GetTestCaseFolderPathsFromCache();
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
            var directoryUtils = new DirectoryUtils(RootFolder);
            var retVal = directoryUtils.GetTestCaseIdsFromCache();
            return retVal;
        }
    }
}

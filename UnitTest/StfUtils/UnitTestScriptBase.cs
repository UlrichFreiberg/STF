// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestScriptBase.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the UnitTestScriptBase type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace UnitTest
{
    using Mir.Stf;
    using Mir.Stf.Utilities.FileUtilities;
    using Mir.Stf.Utilities.Interfaces;
    using Mir.Stf.Utilities.StfTestUtilities;
    using Mir.Stf.Utilities.StringTransformationUtilities;
    using Mir.Stf.Utilities.TestCaseDirectoryUtilities;

    /// <summary>
    /// The unit test utilities.
    /// All StfUtils classes should never be new-ed up - they should be reached by using this class
    /// In that way we test the interfaces are up to date 
    /// </summary>
    public class UnitTestScriptBase : StfTestScriptBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitTestScriptBase"/> class.
        /// </summary>
        /// <param name="testCaseId">
        /// The test case id.
        /// </param>
        /// <param name="rootFolder">
        /// The root folder.
        /// </param>
        public UnitTestScriptBase(int testCaseId, string rootFolder)
        {
            StfTestUtils = new StfTestUtils(testCaseId, rootFolder);
        }

        /// <summary>
        /// The string transformation utils.
        /// </summary>
        public StringTransformationUtils StringTransformationUtils => StfTestUtils.StringTransformationUtils;

        /// <summary>
        /// The file utils.
        /// </summary>
        public FileUtils FileUtils => StfTestUtils.FileUtils;

        /// <summary>
        /// The test case file and folder utils.
        /// </summary>
        public TestCaseFileAndFolderUtils TestCaseFileAndFolderUtils => StfTestUtils.TestCaseFileAndFolderUtils;

        /// <summary>
        /// Gets the stf test utils.
        /// </summary>
        private StfTestUtils StfTestUtils
        {
            get;
        }
    }
}

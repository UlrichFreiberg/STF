// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestStfTestUtils.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the UnitTestCalcFunction type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.StfTestUtilities
{
    using Mir.Stf;
    using Mir.Stf.Utilities.StfTestUtilities;

    /// <summary>
    /// The unit test Stf Test Utilities function.
    /// </summary>
    [TestClass]
    public class UnitTestStfTestUtils : StfTestScriptBase
    {
        /// <summary>
        /// The test Stf Test Utilities.
        /// </summary>
        [TestMethod]
        public void TestStfTestTestCases()
        {
            HelperStfTestUtils("number of unit test cases", 7010, 0);
            HelperStfTestUtils("number of unit test cases", 7011, 1);
            HelperStfTestUtils("number of unit test cases", 7012, 2);
        }

        /// <summary>
        /// The helper stf test Utilities.
        /// </summary>
        /// <param name="testStepDescription">
        /// The testStepDescription.
        /// </param>
        /// <param name="testCaseId">
        /// The test Case Id.
        /// </param>
        /// <param name="expected">
        /// The expected.
        /// </param>
        private void HelperStfTestUtils(string testStepDescription, int testCaseId, int expected)
        {
            var stfTestUtils = new StfTestUtils(testCaseId);
            var newRootFolderPath = stfTestUtils.TestCaseDirectory;
            var stfTestUtils2 = new StfTestUtils(testCaseId, newRootFolderPath);
            var actual = stfTestUtils2.GetTestCaseFolderPathsFromCache();

            StfAssert.AreEqual(testStepDescription, expected, actual.Length);
        }
    }
}

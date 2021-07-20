// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestNumberOfSteps.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The unit test number of steps.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace UnitTest.FileUtilities.TemplateConfigUtils
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Mir.Stf.Utilities.FileUtilities.TemplateConfig;
    using Mir.Stf.Utilities.TestCaseDirectoryUtilities;

    /// <summary>
    /// The unit test number of steps.
    /// </summary>
    [TestClass]
    public class UnitTestNumberOfSteps : UnitTestScriptBase
    {
        /// <summary>
        /// The unit test test data root.
        /// </summary>
        private const string UnitTestTestDataRoot = @".\TestData\FileUtilities\TemplateConfigUtils";

        /// <summary>
        /// The test get test data value simple.
        /// </summary>
        [TestMethod]
        public void TestNumberOfStepsSimple()
        {
            HelperNumberOfSteps("One Template, One Config", 4011, 1);
            HelperNumberOfSteps("One Template, Two Config", 4012, 2);
            HelperNumberOfSteps("Two Template, One Config", 4021, 2);
            HelperNumberOfSteps("Two Template, Two Config", 4022, 2);
            HelperNumberOfSteps("Three Template, Three Config", 4033, 3);
        }

        /// <summary>
        /// The helper number of steps.
        /// </summary>
        /// <param name="testStep">
        /// The test step.
        /// </param>
        /// <param name="testCaseId">
        /// The test case id.
        /// </param>
        /// <param name="expected">
        /// The expected.
        /// </param>
        private void HelperNumberOfSteps(string testStep, int testCaseId, int expected)
        {
            var testCaseFileAndFolderUtils = new TestCaseFileAndFolderUtils(testCaseId, UnitTestTestDataRoot);
            var templateConfigUtils = new TemplateConfigUtils(testCaseFileAndFolderUtils.TestCaseDirectory);
            var actual = templateConfigUtils.NumberOfSteps;

            StfAssert.AreEqual(testStep, expected, actual);
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestNumberOfSteps.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The unit test number of steps.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace UnitTest.FileUtilities.TestCaseStepFilePathUtils
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Mir.Stf.Utilities.FileUtilities;
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
        private const string UnitTestTestDataRoot = @".\TestData\FileUtilities\TestCaseStepFilePathUtils";

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
        /// The test file paths simple.
        /// </summary>
        [TestMethod]
        public void TestFilePathsSimple()
        {
            StfLogger.LogHeader("One Template, One Config");
            HelperFilePaths("One Template, One Config", 4011, "Template.txt", 1, "Template.txt");
            HelperFilePaths("One Template, One Config", 4011, "Config.txt", 1, "Config.txt");

            StfLogger.LogHeader("One Template, Two Config");
            HelperFilePaths("One Template, Two Config", 4012, "Template.txt", 1, "Template.txt");
            HelperFilePaths("One Template, Two Config", 4012, "Template.txt", 2, "Template.txt");
            HelperFilePaths("One Template, Two Config", 4012, "Config.txt", 1, "Config.txt");
            HelperFilePaths("One Template, Two Config", 4012, "Config.txt", 2, "Config2.txt");

            StfLogger.LogHeader("Two Template, One Config");
            HelperFilePaths("Two Template, One Config", 4021, "Template.txt", 1, "Template.txt");
            HelperFilePaths("Two Template, One Config", 4021, "Template.txt", 2, "Template2.txt");
            HelperFilePaths("Two Template, One Config", 4021, "Config.txt", 1, "Config.txt");
            HelperFilePaths("Two Template, One Config", 4021, "Config.txt", 2, "Config.txt");

            StfLogger.LogHeader("Two Template, Two Config");
            HelperFilePaths("Two Template, Two Config", 4022, "Template.txt", 1, "Template.txt");
            HelperFilePaths("Two Template, Two Config", 4022, "Template.txt", 2, "Template2.txt");
            HelperFilePaths("Two Template, Two Config", 4022, "Config.txt", 1, "Config.txt");
            HelperFilePaths("Two Template, Two Config", 4022, "Config.txt", 2, "Config2.txt");

            StfLogger.LogHeader("Three Template, Three Config");
            HelperFilePaths("Three Template, Three Config", 4033, "Template.txt", 1, "Template.txt");
            HelperFilePaths("Three Template, Three Config", 4033, "Template.txt", 2, "Template2.txt");
            HelperFilePaths("Three Template, Three Config", 4033, "Template.txt", 3, "Template3.txt");
            HelperFilePaths("Three Template, Three Config", 4033, "Config.txt", 1, "Config.txt");
            HelperFilePaths("Three Template, Three Config", 4033, "Config.txt", 2, "Config2.txt");
            HelperFilePaths("Three Template, Three Config", 4033, "Config.txt", 3, "Config3.txt");

            StfLogger.LogHeader("Four Steps, Two Template, Three Config");
            HelperFilePaths("Four Steps, Two Template, Three Config", 4423, "Template.txt", 1, "Template.txt");
            HelperFilePaths("Four Steps, Two Template, Three Config", 4423, "Template.txt", 2, "Template.txt");
            HelperFilePaths("Four Steps, Two Template, Three Config", 4423, "Template.txt", 3, "Template3.txt");
            HelperFilePaths("Four Steps, Two Template, Three Config", 4423, "Template.txt", 4, "Template3.txt");
            HelperFilePaths("Four Steps, Two Template, Three Config", 4423, "Config.txt", 1, "Config.txt");
            HelperFilePaths("Four Steps, Two Template, Three Config", 4423, "Config.txt", 2, "Config2.txt");
            HelperFilePaths("Four Steps, Two Template, Three Config", 4423, "Config.txt", 3, "Config2.txt");
            HelperFilePaths("Four Steps, Two Template, Three Config", 4423, "Config.txt", 4, "Config4.txt");

            StfLogger.LogHeader("Four Steps, Three Template, Two Config");
            HelperFilePaths("Four Steps, Three Template, Two Config", 4432, "Template.txt", 1, "Template.txt");
            HelperFilePaths("Four Steps, Three Template, Two Config", 4432, "Template.txt", 2, "Template2.txt");
            HelperFilePaths("Four Steps, Three Template, Two Config", 4432, "Template.txt", 3, "Template3.txt");
            HelperFilePaths("Four Steps, Three Template, Two Config", 4432, "Template.txt", 4, "Template3.txt");
            HelperFilePaths("Four Steps, Three Template, Two Config", 4432, "Config.txt", 1, "Config.txt");
            HelperFilePaths("Four Steps, Three Template, Two Config", 4432, "Config.txt", 2, "Config.txt");
            HelperFilePaths("Four Steps, Three Template, Two Config", 4432, "Config.txt", 3, "Config.txt");
            HelperFilePaths("Four Steps, Three Template, Two Config", 4432, "Config.txt", 4, "Config4.txt");

            StfLogger.LogHeader("Four Steps File Text, Three Template, Three Config");
            HelperFilePaths("Four Steps File Text, Three Template, Three Config", 4433, "Template.txt", 1, "Template.txt");
            HelperFilePaths("Four Steps File Text, Three Template, Three Config", 4433, "Template.txt", 2, "Template2_Uscore3.txt");
            HelperFilePaths("Four Steps File Text, Three Template, Three Config", 4433, "Template.txt", 3, "Template3 spaces 2.txt");
            HelperFilePaths("Four Steps File Text, Three Template, Three Config", 4433, "Template.txt", 4, "Template3 spaces 2.txt");
            HelperFilePaths("Four Steps File Text, Three Template, Three Config", 4433, "Config.txt", 1, "Config.txt");
            HelperFilePaths("Four Steps File Text, Three Template, Three Config", 4433, "Config.txt", 2, "Config2WithNoGap1.txt");
            HelperFilePaths("Four Steps File Text, Three Template, Three Config", 4433, "Config.txt", 3, "Config2WithNoGap1.txt");
            HelperFilePaths("Four Steps File Text, Three Template, Three Config", 4433, "Config.txt", 4, "Config4.extra.dots.12.txt");

        }

        /// <summary>
        /// The test file paths different extensions filters.
        /// </summary>
        [TestMethod]
        public void TestFilePathsDifferentExtensionsFilters()
        {
            StfLogger.LogHeader("TXT TXT XML, Four Template, One Config");
            HelperFilePaths("TXT TXT XML, Four Template, One Config", 4331, "Template.txt", 1, "Template.txt");
            HelperFilePaths("TXT TXT XML, Four Template, One Config", 4331, "Template.txt", 2, "Template2.txt");
            HelperFilePaths("TXT TXT XML, Four Template, One Config", 4331, "Template.txt", 3, "Template3.xml");
            HelperFilePaths("TXT TXT XML, Four Template, One Config", 4331, "Template.txt", 4, "Template4.txt");

        }

        /// <summary>
        /// The test file paths three file filters.
        /// </summary>
        [TestMethod]
        public void TestFilePathsThreeFileFilters()
        {
            StfLogger.LogHeader("One Template, One Config, One Tdv");
            HelperThreeFileFilters("One, One, one", 43111, "Template.txt", 1, "Template.txt");
            HelperThreeFileFilters("One, One, one", 43111, "Config.txt", 1, "Config.txt");
            HelperThreeFileFilters("One, One, one", 43111, "TestDataValues.txt", 1, "TestDataValues.txt");
        }

        /// <summary>
        /// The helper three file filters.
        /// </summary>
        /// <param name="testStep">
        /// The test step.
        /// </param>
        /// <param name="testCaseId">
        /// The test case id.
        /// </param>
        /// <param name="fileNameFilter">
        /// The file name filter.
        /// </param>
        /// <param name="step">
        /// The step.
        /// </param>
        /// <param name="expectedFilePath">
        /// The expected file path.
        /// </param>
        private void HelperThreeFileFilters(
            string testStep,
            int testCaseId,
            string fileNameFilter,
            int step,
            string expectedFilePath)
        {
            StfLogger.LogSubHeader(testStep);

            var testCaseFileAndFolderUtils = new TestCaseFileAndFolderUtils(testCaseId, UnitTestTestDataRoot);
            var fileNameFilters = new[] { "Template.txt", "Config.txt", "TestDataValues.txt" };
            var testCaseStepFilePathUtils = new TestCaseStepFilePathUtils(testCaseFileAndFolderUtils.TestCaseDirectory, fileNameFilters);
            var actual = testCaseStepFilePathUtils.GetFileNameForStep(fileNameFilter, step);

            StfAssert.AreEqual(testStep, expectedFilePath, actual);
        }

        /// <summary>
        /// The helper file paths.
        /// </summary>
        /// <param name="testStep">
        /// The test step.
        /// </param>
        /// <param name="testCaseId">
        /// The test case id.
        /// </param>
        /// <param name="fileNameFilter">
        /// The file name filter.
        /// </param>
        /// <param name="step">
        /// The step.
        /// </param>
        /// <param name="expectedFilePath">
        /// The expected file path.
        /// </param>
        private void HelperFilePaths(
        string testStep,
        int testCaseId,
        string fileNameFilter,
        int step,
        string expectedFilePath)
        {
            StfLogger.LogSubHeader(testStep);

            var testCaseFileAndFolderUtils = new TestCaseFileAndFolderUtils(testCaseId, UnitTestTestDataRoot);
            var fileNameFilters = new[] { "Template.txt", "Config.txt" };
            var testCaseStepFilePathUtils = new TestCaseStepFilePathUtils(testCaseFileAndFolderUtils.TestCaseDirectory, fileNameFilters);
            var actual = testCaseStepFilePathUtils.GetFileNameForStep(fileNameFilter, step);

            StfAssert.AreEqual(testStep, expectedFilePath, actual);
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
            var fileNameFilters = new[] { "Template.txt", "Config.txt" };
            var testCaseStepFilePathUtils = new TestCaseStepFilePathUtils(testCaseFileAndFolderUtils.TestCaseDirectory, fileNameFilters);
            var actual = testCaseStepFilePathUtils.NumberOfSteps;

            StfAssert.AreEqual(testStep, expected, actual);
        }
    }
}

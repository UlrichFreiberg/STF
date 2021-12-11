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
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;

    using Mir.Stf.Utilities.FileUtilities;
    using Mir.Stf.Utilities.TestCaseData;
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
        /// Backing field for TestCaseFileAndFolderUtils
        /// </summary>
        private TestCaseFileAndFolderUtils testCaseFileAndFolderUtils;

        /// <summary>
        /// Backing field for TestCaseStepFilePathUtils
        /// </summary>
        private TestCaseStepFilePathUtils testCaseStepFilePathUtils;

        /// <summary>
        /// Initializes a new instance of the <see cref="StfTestUtilities"/> class.
        /// </summary>
        /// <param name="rootFolder">
        /// The root folder.
        /// </param>
        public StfTestUtilities(string rootFolder)
        {
            RootFolder = rootFolder;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StfTestUtilities"/> class.
        /// </summary>
        /// <param name="rootFolder">
        /// The root folder.
        /// </param>
        /// <param name="testCaseId">
        /// The test case id.
        /// </param>
        public StfTestUtilities(string rootFolder, int testCaseId) : this(rootFolder)
        {
            TestCaseId = testCaseId;
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
        /// Gets the test case id.
        /// </summary>
        public int TestCaseId { get; }

        /// <summary>
        /// Gets the TestCaseFileAndFolderUtils
        /// </summary>
        public TestCaseFileAndFolderUtils TestCaseFileAndFolderUtils
        {
            get
            {
                if (TestCaseId == 0)
                {
                    LogError("TestCaseId may not be null when instantiating TestCaseFileAndFolderUtils");
                    return null;
                }

                var retVal = testCaseFileAndFolderUtils ?? (testCaseFileAndFolderUtils = new TestCaseFileAndFolderUtils(TestCaseId, RootFolder));

                return retVal;
            }
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
            var retVal = testCaseStepFilePathUtils ?? (testCaseStepFilePathUtils = new TestCaseStepFilePathUtils(
                                                           TestCaseFileAndFolderUtils.TestCaseDirectory,
                                                           fileNameFilters,
                                                           ignoreFileExtensions));
            return retVal;
        }

        /// <summary>
        /// Templifies all the steps in the test case
        /// </summary>
        /// <param name="fileNameFilters">
        /// The file Name Filters.
        /// </param>
        /// <param name="ignoreFileExtensions">
        /// The ignore File Extensions.
        /// </param>
        /// 
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool TemplifyAllSteps(string[] fileNameFilters, bool ignoreFileExtensions = false)
        {
            var retVal = false;
            var tcSFPU = testCaseStepFilePathUtils ?? (testCaseStepFilePathUtils = new TestCaseStepFilePathUtils(
                                                           TestCaseFileAndFolderUtils.TestCaseDirectory,
                                                           fileNameFilters,
                                                           ignoreFileExtensions));

            if (tcSFPU == null)
            {
                LogError("Failed to setup TestCaseStepFilePathUtils");
                retVal = false;
                return retVal;
            }

            var testCaseDirectoryDataUtils = new TestCaseDirectoryDataUtils(TestCaseFileAndFolderUtils, this.StringTransformationUtils);

            for (int stepNum = 1; stepNum <= testCaseStepFilePathUtils.NumberOfSteps; stepNum++)
            {
                var configFilePath = testCaseStepFilePathUtils.GetFilePathForStep(fileNameFilters[1], stepNum);
                if (string.IsNullOrEmpty(configFilePath))
                {
                    retVal = false;
                    return retVal;
                }

                var keyValuePairUtils = new KeyValuePairUtils("=");
                var configDictionary = keyValuePairUtils.ReadKeyValuePairsFromFile(configFilePath);
                if (configDictionary == null)
                {
                    LogError("Failed to setup configDictionary");
                    retVal = false;
                    return retVal;
                }

                var templateFilePath = testCaseStepFilePathUtils.GetFilePathForStep(fileNameFilters[0], stepNum);
                var templateFileName = testCaseStepFilePathUtils.GetFileNameForStep(fileNameFilters[0], stepNum);
                var templateFileExtenstion = Path.GetExtension(templateFilePath);
                if (string.IsNullOrEmpty(templateFilePath)
                    ||
                    string.IsNullOrEmpty(templateFileName))
                {
                    LogError("Failed to setup templateFileName and/or Path");
                    retVal = false;
                    return retVal;
                }

                // Transform Template file with config values
                var templateContent = FileUtils.GetFilecontent(templateFilePath);

                // loop through all config Keys and replace occurences in templateContent string 
                // then write back to a file 
                foreach (var key in configDictionary.Keys)
                {
                    var value = (string)configDictionary[key];

                    var evaluatedValue = StringTransformationUtils.Evaluate(value);
                    templateContent = Regex.Replace(
                        templateContent,
                        $"{key}.*$",
                        $"\"{key}\": {evaluatedValue}",
                        RegexOptions.Multiline);
                }

                var resultsFilePath = TestCaseFileAndFolderUtils.GetTestCaseResultsFilePath($"{templateFileName}{".step"}{stepNum}{".results"}", false);
                File.WriteAllText(resultsFilePath, templateContent);
            }

            retVal = true;
            return retVal;
        }

        /// <summary>
        /// The get test step results file path.
        /// </summary>
        /// <param name="fileNameFilters">
        /// The file name filters.
        /// </param>
        /// <param name="stepNum">
        /// The step num.
        /// </param>
        /// <param name="ignoreFileExtensions">
        /// The ignore file extensions.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetTestStepResultsFilePath(string[] fileNameFilters, int stepNum, bool ignoreFileExtensions = false)
        {
            var retVal = String.Empty;
            var tcSFPU = testCaseStepFilePathUtils ?? (testCaseStepFilePathUtils = new TestCaseStepFilePathUtils(TestCaseFileAndFolderUtils.TestCaseDirectory,
                                                           fileNameFilters,
                                                           ignoreFileExtensions
                                                       ));

            if (tcSFPU == null)
            {
                LogError("Failed to setup TestCaseStepFilePathUtils");
                retVal = String.Empty;
                return retVal;
            }

            var templateFilePath = testCaseStepFilePathUtils.GetFilePathForStep(fileNameFilters[0], stepNum);
            var templateFileName = testCaseStepFilePathUtils.GetFileNameForStep(fileNameFilters[0], stepNum);
            if (string.IsNullOrEmpty(templateFilePath)
                ||
                string.IsNullOrEmpty(templateFileName))
            {
                LogError("Failed to setup templateFileName and/or Path");
                retVal = String.Empty;
                return retVal;
            }

            retVal = testCaseFileAndFolderUtils.GetTestCaseResultsFilePath($"{templateFileName}{".step"}{stepNum}{".results"}", true);

            return retVal;
        }


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

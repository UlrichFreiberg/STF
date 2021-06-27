// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestCaseDirectoryDataUtils.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the TestCaseDirectoryDataUtils type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.TestCaseData
{
    using System.Collections.Specialized;

    using Mir.Stf.Utilities.TestCaseDirectoryUtilities;

    /// <summary>
    /// The test case directory data utils.
    /// </summary>
    public class TestCaseDirectoryDataUtils
    {
        /// <summary>
        /// Backing Field.
        /// </summary>
        private OrderedDictionary constantsDict;

        /// <summary>
        /// Backing Field.
        /// </summary>
        private OrderedDictionary testDataValuesDict;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseDirectoryDataUtils"/> class.
        /// </summary>
        /// <param name="testCaseId">
        /// The test case id.
        /// </param>
        /// <param name="testCaseDirectoryRoot">
        /// The test case directory root.
        /// </param>
        public TestCaseDirectoryDataUtils(int testCaseId, string testCaseDirectoryRoot)
        {
            TestCaseDirectoryRoot = testCaseDirectoryRoot;
            TestCaseId = testCaseId;
            TestCaseFileAndFolderUtils = new TestCaseFileAndFolderUtils(TestCaseId, TestCaseDirectoryRoot);
            TestDataValuesFilePath = TestCaseFileAndFolderUtils.GetTestCaseRootFilePath("TestDataValues.txt");
            ConstantsFilePath = TestCaseFileAndFolderUtils.GetTestCaseRootFilePath("Constants.txt");
            KeyValuePairUtils = new KeyValuePairUtils();
            Init();
        }

        /// <summary>
        /// Gets or sets the test case id.
        /// </summary>
        public int TestCaseId { get; set; }

        /// <summary>
        /// Gets or sets the test case directory root.
        /// </summary>
        public string TestCaseDirectoryRoot { get; set; }

        /// <summary>
        /// Gets or sets the test case file and folder utils.
        /// </summary>
        public TestCaseFileAndFolderUtils TestCaseFileAndFolderUtils { get; set; }

        /// <summary>
        /// Gets or sets the test data values file path.
        /// </summary>
        public string TestDataValuesFilePath { get; set; }

        /// <summary>
        /// Gets or sets the constants file path.
        /// </summary>
        public string ConstantsFilePath { get; set; }

        /// <summary>
        /// Gets or sets the key value pair utils.
        /// </summary>
        public KeyValuePairUtils KeyValuePairUtils { get; set; }

        /// <summary>
        /// The constants dictionary.
        /// </summary>
        public OrderedDictionary Constants => constantsDict ?? (constantsDict = GetConstantsValues());

        /// <summary>
        /// The constants dictionary.
        /// </summary>
        public OrderedDictionary TestDataValues => testDataValuesDict ?? (testDataValuesDict = GetTestDataValuesDict());

        /// <summary>
        /// Gets the test data.
        /// </summary>
        public OrderedDictionary TestData => KeyValuePairUtils.ReadKeyValuePairsFromFile(TestDataValuesFilePath);

        /// <summary>
        /// The get test case input.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetTestCaseInput()
        {
            //// (4) TestCaseInput.txt 
            ////    Values picked from TestDataValues and Constants - for solely use in test cases.
            ////    No evaluation of functions
            ////    To be shown in a WebUI

            return "Not Implemented";
        }

        /// <summary>
        /// The get test data value.
        /// </summary>
        /// <param name="keyName">
        /// The key name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetTestDataValue(string keyName)
        {
            //// If (!TestDataValuesDict.ContainsKey(keyName)) {
            ////     return null;
            //// }
            ////
            //// var dictValue = TestDataValuesDict[keyName];
            //// var retVal = Evaluate(dictValue)
            ////
            //// return retVal;
            //// 
            return "Not Implemented";
        }

        /// <summary>
        /// The evaluate.
        /// </summary>
        /// <param name="stringToEvaluate">
        /// The string to evaluate.
        /// </param>
        /// <param name="dictFromCodeToOverlay">
        /// The dict from code to overlay.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string Evaluate(string stringToEvaluate, OrderedDictionary dictFromCodeToOverlay)
        {
            //// var tdvDict = KeyValuePairs.Overlay(TestDataValues, dictFromCodeToOverlay);
            ////  
            //// foreach '{SOMETHING}' in the stringToEvaluate {
            ////     handle {CONSTANT} ;
            ////     handle {TESTDATA} ;
            ////     call StringTransformationUtils  
            //// }
            ////
            //// return retVal;
            //// 
            return "Not Implemented";
        }

        /// <summary>
        /// The init.
        /// </summary>
        private void Init()
        {
        }

        /// <summary>
        /// The get constants values.
        /// </summary>
        /// <returns>
        /// The <see cref="OrderedDictionary"/>.
        /// </returns>
        private OrderedDictionary GetConstantsValues()
        {
            ////(2) Evaluate Constants.txt
            ////    - Temp\Constants.list.txt(all constant files compiled into one file)
            ////    - include files resolved(marked for debug with comments)
            ////    -Temp\Constants.resolved.txt
            ////    - values overlayed
            ////    - Results\Constants.transformed.txt(ready to load using KeyValuePairUtils)

            var retVal = KeyValuePairUtils.ReadKeyValuePairsFromFile(ConstantsFilePath);
            var resolvedConstantFilePath = TestCaseFileAndFolderUtils.GetTestCaseResultsFilePath("Constants.Resolved.txt", false);

            KeyValuePairUtils.SaveKeyValuePairsToFile(resolvedConstantFilePath, retVal, "Resolved Constants");
            return retVal;
        }

        /// <summary>
        /// The get test data values dict.
        /// </summary>
        /// <returns>
        /// The <see cref="OrderedDictionary"/>.
        /// </returns>
        private OrderedDictionary GetTestDataValuesDict()
        {
            //// -Temp\TestDataValues.txt(all TestDataValue files compiled into one file)
            ////    - include files resolved(marked for debug with comments)
            ////
            //// -Temp\TestDataValues.resolved.txt
            ////    - values overlayed
            ////
            //// - Results\TestDataValues.transformed.txt(ready to load using KeyValuePairUtils)
            ////    -values evaluated
            ////    - Constants applied(Results\Constants.transformed.txt)
            ////- Simple functions called

            var retVal = KeyValuePairUtils.ReadKeyValuePairsFromFile(TestDataValuesFilePath);
            var resolvedTestDataValuesFilePath = TestCaseFileAndFolderUtils.GetTestCaseResultsFilePath("TestDataValues.Resolved.txt", false);

            KeyValuePairUtils.SaveKeyValuePairsToFile(resolvedTestDataValuesFilePath, retVal, "Resolved Test Data Values");
            return retVal;
        }
    }
}

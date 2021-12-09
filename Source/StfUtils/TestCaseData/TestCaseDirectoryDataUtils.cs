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
    using Mir.Stf.Utilities.Interfaces;
    using Mir.Stf.Utilities.StringTransformationUtilities;
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
            StringTransformationUtils = new StringTransformationUtils();
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseDirectoryDataUtils"/> class.
        /// </summary>
        /// <param name="testCaseFileAndFolderUtils">
        /// The test Case File And Folder Utils.
        /// </param>
        /// <param name="stringTransformationUtils">
        /// The string Transformation Utils.
        /// </param>
        public TestCaseDirectoryDataUtils(TestCaseFileAndFolderUtils testCaseFileAndFolderUtils, IStringTransformationUtils stringTransformationUtils)
        {
            TestCaseDirectoryRoot = testCaseFileAndFolderUtils.TestCaseDirectory;
            TestCaseId = testCaseFileAndFolderUtils.TestCaseId;
            TestCaseFileAndFolderUtils = testCaseFileAndFolderUtils;
            TestDataValuesFilePath = TestCaseFileAndFolderUtils.GetTestCaseRootFilePath("TestDataValues.txt");
            ConstantsFilePath = TestCaseFileAndFolderUtils.GetTestCaseRootFilePath("Constants.txt");
            KeyValuePairUtils = new KeyValuePairUtils();
            StringTransformationUtils = stringTransformationUtils;
            StringTransformationUtils.RegisterAllStuFunctionsForType(this);
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
        public OrderedDictionary ConstantsDict => constantsDict ?? (constantsDict = GetConstantsValues());

        /// <summary>
        /// The constants dictionary.
        /// </summary>
        public OrderedDictionary TestDataValuesDict => testDataValuesDict ?? (testDataValuesDict = GetTestDataValuesDict());

        /// <summary>
        /// Gets the test data.
        /// </summary>
        public OrderedDictionary TestData => KeyValuePairUtils.ReadKeyValuePairsFromFile(TestDataValuesFilePath);

        /// <summary>
        /// Gets or sets the string transformation utils.
        /// </summary>
        private IStringTransformationUtils StringTransformationUtils { get; set; }

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
            if (!TestDataValuesDict.Contains(keyName)) 
            {
                return null;
            }

            var dictValue = (string)TestDataValuesDict[keyName];
            var retVal = Evaluate(dictValue);

            return retVal;
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
        public string GetConstantValue(string keyName)
        {
            if (!ConstantsDict.Contains(keyName))
            {
                return null;
            }

            var dictValue = (string)ConstantsDict[keyName];
            var retVal = Evaluate(dictValue);

            return retVal;
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
        public string Evaluate(string stringToEvaluate, OrderedDictionary dictFromCodeToOverlay = null)
        {
            var retVal = StringTransformationUtils.Evaluate(stringToEvaluate);

            return retVal;
        }

        /// <summary>
        /// The stu test data.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        [StringTransformationUtilFunction("TESTDATA")]
        public string StuTestData(string arg)
        {
            var retVal = GetTestDataValue(arg);
            return retVal;
        }

        /// <summary>
        /// The stu test data.
        /// </summary>
        /// <param name="arg">sss
        /// The arg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        [StringTransformationUtilFunction("CONSTANT")]
        public string StuConstant(string arg)
        {
            var retVal = this.GetConstantValue(arg);
            return retVal;
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
            //// Compilation Step
            //// -Temp\TestDataValues.txt(all TestDataValue files compiled into one file)
            ////    - include files resolved(marked for debug with comments)
            ////
            //// Resolution Step
            //// -Temp\TestDataValues.resolved.txt
            ////    - values overlayed
            ////
            //// Transformation Step
            //// - Results\TestDataValues.transformed.txt(ready to load using KeyValuePairUtils)
            ////    -values evaluated
            ////    - Constants applied(Results\Constants.transformed.txt)
            ////- Simple functions called

            var compiledDict = CompileTestDataValues();

            var resolvedDict = ResolveTestDataValues();

            var retVal = TransformTestDataValues();
            return retVal;
        }

        /// <summary>
        /// The compile test data values.
        /// </summary>
        /// <returns>
        /// The <see cref="OrderedDictionary"/>.
        /// </returns>
        private OrderedDictionary CompileTestDataValues()
        {
            //// Compilation Step
            //// -Temp\TestDataValues.txt(all TestDataValue files compiled into one file)
            ////    - include files resolved(marked for debug with comments)
            ////
            var originalDict = KeyValuePairUtils.ReadKeyValuePairsFromFile(TestDataValuesFilePath);

            // TODO: Do some compilation here as per comment above (gathering all TDV's together)
            var retVal = originalDict;

            var compiledTestDataValuesFilePath =
                TestCaseFileAndFolderUtils.GetTestCaseTempFilePath("TestDataValues.compiled.txt", false);
            KeyValuePairUtils.SaveKeyValuePairsToFile(compiledTestDataValuesFilePath, retVal, "Compiled Test Data Values");

            return retVal;
        }

        /// <summary>
        /// The resolve test data values.
        /// </summary>
        /// <returns>
        /// The <see cref="OrderedDictionary"/>.
        /// </returns>
        private OrderedDictionary ResolveTestDataValues()
        {
            //// Resolution Step
            //// -Temp\TestDataValues.resolved.txt
            ////    - values overlayed
            ////
            var compiledTestDataValuesFilePath =
                TestCaseFileAndFolderUtils.GetTestCaseTempFilePath("TestDataValues.compiled.txt", false);
            var compiledDict = KeyValuePairUtils.ReadKeyValuePairsFromFile(compiledTestDataValuesFilePath);

            // TODO: Do some resolving and overlaying as per comment above on Resolving step
            var retVal = compiledDict;

            var resolvedTestDataValuesFilePath =
                TestCaseFileAndFolderUtils.GetTestCaseTempFilePath("TestDataValues.resolved.txt", false);
            KeyValuePairUtils.SaveKeyValuePairsToFile(
                resolvedTestDataValuesFilePath,
                retVal,
                "Resolved Test Data Values");
            return retVal;
        }

        /// <summary>
        /// The transform test data values.
        /// </summary>
        /// <returns>
        /// The <see cref="OrderedDictionary"/>.
        /// </returns>
        private OrderedDictionary TransformTestDataValues()
        {
            //// Transformation Step
            //// - Results\TestDataValues.transformed.txt(ready to load using KeyValuePairUtils)
            ////    -values evaluated
            ////    - Constants applied(Results\Constants.transformed.txt)
            ////- Simple functions called
            var resolvedTestDataValuesFilePath =
                TestCaseFileAndFolderUtils.GetTestCaseTempFilePath("TestDataValues.resolved.txt", false);
            var resolvedDict = KeyValuePairUtils.ReadKeyValuePairsFromFile(resolvedTestDataValuesFilePath);

            var transformedDict = new OrderedDictionary();

            // TODO: Is this the kind of transformation we require see above
            foreach (var key in resolvedDict.Keys)
            {
                var value = (string)resolvedDict[key];

                var evaluatedValue = StringTransformationUtils.Evaluate(value);

                transformedDict.Add(key, evaluatedValue);
            }

            var transformedTestDataValuesFilePath =
                TestCaseFileAndFolderUtils.GetTestCaseResultsFilePath("TestDataValues.transformed.txt", false);
            KeyValuePairUtils.SaveKeyValuePairsToFile(
                transformedTestDataValuesFilePath,
                transformedDict,
                "Transformed Test Data Values");
            return transformedDict;
        }
    }
}
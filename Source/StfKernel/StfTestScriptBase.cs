// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfTestScriptBase.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mir.Stf.Utilities;
using Mir.Stf.Utilities.Interfaces;

namespace Mir.Stf
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    using Interfaces;

    using Utilities.Configuration;

    /// <summary>
    /// BaseClass for all Stf test scripts.
    /// The class will set up the right <see cref="Utilities.StfLogger"/> (StfLogger)
    /// and instantiate the <see cref="Utilities.StfAssert"/> (StfAssert)
    /// </summary>
    [TestClass]
    public class StfTestScriptBase : StfKernel
    {
        /// <summary>
        /// The test result files.
        /// </summary>
        private static List<string> testResultFiles;

        /// <summary>
        /// The kernel log file path.
        /// </summary>
        private string kernelLogFilePath;

        /// <summary>
        /// Gets the Stf Asserter.
        /// </summary>
        public StfAssert StfAssert { get; private set; }

        /// <summary>
        /// Gets the Stf logger.
        /// </summary>
        public IStfLogger StfLogger { get; private set; }

        /// <summary>
        /// Gets the Stf Archiver
        /// </summary>
        public StfArchiver StfArchiver { get; private set; }

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// The test context instance is set by the MsTestFrame work
        /// </summary>
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public TestContext TestContext { get; set; }

        /// <summary>
        /// Gets or sets the stf iteration no.
        /// </summary>
        protected int StfIterationNo { get; set; }

        /// <summary>
        /// Gets a value indicating whether Stf believe this iteration should be ignore. 
        /// Used for data driven tests - controlled by a data column value named StfIgnoreRow.
        /// For not datadriven it returns false
        /// </summary>
        /// <returns></returns>
        protected bool StfIgnoreRow
        {
            get
            {
                var stfIgnoreRow = DataRowColumnValue("StfIgnoreRow");
                var retVal = InferBoolValue(stfIgnoreRow);

                return retVal;
            }
        }

        /// <summary>
        /// The TestInitialize for <see cref="StfTestScriptBase"/>.
        /// </summary>
        [TestInitialize]
        public void BaseTestInitialize()
        {
            StfLogger = Get<IStfLogger>();

            var kernelLogErrors = CheckForFailedKernelLog(StfLogger);

            // We're getting the instance of the logger and logging a link to the kernel logger
            kernelLogFilePath = StfLogger.FileName;

            StfLogger.Configuration.LogTitle = TestContext.TestName;
            StfIterationNo = DataRowIndex();

            var logFilePostfix = string.Empty;
            var iterationStatus = "Not datadriven";

            if (StfIterationNo == 1)
            {
                testResultFiles = new List<string>();
            }

            if (StfIterationNo >= 1)
            {
                var numberOfIterations = TestContext.DataRow.Table.Rows.Count;

                logFilePostfix = GetlogFilePostfix(numberOfIterations, StfIterationNo);
                iterationStatus = $"Iteration {StfIterationNo}";
            }

            var logdir = Path.Combine(StfLogDir, TestContext.TestName);

            if (!Directory.Exists(logdir))
            {
                Directory.CreateDirectory(logdir);
            }

            var logFilename = $"{Path.Combine(logdir, TestContext.TestName)}{logFilePostfix}.html";

            StfLogger.FileName = logFilename;
            StfAssert = new StfAssert(StfLogger);

            if (!TestDataDriven())
            {
                TestContext.AddResultFile(StfLogger.FileName);
                TestContext.AddResultFile(kernelLogFilePath);
            }
            else
            {
                if (!testResultFiles.Contains(StfLogger.FileName))
                {
                    // When datadriven we're defer adding resultfiles to testcontext until the last iteration
                    // This prevents MsTest from adding duplicate result files
                    testResultFiles.Add(StfLogger.FileName);
                }

                if (!testResultFiles.Contains(kernelLogFilePath))
                {
                    testResultFiles.Add(kernelLogFilePath);
                }

                for (var index = 0; index < TestContext.DataRow.Table.Columns.Count; index++)
                {
                    var headerCaption = TestContext.DataRow.Table.Columns[index].Caption;

                    StfLogger.LogInfo($"Column[{headerCaption}]=[{TestContext.DataRow[headerCaption]}]");
                }
            }

            SetUpArchiver(TestContext.TestName);

            LogBaseClassMessage("StfTestScriptBase TestInitialize");
            LogKeyValues(kernelLogFilePath, iterationStatus);

            if (kernelLogErrors.Any())
            {
                foreach (var kernelLogError in kernelLogErrors)
                {
                    StfLogger.LogError(kernelLogError);
                }

                StfAssert.AreEqual("No Kernel log errors present", 0, kernelLogErrors.Count);
            }

            if (StfIgnoreRow)
            {
                DoCleanUpAndThrowInconclusive("Row ignored due to StfIgnore is percieved true");
            }
        }

        /// <summary>
        /// The test cleanup.
        /// </summary>
        [TestCleanup]
        public void BaseTestCleanup()
        {
            LogBaseClassMessage("StfTestScriptBase BaseTestCleanup");

            var testFailed = !StfIgnoreRow
                          && TestContext.CurrentTestOutcome != UnitTestOutcome.Passed
                          && TestContext.CurrentTestOutcome != UnitTestOutcome.Inconclusive;

            if (testFailed)
            {
                StfLogger.LogError("Test failed");
            }

            if (!TestDataDriven())
            {
                StfArchiver.AddFile(StfLogger.FileName);
                StfArchiver.AddFile(kernelLogFilePath);
                StfLogger.LogInfo(StfArchiver.Status());
                StfLogger.CloseLogFile();
                StfArchiver.PerformArchive();
            }
            else
            {
                StfIterationNo = DataRowIndex();

                StfLogger.CloseLogFile();

                if (StfIterationNo == TestContext.DataRow.Table.Rows.Count)
                {
                    var myStfSummaryLogger = new StfSummaryLogger();
                    var summaryLogfileLogDirname = Path.GetDirectoryName(StfLogger.FileName);
                    var myLoggerFileName = Path.GetFileName(StfLogger.FileName) ?? string.Empty;
                    var summaryLogfileLogFilename = Regex.Replace(myLoggerFileName, @"_[0-9]+\.html", ".html");
                    var summaryLogfilename = $@"{summaryLogfileLogDirname}\SummaryLogfile_{summaryLogfileLogFilename}";
                    var summaryLogfileLogfilePattern = Regex.Replace(myLoggerFileName, @"_[0-9]+\.html", "_*");

                    myStfSummaryLogger.CreateSummaryLog(summaryLogfilename, summaryLogfileLogDirname, summaryLogfileLogfilePattern);

                    TestContext.AddResultFile(summaryLogfilename);
                    AddResultfiles();

                    StfArchiver.AddFile(summaryLogfilename);
                    StfArchiver.PerformArchive();
                }
            }

            if (StfIgnoreRow)
            {
                // DoCleanUpAndThrowInconclusive will do the throwing if needed
                return;
            }

            if (!testFailed && StfAssert.CurrentInconclusives > 0 && StfAssert.CurrentFailures <= 0)
            {
                var msg = $"Testmethod [{TestContext.TestName}] is inconclusive. Number of inconclusive results: [{StfAssert.CurrentInconclusives}]";

                throw new AssertInconclusiveException(msg);
            }

            if (!testFailed && StfAssert.CurrentFailures > 0)
            {
                var msg = $"Testmethod [{TestContext.TestName}] failed. Number of asserts that failed: [{StfAssert.CurrentFailures}]";

                throw new AssertFailedException(msg);
            }
        }

        /// <summary>
        /// The data row column exists.
        /// </summary>
        /// <param name="columnName">
        /// The column name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected bool DataRowColumnExists(string columnName)
        {
            var retVal = TestContext.DataRow?.Table.Columns.Contains(columnName) ?? false;

            return retVal;
        }

        /// <summary>
        /// The data row column value.
        /// </summary>
        /// <param name="columnName">
        /// The column name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        protected string DataRowColumnValue(string columnName)
        {
            if (!DataRowColumnExists(columnName))
            {
                return null;
            }

            var retVal = TestContext.DataRow[columnName].ToString();

            return retVal;
        }

        /// <summary>
        /// The init test data.
        /// </summary>
        /// <param name="testdataObject">
        /// The testdata object.
        /// </param>
        /// <typeparam name="T">
        /// The testdata object that should be filled with the values from the current row in the TestContext
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        protected T InitTestData<T>(T testdataObject = default(T)) where T : IStfTestData, new()
        {
            if (!TestDataDriven())
            {
                if (testdataObject != null)
                {
                    testdataObject.StfIteration = -1;
                }

                return testdataObject;
            }

            var retVal = new T();
            var properties = typeof(T).GetProperties();
            var attributedProperties = properties.Where(prop => prop.IsDefined(typeof(StfTestDataAttribute), false)).ToList();
            var dataRow = TestContext.DataRow;

            retVal.StfIteration = DataRowIndex();

            foreach (var columnName in dataRow.Table.Columns)
            {
                var propertyName = columnName.ToString();
                var property = properties.FirstOrDefault(pp => pp.Name == propertyName);

                // did we find the correspondig property in the testdata class?
                if (property == null && attributedProperties.Any())
                {
                    // hmm, lets see if there is a map for this column...
                    property = attributedProperties.FirstOrDefault(pp => pp.GetCustomAttribute<StfTestDataAttribute>().ColumnName == propertyName);
                }

                if (property == null)
                {
                    continue;
                }

                var propertyType = property.PropertyType;
                try
                {
                    var val = TypeDescriptor.GetConverter(propertyType).ConvertFromString(dataRow[propertyName].ToString());

                    property.SetValue(retVal, val);
                }
                catch (Exception ex)
                {
                    StfLogger.LogInternal(
                        "Caught error setting value for property [{0}] in testdata object [{1}]. Error: [{2}]",
                        property.Name,
                        typeof(T).Name,
                        ex.Message);
                }
            }

            // we might later alter stuff, so StfIgnoreRow also is triggered by StfIgnore
            retVal.StfIgnoreRow = StfIgnoreRow;

            return retVal;
        }

        /// <summary>
        /// The check for failed kernel log.
        /// </summary>
        /// <param name="kernelLogger">
        /// The kernel logger.
        /// </param>
        /// <returns>
        /// The <see cref="IList{T}"/> of errors encountered when the kernel was initializing and setting things up.
        /// </returns>
        private IList<string> CheckForFailedKernelLog(IStfLogger kernelLogger)
        {
            var retVal = new List<string>();
            var failsToReport = kernelLogger.NumberOfLoglevelMessages[StfLogLevel.Fail] > 0;
            var errorsToReport = kernelLogger.NumberOfLoglevelMessages[StfLogLevel.Error] > 0;

            if (failsToReport)
            {
                retVal.Add("StfKernel encountered a failure. All bets are off");
            }

            if (errorsToReport)
            {
                retVal.Add("StfKernel encountered an error. All bets are off");
            }

            return retVal;
        }

        /// <summary>
        /// The getlog file postfix. Takes into account the number of iteration overall. 
        /// Used as part of the log file name - includes leading zeros for better sorting
        /// </summary>
        /// <param name="numberOfIterations">
        /// The number of iterations overall for this test.
        /// </param>
        /// <param name="iterationNo">
        /// The current iteration number.
        /// </param>
        /// <returns>
        /// a string like "_007" for the seventh iteration of a series of test for 100+ tests
        /// </returns>
        private string GetlogFilePostfix(int numberOfIterations, int iterationNo)
        {
            var digits = Math.Floor(Math.Log10(numberOfIterations)) + 1;
            var format = $"_{{0:D{digits}}}";
            var retVal = string.Format(format, iterationNo);

            return retVal;
        }

        /// <summary>
        /// The add resultfiles.
        /// </summary>
        private void AddResultfiles()
        {
            if (!testResultFiles.Any())
            {
                return;
            }

            foreach (var resultFile in testResultFiles)
            {
                TestContext.AddResultFile(resultFile);
                StfArchiver.AddFile(resultFile);
            }
        }

        /// <summary>
        /// The log key values.
        /// </summary>
        /// <param name="kernelLogfilePath">
        /// The kernel logfile path.
        /// </param>
        /// <param name="iterationStatus">
        /// The iteration status.
        /// </param>
        private void LogKeyValues(string kernelLogfilePath, string iterationStatus)
        {
            StfLogger.LogKeyValue("Test Iteration", iterationStatus, iterationStatus);
            StfLogger.LogKeyValue("Kernel Logger", kernelLogfilePath, "Kernel Logger");
            StfLogger.LogKeyValue("Test Name", TestContext.TestName, "Name of test");
            StfLogger.LogKeyValue("Stf Root", StfRoot, "The Stf Root directory");

            var configuration = Get<StfConfiguration>();
            StfLogger.LogKeyValue("Environment", configuration.Environment, "Configuration.EnvironmentName");
        }

        /// <summary>
        /// Make sure LogLevel is set to internal and set it back again
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        private void LogBaseClassMessage(string message)
        {
            var oldLoglevel = StfLogger.LogLevel;

            StfLogger.LogLevel = StfLogLevel.Internal;
            StfLogger.LogInternal(message);
            StfLogger.LogLevel = oldLoglevel;
        }

        /// <summary>
        /// Checks wether or not if this test is datadriven. 
        /// If so it returns the row number of the current test data.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// If not datadriven it return -1. If datadriven it returns the row number of the current test data.
        /// </returns>
        private int DataRowIndex()
        {
            if (!TestDataDriven())
            {
                return -1;
            }

            var currentIteration = TestContext.DataRow.Table.Rows.IndexOf(TestContext.DataRow);
            return currentIteration + 1;
        }

        /// <summary>
        /// The test data driven.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool TestDataDriven()
        {
            return TestContext.DataRow?.Table.Rows.Count > 0;
        }

        /// <summary>
        /// The set up archiver if necessary.
        /// </summary>
        /// <param name="testName">
        /// The test name.
        /// </param>
        private void SetUpArchiver(string testName)
        {
            var stfConfiguration = Get<StfConfiguration>();
            var archiverConfiguration = new StfArchiverConfiguration();

            try
            {
                stfConfiguration.LoadUserConfiguration(archiverConfiguration);
            }
            catch (Exception exception)
            {
                var msg = $"Something went wrong while loading user configuration for archiver: {exception.Message}";
                StfLogger.LogError(msg);
            }

            archiverConfiguration.ArchiveTopDir = StfTextUtils.ExpandVariables(archiverConfiguration.ArchiveTopDir);
            archiverConfiguration.ArchiveDestination = StfTextUtils.ExpandVariables(archiverConfiguration.ArchiveDestination);
            archiverConfiguration.TempDirectory = StfTextUtils.ExpandVariables(archiverConfiguration.TempDirectory);
            StfArchiver = new StfArchiver(archiverConfiguration, testName);
        }

        /// <summary>
        /// The do clean up and throw inconclusive.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        private void DoCleanUpAndThrowInconclusive(string message)
        {
            // gotta diable the throwing of the inconclusive exception from StfAsserter - othervise BaseTestCleanup() wont be called
            var oldEnableNegativeTesting = StfAssert.EnableNegativeTesting;

            StfAssert.EnableNegativeTesting = true;
            StfAssert.IsInconclusive("TestCleanup", message);
            StfAssert.EnableNegativeTesting = oldEnableNegativeTesting;

            // this wont call the test script cleanup - fair enough as the test script test initialize wasn't called --> symmetry is maintained
            BaseTestCleanup();

            throw new AssertInconclusiveException("Ignoring row");
        }

        /// <summary>
        /// Take a string and see if a boolean true can be parsed - if not, then return false
        /// </summary>
        /// <param name="value">
        /// the string to parse
        /// </param>
        /// <returns>
        /// true, if a boolean true can be parsed - if not, then return false
        /// </returns>
        private bool InferBoolValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            switch (value.ToLower())
            {
                case "1":
                case "ok":
                case "yes":
                case "true":
                    return true;
                default:
                    return false;
            }
        }
    }
}

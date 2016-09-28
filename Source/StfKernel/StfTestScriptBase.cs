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
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mir.Stf.Utilities;
using Mir.Stf.Utilities.Interfaces;

namespace Mir.Stf
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Linq;

    using Mir.Stf.Interfaces;

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
        /// The TestInitialize for <see cref="StfTestScriptBase"/>.
        /// </summary>
        [TestInitialize]
        public void BaseTestInitialize()
        {
            StfLogger = Get<IStfLogger>();

            // We're getting the instance of the logger and logging a link to the kernel logger
            kernelLogFilePath = StfLogger.FileName;

            StfLogger.Configuration.LogTitle = TestContext.TestName;

            var logFilePostfix = string.Empty;
            var iterationNo = DataRowIndex();
            var iterationStatus = "Not datadriven";

            if (iterationNo == 1)
            {
                testResultFiles = new List<string>();
            }

            if (iterationNo >= 1)
            {
                var numberOfIterations = TestContext.DataRow.Table.Rows.Count;

                logFilePostfix = GetlogFilePostfix(numberOfIterations, iterationNo);
                iterationStatus = string.Format("Iteration {0}", iterationNo);
            }

            var logdir = Path.Combine(StfLogDir, TestContext.TestName);

            if (!Directory.Exists(logdir))
            {
                Directory.CreateDirectory(logdir);
            }

            var logFilename = string.Format("{0}{1}.html", Path.Combine(logdir, TestContext.TestName), logFilePostfix);

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

                    StfLogger.LogInfo(string.Format("Column[{0}]=[{1}]", headerCaption, TestContext.DataRow[headerCaption]));
                }
            }

            SetUpArchiver(TestContext.TestName);

            LogBaseClassMessage("StfTestScriptBase TestInitialize");
            LogKeyValues(kernelLogFilePath, iterationStatus);
        }

        /// <summary>
        /// The test cleanup.
        /// </summary>
        [TestCleanup]
        public void BaseTestCleanup()
        {
            LogBaseClassMessage("StfTestScriptBase BaseTestCleanup");

            var testFailed = TestContext.CurrentTestOutcome != UnitTestOutcome.Passed &&
                             TestContext.CurrentTestOutcome != UnitTestOutcome.Inconclusive;

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
                var iterationNo = DataRowIndex();

                StfLogger.CloseLogFile();

                if (iterationNo == TestContext.DataRow.Table.Rows.Count)
                {
                    var myStfSummeryLogger = new StfSummeryLogger();
                    var summeryLogfileLogDirname = Path.GetDirectoryName(StfLogger.FileName);
                    var myLoggerFileName = Path.GetFileName(StfLogger.FileName) ?? string.Empty;
                    var summeryLogfileLogFilename = Regex.Replace(myLoggerFileName, @"_[0-9]+\.html", ".html");
                    var summeryLogfilename = string.Format(@"{0}\SummeryLogfile_{1}", summeryLogfileLogDirname, summeryLogfileLogFilename);
                    var summeryLogfileLogfilePattern = Regex.Replace(myLoggerFileName, @"_[0-9]+\.html", "_*");

                    myStfSummeryLogger.CreateSummeryLog(summeryLogfilename, summeryLogfileLogDirname, summeryLogfileLogfilePattern);

                    TestContext.AddResultFile(summeryLogfilename);
                    AddResultfiles();

                    StfArchiver.AddFile(summeryLogfilename);
                    StfArchiver.PerformArchive();
                }
            }

            if (!testFailed && StfAssert.CurrentInconclusives > 0 && StfAssert.CurrentFailures <= 0)
            {
                var msg = string.Format(
                    "Testmethod [{0}] is inconclusive. Number of inconclusive results: [{1}]",
                    TestContext.TestName,
                    StfAssert.CurrentInconclusives);

                throw new AssertInconclusiveException(msg);
            }

            if (!testFailed && StfAssert.CurrentFailures > 0)
            {
                var msg = string.Format(
                    "Testmethod [{0}] failed. Number of asserts that failed: [{1}]",
                    TestContext.TestName,
                    StfAssert.CurrentFailures);

                throw new AssertFailedException(msg);
            }
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
            var dataRow = TestContext.DataRow;

            retVal.StfIteration = DataRowIndex();

            foreach (var row in dataRow.Table.Columns)
            {
                var propertyName = row.ToString();
                var property = properties.FirstOrDefault(pp => pp.Name == propertyName);

                // did we find the correspondig property in the testdata class?
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
            var format = string.Format("_{{0:D{0}}}", digits);
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
            StfLogger.LogKeyValue("Testname", TestContext.TestName, "Name of test");

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
            if (TestContext.DataRow == null)
            {
                return false;
            }

            return TestContext.DataRow.Table.Rows.Count > 0;
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
                var msg = string.Format("Something went wrong while loading user configuration for archiver: {0}", exception.Message);
                StfLogger.LogError(msg);
            }

            archiverConfiguration.ArchiveTopDir = StfTextUtils.ExpandVariables(archiverConfiguration.ArchiveTopDir);
            archiverConfiguration.ArchiveDestination = StfTextUtils.ExpandVariables(archiverConfiguration.ArchiveDestination);
            archiverConfiguration.TempDirectory = StfTextUtils.ExpandVariables(archiverConfiguration.TempDirectory);
            StfArchiver = new StfArchiver(archiverConfiguration, testName);
        }

        /// <summary>
        /// The check if iteration should be ignored.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool CheckIfIterationShouldBeIgnored()
        {
            var dataRow = TestContext.DataRow;

            if (dataRow == null)
            {
                return false;
            }

            string ignoreRowValue;

            try
            {
                ignoreRowValue = dataRow.Field<string>("StfIgnoreRow");
            }
            catch (Exception)
            {
                // slurp - catch all TODO: Some log statement?
                return false;
            }

            bool retVal;

            if (!bool.TryParse(ignoreRowValue, out retVal))
            {
                return false;
            }

            return retVal;
        }

        /// <summary>
        /// The do clean up and throw inconclusive.
        /// </summary>
        private void DoCleanUpAndThrowInconclusive()
        {
            BaseTestCleanup();

            // TODO: Get the configuration in to determine whether to log or to throw. Logging should be done before basetestcleanup
            throw new AssertInconclusiveException("Ignoring row");
        }
    }
}

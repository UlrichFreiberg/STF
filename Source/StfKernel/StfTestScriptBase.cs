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
    using System.Linq;

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
            var kernelLogFilePath = StfLogger.FileName;

            StfLogger.Configuration.LogTitle = TestContext.TestName;

            var logFilePostfix = string.Empty;
            var iterationNo = DataRowIndex();
            var iterationStatus = "Not datadriven";

            if (iterationNo == 0)
            {
                testResultFiles = new List<string>();
            }

            if (iterationNo >= 0)
            {
                logFilePostfix = string.Format("_{0}", iterationNo);
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
            }
            else
            {
                if (!testResultFiles.Contains(StfLogger.FileName))
                {
                    // When datadriven we're defer adding resultfiles to testcontext until the last iteration
                    // This prevents MsTest from adding duplicate result files
                    testResultFiles.Add(StfLogger.FileName);
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

            var testFailed = TestContext.CurrentTestOutcome != UnitTestOutcome.Passed;

            if (testFailed)
            {
                StfLogger.LogError("Test failed");
            }

            StfLogger.LogInfo(StfArchiver.Status());
            StfLogger.CloseLogFile();

            StfArchiver.AddFile(StfLogger.FileName);

            if (TestDataDriven())
            {
                var iterationNo = DataRowIndex();

                if (iterationNo == TestContext.DataRow.Table.Rows.Count - 1)
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

                return;
            }

            StfArchiver.PerformArchive();

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
            return currentIteration;
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
    }
}

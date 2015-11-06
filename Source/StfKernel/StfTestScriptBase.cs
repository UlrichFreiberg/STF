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

    using Utilities.Configuration;

    /// <summary>
    /// BaseClass for all Stf test scripts.
    /// The class will set up the right <see cref="StfLogger"/> (MyLogger)
    /// and instantiate the <see cref="StfAssert"/> (MyAssert)
    /// </summary>
    [TestClass]
    public class StfTestScriptBase : StfKernel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StfTestScriptBase"/> class.
        /// </summary>
        public StfTestScriptBase()
        {
        }

        /// <summary>
        /// Gets the Stf Asserter.
        /// </summary>
        public StfAssert MyAssert { get; private set; }

        /// <summary>
        /// Gets the Stf logger.
        /// </summary>
        public IStfLogger MyLogger { get; private set; }

        /// <summary>
        /// Gets the Stf Archiver
        /// </summary>
        public StfArchiver MyArchiver { get; private set; }

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// The test context instance is set by the MsTestFrame work
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// The TestInitialize for <see cref="StfTestScriptBase"/>.
        /// </summary>
        [TestInitialize]
        public void BaseTestInitialize()
        {
            MyLogger = Get<IStfLogger>();
            MyLogger.LogTitle = TestContext.TestName;

            var logFilePostfix = string.Empty;
            var iterationNo = DataRowIndex();
            var iterationStatus = "Not datadriven";

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

            MyLogger.FileName = logFilename;
            MyAssert = new StfAssert(MyLogger);

            if (TestDataDriven())
            {
                for (var index = 0; index < TestContext.DataRow.Table.Columns.Count; index++)
                {
                    var headerCaption = TestContext.DataRow.Table.Columns[index].Caption;

                    MyLogger.LogInfo(string.Format("Column[{0}]=[{1}]", headerCaption, TestContext.DataRow[headerCaption]));
                }
            }

            SetUpArchiver(TestContext.TestName);

            LogBaseClassMessage("StfTestScriptBase TestInitialize");
            MyLogger.LogKeyValue("Test Iteration", iterationStatus, iterationStatus);
        }

        /// <summary>
        /// The test cleanup.
        /// </summary>
        [TestCleanup]
        public void BaseTestCleanup()
        {
            LogBaseClassMessage("StfTestScriptBase BaseTestCleanup");

            if (TestContext.CurrentTestOutcome != UnitTestOutcome.Passed)
            {
                MyLogger.LogError("Test failed");
            }

            MyLogger.CloseLogFile();

            MyArchiver.AddFile(MyLogger.FileName);

            if (TestDataDriven())
            {
                var iterationNo = DataRowIndex();

                if (iterationNo == TestContext.DataRow.Table.Rows.Count - 1)
                {
                    var myStfSummeryLogger = new StfSummeryLogger();
                    var summeryLogfileLogDirname = Path.GetDirectoryName(MyLogger.FileName);
                    var summeryLogfileLogFilename = Regex.Replace(Path.GetFileName(MyLogger.FileName), @"_[0-9]+\.html", ".html");
                    var summeryLogfilename = string.Format(@"{0}\SummeryLogfile_{1}", summeryLogfileLogDirname, summeryLogfileLogFilename);
                    var summeryLogfileLogfilePattern = Regex.Replace(Path.GetFileName(MyLogger.FileName), @"_[0-9]+\.html", "_*");

                    myStfSummeryLogger.CreateSummeryLog(summeryLogfilename, summeryLogfileLogDirname, summeryLogfileLogfilePattern);

                    MyArchiver.AddFile(summeryLogfilename);
                    ArchiveFilesIfNecessary();
                }

                return;
            }

            ArchiveFilesIfNecessary();
        }

        /// <summary>
        /// Make sure LogLevel is set to internal and set it back again
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        private void LogBaseClassMessage(string message)
        {
            var oldLoglevel = MyLogger.LogLevel;

            MyLogger.LogLevel = StfLogLevel.Internal;
            MyLogger.LogInternal(message);
            MyLogger.LogLevel = oldLoglevel;
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
            catch (Exception)
            {
                ////TODO: do something intelligent here
            }

            archiverConfiguration.ArchiveTopDir = StfTextUtils.ExpandVariables(archiverConfiguration.ArchiveTopDir);
            archiverConfiguration.ArchiveDestination = StfTextUtils.ExpandVariables(archiverConfiguration.ArchiveDestination);
            archiverConfiguration.TempDirectory = StfTextUtils.ExpandVariables(archiverConfiguration.TempDirectory);
            MyArchiver = new StfArchiver(archiverConfiguration, testName);
        }

        /// <summary>
        /// The archive files if necessary.
        /// </summary>
        private void ArchiveFilesIfNecessary()
        {
            MyArchiver.PerformArchive();
        }
    }
}

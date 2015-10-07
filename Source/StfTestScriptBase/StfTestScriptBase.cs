// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfTestScriptBase.cs" company="Foobar">
//   2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.IO;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mir.Stf.Utilities
{
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
            LogDirRoot = @"c:\temp\StfLogs";
        }

        /// <summary>
        /// Gets the log dir root. Where the logfiles is placed
        /// </summary>
        public string LogDirRoot { get; private set; }

        /// <summary>
        /// Gets the Stf Asserter.
        /// </summary>
        public StfAssert MyAssert { get; private set; }

        /// <summary>
        /// Gets the my logger.
        /// </summary>
        public StfLogger MyLogger { get; private set; }

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
            MyLogger = Get<StfLogger>();

            string logFilePostfix = string.Empty;
            var iterationNo = DataRowIndex();
            string iterationStatus = "Not datadriven";

            if (iterationNo >= 0)
            {
                logFilePostfix = string.Format("_{0}", iterationNo);
                iterationStatus = string.Format("Iteration {0}", iterationNo);
            }

            var logdir = Path.Combine(StfRoot, TestContext.TestName);

            if (!Directory.Exists(logdir))
            {
                Directory.CreateDirectory(logdir);
            }

            var logFilename = string.Format("{0}{1}.html", Path.Combine(logdir, TestContext.TestName), logFilePostfix);

            this.MyLogger.FileName = logFilename;
            this.MyAssert = new StfAssert(this.MyLogger);

            if (TestDataDriven())
            {
                for (var index = 0; index < TestContext.DataRow.Table.Columns.Count; index++)
                {
                    var headerCaption = TestContext.DataRow.Table.Columns[index].Caption;

                    this.MyLogger.LogInfo(string.Format("Column[{0}]=[{1}]", headerCaption, TestContext.DataRow[headerCaption]));
                }
            }

            LogBaseClassMessage("StfTestScriptBase TestInitialize");
            this.MyLogger.LogKeyValue("Test Iteration", iterationStatus, iterationStatus);
        }

        /// <summary>
        /// The test cleanup.
        /// </summary>
        [TestCleanup]
        public void BaseTestCleanup()
        {
            this.LogBaseClassMessage("StfTestScriptBase BaseTestCleanup");
            this.MyLogger.CloseLogFile();

            if (TestDataDriven())
            {
                var iterationNo = DataRowIndex();

                if (iterationNo == TestContext.DataRow.Table.Rows.Count - 1)
                {
                    var MyStfSummeryLogger = new StfSummeryLogger();
                    var SummeryLogfile_LogDirname = Path.GetDirectoryName(MyLogger.FileName);
                    var SummeryLogfile_LogFilename = Regex.Replace(Path.GetFileName(MyLogger.FileName), @"_[0-9]+\.html", ".html");
                    var SummeryLogfilename = string.Format(@"{0}\SummeryLogfile_{1}", SummeryLogfile_LogDirname, SummeryLogfile_LogFilename);
                    var SummeryLogfile_LogfilePattern  = Regex.Replace(Path.GetFileName(MyLogger.FileName), @"_[0-9]+\.html", "_*");

                    MyStfSummeryLogger.CreateSummeryLog(SummeryLogfilename, SummeryLogfile_LogDirname, SummeryLogfile_LogfilePattern);
                }
            }
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

        private bool TestDataDriven()
        {
            if (TestContext.DataRow == null)
            {
                return false;
            }
            
            return TestContext.DataRow.Table.Rows.Count > 0;
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfTestScriptBase.cs" company="Foobar">
//   2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Stf.Utilities
{
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// BaseClass for all Stf test scripts.
    /// The class will set up the right <see cref="StfLogger"/> (MyLogger)
    /// and instantiate the <see cref="StfAssert"/> (MyAssert)
    /// </summary>
    [TestClass]
    public class StfTestScriptBase : StfKernel
    {
        /// <summary>
        /// The log dir root. Where the logfiles is placed
        /// </summary>
        private const string LogDirRoot = @"c:\temp\StfLogs";

        /// <summary>
        /// Gets the Stf Asserter.
        /// </summary>
        public StfAssert MyAssert { get; private set; }

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
            string logFilePostfix = string.Empty;
            var iterationNo = DataRowIndex();
            string iterationStatus = "Not datadriven";

            if (iterationNo >= 0)
            {
                logFilePostfix = string.Format("_{0}", iterationNo);
                iterationStatus = string.Format("Iteration {0}", iterationNo);
            }

            var ovidName = string.Format("{0}{1}.html", Path.Combine(LogDirRoot, TestContext.TestName), logFilePostfix);

            if (!Directory.Exists(LogDirRoot))
            {
                Directory.CreateDirectory(LogDirRoot);
            }

            this.MyLogger.FileName = ovidName;
            this.MyAssert = new StfAssert(this.MyLogger);

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
        }

        /// <summary>
        /// Make sure StfLogLevel is set to internal and set it back again
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        private void LogBaseClassMessage(string message)
        {
            var oldLoglevel = MyLogger.StfLogLevel;
            MyLogger.StfLogLevel = StfLogLevel.Internal;
            this.MyLogger.LogInternal(message);
            MyLogger.StfLogLevel = oldLoglevel;
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
            if (TestContext.DataRow == null)
            {
                return -1;
            }

            var currentIteration = TestContext.DataRow.Table.Rows.IndexOf(TestContext.DataRow);
            return currentIteration;
        }
    }
}

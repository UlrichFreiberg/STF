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
    public class StfTestScriptBase
    {
        private const string LogDirRoot = @"c:\temp\StfLogs";

        /// <summary>
        /// Initializes a new instance of the <see cref="StfTestScriptBase"/> class.
        /// </summary>
        public StfTestScriptBase()
        {
            if (MyLogger == null)
            {
                this.MyLogger = new StfLogger();                
            }
        }

        /// <summary>
        /// Gets the Stf logger.
        /// </summary>
        public StfLogger MyLogger { get; private set; }

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
            var ovidName = string.Format("{0}.html", Path.Combine(LogDirRoot, TestContext.TestName));

            if (!Directory.Exists(LogDirRoot))
            {
                Directory.CreateDirectory(LogDirRoot);
            }

            this.MyLogger.FileName = ovidName;
            this.MyAssert = new StfAssert(this.MyLogger);

            LogBaseClassMessage("StfTestScriptBase TestInitialize");
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
        /// Make sure logLevel is set to internal and set it back again
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        private void LogBaseClassMessage(string message)
        {
            var oldLoglevel = MyLogger.LogLevel;
            MyLogger.LogLevel = LogLevel.Internal;
            this.MyLogger.LogInternal(message);
            MyLogger.LogLevel = oldLoglevel;
        }
    }
}

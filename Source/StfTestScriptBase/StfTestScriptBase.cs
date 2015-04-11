// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfTestScriptBase.cs" company="Foobar">
//   2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Stf.Utilities
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// BaseClass for all Stf test scripts.
    /// The class will set up the right <see cref="StfLogger"/> (MyLogger)
    /// and instantiate the <see cref="StfAssert"/> (MyAssert)
    /// </summary>
    [TestClass]
    public class StfTestScriptBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StfTestScriptBase"/> class.
        /// </summary>
        public StfTestScriptBase()
        {
            this.MyLogger = new StfLogger();
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
            var ovidName = string.Format(@"c:\temp\unittestlogger_{0}.html", TestContext.TestName);

            this.MyLogger.FileName = ovidName;

            this.MyAssert = new StfAssert(this.MyLogger)
            {
                EnableNegativeTesting = true
            };

            this.MyLogger.LogInfo("StfTestScriptBase TestInitialize");
        }

        /// <summary>
        /// The test cleanup.
        /// </summary>
        [TestCleanup]
        public void BaseTestCleanup()
        {
            this.MyLogger.LogInfo("StfTestScriptBase BaseTestCleanup");
            this.MyLogger.CloseLogFile();
        }
    }
}

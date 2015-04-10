// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestScriptHeaders.cs" company="Foobar">
//   2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Stf.Utilities
{
    using Stf.Utilities.Interfaces;

    /// <summary>
    /// The test result html logger.
    /// </summary>
    public partial class StfLogger : ITestScriptHeaders
    {
        /// <summary>
        /// Gets or sets the name of Current Test
        /// </summary>
        public string TestName { get; set; }

        // =============================================================
        // Used by Assertion functions
        // =============================================================

        /// <summary>
        /// The set run status.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool SetRunStatus()
        {
            var statusMsg = string.Empty;
            string retVal;
            LogLevel logLevel;

            if (NumberOfLoglevelMessages[LogLevel.Warning] > 0)
            {
                statusMsg += " - Test showed warnings";
                logLevel = LogLevel.Warning;
            }

            if (this.ErrorOrFail() > 0)
            {
                statusMsg += "Test showed errors";
                logLevel = LogLevel.Error;
            }
            else
            {
                statusMsg += "Test showed no errors";
                logLevel = LogLevel.Pass;
            }

            retVal = string.Format(@"<span id=""runstatus"">{0}</span>", statusMsg);
            LogOneHtmlMessage(logLevel, retVal);
            return true;
        }
    }
}

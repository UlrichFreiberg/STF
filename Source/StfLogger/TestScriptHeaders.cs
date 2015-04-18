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
            string statusMsg;
            StfLogLevel logLevel;

            if (this.ErrorOrFail() > 0)
            {
                statusMsg = "Test showed errors";
                logLevel = StfLogLevel.Error;
            }
            else
            {
                if (NumberOfLoglevelMessages[StfLogLevel.Warning] > 0)
                {
                    statusMsg = "Test showed warnings";
                    logLevel = StfLogLevel.Warning;
                }
                else
                {
                    statusMsg = "Test showed no errors";
                    logLevel = StfLogLevel.Pass;
                }
            }

            var runstats = string.Format(@"<span id=""runstatus"">{0}</span>", statusMsg);
            runstats += string.Format(
                        "<div class=\"line runstats\" passed=\"{0}\" failed=\"{1}\" Errors=\"{2}\" Warnings=\"{3}\">",
                        NumberOfLoglevelMessages[StfLogLevel.Pass],
                        NumberOfLoglevelMessages[StfLogLevel.Fail],
                        NumberOfLoglevelMessages[StfLogLevel.Error],
                        NumberOfLoglevelMessages[StfLogLevel.Warning]);

            runstats += string.Format(
                        "   {0} Passed, {1} Failed, {2} Errors, {3} Warnings",
                        NumberOfLoglevelMessages[StfLogLevel.Pass],
                        NumberOfLoglevelMessages[StfLogLevel.Fail],
                        NumberOfLoglevelMessages[StfLogLevel.Error],
                        NumberOfLoglevelMessages[StfLogLevel.Warning]);
            runstats += "</div>";

            LogOneHtmlMessage(logLevel, runstats);
            return true;
        }
    }
}

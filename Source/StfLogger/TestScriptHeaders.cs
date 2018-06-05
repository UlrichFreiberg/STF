// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestScriptHeaders.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities
{
    /// <summary>
    /// The test result html logger.
    /// </summary>
    public partial class StfLogger
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
            var statusMsg = GetStatusMsgAndSetLoglevel(out this.logLevel);
            var runstats = $@"<span id=""runstatus"">{statusMsg}</span>";

            runstats +=
                $"<div class=\"line runstats\" Passed=\"{NumberOfLoglevelMessages[StfLogLevel.Pass]}\" Failed=\"{NumberOfLoglevelMessages[StfLogLevel.Fail]}\" Inconclusive=\"{NumberOfLoglevelMessages[StfLogLevel.Inconclusive]}\" Errors=\"{NumberOfLoglevelMessages[StfLogLevel.Error]}\" Warnings=\"{NumberOfLoglevelMessages[StfLogLevel.Warning]}\">";

            runstats +=
                $"   {NumberOfLoglevelMessages[StfLogLevel.Pass]} Passed, {NumberOfLoglevelMessages[StfLogLevel.Fail]} Failed, {NumberOfLoglevelMessages[StfLogLevel.Inconclusive]} Inconclusive, {NumberOfLoglevelMessages[StfLogLevel.Error]} Errors, {NumberOfLoglevelMessages[StfLogLevel.Warning]} Warnings";

            runstats += "</div>";

            LogOneHtmlMessage(logLevel, runstats);
            return true;
        }

        /// <summary>
        /// The status msg.
        /// </summary>
        /// <param name="loglevel">
        /// The log level.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetStatusMsgAndSetLoglevel(out StfLogLevel loglevel)
        {
            if (this.ErrorOrFail() > 0)
            {
                loglevel = StfLogLevel.Error;
                return "Test showed errors";
            }

            if (this.NumberOfLoglevelMessages[StfLogLevel.Warning] > 0)
            {
                loglevel = StfLogLevel.Warning;
                return "Test showed warnings";
            }

            loglevel = StfLogLevel.Pass;
            if (this.NumberOfLoglevelMessages[loglevel] < 1)
            {
                this.NumberOfLoglevelMessages[loglevel] = 1;
            }

            return "Test showed no errors";
        }
    }
}

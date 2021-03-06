﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PerformanceManagement.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Mir.Stf.Utilities
{
    /// <summary>
    /// The test result html logger. The <c>IStfLoggerPerformanceManagement</c> part.
    /// </summary>
    public partial class StfLogger
    {
        /// <summary>
        /// Used for indicating performance issues - if not logging, then something takes a long time:-)
        /// </summary>
        private DateTime timeOfLastMessage;

        /// <summary>
        /// The check for performance alert.
        /// </summary>
        public void CheckForPerformanceAlert()
        {
            var elapsedTime = DateTime.Now - this.timeOfLastMessage;
            this.timeOfLastMessage = DateTime.Now;

            if (elapsedTime.Seconds > Configuration.AlertLongInterval)
            {
                LogPerformanceAlert(elapsedTime.TotalSeconds);
            }
        }

        /// <summary>
        /// how long time since last - any performance issues?
        /// </summary>
        /// <param name="elapsedTime">
        /// The elapsed time.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogPerformanceAlert(double elapsedTime)
        {
            var performanceAlert = $"PerfAlert: {elapsedTime} seconds since last logEntry";

            return LogWarning(performanceAlert);
        }
    }
}

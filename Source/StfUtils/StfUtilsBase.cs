// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfUtilsBase.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The stf utils base.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities
{
    using System;

    /// <summary>
    /// The stf utils base.
    /// </summary>
    public class StfUtilsBase
    {
        /// <summary>
        /// The log info.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        internal void LogInfo(string message)
        {
            LogMessage("INFO ", message);
        }

        /// <summary>
        /// The log error.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        protected void LogError(string message)
        {
            LogMessage("ERROR", message);
        }

        /// <summary>
        /// The log message.
        /// </summary>
        /// <param name="level">
        /// The level.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        private void LogMessage(string level, string message)
        {
            var messageToLog = $"{DateTime.Now} : {level} : {message}";

            Console.WriteLine(messageToLog);
        }
    }
}
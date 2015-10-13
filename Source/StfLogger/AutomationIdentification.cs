// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutomationIdentification.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Mir.Stf.Utilities.Interfaces;

namespace Mir.Stf.Utilities
{
    /// <summary>
    /// The test result logger. The <see cref="IStfLoggerAutomationIdentification"/> part.
    /// </summary>
    public partial class StfLogger : IStfLoggerAutomationIdentification
    {
        /// <summary>
        /// The log automation id object func.
        /// </summary>
        /// <param name="stfLogLevel">
        /// The log level.
        /// </param>
        /// <param name="automationIdObj">
        /// The automation id obj.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public delegate int LogAutomationIdObjectFunc(StfLogLevel stfLogLevel, object automationIdObj, string message);

        /// <summary>
        /// Gets or sets the LogAutomationIdObject user function.
        /// </summary>
        public LogAutomationIdObjectFunc LogAutomationIdObjectUserFunction { get; set; }

        /// <summary>
        /// Dump an AutomationIdentification <c>object</c>.
        /// </summary>
        /// <param name="loglevel">
        /// The log level.
        /// </param>
        /// <param name="automationIdObj">
        /// The automation id obj.
        /// </param>
        /// <param name="message">
        /// The Message.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogAutomationIdObject(StfLogLevel loglevel, object automationIdObj, string message)
        {
            return LogAutomationIdObjectUserFunction(loglevel, automationIdObj, message);
        }
    }
}

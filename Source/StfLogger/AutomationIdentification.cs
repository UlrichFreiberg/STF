// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutomationIdentification.cs" company="Foobar">
//   2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Mir.Stf.Utilities.Interfaces;

namespace Mir.Stf.Utilities
{
    /// <summary>
    /// The test result logger. The <see cref="IAutomationIdentification"/> part.
    /// </summary>
    public partial class StfLogger : IAutomationIdentification
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
        /// <param name="logLevel">
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
        public int LogAutomationIdObject(StfLogLevel logLevel, object automationIdObj, string message)
        {
            return LogAutomationIdObjectUserFunction(logLevel, automationIdObj, message);
        }
    }
}

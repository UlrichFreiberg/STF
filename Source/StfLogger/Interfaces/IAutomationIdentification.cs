// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAutomationIdentification.cs" company="Foobar">
//   2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Stf.Utilities.Interfaces
{
    /// <summary>
    /// The AutomationIdentification <c>interface</c>.
    /// </summary>
    public interface IAutomationIdentification
    {
        /// <summary>
        /// dump an AutomationIdentification to the log
        /// </summary>
        /// <param name="logLevel">
        /// The log level.
        /// </param>
        /// <param name="automationIdObj">
        /// The automation id obj.
        /// </param>
        /// <param name="message">
        /// Describing the Aid.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogAutomationIdObject(StfLogLevel logLevel, object automationIdObj, string message);
    }
}

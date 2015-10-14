// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStfLoggerAutomationIdentification.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.Interfaces
{
    /// <summary>
    /// The AutomationIdentification <c>interface</c>.
    /// </summary>
    public interface IStfLoggerAutomationIdentification
    {
        /// <summary>
        /// dump an AutomationIdentification to the log
        /// </summary>
        /// <param name="loglevel">
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
        int LogAutomationIdObject(StfLogLevel loglevel, object automationIdObj, string message);
    }
}

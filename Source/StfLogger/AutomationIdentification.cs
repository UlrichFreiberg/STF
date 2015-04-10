﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutomationIdentification.cs" company="Foobar">
//   2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Stf.Utilities
{
    using Stf.Utilities.Interfaces;

    /// <summary>
    /// The test result logger. The <see cref="IAutomationIdentification"/> part.
    /// </summary>
    public partial class StfLogger : IAutomationIdentification
    {
        /// <summary>
        /// The log automation id object func.
        /// </summary>
        /// <param name="logLevel">
        /// The log level.
        /// </param>
        /// <param name="automationIdObj">
        /// The automation id obj.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        public delegate int LogAutomationIdObjectFunc(LogLevel logLevel, object automationIdObj, string message);

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
        public int LogAutomationIdObject(LogLevel logLevel, object automationIdObj, string message)
        {
            return LogAutomationIdObjectUserFunction(logLevel, automationIdObj, message);
        }
    }
}

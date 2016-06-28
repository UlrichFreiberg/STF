// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStfLoggerLoggingFunctions.cs" company="Mir Software">
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
    /// The LoggingFunctions <c>interface</c>.
    /// </summary>
    public interface IStfLoggerLoggingFunctions
    {
        // normal logging functions - test scripts/models/adapters

        /// <summary>
        /// Logging one error <paramref name="message"/>. Something bad has happened.
        /// </summary>
        /// <param name="message">
        /// The message describing the error
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogError(string message, params object[] args);

        /// <summary>
        /// Logging one warning <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// C:\Users\Ulrich\Documents\GitHub\StfLogger\StfLogger\Interfaces\IStfLoggerLoggingFunctions.cs
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogWarning(string message, params object[] args);

        /// <summary>
        /// Logging one info <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogInfo(string message, params object[] args);

        /// <summary>
        /// Logging one debug <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogDebug(string message, params object[] args);

        // normal logging functions - models and adapters

        /// <summary>
        /// Logging one trace <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogTrace(string message, params object[] args);

        /// <summary>
        /// Logging one <c>internal</c> <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogInternal(string message, params object[] args);

        // used solely by Assert functions

        /// <summary>
        /// Logging a test step assertion did pass.
        /// </summary>
        /// <param name="testStepName">
        /// The test step name.
        /// </param>
        /// <param name="message">
        /// Further information related to the passing test step - if any.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogPass(string testStepName, string message, params object[] args);

        /// <summary>
        /// Logging a test step assertion did fail.
        /// </summary>
        /// <param name="testStepName">
        /// The test step name.
        /// </param>
        /// <param name="message">
        /// Further information related to the failing test step - if any.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogFail(string testStepName, string message, params object[] args);

        /// <summary>
        /// The log inconclusive.
        /// </summary>
        /// <param name="testStepName">
        /// The test Step Name.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogInconclusive(string testStepName, string message, params object[] args);

        /// <summary>
        /// Logging a KeyValue pair - as you see fit. The logger will log some, that are generally useful - like OS
        /// Machine, CurrentDirectory etc..
        /// </summary>
        /// <param name="key">
        /// The <c>key</c> part of the pair.
        /// </param>
        /// <param name="value">
        /// The <c>value</c> part of the pair.
        /// </param>
        /// <param name="message">
        /// Further information related to the KeyValue pair - if any.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogKeyValue(string key, string value, string message, params object[] args);

        // Headers in test scripts

        /// <summary>
        /// Insert a header in the log file - makes it easier to overview the logfile
        /// </summary>
        /// <param name="headerMessage">
        /// The header message.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogHeader(string headerMessage, params object[] args);

        /// <summary>
        /// Insert a sub header in the log file - makes it easier to overview the logfile
        /// </summary>
        /// <param name="subHeaderMessage">
        /// The sub header message.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogSubHeader(string subHeaderMessage, params object[] args);

        /// <summary>
        /// The log xml message.
        /// </summary>
        /// <param name="xmlMessage">
        /// The xml message.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogXmlMessage(string xmlMessage);
    }
}

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
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogError(string message);

        /// <summary>
        /// Logging one warning <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// C:\Users\Ulrich\Documents\GitHub\StfLogger\StfLogger\Interfaces\IStfLoggerLoggingFunctions.cs
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogWarning(string message);

        /// <summary>
        /// Logging one info <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogInfo(string message);

        /// <summary>
        /// Logging one debug <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogDebug(string message);

        // normal logging functions - models and adapters

        /// <summary>
        /// Logging one trace <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogTrace(string message);

        /// <summary>
        /// Logging one <c>internal</c> <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogInternal(string message);

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
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogPass(string testStepName, string message);

        /// <summary>
        /// Logging a test step assertion did fail.
        /// </summary>
        /// <param name="testStepName">
        /// The test step name.
        /// </param>
        /// <param name="message">
        /// Further information related to the failing test step - if any.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogFail(string testStepName, string message);

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
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogKeyValue(string key, string value, string message);

        // Headers in test scripts

        /// <summary>
        /// Insert a header in the log file - makes it easier to overview the logfile
        /// </summary>
        /// <param name="headerMessage">
        /// The header message.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogHeader(string headerMessage);

        /// <summary>
        /// Insert a sub header in the log file - makes it easier to overview the logfile
        /// </summary>
        /// <param name="subHeaderMessage">
        /// The sub header message.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogSubHeader(string subHeaderMessage);
    }
}

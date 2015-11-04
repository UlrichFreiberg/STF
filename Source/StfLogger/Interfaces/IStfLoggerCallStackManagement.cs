// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStfLoggerCallStackManagement.cs" company="Mir Software">
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
    /// The CallStackManagement <see langword="interface"/>. If used by the logger, it will indent the logging - indicating a call stack.
    /// </summary>
    public interface IStfLoggerCallStackManagement
    {
        /// <summary>
        /// The log function enter.
        /// </summary>
        /// <param name="loglevel">
        /// The loglevel.
        /// </param>
        /// <param name="nameOfReturnType">
        /// The name of return type.
        /// </param>
        /// <param name="functionName">
        /// The function name.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogFunctionEnter(StfLogLevel loglevel, string nameOfReturnType, string functionName);

        /// <summary>
        /// The log function enter. Should be called/inserted when entering a model/adapter function.
        /// </summary>
        /// <param name="loglevel">
        /// The log level.
        /// </param>
        /// <param name="nameOfReturnType">
        /// The name of return type.
        /// </param>
        /// <param name="functionName">
        /// The function name.
        /// </param>
        /// <param name="argValues">
        /// The <c>arg</c> values.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogFunctionEnter(StfLogLevel loglevel, string nameOfReturnType, string functionName, object[] argValues);

        /// <summary>
        /// The log function exit.
        /// </summary>
        /// <param name="loglevel">
        /// The loglevel.
        /// </param>
        /// <param name="functionName">
        /// The function name.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogFunctionExit(StfLogLevel loglevel, string functionName);

        /// <summary>
        /// The log function exit. Should be called/inserted when exiting a model/adapter function.
        /// </summary>
        /// <param name="loglevel">
        /// The log level.
        /// </param>
        /// <param name="functionName">
        /// The function name.
        /// </param>
        /// <param name="returnValue">
        /// The return value.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogFunctionExit(StfLogLevel loglevel, string functionName, object returnValue);

        // Properties in models/adapters

        /// <summary>
        /// The log get.
        /// </summary>
        /// <param name="loglevel">
        /// The log level.
        /// </param>
        /// <param name="callingProperty">
        /// The calling property.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogGetEnter(StfLogLevel loglevel, string callingProperty);

        /// <summary>
        /// The log get exit.
        /// </summary>
        /// <param name="loglevel">
        /// The loglevel.
        /// </param>
        /// <param name="callingProperty">
        /// The calling property.
        /// </param>
        /// <param name="getValue">
        /// The get value.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogGetExit(StfLogLevel loglevel, string callingProperty, object getValue);

        /// <summary>
        /// The log set.
        /// </summary>
        /// <param name="loglevel">
        /// The log level.
        /// </param>
        /// <param name="callingProperty">
        /// The calling property.
        /// </param>
        /// <param name="setValue">
        /// The set value.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogSetEnter(StfLogLevel loglevel, string callingProperty, object setValue);

        /// <summary>
        /// The log set exit.
        /// </summary>
        /// <param name="loglevel">
        /// The loglevel.
        /// </param>
        /// <param name="callingProperty">
        /// The calling property.
        /// </param>
        /// <param name="setValue">
        /// The set value.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogSetExit(StfLogLevel loglevel, string callingProperty, object setValue);
    }
}

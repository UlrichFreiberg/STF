// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICallStackManagement.cs" company="Foobar">
//   2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.Interfaces
{
    /// <summary>
    /// The CallStackManagement <see langword="interface"/>. If used by the logger, it will indent the logging - indicating a call stack.
    /// </summary>
    public interface ICallStackManagement
    {
        /// <summary>
        /// The log function enter. Should be called/inserted when entering a model/adapter function.
        /// </summary>
        /// <param name="logLevel">
        /// The log level.
        /// </param>
        /// <param name="nameOfReturnType">
        /// The name of return type.
        /// </param>
        /// <param name="functionName">
        /// The function name.
        /// </param>
        /// <param name="args">
        /// The <c>args</c>.
        /// </param>
        /// <param name="argValues">
        /// The <c>arg</c> values.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogFunctionEnter(StfLogLevel logLevel, string nameOfReturnType, string functionName, string[] args, object[] argValues);

        /// <summary>
        /// The log function exit. Should be called/inserted when exiting a model/adapter function.
        /// </summary>
        /// <param name="lLogLevel">
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
        int LogFunctionExit(StfLogLevel lLogLevel, string functionName, object returnValue);

        // Properties in models/adapters

        /// <summary>
        /// The log get.
        /// </summary>
        /// <param name="logLevel">
        /// The log level.
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
        int LogGet(StfLogLevel logLevel, string callingProperty, object getValue);

        /// <summary>
        /// The log set.
        /// </summary>
        /// <param name="logLevel">
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
        int LogSet(StfLogLevel logLevel, string callingProperty, object setValue);
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CallStackManagement.cs" company="Foobar">
//   2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Mir.Stf.Utilities.Interfaces;

namespace Mir.Stf.Utilities
{
    /// <summary>
    /// The test result html logger. The <see cref="ICallStackManagement"/> part
    /// </summary>
    public partial class StfLogger : ICallStackManagement
    {
        /// <summary>
        /// The _call stack.
        /// </summary>
        private readonly Stack<string> callStack = new Stack<string>();

        // =============================================================
        // Functions in models/adapters
        // =============================================================

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
        public int LogFunctionEnter(StfLogLevel logLevel, string nameOfReturnType, string functionName, string[] args, object[] argValues)
        {
            const string ArgsString = "TODO: Concatenated string of argName and Values";
            var message = string.Format("> {0} {1} returning {2}", functionName, ArgsString, nameOfReturnType);

            this.callStack.Push(functionName);
            return LogOneHtmlMessage(logLevel, message);
        }

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
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogFunctionEnter(StfLogLevel logLevel, string nameOfReturnType, string functionName)
        {
            return LogFunctionEnter(logLevel, nameOfReturnType, functionName, null, null);
        }

        /// <summary>
        /// The log function exit. Should be called/inserted when exiting a model/adapter function.
        /// </summary>
        /// <param name="logLevel">
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
        public int LogFunctionExit(StfLogLevel logLevel, string functionName, object returnValue)
        {
            var poppedName = this.callStack.Pop();
            var message = string.Format("< Exited {0} returning {1}", poppedName, "returnValue.ToString");

            return LogOneHtmlMessage(logLevel, message);
        }

        /// <summary>
        /// The log function exit. Should be called/inserted when exiting a model/adapter function.
        /// </summary>
        /// <param name="logLevel">
        /// The log level.
        /// </param>
        /// <param name="functionName">
        /// The function name.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogFunctionExit(StfLogLevel logLevel, string functionName)
        {
            return LogFunctionExit(logLevel, functionName, null);
        }

        // =============================================================
        // Properties in models/adapters
        // =============================================================

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
        public int LogGet(StfLogLevel logLevel, string callingProperty, object getValue)
        {
            var valueString = getValue == null ? "null" : getValue.ToString();
            var message = string.Format("Property {0} Get value [{1}]", callingProperty, valueString);

            return LogOneHtmlMessage(logLevel, message);
        }

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
        public int LogSet(StfLogLevel logLevel, string callingProperty, object setValue)
        {
            var valueString = setValue == null ? "null" : setValue.ToString();
            var message = string.Format("Property {0} Set value [{1}]", callingProperty, valueString);

            return LogOneHtmlMessage(logLevel, message);
        }

        /// <summary>
        /// The indent string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string IndentString()
        {
            var dotCount = this.callStack.Count;
            var retVal = string.Empty;

            for (var i = 0; i < dotCount; i++)
            {
                retVal += " . . .";
            }

            return retVal;
        }
    }
}

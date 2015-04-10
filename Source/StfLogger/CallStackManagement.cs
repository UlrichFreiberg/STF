// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CallStackManagement.cs" company="Foobar">
//   2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Stf.Utilities
{
    using System.Collections.Generic;

    using Stf.Utilities.Interfaces;

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
        public int LogFunctionEnter(LogLevel logLevel, string nameOfReturnType, string functionName, string[] args, object[] argValues)
        {
            string message;
            string argsString;

            argsString = "TODO: Concatenated string of argName and Values";

            message = string.Format("> {0} {1} returning {2}", functionName, argsString, nameOfReturnType);
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
        public int LogFunctionEnter(LogLevel logLevel, string nameOfReturnType, string functionName)
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
        public int LogFunctionExit(LogLevel logLevel, string functionName, object returnValue)
        {
            string message;
            string poppedName;

            poppedName = this.callStack.Pop();
            message = string.Format("< Exited {0} returning {1}", poppedName, "returnValue.ToString");
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
        public int LogFunctionExit(LogLevel logLevel, string functionName)
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
        public int LogGet(LogLevel logLevel, string callingProperty, object getValue)
        {
            string message;
            string valueString;

            valueString = getValue == null ? "null" : getValue.ToString();

            message = string.Format("Property {0} Get value [{1}]", callingProperty, valueString);
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
        public int LogSet(LogLevel logLevel, string callingProperty, object setValue)
        {
            string message;
            string valueString;

            valueString = setValue == null ? "null" : setValue.ToString();

            message = string.Format("Property {0} Set value [{1}]", callingProperty, valueString);
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

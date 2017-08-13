// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CallStackManagement.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Text;
using Mir.Stf.Utilities.Interfaces;

namespace Mir.Stf.Utilities
{
    /// <summary>
    /// The test result html logger. The <see cref="IStfLoggerCallStackManagement"/> part
    /// </summary>
    public partial class StfLogger
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
        public int LogFunctionEnter(StfLogLevel loglevel, string nameOfReturnType, string functionName, object[] argValues)
        {
            var argsString = CreateStringFromArgsAndArgsValues(argValues);
            var message = $"> Entering [{functionName}] with values [{argsString}] returning [{nameOfReturnType}]";

            callStack.Push(functionName);
            return LogOneHtmlMessage(loglevel, message);
        }

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
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogFunctionEnter(StfLogLevel loglevel, string nameOfReturnType, string functionName)
        {
            return LogFunctionEnter(loglevel, nameOfReturnType, functionName, null);
        }

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
        public int LogFunctionExit(StfLogLevel loglevel, string functionName, object returnValue)
        {
            var message = $"< Exited [{functionName}] returning [{returnValue}]";
            var retVal = LogOneHtmlMessage(loglevel, message);

            callStack.Pop();

            return retVal;
        }

        /// <summary>
        /// The log function exit. Should be called/inserted when exiting a model/adapter function.
        /// </summary>
        /// <param name="loglevel">
        /// The log level.
        /// </param>
        /// <param name="functionName">
        /// The function name.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogFunctionExit(StfLogLevel loglevel, string functionName)
        {
            return LogFunctionExit(loglevel, functionName, null);
        }

        // =============================================================
        // Properties in models/adapters
        // =============================================================

        /// <summary>
        /// The log get.
        /// </summary>
        /// <param name="loglevel">
        /// The loglevel.
        /// </param>
        /// <param name="callingProperty">
        /// The calling property.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogGetEnter(StfLogLevel loglevel, string callingProperty)
        {
            var propName = GetLogFriendlyPropName(callingProperty);
            var message = $"> Entering Get [{propName}]";

            callStack.Push(propName);

            return LogOneHtmlMessage(loglevel, message);
        }

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
        public int LogGetExit(StfLogLevel loglevel, string callingProperty, object getValue)
        {
            var propName = GetLogFriendlyPropName(callingProperty);
            var valueString = getValue?.ToString() ?? "null";
            var message = $"< Exiting Get [{propName}] with value [{valueString}]";

            var retVal = LogOneHtmlMessage(loglevel, message);

            callStack.Pop();

            return retVal;
        }

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
        public int LogSetEnter(StfLogLevel loglevel, string callingProperty, object setValue)
        {
            var propName = GetLogFriendlyPropName(callingProperty);
            var valueString = setValue?.ToString() ?? "null";
            var message = $"> Entering Set [{propName}] with value [{valueString}]";

            callStack.Push(propName);

            return LogOneHtmlMessage(loglevel, message);
        }

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
        public int LogSetExit(StfLogLevel loglevel, string callingProperty, object setValue)
        {
            var propName = GetLogFriendlyPropName(callingProperty);
            var valueString = setValue?.ToString() ?? "null";
            var message = $"< Exiting Set [{propName}] after setting value [{valueString}]";

            var retVal = LogOneHtmlMessage(loglevel, message);

            callStack.Pop();

            return retVal;
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

        /// <summary>
        /// The create string from parameter collection.
        /// </summary>
        /// <param name="argumentValues">
        /// The argument values.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string CreateStringFromArgsAndArgsValues(object[] argumentValues)
        {
            if (argumentValues == null)
            {
                return string.Empty;
            }

            var formattedString = "{0}, ";
            var stringBuilder = new StringBuilder();

            foreach (object argValue in argumentValues)
            {
                if (argumentValues.Length == 1 || argumentValues[argumentValues.Length - 1] == argValue)
                {
                    formattedString = "{0}";
                }

                stringBuilder.Append(string.Format(formattedString, argValue));
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// The get log friendly prop name.
        /// </summary>
        /// <param name="propName">
        /// The prop name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetLogFriendlyPropName(string propName)
        {
            return string.IsNullOrEmpty(propName) ? propName : propName.Replace("get_", string.Empty).Replace("set_", string.Empty);
        }
    }
}

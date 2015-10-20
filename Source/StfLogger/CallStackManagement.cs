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
    public partial class StfLogger : IStfLoggerCallStackManagement
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
            var message = string.Format("> Entering [{0}] with values [{1}] returning [{2}]", functionName, argsString, nameOfReturnType);

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
            var poppedName = this.callStack.Pop();
            var message = string.Format("< Exited [{0}] returning [{1}]", poppedName, returnValue);

            return LogOneHtmlMessage(loglevel, message);
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
            var message = string.Format("Entering Get Property [{0}]", propName);

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
            var valueString = getValue == null ? "null" : getValue.ToString();
            var message = string.Format("Exiting Get Property [{0}] with value [{1}]", propName, valueString);

            return LogOneHtmlMessage(loglevel, message);
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
            var valueString = setValue == null ? "null" : setValue.ToString();
            var message = string.Format("Entering Set Property [{0}] with value [{1}]", propName, valueString);

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
            var valueString = setValue == null ? "null" : setValue.ToString();
            var message = string.Format("Exiting Set Property [{0}] after setting value [{1}]", propName, valueString);

            return LogOneHtmlMessage(loglevel, message);
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

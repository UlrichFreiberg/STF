// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggingHandler.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the LoggingHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Text;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Mir.Stf.Utilities.Interceptors
{
    /// <summary>
    /// The logging handler.
    /// </summary>
    public class LoggingHandler : ICallHandler
    {
        /// <summary>
        /// The stf logger.
        /// </summary>
        private readonly StfLogger stfLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingHandler"/> class.
        /// </summary>
        /// <param name="stfLogger">
        /// The stf logger.
        /// </param>
        public LoggingHandler(StfLogger stfLogger)
        {
            this.stfLogger = stfLogger;
        }

        /// <summary>
        /// Type of message to log. Before method call and after
        /// </summary>
        private enum LogMessageType
        {
            /// <summary>
            /// Before method/property call
            /// </summary>
            Enter,

            /// <summary>
            /// After method property call
            /// </summary>
            Exit
        }

        /// <summary>
        /// The method types.
        /// </summary>
        private enum MemberType
        {
            /// <summary>
            /// The set property.
            /// </summary>
            SetProperty,

            /// <summary>
            /// The get property.
            /// </summary>
            GetProperty,

            /// <summary>
            /// The method.
            /// </summary>
            Method
        }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// The invoke.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <param name="getNext">
        /// The get next.
        /// </param>
        /// <returns>
        /// The <see cref="IMethodReturn"/>.
        /// </returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            LogThisInfo(input.MethodBase.Name, LogMessageType.Enter, input.Inputs, string.Empty);

            var result = getNext().Invoke(input, getNext);

            if (result.Exception != null)
            {
                stfLogger.LogError(string.Format(
                    "Encountered error [{0}] when invoking [{1}]", 
                    result.Exception.Message,
                    input.MethodBase.Name));
            }
            else
            {
                LogThisInfo(input.MethodBase.Name, LogMessageType.Exit, input.Inputs, result.ReturnValue);
            }

            return result;
        }

        /// <summary>
        /// The log this info.
        /// </summary>
        /// <param name="methodName">
        /// Name of method or property
        /// </param>
        /// <param name="messageType">
        /// Enter or exit
        /// </param>
        /// <param name="inputs">
        /// Inputs for method or property call
        /// </param>
        /// <param name="returnValue">
        /// Returnvalue of method call
        /// </param>
        private void LogThisInfo(string methodName, LogMessageType messageType, IParameterCollection inputs, object returnValue)
        {
            var typeOfMethod = GetTypeOfMember(methodName);
            switch (typeOfMethod)
            {
                case MemberType.SetProperty:
                    switch (messageType)
                    {
                        case LogMessageType.Enter:
                            var enterSet = "Setting [{0}] to value [{1}]";
                            LogPropertyMessage(methodName, enterSet, CreateStringFromParameterCollection(inputs));
                            break;
                        case LogMessageType.Exit:
                            var exitSet = "Set [{0}] to value [{1}]";
                            LogPropertyMessage(methodName, exitSet, CreateStringFromParameterCollection(inputs));
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(@"messageType", messageType.ToString(), @"Unknown log message type");
                    }

                    break;
                case MemberType.GetProperty:
                    switch (messageType)
                    {
                        case LogMessageType.Enter:
                            var enterGet = "Getting value from [{0}]";
                            LogPropertyMessage(methodName, enterGet, null);
                            break;
                        case LogMessageType.Exit:
                            var exitGet = "Got value [{1}] from [{0}]";
                            LogPropertyMessage(methodName, exitGet, returnValue);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(@"messageType", messageType.ToString(), @"Unknown log message type");
                    }

                    break;
                case MemberType.Method:
                    switch (messageType)
                    {
                        case LogMessageType.Enter:
                            LogEnterMethodCall(methodName, CreateStringFromParameterCollection(inputs));
                            break;
                        case LogMessageType.Exit:
                            LogExitMethodCall(methodName, returnValue);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(@"messageType", messageType.ToString(), @"Unknown log message type");
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// The log property message.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        /// <param name="templateMessage">
        /// The template message.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        private void LogPropertyMessage(string propertyName, string templateMessage, object value)
        {
            if (value == null)
            {
                value = string.Empty;
            }

            var propName = propertyName.Replace("get_", string.Empty).Replace("set_", string.Empty);
            stfLogger.LogInfo(string.Format(templateMessage, propName, value));
        }

        /// <summary>
        /// Log call to a method
        /// </summary>
        /// <param name="methodName">
        /// Name of method
        /// </param>
        /// <param name="returnValue">
        /// The object representing the return value
        /// </param>
        private void LogExitMethodCall(string methodName, object returnValue)
        {
            var logMessage = "Exiting [{0}] with return value [{1}]";
            if (returnValue == null)
            {
                logMessage = "Exiting [{0}]";
            }

            stfLogger.LogInfo(string.Format(logMessage, methodName, returnValue));
        }

        /// <summary>
        /// Log message when entering method
        /// </summary>
        /// <param name="methodName">
        /// The name of the method
        /// </param>
        /// <param name="methodParametersAsString">
        /// Parameters as a string
        /// </param>
        private void LogEnterMethodCall(string methodName, string methodParametersAsString)
        {
            var logMessage = "Entering [{0}] with parameters [{1}]";
            if (string.IsNullOrEmpty(methodParametersAsString))
            {
                logMessage = "Entering [{0}]";
            }

            stfLogger.LogInfo(string.Format(logMessage, methodName, methodParametersAsString));
        }

        /// <summary>
        /// The get method type.
        /// </summary>
        /// <param name="methodName">
        /// The method name.
        /// </param>
        /// <returns>
        /// The <see cref="MemberType"/>.
        /// </returns>
        private MemberType GetTypeOfMember(string methodName)
        {
            // TODO: Use MemberInfo.TypeInfo ...
            if (methodName.ToLower().StartsWith("get_"))
            {
                return MemberType.GetProperty;
            }

            return methodName.ToLower().StartsWith("set_") ? MemberType.SetProperty : MemberType.Method;
        }

        /// <summary>
        /// Create a string from collection of parameters
        /// </summary>
        /// <param name="parameterCollection">
        /// Parameters to convert to a string
        /// </param>
        /// <returns>
        /// A string containing names of all parameters in collection
        /// </returns>
        private string CreateStringFromParameterCollection(IParameterCollection parameterCollection)
        {
            if (parameterCollection == null)
            {
                return string.Empty;
            }

            var formattedString = "{0}, ";
            var stringBuilder = new StringBuilder();
            foreach (var param in parameterCollection)
            {
                if (parameterCollection.IndexOf(param) + 1 == parameterCollection.Count ||
                    parameterCollection.Count == 1)
                {
                    formattedString = "{0}";
                }

                stringBuilder.Append(string.Format(formattedString, param));
            }

            return stringBuilder.ToString();
        }
    }
}

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
using System.Reflection;
using System.Text;
using Microsoft.Practices.Unity.InterceptionExtension;
using Mir.Stf.Utilities.Extensions;

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
            LogThisMessage(input.MethodBase.Name, LogMessageType.Enter, input.Inputs, GetReturnType(input.MethodBase), string.Empty);

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
                LogThisMessage(input.MethodBase.Name, LogMessageType.Exit, input.Inputs, GetReturnType(input.MethodBase), result.ReturnValue);
            }

            return result;
        }

        /// <summary>
        /// The log this message.
        /// </summary>
        /// <param name="methodName">
        /// The method name.
        /// </param>
        /// <param name="messageType">
        /// The message type.
        /// </param>
        /// <param name="inputs">
        /// The inputs.
        /// </param>
        /// <param name="returnTypeName">
        /// The return Type Name.
        /// </param>
        /// <param name="returnValue">
        /// The return value.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If unrecognized membertype
        /// </exception>
        private void LogThisMessage(
            string methodName, 
            LogMessageType messageType, 
            IParameterCollection inputs,
            string returnTypeName, 
            object returnValue)
        {
            var typeOfMethod = GetTypeOfMember(methodName);
            switch (typeOfMethod)
            {
                case MemberType.SetProperty:
                    switch (messageType)
                    {
                        case LogMessageType.Enter:
                            stfLogger.LogSetEnter(StfLogLevel.Info, methodName, CreateStringFromParameterCollection(inputs));
                            break;
                        case LogMessageType.Exit:
                            stfLogger.LogSetExit(StfLogLevel.Info, methodName, CreateStringFromParameterCollection(inputs));
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(@"messageType", messageType.ToString(), @"Unknown log message type");
                    }

                    break;
                case MemberType.GetProperty:
                    switch (messageType)
                    {
                        case LogMessageType.Enter:
                            stfLogger.LogGetEnter(StfLogLevel.Info, methodName);
                            break;
                        case LogMessageType.Exit:
                            stfLogger.LogGetExit(StfLogLevel.Info, methodName, returnValue);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(@"messageType", messageType.ToString(), @"Unknown log message type");
                    }

                    break;
                case MemberType.Method:
                    switch (messageType)
                    {
                        case LogMessageType.Enter:
                            stfLogger.LogFunctionEnter(StfLogLevel.Info, returnTypeName, methodName, inputs.ToArray());
                            break;
                        case LogMessageType.Exit:
                            stfLogger.LogFunctionExit(StfLogLevel.Info, methodName, returnValue);
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

        /// <summary>
        /// The get return type.
        /// </summary>
        /// <param name="methodBase">
        /// The method base.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetReturnType(MethodBase methodBase)
        {
            var methodInfo = methodBase as MethodInfo;
            return methodInfo == null ? string.Empty : methodInfo.ReturnType.Name;
        }

    }
}

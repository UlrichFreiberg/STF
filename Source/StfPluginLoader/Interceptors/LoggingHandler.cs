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
using Mir.Stf.Utilities.Interfaces;

namespace Mir.Stf.Utilities.Interceptors
{
    using System.Linq;
    using System.Text.RegularExpressions;

    using Mir.Stf.Utilities.Attributes;

    /// <summary>
    /// The logging handler.
    /// </summary>
    public class LoggingHandler : ICallHandler
    {
        /// <summary>
        /// The stf logger.
        /// </summary>
        private readonly IStfLogger stfLogger;

        /// <summary>
        /// The default log level.
        /// </summary>
        private StfLogLevel defaultLogLevel = StfLogLevel.Info;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingHandler"/> class.
        /// </summary>
        /// <param name="stfLogger">
        /// The stf logger.
        /// </param>
        public LoggingHandler(IStfLogger stfLogger)
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
            LogThisMessage(input.MethodBase, LogMessageType.Enter, input.Inputs, GetReturnType(input.MethodBase), string.Empty);

            var result = getNext().Invoke(input, getNext);

            if (result.Exception != null)
            {
                stfLogger.LogError(string.Format(
                    "Encountered error [{0}] when invoking [{1}]", 
                    result.Exception.Message,
                    input.MethodBase.Name));

                stfLogger.LogDebug(result.Exception.StackTrace);
                stfLogger.LogScreenshot(StfLogLevel.Error, "Error encountered");
            }
            else
            {
                LogThisMessage(input.MethodBase, LogMessageType.Exit, input.Inputs, GetReturnType(input.MethodBase), result.ReturnValue);
            }

            return result;
        }

        /// <summary>
        /// The log this message.
        /// </summary>
        /// <param name="methodBase">
        /// The method Base.
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
            MethodBase methodBase, 
            LogMessageType messageType, 
            IParameterCollection inputs,
            string returnTypeName, 
            object returnValue)
        {
            var methodName = methodBase.Name;
            var typeOfMethod = GetTypeOfMember(methodName);
            var loglevel = GetLogLevel(methodBase);

            switch (typeOfMethod)
            {
                case MemberType.SetProperty:
                    switch (messageType)
                    {
                        case LogMessageType.Enter:
                            stfLogger.LogSetEnter(loglevel, methodName, CreateStringFromParameterCollection(inputs));
                            break;
                        case LogMessageType.Exit:
                            stfLogger.LogSetExit(loglevel, methodName, CreateStringFromParameterCollection(inputs));
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(@"messageType", messageType.ToString(), @"Unknown log message type");
                    }

                    break;
                case MemberType.GetProperty:
                    switch (messageType)
                    {
                        case LogMessageType.Enter:
                            stfLogger.LogGetEnter(loglevel, methodName);
                            break;
                        case LogMessageType.Exit:
                            stfLogger.LogGetExit(loglevel, methodName, returnValue);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(@"messageType", messageType.ToString(), @"Unknown log message type");
                    }

                    break;
                case MemberType.Method:
                    switch (messageType)
                    {
                        case LogMessageType.Enter:
                            stfLogger.LogFunctionEnter(loglevel, returnTypeName, methodName, inputs.ToArray());
                            break;
                        case LogMessageType.Exit:
                            stfLogger.LogFunctionExit(loglevel, methodName, returnValue);
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

        /// <summary>
        /// The get log level.
        /// </summary>
        /// <param name="methodBase">
        /// The method base.
        /// </param>
        /// <returns>
        /// The <see cref="StfLogLevel"/>.
        /// </returns>
        private StfLogLevel GetLogLevel(MethodBase methodBase)
        {
            var retVal = defaultLogLevel;

            if (methodBase == null)
            {
                return retVal;
            }

            retVal = GetInterfaceLogLevel(methodBase.DeclaringType, retVal);

            try
            {
                var memberLogLevel = methodBase.GetCustomAttribute<StfMemberLogLevelAttribute>();
                retVal = memberLogLevel != null ? memberLogLevel.LogLevel : GetPropertyLogLevel(methodBase, retVal);
            }
            catch (Exception exception)
            {
                stfLogger.LogInternal("Exception caught getting loglevel: [{0}]", exception.Message);
            }
            
            return retVal;
        }

        /// <summary>
        /// The get interface log level.
        /// </summary>
        /// <param name="declaringType">
        /// The declaring type.
        /// </param>
        /// <param name="logLevel">
        /// The log level.
        /// </param>
        /// <returns>
        /// The <see cref="StfLogLevel"/>.
        /// </returns>
        private StfLogLevel GetInterfaceLogLevel(Type declaringType, StfLogLevel logLevel)
        {
            var retVal = logLevel;

            if (declaringType == null)
            {
                return retVal;
            }

            try
            {
                var classLoglevel = declaringType.GetCustomAttribute<StfInterfaceLogLevelAttribute>();

                if (classLoglevel != null)
                {
                    retVal = classLoglevel.LogLevel;
                }
            }
            catch (Exception exception)
            {
                stfLogger.LogInternal("Exception caught getting interface loglevel: [{0}]", exception.Message);
            }
            
            return retVal;
        }

        /// <summary>
        /// The get property log level.
        /// </summary>
        /// <param name="methodBase">
        /// The method base.
        /// </param>
        /// <param name="logLevel">
        /// The log level.
        /// </param>
        /// <returns>
        /// The <see cref="StfLogLevel"/>.
        /// </returns>
        private StfLogLevel GetPropertyLogLevel(MethodBase methodBase, StfLogLevel logLevel)
        {
            var retVal = logLevel;

            if (methodBase == null)
            {
                return retVal;
            }

            var match = Regex.Match(methodBase.Name, @"^(set_|get_)(?<PropertyName>[^\s].+)");

            if (!match.Success)
            {
                return retVal;
            }

            var propertyName = match.Groups["PropertyName"].Value;
            var declaringType = methodBase.DeclaringType;

            if (declaringType == null)
            {
                return retVal;
            }

            PropertyInfo property;

            try
            {
                property = declaringType.GetProperties().FirstOrDefault(p => p.Name.Equals(propertyName));
            }
            catch (Exception exception)
            {
                stfLogger.LogInternal(
                    "Caught exception trying to get info for property with name [{0}]. Error message [{0}]",
                    propertyName,
                    exception.Message);

                return retVal;
            }

            if (property == null)
            {
                return retVal;
            }

            var propertyLogLevel = property.GetCustomAttribute<StfMemberLogLevelAttribute>();

            if (propertyLogLevel != null)
            {
                retVal = propertyLogLevel.LogLevel;
            }

            return retVal;
        }
    }
}

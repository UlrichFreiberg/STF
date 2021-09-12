﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringTransformationUtils.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the StringTransformationUtils type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Mir.Stf.Utilities.StringTransformationUtilities
{
    using System.Linq;
    using System.Text.RegularExpressions;

    using Mir.Stf.Utilities.Interfaces;

    /// <summary>
    /// The stu boolean.
    /// </summary>
    public enum StuBoolean
    {
        /// <summary>
        /// The true.
        /// </summary>
        True,

        /// <summary>
        /// The false.
        /// </summary>
        False
    }

    /// <summary>
    /// The string transformation utils.
    /// </summary>
    public class StringTransformationUtils : IStringTransformationUtils
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringTransformationUtils"/> class.
        /// </summary>
        public StringTransformationUtils()
        {
            StuObjects = new Dictionary<string, object>();
            StuFunctions = new Dictionary<string, StuFunctionInfo>();

            RegisterAllStuFunctionsForType(this);
            RegisterAllStuFunctionsForType(new SimpleFunctions());
            RegisterAllStuFunctionsForType(new MapValuesFunction());
            RegisterAllStuFunctionsForType(new SelectFunction());
            RegisterAllStuFunctionsForType(new CalcFunction());
            RegisterAllStuFunctionsForType(new UniqueFunctions());
            RegisterAllStuFunctionsForType(new StringFunction());
            RegisterAllStuFunctionsForType(new FormatFunction());
        }

        /// <summary>
        /// Gets or sets the stu objects. Dictionary for all the instances registered to STU
        /// </summary>
        internal Dictionary<string, object> StuObjects { get; set; }

        /*
                /// <summary>
                /// This is the signature for all String Transformation functions.
                /// </summary>
                /// <param name="arg">
                /// The string af the first space in {BOB ......}
                /// </param>
                /// <returns>
                /// The result of the evaluation - null for error, Empty for nothing
                /// </returns>
                internal delegate string StuFunction(string arg);
        */

        /// <summary>
        /// Gets the stu functions.
        /// </summary>
        internal Dictionary<string, StuFunctionInfo> StuFunctions { get; }

        /// <summary>
        /// The register all string functions for type.
        /// </summary>
        /// <typeparam name="T">
        /// An instance of a class implementing some STU functions
        /// </typeparam>
        /// <param name="stuCustomClass">
        /// The stu Custom Class.
        /// </param>
        /// <returns>
        /// Indication of successful registration
        /// </returns>
        public bool RegisterAllStuFunctionsForType<T>(T stuCustomClass)
        {
            var functions = typeof(T)
                          .GetMethods()
                          .Where(m => m.GetCustomAttributes(typeof(StringTransformationUtilFunction), false).Length > 0)
                          .ToArray();

            foreach (var function in functions)
            {
                var stuFunctionInfo = new StuFunctionInfo
                {
                    StuFunctionMethodInfo = function.GetBaseDefinition(),
                    StuObject = stuCustomClass
                };

                var functionAttribute = (StringTransformationUtilFunction)Attribute.GetCustomAttribute(stuFunctionInfo.StuFunctionMethodInfo, typeof(StringTransformationUtilFunction));

                StuFunctions.Add(functionAttribute.FunctionName, stuFunctionInfo);
            }

            return true;
        }

        /// <summary>
        /// The evaluate function.
        /// </summary>
        /// <param name="functionName">
        /// The function name.
        /// </param>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string EvaluateFunction(string functionName, string arg)
        {
            var stuFunctionKey = StuFunctions.Keys.FirstOrDefault(name => name.Equals(functionName));

            if (stuFunctionKey == null)
            {
                // don't have a function by that name
                return null;
            }

            var stuFunction = StuFunctions[stuFunctionKey].StuFunctionMethodInfo;
            var stuInstance = StuFunctions[stuFunctionKey].StuObject;

            // now invoke the function in the realm of the provided instance - with one argument the 'arg' 
            var retVal = (string)stuFunction.Invoke(stuInstance, parameters: new object[] { arg });

            return retVal;
        }

        /// <summary>
        /// The evaluate.
        /// </summary>
        /// <param name="stringToEvaluate">
        /// The string to evaluate.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string Evaluate(string stringToEvaluate)
        {
            const string CommandArgsExpression = @"{(?<Command>[^{}\s]+)\s*(?<Arguments>[^{}]*)}";
            var retVal = stringToEvaluate;
            var matches = Regex.Matches(retVal, CommandArgsExpression, RegexOptions.Multiline);

            foreach (Match match in matches)
            {
                var command = match.Groups["Command"].Value.Trim();
                var arguments = match.Groups["Arguments"].Value.Trim();
                var evaluatedValue = EvaluateFunction(command, arguments);

                if (evaluatedValue != null)
                {
                    retVal = retVal.Replace(match.Value, evaluatedValue);
                }
            }

            if (retVal.Equals(stringToEvaluate))
            {
                // there might be {} functions in there - but none we know of, so we are done for now
                return retVal;
            }

            retVal = Evaluate(retVal);

            return retVal;
        }
    }
}

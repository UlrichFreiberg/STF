// --------------------------------------------------------------------------------------------------------------------
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
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <summary>
    /// The string transformation utils.
    /// </summary>
    public class StringTransformationUtils
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
        }

        /// <summary>
        /// Gets or sets the stu objects. Dictionary for all the instances registered to STU
        /// </summary>
        public Dictionary<string, object> StuObjects { get; set; }

        /// <summary>
        /// This is the signature for all String Transformation functions.
        /// </summary>
        /// <param name="arg">
        /// The string af the first space in {BOB ......}
        /// </param>
        /// <returns>
        /// The result of the evaluation - null for error, Empty for nothing
        /// </returns>
        public delegate string StuFunction(string arg);

        /// <summary>
        /// Gets the stu functions.
        /// </summary>
        protected Dictionary<string, StuFunctionInfo> StuFunctions { get; }

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
    }

    /// <summary>
    /// The string transformation util function.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed. Suppression is OK here.")]
    [AttributeUsage(AttributeTargets.Method)]
    public class StringTransformationUtilFunction : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringTransformationUtilFunction"/> class.
        /// </summary>
        /// <param name="functionName">
        /// The function name.
        /// </param>
        public StringTransformationUtilFunction(string functionName)
        {
            FunctionName = functionName;
        }

        /// <summary>
        /// Gets or sets the function name.
        /// </summary>
        public string FunctionName { get; set; }
    }

    /// <summary>
    /// The stu function info.
    /// </summary>
    public class StuFunctionInfo
    {
        /// <summary>
        /// Gets or sets the stu function method info.
        /// </summary>
        public System.Reflection.MethodInfo StuFunctionMethodInfo { get; set; }

        /// <summary>
        /// Gets or sets the stu object.
        /// </summary>
        public object StuObject { get; set; }
    }
}

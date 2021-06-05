// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringTransformationUtilFunction.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The attribute class for custom extensions to the string transformation util function.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.StringTransformationUtilities
{
    using System;

    /// <summary>
    /// The string transformation util function.
    /// </summary>
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
}
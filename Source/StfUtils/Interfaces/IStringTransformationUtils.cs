// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStringTransformationUtils.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the IStringTransformationUtils type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.Interfaces
{
    /// <summary>
    /// The StringTransformationUtils interface.
    /// </summary>
    public interface IStringTransformationUtils
    {
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
        bool RegisterAllStuFunctionsForType<T>(T stuCustomClass);

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
        string EvaluateFunction(string functionName, string arg);
    }
}
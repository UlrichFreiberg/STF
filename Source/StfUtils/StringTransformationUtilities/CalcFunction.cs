// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalcFunction.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the CalcFunction type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.StringTransformationUtilities
{
    using NCalc;

    /// <summary>
    /// The calc function.
    /// </summary>
    public class CalcFunction
    {
        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// The stu calc.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        [StringTransformationUtilFunction("CALC")]
        public string StuCalc(string arg)
        {
            if (string.IsNullOrEmpty(arg))
            {
                ErrorMessage = $"arg [{arg}] is null or empty";
                return null;
            }

            var evaluatedValue = new Expression(arg).Evaluate();
            var retVal = evaluatedValue.ToString();

            return retVal;
        }
    }
}

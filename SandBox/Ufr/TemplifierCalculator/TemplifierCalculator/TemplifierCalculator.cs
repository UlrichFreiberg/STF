// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemplifierCalculator.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the TemplifierCalculator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Templifier
{
    using System.Text.RegularExpressions;

    using NCalc;

    /// <summary>
    /// The templifier calculator.
    /// </summary>
    public class TemplifierCalculator
    {
        /// <summary>
        /// The operator regexp.
        /// </summary>
        private const string OperatorRegexp = @"+-/*";

        /// <summary>
        /// The calculator regexp.
        /// </summary>
        private readonly string calculatorRegexp;

        /// <summary>
        /// The initialized.
        /// </summary>
        private bool initialized;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplifierCalculator"/> class.
        /// </summary>
        public TemplifierCalculator()
        {
            calculatorRegexp = string.Format(@"{{CALC\s*(?<Statement>(?<LeftOperator>[^{0}]+)\s*(?<Operation>[{0}])\s*(?<RightOperator>[^{0}]+))\s*}}", OperatorRegexp);
        }

        /// <summary>
        /// Gets or sets the current calc statement. As it is seen in the Templifier config file
        /// </summary>
        public string CurrentTemplifierStatement { get; set; }

        /// <summary>
        /// Gets or sets the current statement. Cleaned for Templifier notations 
        /// </summary>
        public string CurrentStatement { get; set; }

        /// <summary>
        /// Gets the Resolved Statement. Cleaned for Templifier notations - possible evaluation/resolving of arguments/operators
        /// </summary>
        public string ResolvedStatement
        {
            get
            {
                var retVal = string.Format("{0} {1} {2}", LeftOperator, Operation, RightOperator);

                return retVal;
            }
        }

        /// <summary>
        /// Gets or sets the right operator.
        /// </summary>
        public string RightOperator { get; set; }

        /// <summary>
        /// Gets or sets the operation.
        /// </summary>
        public string Operation { get; set; }

        /// <summary>
        /// Gets or sets the left operator.
        /// </summary>
        public string LeftOperator { get; set; }

        /// <summary>
        /// The is calculator statement.
        /// </summary>
        /// <param name="line">
        /// The line.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsCalculatorStatement(string line)
        {
            return Regex.IsMatch(line, calculatorRegexp);
        }

        /// <summary>
        /// The init.
        /// </summary>
        /// <param name="line">
        /// The line.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Init(string line)
        {
            initialized = false;
            var matches = Regex.Match(line, calculatorRegexp);

            if (!matches.Success)
            {
                return false;
            }

            CurrentTemplifierStatement = line;
            CurrentStatement = matches.Groups["Statement"].Value;
            LeftOperator = matches.Groups["LeftOperator"].Value;
            Operation = matches.Groups["Operation"].Value;
            RightOperator = matches.Groups["RightOperator"].Value;

            initialized = true;
            return true;
        }

        /// <summary>
        /// The get raw statement.
        /// </summary>
        /// <param name="line">
        /// The line.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetRawStatement(string line)
        {
            var matches = Regex.Match(line, calculatorRegexp);
            string retVal = null;

            if (matches.Success)
            {
                retVal = matches.Groups["Expression"].Value;
            }

            return retVal;
        }

        /// <summary>
        /// The calculate expression.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string CalculateExpression()
        {
            if (!initialized)
            {
                return null;
            }

            var ncalc = new Expression(ResolvedStatement, EvaluateOptions.None);
            var ncalRetVal = ncalc.Evaluate();
            var retVal = ncalRetVal.ToString();

            return retVal;
        }
    }
}

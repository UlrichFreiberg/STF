// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Statement.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the Statement type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OperatorGuessing
{
    using System;

    /// <summary>
    /// The statement.
    /// </summary>
    public class Statement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Statement"/> class.
        /// </summary>
        /// <param name="numberOfOperators">
        /// The number of operators.
        /// </param>
        public Statement(int numberOfOperators)
        {
            NumberOfOperators = numberOfOperators;
        }

        /// <summary>
        /// Gets or sets the number of operators.
        /// </summary>
        public int NumberOfOperators { get; set; }

        /// <summary>
        /// Gets or sets the operators.
        /// </summary>
        public Operator[] Operators { get; set; }

        /// <summary>
        /// Gets or sets the numbers.
        /// </summary>
        public Number[] Numbers { get; set; }

        public int CurrentResult { get; set; }

        public string CurrentStatement { get; set; }

        /// <summary>
        /// The get challenge.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public Challenge GetChallenge()
        {
            var numberHandler = new NumberHandler();
            var operatorHandler = new OperatorHandler();
            var statement = string.Empty;

            // we have a loop because operation Divide might result in fractions 
            // and so far we dont want that - GetStatement returns null if fractions are meet
            while (string.IsNullOrEmpty(statement))
            {
                Numbers = numberHandler.GetRandomNumbers(NumberOfOperators + 1);
                Operators = operatorHandler.GetRandomOperators(NumberOfOperators);
                statement = GetStatement();
            }

            var retVal = new Challenge { Result = CurrentResult, Statement = CurrentStatement };

            return retVal;
        }

        /// <summary>
        /// The get statement.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetStatement()
        {
            if (NumberOfOperators <= 0)
            {
                return string.Empty;
            }

            var firstNumber = Numbers[0].CurrentValue;
            var statement = $"{firstNumber}";
            int? result = firstNumber;

            for (var i = 0; i < NumberOfOperators; i++)
            {
                var op = Operators[i];
                var number = Numbers[i + 1];

                statement += $" {op:G} {number.CurrentValue}";
                result = CalculateNumber(result, op, number);

                if (!result.HasValue)
                {
                    return null;
                }
            }

            CurrentResult = (int)result;
            CurrentStatement = statement;

            return statement;
        }

        /// <summary>
        /// The calculate number.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <param name="op">
        /// The op.
        /// </param>
        /// <param name="number">
        /// The number.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private int? CalculateNumber(int? result, Operator op, Number number)
        {
            var retVal = result;
            var value = number.CurrentValue;

            switch (op)
            {
                case Operator.Plus:
                    retVal = retVal + value;
                    break;
                case Operator.Minus:
                    retVal = retVal - value;
                    break;
                case Operator.Multiply:
                    retVal = retVal * value;
                    break;
                case Operator.Divide:
                    var reminder = retVal % value;

                    if (reminder != 0)
                    {
                        // we dont want fractions:-)
                        return null;
                    }

                    retVal = retVal / value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(op), op, null);
            }

            return retVal;
        }
    }
}

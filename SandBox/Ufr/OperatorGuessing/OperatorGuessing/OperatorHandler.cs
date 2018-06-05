// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperatorHandler.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the Operator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OperatorGuessing
{
    using System;

    /// <summary>
    /// The operator.
    /// </summary>
    public enum Operator
    {
        /// <summary>
        /// The plus.
        /// </summary>
        Plus,

        /// <summary>
        /// The minus.
        /// </summary>
        Minus,

        /// <summary>
        /// The multiply.
        /// </summary>
        Multiply,

        /// <summary>
        /// The divide.
        /// </summary>
        Divide
    }

    /// <summary>
    /// The operator.
    /// </summary>
    public class OperatorHandler
    {
        /// <summary>
        /// The random.
        /// </summary>
        private static readonly Random RandomOperator = new Random();

        /// <summary>
        /// The get random operator.
        /// </summary>
        /// <returns>
        /// The <see cref="Operator"/>.
        /// </returns>
        public Operator GetRandomOperator()
        {
            var values = Enum.GetValues(typeof(Operator));
            var nextValue = RandomOperator.Next(values.Length);

            return (Operator)values.GetValue(nextValue);
        }

        /// <summary>
        /// The get random operators.
        /// </summary>
        /// <param name="numberOfOperators">
        /// The number of operators.
        /// </param>
        /// <returns>
        /// An array of <see cref="Operator"/>.
        /// </returns>
        public Operator[] GetRandomOperators(int numberOfOperators)
        {
            var operators = new Operator[numberOfOperators];

            for (var i = 0; i < numberOfOperators; i++)
            {
                operators[i] = GetRandomOperator();
            }

            return operators;
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Number.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the Number type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OperatorGuessing
{
    /// <summary>
    /// The number choice.
    /// </summary>
    public class NumberHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NumberHandler"/> class. 
        /// Initializes a new instance of the <see cref="Number"/> class.
        /// </summary>
        /// <param name="minValue">
        /// The min value.
        /// </param>
        /// <param name="maxValue">
        /// The max value.
        /// </param>
        public NumberHandler(int minValue = 0, int maxValue = 20)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        /// <summary>
        /// Gets or sets the max value.
        /// </summary>
        public int MaxValue { get; set; }

        /// <summary>
        /// Gets or sets the min value.
        /// </summary>
        public int MinValue { get; set; }

        /// <summary>
        /// Gets or sets the numbers.
        /// </summary>
        public Number[] Numbers { get; set; }

        /// <summary>
        /// The get random numbers.
        /// </summary>
        /// <param name="numberOfNumbers">
        /// The number of numbers.
        /// </param>
        /// <returns>
        /// The <see cref="Number[]"/>.
        /// </returns>
        public Number[] GetRandomNumbers(int numberOfNumbers)
        {
            Numbers = new Number[numberOfNumbers];

            for (var i = 0; i < numberOfNumbers; i++)
            {
                Numbers[i] = new Number(MinValue, MaxValue);
            }

            return Numbers;
        }
    }
}

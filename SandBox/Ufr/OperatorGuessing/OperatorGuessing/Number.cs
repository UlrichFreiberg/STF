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
    using System;

    /// <summary>
    /// The number choice.
    /// </summary>
    public class Number
    {
        /// <summary>
        /// The random.
        /// </summary>
        private static readonly Random RandomNumber = new Random();

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> class.
        /// </summary>
        /// <param name="minValue">
        /// The min value.
        /// </param>
        /// <param name="maxValue">
        /// The max value.
        /// </param>
        public Number(int minValue = 0, int maxValue = 20)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            SetCurrentValueToRandomNumber();
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
        /// Gets or sets the current value.
        /// </summary>
        public int CurrentValue { get; set; }

        /// <summary>
        /// Sets current value to a random value
        /// </summary>
        public void SetCurrentValueToRandomNumber()
        {
            CurrentValue = RandomNumber.Next(MinValue, MaxValue);
        }
    }
}

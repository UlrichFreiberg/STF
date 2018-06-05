// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Challenge.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the Challenge type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OperatorGuessing
{
    /// <summary>
    /// The challenge.
    /// </summary>
    public class Challenge
    {
        /// <summary>
        /// Gets or sets the result of the calculation statment
        /// </summary>
        public int Result { get; set; }

        /// <summary>
        /// Gets or sets the challenges calculation statement.
        /// </summary>
        public string Statement { get; set; }
    }
}

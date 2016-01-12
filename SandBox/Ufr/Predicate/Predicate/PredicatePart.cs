// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PredicatePart.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   A predicate consists of parts of predicates seperated by a seperator. Like:
//      OrderType = "Spot" ; OrderPrice = 56
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Predicate
{
    /// <summary>
    /// Class to handle parts of a predicate
    /// </summary>
    public class PredicatePart
    {
        /// <summary>
        /// The predicate string
        /// </summary>
        private string predicate;

        /// <summary>
        /// </summary>
        /// <param name="predicate">
        /// </param>
        public PredicatePart(string predicate)
        {
            this.predicate = predicate;
            var tokens = predicate.Split('=');

            Quantifier = tokens.Length <= 0 ? string.Empty : tokens[0];
            Value = tokens.Length <= 1 ? string.Empty : tokens[1];
        }

        /// <summary>
        /// Gets or sets the Left Hand side of the predicate
        /// </summary>
        public string Quantifier { get; set; }

        /// <summary>
        /// Gets or sets the Right Hand side of the predicate
        /// </summary>
        public string Value { get; set; }
    }
}

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
    using System.Text.RegularExpressions;

    /// <summary>
    /// Class to handle parts of a predicate
    /// </summary>
    public class PredicatePart
    {
        /// <summary>
        /// </summary>
        /// <param name="predicateExpr">
        /// </param>
        public PredicatePart(string predicateExpr = null)
        {
            if (!string.IsNullOrEmpty(predicateExpr))
            {
                PredicateExpr = predicateExpr;                
            }

            var matches = Regex.Match(PredicateExpr, "(?<Qualifier>[^=]+)=(?<Value>.*)");

            if (!matches.Success)
            {
                Quantifier = null;
                Value = null;
                return;
            }

            Quantifier = matches.Groups["Qualifier"].Value.Trim();
            Value = matches.Groups["Value"].Value.Trim();
        }

        /// <summary>
        /// Gets or sets the predicate expression as a string
        /// </summary>
        public string PredicateExpr { get; set; }

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

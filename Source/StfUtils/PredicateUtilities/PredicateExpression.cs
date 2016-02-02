// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PredicateExpression.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   A predicate consists of parts of predicates seperated by a seperator. Like:
//   OrderType = "Spot" ; OrderPrice = 56
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.PredicateUtilities
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// Class to handle parts of a predicate
    /// </summary>
    public class PredicateExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PredicateExpression"/> class. 
        /// </summary>
        /// <param name="predicateExpr">
        /// A predicate expression LHS = RHS
        /// </param>
        public PredicateExpression(string predicateExpr = null)
        {
            if (!string.IsNullOrEmpty(predicateExpr))
            {
                PredicateExpr = predicateExpr;                
            }

            var matches = Regex.Match(PredicateExpr, "(?<LeftHandSide>[^=]+)=(?<RightHandSide>.*)");

            if (!matches.Success)
            {
                LeftHandSide = null;
                RightHandSide = null;
                return;
            }

            LeftHandSide = matches.Groups["LeftHandSide"].Value.Trim();
            RightHandSide = matches.Groups["RightHandSide"].Value.Trim();
        }

        /// <summary>
        /// Gets or sets the predicate expression as a string
        /// </summary>
        public string PredicateExpr { get; set; }

        /// <summary>
        /// Gets or sets the Left Hand side of the predicate
        /// </summary>
        public string LeftHandSide { get; set; }

        /// <summary>
        /// Gets or sets the Right Hand side of the predicate
        /// </summary>
        public string RightHandSide { get; set; }
    }
}

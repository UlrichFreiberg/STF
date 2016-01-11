// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PeopleFilter.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PredidcateTests
{
    using Predicate;

    public class PeopleFilter : People
    {
        [PredicateMap("Age")]
        public int? AgeFilter { get; set; }
    }
}

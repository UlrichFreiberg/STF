// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PeopleFilter.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace UnitTest.PredicateUtils
{
    using Mir.Stf.Utilities.PredicateUtilities;

    /// <summary>
    /// People Filter class - used for the unit test
    /// </summary>
    public class PeopleFilter : People
    {
        /// <summary>
        /// Gets or sets the age in base class - Front for Age
        /// </summary>
        [PredicateMap("Age")]
        public int? AgeFilter { get; set; }
    }
}

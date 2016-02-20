// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestTestDataInput.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the UnitTestTestDataInput type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DataDriven
{
    /// <summary>
    /// The test data input.
    /// </summary>
    public class UnitTestTestDataInput : StfTestDataBase
    {
        /// <summary>
        /// Gets or sets the a.
        /// </summary>
        public string A { get; set; }

        /// <summary>
        /// Gets or sets the b.
        /// </summary>
        public string B { get; set; }

        /// <summary>
        /// Gets or sets the one number.
        /// </summary>
        public int OneNumber { get; set; }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestTestDataObject.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The unit test test data object.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace UnitTest
{
    using Mir.Stf;

    /// <summary>
    /// The unit test test data object.
    /// </summary>
    public class UnitTestTestDataObject : StfTestDataBase
    {
        /// <summary>
        /// Gets or sets the iteration.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the iteration.
        /// </summary>
        public string Iteration { get; set; }

        [StfTestData("COLUMN MAP TEST")]
        public string ColumnTest { get; set; }
    }
}

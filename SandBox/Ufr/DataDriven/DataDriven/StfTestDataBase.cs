// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfTestDataBase.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the IStfTestData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DataDriven
{
    /// <summary>
    /// The StfTestData interface.
    /// </summary>
    public class StfTestDataBase : IStfTestData
    {
        /// <summary>
        /// Gets or sets a value indicating whether ignore row.
        /// Useful when debugging the 50th data row:-)
        /// </summary>
        public bool IgnoreRow { get; set; }
    }
}

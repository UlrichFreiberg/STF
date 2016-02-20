// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStfTestData.cs" company="Mir Software">
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
    public interface IStfTestData
    {
        /// <summary>
        /// Gets or sets a value indicating whether ignore row.
        /// </summary>
        bool IgnoreRow { get; set; }
    }
}

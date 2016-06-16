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

        /// <summary>
        /// Gets or sets the log level.
        /// </summary>
        string LogLevel { get; set; }

        /// <summary>
        /// Gets or sets the environment.
        /// </summary>
        string Environment { get; set; }
    }
}

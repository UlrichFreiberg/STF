// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfTestDataBase.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the IStfTestData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf
{
    using Mir.Stf.Interfaces;

    /// <summary>
    /// The StfTestData interface.
    /// </summary>
    public class StfTestDataBase : IStfTestData
    {
        /// <summary>
        /// Gets or sets a value indicating whether ignore row.
        /// Useful when debugging the 50th data row:-)
        /// </summary>
        public bool StfIgnoreRow { get; set; }

        /// <summary>
        /// Gets or sets the log level.
        /// </summary>
        public string StfLogLevel { get; set; }

        /// <summary>
        /// Gets or sets the environment.
        /// </summary>
        public string StfEnvironment { get; set; }

        /// <summary>
        /// Gets or sets the iteration.
        /// </summary>
        public int StfIteration { get; set; }
    }
}

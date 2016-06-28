// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssertStats.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The assert stats.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.Utilities
{
    /// <summary>
    /// The assert stats.
    /// </summary>
    internal class AssertStats
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssertStats"/> class.
        /// </summary>
        public AssertStats()
        {
            AssertFailedCount = 0;
            AssertPassCount = 0;
            AssertInconclusiveCount = 0;
        }

        /// <summary>
        /// Gets or sets the error count.
        /// </summary>
        public int AssertFailedCount { get; set; }

        /// <summary>
        /// Gets or sets the assert pass count.
        /// </summary>
        public int AssertPassCount { get; set; }

        /// <summary>
        /// Gets or sets the assert inconclusive count.
        /// </summary>
        public int AssertInconclusiveCount { get; set; }
    }
}

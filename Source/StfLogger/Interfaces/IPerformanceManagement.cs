// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPerformanceManagement.cs" company="Foobar">
//   2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Stf.Utilities.Interfaces
{
    /// <summary>
    /// The PerformanceManagement <c>interface</c>.
    /// </summary>
    public interface IPerformanceManagement
    {
        /// <summary>
        /// Check how many seconds since last log entry - any performance issues?
        /// </summary>
        /// <param name="elapsedTime">
        /// seconds since last log entry
        /// </param>
        /// <returns>
        /// Number of alerts issued.
        /// </returns>
        int LogPerformanceAlert(double elapsedTime);
    }
}

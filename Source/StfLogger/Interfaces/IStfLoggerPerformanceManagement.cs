// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStfLoggerPerformanceManagement.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.Interfaces
{
    /// <summary>
    /// The PerformanceManagement <c>interface</c>.
    /// </summary>
    public interface IStfLoggerPerformanceManagement
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

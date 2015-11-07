// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStfLoggerLogfileManagement.cs" company="Mir Software">
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
    /// The LogfileManagement <c>interface</c>.
    /// </summary>
    public interface IStfLoggerLogfileManagement
    {
        /// <summary>
        /// Have we logged a Error or Fail? 
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int ErrorOrFail();

        /// <summary>
        /// The close log file.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool CloseLogFile();
    }
}

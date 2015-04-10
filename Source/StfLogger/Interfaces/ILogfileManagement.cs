// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILogfileManagement.cs" company="Foobar">
//   2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Stf.Utilities.Interfaces
{
    /// <summary>
    /// The LogfileManagement <c>interface</c>.
    /// </summary>
    public interface ILogfileManagement
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

        /// <summary>
        /// Archive <c>this</c> log file.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool ArchiveThisLogFile();
    }
}

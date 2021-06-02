// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFileUtils.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the IFileUtils type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.Interfaces
{
    using System.IO;

    /// <summary>
    /// The FileUtils interface.
    /// </summary>
    public interface IFileUtils
    {

        /// <summary>
        /// The exists file.
        /// </summary>
        /// <param name="filename">
        /// The filename.
        /// </param>
        /// <param name="ensureWaitSeconds">
        /// The ensureWaitSeconds.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool ExistsFile(string filename, int ensureWaitSeconds = 30);

        /// <summary>
        /// The not exists file.
        /// </summary>
        /// <param name="filename">
        /// The filename.
        /// </param>
        /// <param name="ensureWaitSeconds">
        /// The ensureWaitSeconds.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool NotExistsFile(string filename, int ensureWaitSeconds = 30);

        /// <summary>
        /// The delete file.
        /// </summary>
        /// <param name="filename">
        /// The filename.
        /// </param>
        /// <param name="ensureWaitSeconds">
        /// The ensureWaitSeconds.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool DeleteFile(string filename, int ensureWaitSeconds = 30);

        /// <summary>
        /// The create textfile.
        /// </summary>
        /// <param name="filename">
        /// The filename.
        /// </param>
        /// <param name="ensureWaitSeconds">
        /// The ensureWaitSeconds.
        /// </param>
        /// <returns>
        /// The <see cref="StreamWriter"/>.
        /// </returns>
        StreamWriter CreateTextfile(string filename, int ensureWaitSeconds = 30);

        /// <summary>
        /// The copy file.
        /// </summary>
        /// <param name="sourceFilename">
        /// The source filename.
        /// </param>
        /// <param name="destinationFilename">
        /// The destination filename.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool CopyFile(string sourceFilename, string destinationFilename);

        /// <summary>
        /// The WriteAllText file.
        /// </summary>
        /// <param name="filename">
        /// The filename.
        /// </param>
        /// <param name="text">
        /// The text to write
        /// </param>
        /// <param name="ensureWaitSeconds">
        /// Time to wait for write to complete in seconds
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool WriteAllTextFile(string filename, string text, int ensureWaitSeconds = 30);

    }
}
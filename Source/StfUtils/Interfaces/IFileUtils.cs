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
        /// The delete file.
        /// </summary>
        /// <param name="filename">
        /// The filename.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool DeleteFile(string filename);

        /// <summary>
        /// The create textfile.
        /// </summary>
        /// <param name="filename">
        /// The filename.
        /// </param>
        /// <returns>
        /// The <see cref="StreamWriter"/>.
        /// </returns>
        StreamWriter CreateTextfile(string filename);

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
        /// The get temp folder.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetTempFolder();

        /// <summary>
        /// The mirror folder.
        /// </summary>
        /// <param name="sourceDirname">
        /// The source dirname.
        /// </param>
        /// <param name="destinationDirname">
        /// The destination dirname.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool MirrorFolder(string sourceDirname, string destinationDirname);
    }
}
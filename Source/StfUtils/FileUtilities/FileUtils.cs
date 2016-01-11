// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileUtils.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the FileUtils type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.FileUtilities
{
    using System;
    using System.IO;

    using Mir.Stf.Utilities.Interfaces;

    /// <summary>
    /// The file utils.
    /// </summary>
    public class FileUtils : IFileUtils
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
        public bool DeleteFile(string filename)
        {
            if (!File.Exists(filename))
            {
                return true;
            }

            File.Delete(filename);
            return !File.Exists(filename);
        }

        /// <summary>
        /// The create textfile.
        /// </summary>
        /// <param name="filename">
        /// The filename.
        /// </param>
        /// <returns>
        /// The <see cref="StreamWriter"/>.
        /// </returns>
        public StreamWriter CreateTextfile(string filename) 
        {
            var streamWriter  = File.CreateText(filename);

            if (!File.Exists(filename))
            {
                return null;
            }

            return streamWriter;
        }

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
        public bool CopyFile(string sourceFilename, string destinationFilename)
        {
            if (!File.Exists(sourceFilename))
            {
                return false;
            }

            if (!DeleteFile(destinationFilename))
            {
                return false;
            }

            File.Copy(sourceFilename, destinationFilename);

            return File.Exists(destinationFilename);
        }

        /// <summary>
        /// The get temp folder.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetTempFolder()
        {
            var tempDirname = Path.Combine(Environment.ExpandEnvironmentVariables("TEMP"), Guid.NewGuid().ToString());

            Directory.CreateDirectory(tempDirname);

            return Directory.Exists(tempDirname) ? tempDirname : null;
        }

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
        public bool MirrorFolder(string sourceDirname, string destinationDirname)
        {
            // get to the RoboCopy Wrapper
            return false;
        }
    }
}

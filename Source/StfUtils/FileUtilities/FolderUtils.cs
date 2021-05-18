// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FolderUtils.cs" company="Mir Software">
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
    public class FolderUtils : IFolderUtils
    {
        public string FolderUtilsRoot { get; }

        public FolderUtils(string folderUtilsRoot = null)
        {
            FolderUtilsRoot = folderUtilsRoot;
        }

        public bool Create(string folderPath)
        {
            string pathToUse = GetAbsolutePathFor(folderPath);

            if (Exists(pathToUse))
            {
                return true;
            }

            var dirInfo = Directory.CreateDirectory(pathToUse);

            if (dirInfo == null)
            {
                return false;
            }

            var retVal = Exists(pathToUse);

            return retVal;
        }

        public bool Exists(string folderPath)
        {
            var pathToUse = GetAbsolutePathFor(folderPath);
            var retVal = Directory.Exists(pathToUse);

            return retVal;
        }

        public bool Delete(string folderPath, bool recursive = false)
        {
            var pathToUse = GetAbsolutePathFor(folderPath);

            if (!Exists(pathToUse))
            {
                return true;
            }

            Directory.Delete(pathToUse, recursive);

            var retVal = Exists(pathToUse);

            return retVal;
        }

        public string GetAbsolutePathFor(string folderPath)
        {
            var pathToUse = folderPath;

            if (!Path.IsPathRooted(folderPath))
            {
                pathToUse = Path.Combine(FolderUtilsRoot, folderPath);
            }

            return pathToUse;
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

        public bool InitializeTempResult(string folderPath = null)
        {
            var pathToUse = GetAbsolutePathFor(folderPath);
            var tempDir = Path.Combine(pathToUse, "Temp");
            var resultsDir = Path.Combine(pathToUse, "Results");
            var retVal = Create(tempDir);

            if (!retVal)
            {
                return false;
            }

            retVal = Create(resultsDir);

            if (!retVal)
            {
                return false;
            }

            return true;
        }
    }
}

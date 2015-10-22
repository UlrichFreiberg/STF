// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfArchiver.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
// Util to archive files and directories as evidence after a test run
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Util to archive files and directories as evidence after a test run
    /// </summary>
    public class StfArchiver
    {
        /// <summary>
        /// Gets or sets the destination to archive 
        /// </summary>
        public string ArchiveDestination { get; set; }

        /// <summary>
        /// Gets or sets the name of ZipFile if the archive should be zipped.
        /// </summary>
        public string ZipFilename { get; set; }

        /// <summary>
        /// Gets or sets the list of files to archive
        /// </summary>
        public IList<string> FilesToArchive { get; set; }

        /// <summary>
        /// Gets or sets the list of directories to archive
        /// </summary>
        public IList<string> DirectoriesToArchive { get; set; }

        /// <summary>
        /// Sets up the archiver.
        /// </summary>
        /// <returns>
        /// indication of success
        /// </returns>
        public bool Init()
        {
            FilesToArchive = new List<string>();
            DirectoriesToArchive = new List<string>();

            ArchiveDestination = CalculateDefaultarchiveDestination();
            return true;
        }

        /// <summary>
        /// Compiles a list of files and directories to archive
        /// </summary>
        /// <returns>
        /// returns a multi line status 
        /// </returns>
        public string Status()
        {
            var retVal = string.Empty;
            var headerAdded = false;

            foreach (var directory in DirectoriesToArchive)
            {
                if (!headerAdded)
                {
                    headerAdded = true;
                    retVal = "Directories to archive\n";
                }

                retVal += string.Format("\t{0}\n", directory);
            }

            foreach (var file in FilesToArchive)
            {
                if (!headerAdded)
                {
                    headerAdded = true;
                    retVal = "Files to archive\n";
                }

                retVal += string.Format("\t{0}\n", file);
            }

            return retVal;
        }

        /// <summary>
        /// Performs the archive
        /// </summary>
        /// <returns>
        /// Indication of success
        /// </returns>
        public bool ArchiveFiles()
        {
            foreach (var directory in DirectoriesToArchive)
            {

            }

            foreach (var file in FilesToArchive)
            {

            }

            // TODO: Generate filelist.txt and place it in the DestinationDir
            var retVal = ZipDestination();
            return true;
        }

        /// <summary>
        /// Add a single file to be archived
        /// </summary>
        /// <param name="filename">
        /// Name of file
        /// </param>
        /// <param name="archiveComment">
        /// Any comments to be attahced to the status - why do we archive this file
        /// </param>
        /// <returns>
        /// Indication of success
        /// </returns>
        public bool AddFile(string filename, string archiveComment = null)
        {
            if (!File.Exists(filename))
            {
                return false;
            }

            FilesToArchive.Add(filename);
            return true;
        }

        /// <summary>
        /// Add a single file to be archived
        /// </summary>
        /// <param name="dirname">
        /// Name of file
        /// </param>
        /// <param name="archiveComment">
        /// Any comments to be attahced to the status - why do we archive this file
        /// </param>
        /// <returns>
        /// Indication of success
        /// </returns>
        public bool AddDirectory(string dirname, string archiveComment = null)
        {
            if (!File.Exists(dirname))
            {
                return false;
            }

            DirectoriesToArchive.Add(dirname);
            return true;
        }

        /// <summary>
        /// Zip up the destination directory
        /// </summary>
        /// <returns>
        /// Indication of success
        /// </returns>
        private bool ZipDestination()
        {
            var zipDestination = false; // TODO: get it from configuration

            if (!zipDestination)
            {
                return true;
            }

            if (File.Exists(ZipFilename))
            {
                File.Delete(ZipFilename);
            }

            // todo: zip the stuff 

            return true;
        }

        /// <summary>
        /// Get a best guess for the path to a archive destination
        /// </summary>
        /// <returns>
        /// The directory path
        /// </returns>
        private string CalculateDefaultarchiveDestination()
        {
            var currentUser = "Ulrich";
            var testName = "Tc65TestName";
            var unique = DateTime.Now.ToString("yy-MM-ddHHmmss");
            var archiveTopDir = @"c:\temp\stf\archiveDir";  // TODO: Get from config

            if (!File.Exists(archiveTopDir))
            {
                Directory.CreateDirectory(archiveTopDir);
            }

            var retVal = Path.Combine(archiveTopDir, currentUser);
            retVal = Path.Combine(retVal, currentUser);     // TODO: To be controlled from config
            retVal = Path.Combine(retVal, testName);        // TODO: To be controlled from config
            retVal = Path.Combine(retVal, unique);          // TODO: To be controlled from config

            if (!File.Exists(retVal))
            {
                Directory.CreateDirectory(retVal);
            }

            return retVal;
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfArchiver.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;

    using Configuration;

    /// <summary>
    /// Util to archive files and directories as evidence after a test run
    /// </summary>
    public class StfArchiver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StfArchiver"/> class.
        /// </summary>
        /// <param name="config">
        /// A configuration for the StfArchiver
        /// </param>
        /// <param name="testname">
        /// Name of the test
        /// </param>
        public StfArchiver(IStfArchiverConfiguration config, string testname)
        {
            Configuration = config;

            Init(testname);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StfArchiver"/> class.
        /// </summary>
        /// <param name="testname">
        /// The testname.
        /// </param>
        public StfArchiver(string testname)
        {
            Init(testname);
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public IStfArchiverConfiguration Configuration { get; private set; }

        /// <summary>
        /// Gets or sets the list of files to archive
        /// </summary>
        private IList<string> FilesToArchive { get; set; }

        /// <summary>
        /// Gets or sets the list of directories to archive
        /// </summary>
        private IList<string> DirectoriesToArchive { get; set; }

        /// <summary>
        /// Sets up the archiver
        /// </summary>
        /// <param name="testname">
        /// The testname.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Init(string testname)
        {
            if (Configuration == null)
            {
                var tempPath = Path.GetTempPath();
                var tempDir = string.Format(
                    "StfArchiver_{0}_{1}",
                    Environment.UserName,
                    DateTime.Now.ToString("dd-MMM-yyyy_HH-mm-ss"));

                Configuration = new StfArchiverConfiguration();
                Configuration.TempDirectory = Path.Combine(tempPath, tempDir);
            }

            if (string.IsNullOrEmpty(Configuration.ArchiveDestination))
            {
                Configuration.ArchiveDestination = GetDefaultArchiveDestination(testname);
            }

            Configuration.ZipFilename = GetDefaultZipFilename(testname);

            FilesToArchive = new List<string>();
            DirectoriesToArchive = new List<string>();
           
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
                    retVal += "Files to archive\n";
                }

                retVal += string.Format("\t{0}\n", file);
            }

            if (!headerAdded)
            {
                retVal = "Nothing to Archive";
            }

            return retVal;
        }

        /// <summary>
        /// Performs the archive
        /// </summary>
        /// <returns>
        /// Indication of success
        /// </returns>
        public bool PerformArchive()
        {
            if (!Directory.Exists(Configuration.TempDirectory))
            {
                Directory.CreateDirectory(Configuration.TempDirectory);
            }

            foreach (var directory in DirectoriesToArchive)
            {
                RoboCopyWrapper.MirrorDir(directory, Configuration.TempDirectory);
            }

            foreach (var filename in FilesToArchive)
            {
                var filenameNoPath = Path.GetFileName(filename);
                if (filenameNoPath == null)
                {
                    continue;
                }

                var destFilename = Path.Combine(Configuration.TempDirectory, filenameNoPath);

                if (File.Exists(destFilename))
                {
                    File.Delete(destFilename);
                }

                File.Copy(filename, destFilename);
            }

            if (!Directory.Exists(Configuration.ArchiveDestination))
            {
                Directory.CreateDirectory(Configuration.ArchiveDestination);
            }

            // TODO: Generate filelist.txt and place it in the DestinationDir

            // TODO: Let configuration control if to MirrorDir
            RoboCopyWrapper.MirrorDir(Configuration.TempDirectory, Configuration.ArchiveDestination);

            // TODO: Let configuration control if to MirrorDir
            ZipDestination();

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
            if (!Directory.Exists(dirname))
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
            if (string.IsNullOrEmpty(Configuration.ZipFilename))
            {
                return false;
            }

            if (File.Exists(Configuration.ZipFilename))
            {
                File.Delete(Configuration.ZipFilename);
            }

            ZipFile.CreateFromDirectory(Configuration.TempDirectory, Configuration.ZipFilename);
            return true;
        }

        /// <summary>
        /// The get default zip filename.
        /// </summary>
        /// <param name="testname">
        /// The testname.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetDefaultZipFilename(string testname)
        {
            bool createZipFile = true; // TODO: get from configuration

            if (!createZipFile)
            {
                return null;
            }

            var filename = string.Format("{0}.zip", testname);
            if (!string.IsNullOrEmpty(Configuration.ZipFilename))
            {
                filename = Path.GetFileName(Configuration.ZipFilename);
            }

            var retVal = Path.Combine(Configuration.ArchiveDestination, filename);

            return retVal;
        }

        /// <summary>
        /// The get default archive destination.
        /// </summary>
        /// <param name="testname">
        /// The testname.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetDefaultArchiveDestination(string testname)
        {
            var currentUser = Environment.UserName;  
            var unique = DateTime.Now.ToString("dd-MMM-yyyy_HH-mm-ss");
            var defaultArchiveTopDir = @"c:\temp\stf\archiveDir";

            if (string.IsNullOrEmpty(Configuration.ArchiveTopDir))
            {
                Configuration.ArchiveTopDir = defaultArchiveTopDir;
            }

            if (!Directory.Exists(Configuration.ArchiveTopDir))
            {
                Directory.CreateDirectory(Configuration.ArchiveTopDir);
            }

            if (string.IsNullOrEmpty(testname))
            {
                testname = "DefaultTestDir";
            }

            var retVal = Path.Combine(Configuration.ArchiveTopDir, currentUser);
            retVal = Path.Combine(retVal, testname);        // TODO: To be controlled from config
            retVal = Path.Combine(retVal, unique);          // TODO: To be controlled from config

            return retVal;
        }
    }
}

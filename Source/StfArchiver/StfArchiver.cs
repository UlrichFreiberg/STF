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
    using System.Linq;

    using Configuration;

    /// <summary>
    /// The stf archiver.
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
            FilesToArchive = new List<string>();
            DirectoriesToArchive = new List<string>();

            SetDefaultConfiguration();
            SetDefaultArchiveDestination(testname);
            SetDefaultZipFilename(testname);

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

            if (DirectoriesToArchive.Count > 0)
            {
                retVal = "Directories to archive\n";

                retVal = DirectoriesToArchive.Aggregate(
                    retVal,
                    (current, directory) => current + $"\t{directory}\n");
            }

            if (FilesToArchive.Count > 0)
            {
                retVal += "Files to archive\n";
                retVal = FilesToArchive.Aggregate(
                    retVal,
                    (current, file) => current + $"\t{file}\n");
            }

            if (string.IsNullOrEmpty(retVal))
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
            var tempArchiveDir = Path.Combine(Configuration.TempDirectory, Guid.NewGuid().ToString());

            if (!Directory.Exists(tempArchiveDir))
            {
                Directory.CreateDirectory(tempArchiveDir);
            }

            if (DirectoriesToArchive.Any(directory => RoboCopyWrapper.MirrorDir(directory, tempArchiveDir) <= 0))
            {
                return false;
            }

            foreach (var filename in FilesToArchive)
            {
                var filenameNoPath = Path.GetFileName(filename);

                if (filenameNoPath == null)
                {
                    continue;
                }

                var destFilename = Path.Combine(tempArchiveDir, filenameNoPath);

                if (File.Exists(destFilename))
                {
                    File.Delete(destFilename);
                }

                File.Copy(filename, destFilename);
            }

            var retVal = true;

            if (Configuration.DoArchiveFoldersAndFiles)
            {
                if (!Directory.Exists(Configuration.ArchiveDestination))
                {
                    Directory.CreateDirectory(Configuration.ArchiveDestination);
                }

                // TODO: Generate filelist.txt and place it in the DestinationDir

                // TODO: Let configuration control if to MirrorDir
                if (RoboCopyWrapper.MirrorDir(tempArchiveDir, Configuration.ArchiveDestination) <= 0)
                {
                    retVal = false;
                }
            }

            if (Configuration.DoArchiveToZipfile)
            {
                var zipRetVal = ZipDirectory(tempArchiveDir, Configuration.ZipFilename);

                retVal = retVal && zipRetVal;
            }

            // remove the temp archive directory
            Directory.Delete(tempArchiveDir, true);

            return retVal;
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
        /// If no configuration is provided, then create a default one.
        /// </summary>
        private void SetDefaultConfiguration()
        {
            if (Configuration != null)
            {
                return;
            }

            Configuration = new StfArchiverConfiguration();
        }

        /// <summary>
        /// Zip up the destination directory
        /// </summary>
        /// <param name="directoryToZip">
        /// The directory To Zip.
        /// </param>
        /// <param name="zipFilename">
        /// The zip Filename.
        /// </param>
        /// <returns>
        /// Indication of success
        /// </returns>
        private bool ZipDirectory(string directoryToZip, string zipFilename)
        {
            if (string.IsNullOrEmpty(Configuration.ZipFilename))
            {
                return false;
            }

            if (File.Exists(zipFilename))
            {
                File.Delete(zipFilename);
            }

            // make sure the dir for the zip file exists
            var zipfileDirname = new FileInfo(zipFilename).DirectoryName ?? @"c:\temp\StfZipFilenameError";
            if (!Directory.Exists(zipfileDirname))
            {
                Directory.CreateDirectory(zipfileDirname);
            }

            ZipFile.CreateFromDirectory(directoryToZip, Configuration.ZipFilename);
            return true;
        }

        /// <summary>
        /// The get default zip filename.
        /// </summary>
        /// <param name="testname">
        /// The testname.
        /// </param>
        private void SetDefaultZipFilename(string testname)
        {
            if (!Configuration.DoArchiveToZipfile)
            {
                Configuration.ZipFilename = string.Empty;
                return;
            }

            var filename = $"{testname}.zip";
            if (!string.IsNullOrEmpty(Configuration.ZipFilename))
            {
                filename = Path.GetFileName(Configuration.ZipFilename);
            }

            var newArchiveDestination = Path.Combine(Configuration.ArchiveDestination, filename);
            Configuration.ZipFilename = newArchiveDestination;
        }

        /// <summary>
        /// The set default archive destination.
        /// </summary>
        /// <param name="testname">
        /// The testname.
        /// </param>
        private void SetDefaultArchiveDestination(string testname)
        {
            // if allready set then dont do any defaulting
            if (!string.IsNullOrEmpty(Configuration.ArchiveDestination))
            {
                return;
            }

            testname = SetDefaultIfNull(testname, "DefaultTestDir");

            var arr = Configuration.DirectoryOrderInPath.Split(';');

            for (var i = 0; i < arr.Length; i++)
            {
                switch (arr[i].Trim().ToLower())
                {
                    case "t":
                    case "testname":
                        AddToPath(Configuration.UseTestNameInPath, testname);
                        break;

                    case "l":
                    case "u":
                    case "loginname":
                    case "username":
                        AddToPath(Configuration.UseLoginNameInPath, Environment.UserName);
                        break;

                    case "d":
                    case "date":
                    case "datenow":
                        AddToPath(Configuration.UseDateNowInPath, DateTime.Now.ToString("dd-MMM-yyyy_HH-mm-ss"));
                        break;
                }                
            }
        }

        /// <summary>
        /// Checks is string is empty - if so it return the default value
        /// </summary>
        /// <param name="source">
        /// the string to check
        /// </param>
        /// <param name="defaultValue">
        /// The default value
        /// </param>
        /// <returns>
        /// The string
        /// </returns>
        private string SetDefaultIfNull(string source, string defaultValue)
        {
            if (string.IsNullOrEmpty(source))
            {
                source = defaultValue;
            }

            return source;
        }

        /// <summary>
        /// Depending on a condition, add a string as a new dir in the path for ArchiveDestination
        /// </summary>
        /// <param name="condition">
        /// String from configuration holding a bool value
        /// </param>
        /// <param name="dirElement">
        /// The element
        /// </param>
        private void AddToPath(string condition, string dirElement)
        {
            if (string.IsNullOrEmpty(Configuration.ArchiveDestination))
            {
                Configuration.ArchiveDestination = Configuration.ArchiveTopDir;
            }

            if (!InferBoolValue(condition))
            {
                return;
            }

            var newArchiveDestination = Path.Combine(Configuration.ArchiveDestination, dirElement);
            Configuration.ArchiveDestination = newArchiveDestination;
        }

        /// <summary>
        /// Take a string and see if a boolean true can be parsed - if not, then return false
        /// </summary>
        /// <param name="value">
        /// the string to parse
        /// </param>
        /// <returns>
        /// true, if a boolean true can be parsed - if not, then return false
        /// </returns>
        private bool InferBoolValue(string value)
        {
            switch (value)
            {
                case "1":
                case "yes":
                case "true":
                    return true;
                default:
                    return false;
            }
        }
    }
}

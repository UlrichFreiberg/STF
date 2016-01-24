// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStfArchiverConfiguration.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.Configuration
{
    /// <summary>
    /// The interface for configuring the StfArchiver
    /// </summary>
    public interface IStfArchiverConfiguration
    {
        /// <summary>
        /// Gets or sets a value indicating whether or not to Archive folders and files
        /// </summary>
        bool DoArchiveFoldersAndFiles { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to Archive to zip file
        /// </summary>
        bool DoArchiveToZipfile { get; set; }

        /// <summary>
        /// Gets or sets the destination to archive 
        /// </summary>
        string ArchiveDestination { get; set; }

        /// <summary>
        /// Gets or sets the name of ZipFile if the archive should be zipped.
        /// </summary>
        string ZipFilename { get; set; }

        /// <summary>
        /// Gets or sets the archive top dir.
        /// </summary>
        string ArchiveTopDir { get; set; }

        /// <summary>
        /// Gets or sets the temp directory.
        /// </summary>
        string TempDirectory { get; set; }

        /// <summary>
        /// Gets or sets the use loginName in path.
        /// </summary>
        string UseLoginNameInPath { get; set; }

        /// <summary>
        /// Gets or sets the use date time in path.
        /// </summary>
        string UseDateTimeInPath { get; set; }

        /// <summary>
        /// Gets or sets the use test name in path.
        /// </summary>
        string UseTestNameInPath { get; set; }

        /// <summary>
        /// Gets or sets the use DateNow in path.
        /// </summary>
        string UseDateNowInPath { get; set; }

        /// <summary>
        /// Gets or sets the order of directories to use in archive path.
        /// </summary>
        string DirectoryOrderInPath { get; set; }
    }
}

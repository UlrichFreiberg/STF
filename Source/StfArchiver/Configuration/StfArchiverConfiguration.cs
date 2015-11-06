// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfArchiverConfiguration.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel;

namespace Mir.Stf.Utilities.Configuration
{
    /// <summary>
    /// The stf archiver configuration object
    /// </summary>
    public class StfArchiverConfiguration : IStfArchiverConfiguration
    {
        /// <summary>
        /// Gets or sets wether or not to Archive folders and files
        /// </summary>
        [StfConfiguration("Configuration.StfKernel.StfArchiver.DoArchiveFoldersAndFiles", DefaultValue = "false")]
        public string DoArchiveFoldersAndFiles { get; set; }

        /// <summary>
        /// Gets or sets wether or not to Archive to zip file
        /// </summary>
        [StfConfiguration("Configuration.StfKernel.StfArchiver.DoArchiveToZipfile", DefaultValue = "false")]
        public string DoArchiveToZipfile { get; set; }

        /// <summary>
        /// Gets or sets the destination to archive 
        /// </summary>
        [StfConfiguration("Configuration.StfKernel.StfArchiver.ArchiveDestination")]
        public string ArchiveDestination { get; set; }

        /// <summary>
        /// Gets or sets the name of ZipFile if the archive should be zipped.
        /// </summary>
        [StfConfiguration("Configuration.StfKernel.StfArchiver.ZipFilename", DefaultValue = null)]
        public string ZipFilename { get; set; }

        /// <summary>
        /// Gets or sets the archive top dir.
        /// </summary>
        [StfConfiguration("Configuration.StfKernel.StfArchiver.ArchiveTopDir", DefaultValue = @"c:\temp\Stf\StfArchive")]
        public string ArchiveTopDir { get; set; }

        /// <summary>
        /// Gets or sets the temp directory.
        /// </summary>
        [StfConfiguration("Configuration.StfKernel.StfArchiver.TempDirectory", DefaultValue = @"c:\temp\Stf\Temp")]
        public string TempDirectory { get; set; }

        /// <summary>
        /// Gets or sets the use date time in path.
        /// </summary>
        [StfConfiguration("Configuration.StfKernel.StfArchiver.UseDateTimeInPath", DefaultValue = "true")]
        public string UseDateTimeInPath { get; set; }

        /// <summary>
        /// Gets or sets the use test name in path.
        /// </summary>
        [StfConfiguration("Configuration.StfKernel.StfArchiver.UseTestnameInPath", DefaultValue = "true")]
        public string UseTestNameInPath { get; set; }

        /// <summary>
        /// Gets or sets the use LoginName in path.
        /// </summary>
        [StfConfiguration("Configuration.StfKernel.StfArchiver.UseLoginNameInPath", DefaultValue = "true")]
        public string UseLoginNameInPath { get; set; }
    }
}

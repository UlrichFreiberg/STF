// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfArchiverConfiguration.cs" company="Mir Software">
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
    /// The stf archiver configuration object
    /// </summary>
    public class StfArchiverConfiguration : IStfArchiverConfiguration
    {
        /// <summary>
        /// Gets or sets the destination to archive 
        /// </summary>
        [ConfigInfo("Configuration.StfKernel.StfArchiver.ArchiveDestination")]
        public string ArchiveDestination { get; set; }

        /// <summary>
        /// Gets or sets the name of ZipFile if the archive should be zipped.
        /// </summary>
        [ConfigInfo("Configuration.StfKernel.StfArchiver.ZipFilename")]
        public string ZipFilename { get; set; }

        /// <summary>
        /// Gets or sets the archive top dir.
        /// </summary>
        [ConfigInfo("Configuration.StfKernel.StfArchiver.ArchiveTopDir")]
        public string ArchiveTopDir { get; set; }

        /// <summary>
        /// Gets or sets the temp directory.
        /// </summary>
        [ConfigInfo("Configuration.StfKernel.StfArchiver.TempDirectory")]
        public string TempDirectory { get; set; }

        /// <summary>
        /// Gets or sets the use date time in path.
        /// </summary>
        [ConfigInfo("Configuration.StfKernel.StfArchiver.UseDateTimeInPath")]
        public string UseDateTimeInPath { get; set; }

        /// <summary>
        /// Gets or sets the use test name in path.
        /// </summary>
        [ConfigInfo("Configuration.StfKernel.StfArchiver.UseTestnameInPath")]
        public string UseTestNameInPath { get; set; }
    }
}

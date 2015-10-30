namespace Mir.Stf.Utilities.Configuration
{
    public class StfArchiverConfiguration : IStfArchiverConfiguration
    {
        /// <summary>
        /// Gets or sets the destination to archive 
        /// </summary>
        [ConfigAttributes.ConfigInfo("StfKernel.StfArchiver.ArchiveDestination")]
        public string ArchiveDestination { get; set; }

        /// <summary>
        /// Gets or sets the name of ZipFile if the archive should be zipped.
        /// </summary>
        [ConfigAttributes.ConfigInfo("StfKernel.StfArchiver.ZipFilename")]
        public string ZipFilename { get; set; }

        /// <summary>
        /// Gets or sets the archive top dir.
        /// </summary>
        [ConfigAttributes.ConfigInfo("StfKernel.StfArchiver.ArchiveTopDir")]
        public string ArchiveTopDir { get; set; }

        /// <summary>
        /// Gets or sets the temp directory.
        /// </summary>
        [ConfigAttributes.ConfigInfo("StfKernel.StfArchiver.TempDirectory")]
        public string TempDirectory { get; set; }
    }
}

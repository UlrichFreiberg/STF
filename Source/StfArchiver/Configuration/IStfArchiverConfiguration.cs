namespace Mir.Stf.Utilities.Configuration
{
    public interface IStfArchiverConfiguration
    {
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
    }

}

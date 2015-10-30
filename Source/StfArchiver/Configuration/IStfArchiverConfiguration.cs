﻿// --------------------------------------------------------------------------------------------------------------------
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

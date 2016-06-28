// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfLogLevel.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities
{
    /// <summary>
    /// The log level.
    /// </summary>
    public enum StfLogLevel
    {
        /// <summary>
        /// The error.
        /// </summary>
        Error, 

        /// <summary>
        /// The warning.
        /// </summary>
        Warning, 

        /// <summary>
        /// The info.
        /// </summary>
        Info, 

        /// <summary>
        /// The debug.
        /// </summary>
        Debug, 

        /// <summary>
        /// The trace.
        /// </summary>
        Trace, 

        /// <summary>
        /// The internal.
        /// </summary>
        Internal, 

        /// <summary>
        /// The header.
        /// </summary>
        Header, 

        /// <summary>
        /// The sub header.
        /// </summary>
        SubHeader, 

        // ========================================
        // TestResults
        // ========================================

        /// <summary>
        /// The pass.
        /// </summary>
        Pass, 

        /// <summary>
        /// The fail.
        /// </summary>
        Fail,

        /// <summary>
        /// The inconclusive.
        /// </summary>
        Inconclusive,

        // ========================================
        // Level used to log keyvalues like OS, URL, VersionInfo etc...
        // ========================================

        /// <summary>
        /// The key value.
        /// </summary>
        KeyValue
    }
}

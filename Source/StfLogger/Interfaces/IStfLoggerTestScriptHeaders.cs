// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStfLoggerTestScriptHeaders.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.Interfaces
{
    /// <summary>
    /// The TestScriptHeaders <c>interface</c>.
    /// </summary>
    public interface IStfLoggerTestScriptHeaders
    {
        /// <summary>
        /// The set run status. When closing down the logfile, <c>this</c> gives an overall status for the tests performed so far.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool SetRunStatus();
    }
}

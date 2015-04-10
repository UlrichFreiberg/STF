// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITestScriptHeaders.cs" company="Foobar">
//   2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Stf.Utilities.Interfaces
{
    /// <summary>
    /// The TestScriptHeaders <c>interface</c>.
    /// </summary>
    public interface ITestScriptHeaders
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

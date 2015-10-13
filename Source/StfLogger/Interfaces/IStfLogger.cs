// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStfLogger.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.Interfaces
{
    /// <summary>
    /// An interface for the StfLogger
    /// </summary>
    public interface IStfLogger : IStfLoggerAutomationIdentification,
                                  IStfLoggerCallStackManagement,
                                  IStfLoggerLogfileManagement,
                                  IStfLoggerLoggingFunctions,
                                  IStfLoggerPerformanceManagement,
                                  IStfLoggerScreenshots,
                                  IStfLoggerTestScriptHeaders
    {
    }
}

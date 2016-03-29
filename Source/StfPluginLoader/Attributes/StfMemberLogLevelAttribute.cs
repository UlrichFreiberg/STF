// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfMemberLogLevelAttribute.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the StfMemberLogLevelAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.Attributes
{
    using System;

    /// <summary>
    /// The stf member log level attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public class StfMemberLogLevelAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StfMemberLogLevelAttribute"/> class.
        /// </summary>
        /// <param name="logLevel">
        /// The log level.
        /// </param>
        public StfMemberLogLevelAttribute(StfLogLevel logLevel)
        {
            LogLevel = logLevel;
        }

        /// <summary>
        /// Gets the log level.
        /// </summary>
        public StfLogLevel LogLevel { get; private set; }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfInterfaceLogLevelAttribute.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the StfInterfaceLogLevelAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Mir.Stf.Utilities.Attributes
{
    /// <summary>
    /// The stf class log level attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class StfInterfaceLogLevelAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StfInterfaceLogLevelAttribute"/> class.
        /// </summary>
        /// <param name="logLevel">
        /// The log level.
        /// </param>
        public StfInterfaceLogLevelAttribute(StfLogLevel logLevel)
        {
            LogLevel = logLevel;
        }
        
        /// <summary>
        /// Gets the log level.
        /// </summary>
        public StfLogLevel LogLevel { get; private set; }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfConfigurationException.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The stf configuration exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Mir.Stf.Exceptions
{
    /// <summary>
    /// The stf configuration exception.
    /// </summary>
    public class StfConfigurationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StfConfigurationException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public StfConfigurationException(string message) : base(message)
        {
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfTypeResolutionException.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the StfTypeResolutionException type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.Exceptions
{
    using System;

    /// <summary>
    /// The stf type resolution exception.
    /// </summary>
    public class StfTypeResolutionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StfTypeResolutionException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner Exception.
        /// </param>
        public StfTypeResolutionException(string message, Exception innerException = null)
            : base(message, innerException)
        {
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfKernel.cs" company="Foobar">
//   2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Stf
{
    using Stf.Utilities;

    /// <summary>
    /// The stf kernel.
    /// </summary>
    public class StfKernel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StfKernel"/> class.
        /// </summary>
        public StfKernel()
        {
            this.MyLogger = new StfLogger();
        }

        /// <summary>
        /// Gets the Stf logger.
        /// </summary>
        public StfLogger MyLogger { get; private set; }
    }
}

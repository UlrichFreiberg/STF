// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfModelBase.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the StfModelBase type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Mir.Stf.Utilities.Interfaces;

namespace Mir.Stf.Utilities
{
    /// <summary>
    /// The stf model base.
    /// </summary>
    public abstract class StfModelBase : IStfModelBase
    {
        /// <summary>
        /// Gets or sets the model state.
        /// </summary>
        public StfModelState ModelState { get; set; }

        /// <summary>
        /// Gets or sets the stf container.
        /// </summary>
        public IStfContainer StfContainer { get; set; }

        /// <summary>
        /// Gets or sets the my logger.
        /// </summary>
        public IStfLogger StfLogger { get; set; }

        /// <summary>
        /// The get.
        /// </summary>
        /// <typeparam name="T">
        /// The requested type to be resolved by the container
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T Get<T>()
        {
            return StfContainer.Get<T>();
        }

        /// <summary>
        /// The log.
        /// </summary>
        /// <param name="logMessage">
        /// The log message.
        /// </param>
        public void Log(string logMessage)
        {
            StfLogger.LogInfo(logMessage);
        }

        /// <summary>
        /// The log.
        /// </summary>
        /// <param name="logMessage">
        /// The log message.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        public void Log(string logMessage, params object[] args)
        {
            Log(string.Format(logMessage, args));
        }
    }
}

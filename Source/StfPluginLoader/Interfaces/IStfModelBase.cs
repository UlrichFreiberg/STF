// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStfModelBase.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the IStfModelBase type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.Interfaces
{
    /// <summary>
    /// The stf model state.
    /// </summary>
    public enum StfModelState
    {
        /// <summary>
        /// The model is initialized.
        /// </summary>
        Initialized,

        /// <summary>
        /// The model is un-initialized.
        /// </summary>
        UnInitialized,

        /// <summary>
        /// Model is ok.
        /// </summary>
        Ok,

        /// <summary>
        /// Model is validated and is valid.
        /// </summary>
        Valid,

        /// <summary>
        /// Model is validated and is invalid.
        /// </summary>
        Invalid,

        /// <summary>
        /// Model is in error.
        /// </summary>
        Error,

        /// <summary>
        /// Model state might be fishy.
        /// </summary>
        Suspect,

        /// <summary>
        /// Have no idea of the state of the model.
        /// </summary>
        Unknown
    }

    /// <summary>
    /// The StfModelBase interface.
    /// </summary>
    public interface IStfModelBase : IStfLoggable, IStfGettable
    {
        /// <summary>
        /// Gets or sets the model state.
        /// </summary>
        StfModelState ModelState { get; set; }

        /// <summary>
        /// The get.
        /// </summary>
        /// <typeparam name="T">
        /// The requested type to be resolved by the container
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T Get<T>();

        /// <summary>
        /// The log.
        /// </summary>
        /// <param name="logMessage">
        /// The log message.
        /// </param>
        void Log(string logMessage);

        /// <summary>
        /// The log.
        /// </summary>
        /// <param name="logMessage">
        ///     The log message.
        /// </param>
        /// <param name="args">
        ///     The args.
        /// </param>
        void Log(string logMessage, params object[] args);
    }
}

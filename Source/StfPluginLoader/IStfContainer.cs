// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStfContainer.cs" company="Foobar">
//   2015
// </copyright>
// <summary>
//   Defines the IStfContainer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Stf.Utilities
{
    /// <summary>
    /// The StfContainer interface.
    /// </summary>
    public interface IStfContainer
    {
        /// <summary>
        /// The register type.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        void RegisterType<T>();

        /// <summary>
        /// The register type.
        /// </summary>
        /// <typeparam name="TFrom">
        /// </typeparam>
        /// <typeparam name="TTo">
        /// </typeparam>
        void RegisterType<TFrom, TTo>() where TTo : TFrom;
    }
}

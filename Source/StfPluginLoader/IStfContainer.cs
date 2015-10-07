// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStfContainer.cs" company="Foobar">
//   2015
// </copyright>
// <summary>
//   Defines the IStfContainer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities
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
        /// the type to register
        /// </typeparam>
        void RegisterType<T>();

        /// <summary>
        /// The register type.
        /// </summary>
        /// <typeparam name="TFrom">
        /// The type interface to register
        /// </typeparam>
        /// <typeparam name="TTo">
        /// The type to register
        /// </typeparam>
        void RegisterType<TFrom, TTo>() where TTo : TFrom;

        /// <summary>
        /// The get.
        /// </summary>
        /// <typeparam name="T">
        /// The type to get from the container
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T Get<T>();
    }
}

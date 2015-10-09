// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfContainer.cs" company="Foobar">
//   2015
// </copyright>
// <summary>
//   Defines the StfContainer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Practices.Unity;
using Mir.Stf.Utilities.Extensions;

namespace Mir.Stf.Utilities
{
    /// <summary>
    /// The stf container.
    /// </summary>
    public class StfContainer : IStfContainer
    {
        /// <summary>
        /// The container.
        /// </summary>
        private readonly IUnityContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="StfContainer"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public StfContainer(IUnityContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// The register type.
        /// </summary>
        /// <typeparam name="T">
        /// The type to register with the Stf Container
        /// </typeparam>
        public void RegisterType<T>()
        {
            container.RegisterType<T>();
        }

        /// <summary>
        /// The register type.
        /// </summary>
        /// <typeparam name="TFrom">
        /// The interface to the type
        /// </typeparam>
        /// <typeparam name="TTo">
        /// The implementing type
        /// </typeparam>
        public void RegisterType<TFrom, TTo>() where TTo : TFrom
        {
            container.RegisterType<TFrom, TTo>();
        }

        /// <summary>
        /// The get.
        /// </summary>
        /// <typeparam name="T">
        /// The type to get from the container
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T Get<T>()
        {
            return container.ResolveType<T>();
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfContainer.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The stf container.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Mir.Stf.Utilities.Extensions;

namespace Mir.Stf.Utilities
{
    using Unity;

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
            container.RegisterMyType(typeof(T));
        }

        /// <summary>
        /// The register types.
        /// </summary>
        /// <param name="dictionary">
        /// The dictionary.
        /// </param>
        public void RegisterTypes(Dictionary<Type, Type> dictionary)
        {
            // There is no guarantee as to the order (interface or implementing type) in the dictionary
            // TODO: Do a check on the types to make sure we get the order right
            foreach (var pluginType in dictionary)
            {
                container.RegisterMyType(pluginType.Key, pluginType.Value);
            }
        }

        /// <summary>
        /// The register instance.
        /// </summary>
        /// <param name="instance">
        /// The instance.
        /// </param>
        public void RegisterInstance(object instance)
        {
            container.RegisterInstance(instance);
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
            container.RegisterMyType(typeof(TFrom), typeof(TTo));
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

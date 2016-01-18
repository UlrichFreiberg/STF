// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStfContainer.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The StfContainer interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

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
        /// The register types.
        /// </summary>
        /// <param name="dictionary">
        /// The dictionary.
        /// </param>
        void RegisterTypes(Dictionary<Type, Type> dictionary);

        /// <summary>
        /// The register instance.
        /// </summary>
        /// <param name="instance">
        /// The instance.
        /// </param>
        void RegisterInstance(object instance);

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

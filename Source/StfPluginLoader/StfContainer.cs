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
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Mir.Stf.Utilities.Extensions;
using Mir.Stf.Utilities.Interceptors;

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
        /// The register types.
        /// </summary>
        /// <param name="dictionary">
        /// The dictionary.
        /// </param>
        public void RegisterTypes(Dictionary<Type, Type> dictionary)
        {
            foreach (var pluginType in dictionary)
            {
                container.RegisterMyType(pluginType.Key, pluginType.Value);
            }
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

        /////// <summary>
        /////// The register type.
        /////// </summary>
        /////// <param name="typeFrom">
        /////// The type from.
        /////// </param>
        /////// <param name="typeTo">
        /////// The type to.
        /////// </param>
        ////private void RegisterType(Type typeFrom, Type typeTo)
        ////{
        ////    var injectionMembers = new List<InjectionMember>();
            
        ////    if (CheckTypeHasInterface(typeFrom, typeof(IStfGettable)))
        ////    {
        ////        injectionMembers.Add(new InjectionProperty("StfContainer"));
        ////    }

        ////    if (CheckTypeHasInterface(typeFrom, typeof(IStfLoggable)))
        ////    {
        ////        injectionMembers.Add(new InterceptionBehavior<PolicyInjectionBehavior>());
        ////        injectionMembers.Add(new Interceptor<InterfaceInterceptor>());

        ////        container.Configure<Interception>()
        ////        .AddPolicy(string.Format("LoggingFor{0}", typeFrom.Name))
        ////        .AddMatchingRule<TypeMatchingRule>(new InjectionConstructor(typeFrom.Name))
        ////        .AddCallHandler<LoggingHandler>(
        ////            new ContainerControlledLifetimeManager(),
        ////            new InjectionConstructor(new ResolvedParameter<StfLogger>()),
        ////            new InjectionProperty("Order", 1));
        ////    }

        ////    container.RegisterType(typeFrom, typeTo, injectionMembers.ToArray());
        ////}

        /////// <summary>
        /////// The type has interface.
        /////// </summary>
        /////// <param name="theType">
        /////// The the type.
        /////// </param>
        /////// <param name="expectedInterface">
        /////// The expected Interface.
        /////// </param>
        /////// <returns>
        /////// The <see cref="bool"/>.
        /////// </returns>
        ////private bool CheckTypeHasInterface(Type theType, Type expectedInterface)
        ////{
        ////    var theInterface = theType.GetInterface(expectedInterface.Name);
        ////    return theInterface != null;
        ////}
    }
}

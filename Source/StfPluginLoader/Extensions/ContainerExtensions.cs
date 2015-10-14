// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContainerExtensions.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The container extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Mir.Stf.Utilities.Interceptors;

namespace Mir.Stf.Utilities.Extensions
{
    /// <summary>
    /// The container extensions.
    /// </summary>
    public static class ContainerExtensions
    {
        /// <summary>
        /// The resolve type.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <typeparam name="T">
        /// The type to get
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T ResolveType<T>(this IUnityContainer container)
        {
            var returnObject = container.Resolve<T>();

            var pluginObject = returnObject as IStfPlugin;
            if (pluginObject != null)
            {
                pluginObject.Init();
            }

            return returnObject;
        }

        /// <summary>
        /// The register my type.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="typeFrom">
        /// The type from.
        /// </param>
        /// <param name="typeTo">
        /// The type to.
        /// </param>
        public static void RegisterMyType(this IUnityContainer container, Type typeFrom, Type typeTo)
        {
            if (IsAlreadyMapped(container, typeFrom, typeTo))
            {
                return;
            }

            var injectionMembers = new List<InjectionMember>();

            if (CheckTypeHasInterface<IStfGettable>(typeFrom))
            {
                injectionMembers.Add(new InjectionProperty("StfContainer"));
            }

            if (CheckTypeHasInterface<IStfLoggable>(typeFrom))
            {
                injectionMembers.Add(new InterceptionBehavior<PolicyInjectionBehavior>());
                injectionMembers.Add(new Interceptor<InterfaceInterceptor>());

                container.Configure<Interception>()
                .AddPolicy(string.Format("LoggingFor{0}", typeFrom.Name))
                .AddMatchingRule<TypeMatchingRule>(new InjectionConstructor(typeFrom.FullName))
                .AddCallHandler<LoggingHandler>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionConstructor(new ResolvedParameter<StfLogger>()),
                    new InjectionProperty("Order", 1));
            }

            container.RegisterType(typeFrom, typeTo, injectionMembers.ToArray());
        }

        /// <summary>
        /// The is already mapped.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="typeFrom">
        /// The type from.
        /// </param>
        /// <param name="typeTo">
        /// The type to.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <remarks>
        /// Unity's own .IsRegistered method only checks on registered type.
        /// We want to check on the mapped to type as well to prevent
        /// redundant interception policies from being registered
        /// but still allow custom implementations of StfInterfaces to 
        /// overwrite the default ones we register on startup
        /// </remarks>
        private static bool IsAlreadyMapped(IUnityContainer container, Type typeFrom, Type typeTo)
        {
            return container.Registrations.Any(
                containerRegistration =>
                    containerRegistration.RegisteredType == typeFrom && 
                    containerRegistration.MappedToType == typeTo &&
                    string.IsNullOrEmpty(containerRegistration.Name));
        }

        /// <summary>
        /// The type has interface.
        /// </summary>
        /// <typeparam name="T">
        /// The type of interface to check
        /// </typeparam>
        /// <param name="theType">
        /// The the type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool CheckTypeHasInterface<T>(Type theType)
        {
            var theInterface = theType.GetInterface(typeof(T).Name);
            return theInterface != null;
        }
    }
}

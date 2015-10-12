// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContainerExtensions.cs" company="Foobar">
//   2015
// </copyright>
// <summary>
//   The container extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
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
            var injectionMembers = new List<InjectionMember>();

            if (CheckTypeHasInterface(typeFrom, typeof(IStfGettable)))
            {
                injectionMembers.Add(new InjectionProperty("StfContainer"));
            }

            if (CheckTypeHasInterface(typeFrom, typeof(IStfLoggable)))
            {
                injectionMembers.Add(new InterceptionBehavior<PolicyInjectionBehavior>());
                injectionMembers.Add(new Interceptor<InterfaceInterceptor>());

                container.Configure<Interception>()
                .AddPolicy(string.Format("LoggingFor{0}", typeFrom.Name))
                .AddMatchingRule<TypeMatchingRule>(new InjectionConstructor(typeFrom.Name))
                .AddCallHandler<LoggingHandler>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionConstructor(new ResolvedParameter<StfLogger>()),
                    new InjectionProperty("Order", 1));
            }

            container.RegisterType(typeFrom, typeTo, injectionMembers.ToArray());
        }

        /// <summary>
        /// The type has interface.
        /// </summary>
        /// <param name="theType">
        /// The the type.
        /// </param>
        /// <param name="expectedInterface">
        /// The expected Interface.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool CheckTypeHasInterface(Type theType, Type expectedInterface)
        {
            var theInterface = theType.GetInterface(expectedInterface.Name);
            return theInterface != null;
        }
    }
}

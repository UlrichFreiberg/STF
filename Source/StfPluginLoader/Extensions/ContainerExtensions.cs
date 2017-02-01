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
using Mir.Stf.Utilities.Interfaces;

namespace Mir.Stf.Utilities.Extensions
{
    using System.Reflection;

    using Mir.Stf.Utilities.Attributes;

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
                if (!pluginObject.Init())
                {
                    throw new TypeInitializationException(
                        pluginObject.GetType().FullName,
                        new Exception(string.Format(
                            "Init returned false for StfPlugin: {0}",
                            pluginObject.GetType().Name)));
                }
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

            var injectionMembers = GetInjectionMembers(typeFrom, typeTo);
            ((List<InjectionMember>)injectionMembers)
                .AddRange(ConfigureForInterceptionIfNecessary(container, typeFrom, typeTo));

            RegisterType(container, typeFrom, typeTo, injectionMembers.ToArray());
        }

        /// <summary>
        /// The register my type.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="typeToRegister">
        /// The type to register.
        /// </param>
        public static void RegisterMyType(this IUnityContainer container, Type typeToRegister)
        {
            var injectionMembers = GetInjectionMembers(typeToRegister);
            RegisterType(container, typeToRegister, injectionMembers.ToArray());
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

        /// <summary>
        /// The check type has attribute.
        /// </summary>
        /// <param name="theType">
        /// The the type.
        /// </param>
        /// <typeparam name="T">
        /// The attribute to check for
        /// </typeparam>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool CheckTypeHasAttribute<T>(Type theType) where T : Attribute
        {
            var attribute = theType.GetCustomAttribute<T>();
            return attribute != null;
        }

        /// <summary>
        /// The get injection members.
        /// </summary>
        /// <param name="typeToRegister">
        /// The type to register.
        /// </param>
        /// <returns>
        /// The <see cref="IList{T}"/>.
        /// </returns>
        private static IList<InjectionMember> GetInjectionMembers(Type typeToRegister)
        {
            var injectionMembers = new List<InjectionMember>();

            if (CheckTypeHasInterface<IStfGettable>(typeToRegister))
            {
                injectionMembers.Add(new InjectionProperty("StfContainer"));
            }

            if (CheckTypeHasInterface<IStfLoggable>(typeToRegister))
            {
                injectionMembers.Add(new InjectionProperty("StfLogger"));
            }

            return injectionMembers;
        }

        /// <summary>
        /// The get injection members.
        /// </summary>
        /// <param name="typeFrom">
        /// The type from.
        /// </param>
        /// <param name="typeTo">
        /// The type to.
        /// </param>
        /// <returns>
        /// The <see cref="IList{T}"/>.
        /// </returns>
        private static IList<InjectionMember> GetInjectionMembers(Type typeFrom, Type typeTo)
        {
            // TODO: FIX PLEASE - We are getting redundant injectionmembers here! (they're not breaking anything, though, Unity doesn't care)
            var injectionMembers = GetInjectionMembers(typeFrom);
            ((List<InjectionMember>)injectionMembers)
                .AddRange(GetInjectionMembers(typeTo)
                .Where(i => !injectionMembers.Contains(i)));

            return injectionMembers;
        }

        /// <summary>
        /// The configure for interception if necessary.
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
        /// The <see cref="IList{T}"/>.
        /// </returns>
        private static IList<InjectionMember> ConfigureForInterceptionIfNecessary(IUnityContainer container, Type typeFrom, Type typeTo)
        {
            var injectionMembers = new List<InjectionMember>();

            if (CheckTypeHasInterface<IStfLoggable>(typeFrom) ||
                CheckTypeHasInterface<IStfModelBase>(typeTo) ||
                CheckTypeHasInterface<IStfAdapterBase>(typeTo))
            {
                injectionMembers.AddRange(AddInterceptionMembers(container, typeFrom));
                return injectionMembers;
            }

            return injectionMembers;
        }

        /// <summary>
        /// The add interception members.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="theType">
        /// The the type.
        /// </param>
        /// <returns>
        /// The <see cref="IList{T}"/>.
        /// </returns>
        private static IList<InjectionMember> AddInterceptionMembers(IUnityContainer container, Type theType)
        {
            var injectionMembers = new List<InjectionMember>();

            if (!theType.IsInterface)
            {
                return injectionMembers;
            }

            injectionMembers.Add(new InterceptionBehavior<PolicyInjectionBehavior>());
            injectionMembers.Add(new Interceptor<InterfaceInterceptor>());

            container.Configure<Interception>()
                .AddPolicy(string.Format("LoggingFor{0}", theType.Name))
                .AddMatchingRule<TypeMatchingRule>(new InjectionConstructor(theType.FullName))
                .AddCallHandler<LoggingHandler>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionConstructor(new ResolvedParameter<IStfLogger>()),
                    new InjectionProperty("Order", 1));

            return injectionMembers;
        }

        /// <summary>
        /// The register type.
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
        /// <param name="injectionMembers">
        /// The injection members.
        /// </param>
        private static void RegisterType(
            IUnityContainer container,
            Type typeFrom,
            Type typeTo,
            IList<InjectionMember> injectionMembers)
        {
            if (CheckTypeHasAttribute<StfSingletonAttribute>(typeFrom) || 
                CheckTypeHasAttribute<StfSingletonAttribute>(typeTo))
            {
                container.RegisterType(typeFrom, typeTo, new ContainerControlledLifetimeManager(), injectionMembers.ToArray());
            }
            else
            {
                container.RegisterType(typeFrom, typeTo, injectionMembers.ToArray());
            }
        }

        /// <summary>
        /// The register type.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="typeToRegister">
        /// The type to register.
        /// </param>
        /// <param name="injectionMembers">
        /// The injection members.
        /// </param>
        private static void RegisterType(
            IUnityContainer container,
            Type typeToRegister,
            IList<InjectionMember> injectionMembers)
        {
            if (CheckTypeHasAttribute<StfSingletonAttribute>(typeToRegister))
            {
                container.RegisterType(typeToRegister, new ContainerControlledLifetimeManager(), injectionMembers.ToArray());
            }
            else
            {
                container.RegisterType(typeToRegister, injectionMembers.ToArray());
            }
        }
    }
}

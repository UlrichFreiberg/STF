// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the MyType type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AssemblyResolve
{
    using System;
    using System.Reflection;

    /// <summary>
    /// The test.
    /// </summary>
    public class Test
    {
        /// <summary>
        /// The main.
        /// </summary>
        public static void Main()
        {
            var currentDomain = AppDomain.CurrentDomain;

            // This call will fail to create an instance of MyType since the
            // assembly resolver is not set
            InstantiateMyTypeFail(currentDomain);

            currentDomain.AssemblyResolve += MyResolveEventHandler;

            // This call will succeed in creating an instance of MyType since the
            // assembly resolver is now set.
            InstantiateMyTypeFail(currentDomain);

            // This call will succeed in creating an instance of MyType since the
            // assembly name is valid.
            InstantiateMyTypeSucceed(currentDomain);
        }

        /// <summary>
        /// The instantiate my type fail.
        /// </summary>
        /// <param name="domain">
        /// The domain.
        /// </param>
        private static void InstantiateMyTypeFail(AppDomain domain)
        {
            // Calling InstantiateMyType will always fail since the assembly info
            // given to CreateInstance is invalid.
            try
            {
                // You must supply a valid fully qualified assembly name here.
                //domain.CreateInstance("AssemblyResolve, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "MyType");
                domain.CreateInstance("AssemblyResolve", "MyType1");
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// The instantiate my type succeed.
        /// </summary>
        /// <param name="domain">
        /// The domain.
        /// </param>
        private static void InstantiateMyTypeSucceed(AppDomain domain)
        {
            try
            {
                var asmname = Assembly.GetCallingAssembly().FullName;

                domain.CreateInstance("AssemblyResolve", "MyType");
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// The my resolve event handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="Assembly"/>.
        /// </returns>
        private static Assembly MyResolveEventHandler(object sender, ResolveEventArgs args)
        {
            Console.WriteLine("Resolving...");

            return typeof(MyType).Assembly;
        }
    }
}
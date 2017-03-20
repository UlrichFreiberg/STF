// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReflectionTest.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the ReflectionTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mir.Stf;
using Mir.Stf.Utilities;

namespace Tests
{
    /// <summary>
    /// The reflection test.
    /// </summary>
    [TestClass]
    public class DefaultTest : StfTestScriptBase
    {
        [TestMethod]
        public void TestDefaultNoDefaultSection()
        {
            // Load a configuration in StfConfiguration
            var stfConfiguration = new StfConfiguration(@"TestData\Defaulting\NoDefaultSection.xml");

            var uUsername = stfConfiguration.GetKeyValue("Users.Ulrich.Username");
            var uPassword = stfConfiguration.GetKeyValue("Users.Ulrich.Password");
            var kUsername = stfConfiguration.GetKeyValue("Users.Kasper.Username");
            var kPassword = stfConfiguration.GetKeyValue("Users.Kasper.Password");

            StfAssert.AreEqual("Username is Ulrich", "User_Ulrich", uUsername);
            StfAssert.AreEqual("Password for Ulrich is U777", "U777", uPassword);
            StfAssert.AreEqual("Username is Kasper", "User_Kasper", kUsername);
            StfAssert.AreEqual("Password for Kasper is K999", "K999", kPassword);
        }

        [TestMethod]
        public void TestDefaultDefaultSection()
        {
            // Load a configuration in StfConfiguration
            var stfConfiguration = new StfConfiguration(@"TestData\Defaulting\DefaultSection.xml");

            var dUsername = stfConfiguration.GetKeyValue("Users.Username");
            var dPassword = stfConfiguration.GetKeyValue("Users.Password");
            StfAssert.AreEqual("Default Username is User_Kasper", "User_Kasper", dUsername);
            StfAssert.AreEqual("Default Password is K999", "K999", dPassword);

            var uUsername = stfConfiguration.GetKeyValue("Users.Ulrich.Username");
            var uPassword = stfConfiguration.GetKeyValue("Users.Ulrich.Password");
            var kUsername = stfConfiguration.GetKeyValue("Users.Kasper.Username");
            var kPassword = stfConfiguration.GetKeyValue("Users.Kasper.Password");

            StfAssert.AreEqual("Username is User_Ulrich", "User_Ulrich", uUsername);
            StfAssert.AreEqual("Password for Ulrich is U777", "U777", uPassword);
            StfAssert.AreEqual("Username is Kasper", "User_Kasper", kUsername);
            StfAssert.AreEqual("Password for Kasper is K999", "K999", kPassword);
        }

        [TestMethod]
        public void TestDefaultGetSetConfigValues()
        {
            // Load a configuration in StfConfiguration
            var stfConfiguration = new StfConfiguration(@"TestData\Defaulting\DefaultSectionWithEnvironments.xml");

            stfConfiguration.Environment = stfConfiguration.DefaultEnvironment;

            var dUsername = stfConfiguration.GetConfigValue("Users.Username");
            var dPassword = stfConfiguration.GetConfigValue("Users.Password");

            StfAssert.AreEqual("Default Username is User_Kasper", "User_Kasper", dUsername);
            StfAssert.AreEqual("Default Password is K999", "K999", dPassword);

            var newUserName = "User_Kasper_Updated";
            var newUserPassword = "K999_Updated";

            stfConfiguration.SetConfigValue("Users.Username", newUserName);
            stfConfiguration.SetConfigValue("Users.Password", newUserPassword);

            dUsername = stfConfiguration.GetConfigValue("Users.Username");
            dPassword = stfConfiguration.GetConfigValue("Users.Password");

            StfAssert.AreEqual("Default Username is updated", newUserName, dUsername);
            StfAssert.AreEqual("Default Password is updated", newUserPassword, dPassword);
        }

        [TestMethod]
        public void TestDefaultDefaultSectionWithVariables()
        {
            System.Environment.SetEnvironmentVariable("STFUSERNAME", "Bent");

            // Load a configuration in StfConfiguration
            var stfConfiguration = new StfConfiguration(@"TestData\Defaulting\DefaultSectionWithVariable.xml");

            var dUsername = stfConfiguration.GetKeyValue("Users.Username");
            var dPassword = stfConfiguration.GetKeyValue("Users.Password");
            StfAssert.AreEqual("Default Username is User_Bent", "User_Bent", dUsername);
            StfAssert.AreEqual("Default Password is K999", "B42", dPassword);

            var uUsername = stfConfiguration.GetKeyValue("Users.Ulrich.Username");
            var uPassword = stfConfiguration.GetKeyValue("Users.Ulrich.Password");
            var kUsername = stfConfiguration.GetKeyValue("Users.Kasper.Username");
            var kPassword = stfConfiguration.GetKeyValue("Users.Kasper.Password");

            StfAssert.AreEqual("Username is User_Ulrich", "User_Ulrich", uUsername);
            StfAssert.AreEqual("Password for Ulrich is U777", "U777", uPassword);
            StfAssert.AreEqual("Username is Kasper", "User_Kasper", kUsername);
            StfAssert.AreEqual("Password for Kasper is K999", "K999", kPassword);
        }

        
    }
}

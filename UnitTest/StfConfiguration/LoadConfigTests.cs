// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoadConfigTests.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the UnitTest1 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mir.Stf;
using Mir.Stf.Utilities;

namespace Tests
{
    /// <summary>
    /// The load config tests.
    /// </summary>
    [TestClass]
    public class LoadConfigTests : StfTestScriptBase
    {
        /// <summary>
        /// Test of load config with 2 sub sections.
        /// </summary>
        [TestMethod]
        public void LoadConfig2SubSections()
        {
            var stfConfiguration = new StfConfiguration();
            var conf = stfConfiguration.LoadConfig(@"TestData\ConfigOverlay\Config1.xml");

            Assert.AreEqual(1, conf.Sections.Count, 1);
            var sectionTotest = conf.Sections["SectionName"];
            Assert.AreEqual("SectionName", sectionTotest.SectionName);
            Assert.AreEqual("SubSectionName1", sectionTotest.DefaultSection);
            Assert.AreEqual(2, sectionTotest.Sections.Count);
            Assert.AreEqual(1, sectionTotest.Keys.Count);
            Assert.AreEqual("config1_key1value", sectionTotest.Keys["k1"].KeyValue);

            // first subkey
            sectionTotest = conf.Sections["SectionName"].Sections["SubSectionName1"];
            Assert.AreEqual(1, sectionTotest.Keys.Count);
            Assert.AreEqual("SubSectionName1", sectionTotest.SectionName);
            Assert.IsTrue(string.IsNullOrEmpty(sectionTotest.DefaultSection));
            Assert.AreEqual("config1_subkey1value", sectionTotest.Keys["sk1"].KeyValue);
            Assert.AreEqual(0, sectionTotest.Sections.Count);

            // second subkey
            sectionTotest = conf.Sections["SectionName"].Sections["SubSectionName2"];
            Assert.AreEqual(1, sectionTotest.Keys.Count);
            Assert.AreEqual("SubSectionName2", sectionTotest.SectionName);
            Assert.IsTrue(string.IsNullOrEmpty(sectionTotest.DefaultSection));
            Assert.AreEqual("config1_subkey2value", sectionTotest.Keys["sk2"].KeyValue);
            Assert.AreEqual(0, sectionTotest.Sections.Count);
        }

        [TestMethod]
        public void TestSetValue()
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

            StfAssert.IsTrue("Setting config value", stfConfiguration.SetConfigValue("Users.Ulrich.Username", "New_Ulrich"));
            StfAssert.AreEqual("Username for Ulrich updated", "New_Ulrich",
                stfConfiguration.GetKeyValue("Users.Ulrich.Username"));

            StfAssert.IsTrue("Setting config value", stfConfiguration.SetConfigValue("Users.Ulrich.Password", "Super1234"));
            StfAssert.AreEqual("Password for Ulrich updated", "Super1234",
                stfConfiguration.GetKeyValue("Users.Ulrich.Password"));
        }

        [TestMethod]
        public void TestSetValueOnExistingKey()
        {
            var stfConfiguration = new StfConfiguration(@"TestData\Defaulting\NoDefaultSection.xml");

            StfAssert.IsFalse("Trying to set non existing value",
                stfConfiguration.SetConfigValue("Users.Ulrich.FortyTwo", "New_Ulrich"));

            string configValue;
            StfAssert.IsFalse("Can't get value", stfConfiguration.TryGetKeyValue("Users.Ulrich.FortyTwo", out configValue));
        }

        /// <summary>
        /// Test of load config with 1 section 1 key.
        /// </summary>
        [TestMethod]
        public void LoadConfig1Section1Key()
        {
            var stfConfiguration = new StfConfiguration();
            var conf = stfConfiguration.LoadConfig(@"TestData\ConfigOverlay\Config2.xml");

            Assert.AreEqual(1, conf.Sections.Count);
            var sectionTotest = conf.Sections["SectionName"];
            Assert.AreEqual("SectionName", sectionTotest.SectionName);
            Assert.AreEqual(0, sectionTotest.Sections.Count);
            Assert.AreEqual(1, sectionTotest.Keys.Count);
            Assert.AreEqual("config2_key1value", sectionTotest.Keys["k1"].KeyValue);
        }

        /// <summary>
        /// Test of load config with 1 section 2 keys.
        /// </summary>
        [TestMethod]
        public void LoadConfig1Section2Keys()
        {
            var stfConfiguration = new StfConfiguration();
            var conf = stfConfiguration.LoadConfig(@"TestData\ConfigOverlay\Config3.xml");

            var sectionTotest = conf.Sections["SectionName"];
            Assert.AreEqual("SectionName", sectionTotest.SectionName);
            Assert.AreEqual(0, sectionTotest.Sections.Count);
            Assert.AreEqual(2, sectionTotest.Keys.Count);
            Assert.AreEqual("config3_key2value", sectionTotest.Keys["k2"].KeyValue);
            Assert.AreEqual("config3_key3value", sectionTotest.Keys["k3"].KeyValue);
        }

        /// <summary>
        /// Test of load config one section within one section.
        /// </summary>
        [TestMethod]
        public void LoadConfigOneSectionWithinOneSection()
        {
            var stfConfiguration = new StfConfiguration();
            var conf = stfConfiguration.LoadConfig(@"TestData\ConfigOverlay\ConfigOneSectionWithinOneSection.xml");

            var sectionTotest = conf.Sections["SectionNameOuter"];
            Assert.AreEqual("SectionNameOuter", sectionTotest.SectionName);

            sectionTotest = sectionTotest.Sections["SectionNameInner"];
            Assert.AreEqual("SectionNameInner", sectionTotest.SectionName);
        }

        /// <summary>
        /// Test of the load config two sections side by side.
        /// </summary>
        [TestMethod]
        public void LoadConfigTwoSectionsSideBySide()
        {
            var stfConfiguration = new StfConfiguration();
            var conf = stfConfiguration.LoadConfig(@"TestData\ConfigOverlay\ConfigTwoSectionsSideBySide.xml");

            var sectionTotest = conf.Sections["SectionNameFirst"];
            Assert.AreEqual("SectionNameFirst", sectionTotest.SectionName);

            sectionTotest = conf.Sections["SectionNameSecond"];
            Assert.AreEqual("SectionNameSecond", sectionTotest.SectionName);
        }
    }
}

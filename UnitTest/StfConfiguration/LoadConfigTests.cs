// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoadConfigTests.cs" company="Mir Software">
//   Copyright 2013 Mir Software. All rights reserved.
// </copyright>
// <summary>
//   Defines the UnitTest1 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    using Stf.Utilities;

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
            var yacf = new StfConfiguration();
            var conf = yacf.LoadConfig(@"TestData\ConfigOverlay\Config1.xml");

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

        /// <summary>
        /// Test of load config with 1 section 1 key.
        /// </summary>
        [TestMethod]
        public void LoadConfig1Section1Key()
        {
            var yacf = new StfConfiguration();
            var conf = yacf.LoadConfig(@"TestData\ConfigOverlay\Config2.xml");

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
            var yacf = new StfConfiguration();
            var conf = yacf.LoadConfig(@"TestData\ConfigOverlay\Config3.xml");

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
            var yacf = new StfConfiguration();
            var conf = yacf.LoadConfig(@"TestData\ConfigOverlay\ConfigOneSectionWithinOneSection.xml");

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
            var yacf = new StfConfiguration();
            var conf = yacf.LoadConfig(@"TestData\ConfigOverlay\ConfigTwoSectionsSideBySide.xml");

            var sectionTotest = conf.Sections["SectionNameFirst"];
            Assert.AreEqual("SectionNameFirst", sectionTotest.SectionName);

            sectionTotest = conf.Sections["SectionNameSecond"];
            Assert.AreEqual("SectionNameSecond", sectionTotest.SectionName);
        }
    }
}

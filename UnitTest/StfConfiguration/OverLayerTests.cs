// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OverLayerTests.cs" company="Mir Software">
//   Copyright 2013 Mir Software. All rights reserved.
// </copyright>
// <summary>
//   Defines the OverLayerTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    using System.IO;

    using Stf.Utilities;

    /// <summary>
    /// The over layer tests.
    /// </summary>
    [TestClass]
    public class OverLayerTests : StfTestScriptBase
    {
        /// <summary>
        /// TODO The over lay config1 with config2.
        /// </summary>
        [TestMethod]
        public void OverLay12()
        {
            var yacf = new StfConfiguration();
            var conf1 = yacf.LoadConfig(@"TestData\ConfigOverlay\Config1.xml");
            var conf2 = yacf.LoadConfig(@"TestData\ConfigOverlay\Config2.xml");
            var conf12 = yacf.LoadConfig(@"TestData\ConfigOverlay\Config12.xml");

            var overLayed = yacf.OverLay(conf1, conf2);

            dumpTree(overLayed, @"overLayed.xml");
            dumpTree(conf12, @"expected.xml");

            Assert.IsTrue(conf12.Identical(conf12, overLayed));
        }

        /// <summary>
        /// Test of over lay config with 1 section 2 keys.
        /// </summary>
        [TestMethod]
        public void OverLay23()
        {
            var yacf = new StfConfiguration();
            var conf2 = yacf.LoadConfig(@"TestData\ConfigOverlay\Config2.xml");
            var conf3 = yacf.LoadConfig(@"TestData\ConfigOverlay\Config3.xml");
            var conf23 = yacf.LoadConfig(@"TestData\ConfigOverlay\Config23.xml");

            var overLayed = yacf.OverLay(conf2, conf3);
            dumpTree(overLayed, @"overLayed.xml");
            dumpTree(conf23, @"expected.xml");

            Assert.IsTrue(conf2.Identical(conf23, overLayed));
        }

        /// <summary>
        /// Test of the over lay simple with null.
        /// </summary>
        [TestMethod]
        public void OverLaySimpleWithNull()
        {
            var yacf = new StfConfiguration();
            var conf2 = yacf.LoadConfig(@"TestData\ConfigOverlay\Config2.xml");

            var overLayed = yacf.OverLay(conf2, null);

            dumpTree(overLayed, @"overLayed.xml");
            dumpTree(conf2, @"expected.xml");
            
            Assert.IsTrue(conf2.Identical(conf2, overLayed));
        }

        /// <summary>
        /// Test of the over lay null with simple.
        /// </summary>
        [TestMethod]
        public void OverLayNullWithSimple()
        {
            var yacf = new StfConfiguration();
            var conf2 = yacf.LoadConfig(@"TestData\ConfigOverlay\Config2.xml");

            var overLayed = yacf.OverLay(null, conf2);

            dumpTree(overLayed, @"overLayed.xml");
            dumpTree(conf2, @"expected.xml");

            Assert.IsTrue(conf2.Identical(conf2, overLayed));
        }

        /// <summary>
        /// Test of the over lay config two sections side by side.
        /// </summary>
        [TestMethod]
        public void OverLayConfigTwoSectionsSideBySide()
        {
            var yacf = new StfConfiguration();
            var conf1 = yacf.LoadConfig(@"TestData\ConfigOverlay\ConfigTwoSectionsSideBySide.xml");
            var conf2 = yacf.LoadConfig(@"TestData\ConfigOverlay\ConfigTwoSectionsSideBySide.xml");

            var overLayed = yacf.OverLay(conf1, conf2);

            dumpTree(overLayed, @"overLayed.xml");
            dumpTree(conf2, @"expected.xml");
            
            Assert.IsTrue(conf2.Identical(conf2, overLayed));
        }

        /// <summary>
        /// Test of the over lay config config one section within one section.
        /// </summary>
        [TestMethod]
        public void OverLayConfigConfigOneSectionWithinOneSection()
        {
            var yacf = new StfConfiguration();
            var conf1 = yacf.LoadConfig(@"TestData\ConfigOverlay\ConfigOneSectionWithinOneSection.xml");
            var conf2 = yacf.LoadConfig(@"TestData\ConfigOverlay\ConfigOneSectionWithinOneSection.xml");

            var overLayed = yacf.OverLay(conf1, conf2);

            dumpTree(overLayed, @"overLayed.xml");
            dumpTree(conf2, @"expected.xml");

            Assert.IsTrue(conf2.Identical(conf2, overLayed));
        }

        /// <summary>
        /// Test of the over lay config config one section within one section.
        /// </summary>
        [TestMethod]
        public void OverLayConfigConfigOneSectionWithinOneSectionWithinOneSection()
        {
            var yacf = new StfConfiguration();
            var conf1 = yacf.LoadConfig(@"TestData\ConfigOverlay\ConfigOneSectionWithinOneSectionWithinOneSection.xml");
            var conf2 = yacf.LoadConfig(@"TestData\ConfigOverlay\ConfigOneSectionWithinOneSectionWithinOneSection.xml");

            var overLayed = yacf.OverLay(conf1, conf2);

            dumpTree(overLayed, @"overLayed.xml");
            dumpTree(conf2, @"expected.xml");
            
            Assert.IsTrue(conf2.Identical(conf2, overLayed));
        }

        /// <summary>
        /// Test MakeCopy using all the test config files
        /// </summary>
        [TestMethod]
        public void MakeCopyTests()
        {
            var yacf = new StfConfiguration();

            var configFiles = Directory.EnumerateFiles(".", "config*.xml");

            foreach (var configFile in configFiles)
            {
                var conf = yacf.LoadConfig(configFile);
                var confCopy = conf.MakeCopy();

                dumpTree(conf, "Original" + configFile.Replace(@".\", string.Empty));
                dumpTree(confCopy, "Copy" + configFile.Replace(@".\", string.Empty));

                Assert.IsTrue(conf.Identical(conf, confCopy));
            }
        }

        private void dumpTree(Section Section2Dump, string fileName)
        {
            var dumpFile = Path.Combine(@"c:\temp\", TestContext.TestName + "_" + fileName);
            Section2Dump.DumpSection(Section.DumpAs.AsXml, dumpFile);
        }
    }
}

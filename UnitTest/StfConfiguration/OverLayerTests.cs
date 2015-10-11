// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OverLayerTests.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the OverLayerTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mir.Stf;
using Mir.Stf.Utilities;

namespace Tests
{
    using System.IO;

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
            var stfConfiguration = new StfConfiguration();
            var conf1 = stfConfiguration.LoadConfig(@"TestData\ConfigOverlay\Config1.xml");
            var conf2 = stfConfiguration.LoadConfig(@"TestData\ConfigOverlay\Config2.xml");
            var conf12 = stfConfiguration.LoadConfig(@"TestData\ConfigOverlay\Config12.xml");

            var overLayed = stfConfiguration.OverLay(conf1, conf2);

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
            var stfConfiguration = new StfConfiguration();
            var conf2 = stfConfiguration.LoadConfig(@"TestData\ConfigOverlay\Config2.xml");
            var conf3 = stfConfiguration.LoadConfig(@"TestData\ConfigOverlay\Config3.xml");
            var conf23 = stfConfiguration.LoadConfig(@"TestData\ConfigOverlay\Config23.xml");

            var overLayed = stfConfiguration.OverLay(conf2, conf3);
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
            var stfConfiguration = new StfConfiguration();
            var conf2 = stfConfiguration.LoadConfig(@"TestData\ConfigOverlay\Config2.xml");

            var overLayed = stfConfiguration.OverLay(conf2, null);

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
            var stfConfiguration = new StfConfiguration();
            var conf2 = stfConfiguration.LoadConfig(@"TestData\ConfigOverlay\Config2.xml");

            var overLayed = stfConfiguration.OverLay(null, conf2);

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
            var stfConfiguration = new StfConfiguration();
            var conf1 = stfConfiguration.LoadConfig(@"TestData\ConfigOverlay\ConfigTwoSectionsSideBySide.xml");
            var conf2 = stfConfiguration.LoadConfig(@"TestData\ConfigOverlay\ConfigTwoSectionsSideBySide.xml");

            var overLayed = stfConfiguration.OverLay(conf1, conf2);

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
            var stfConfiguration = new StfConfiguration();
            var conf1 = stfConfiguration.LoadConfig(@"TestData\ConfigOverlay\ConfigOneSectionWithinOneSection.xml");
            var conf2 = stfConfiguration.LoadConfig(@"TestData\ConfigOverlay\ConfigOneSectionWithinOneSection.xml");

            var overLayed = stfConfiguration.OverLay(conf1, conf2);

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
            var stfConfiguration = new StfConfiguration();
            var conf1 = stfConfiguration.LoadConfig(@"TestData\ConfigOverlay\ConfigOneSectionWithinOneSectionWithinOneSection.xml");
            var conf2 = stfConfiguration.LoadConfig(@"TestData\ConfigOverlay\ConfigOneSectionWithinOneSectionWithinOneSection.xml");

            var overLayed = stfConfiguration.OverLay(conf1, conf2);

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
            var stfConfiguration = new StfConfiguration();

            var configFiles = Directory.EnumerateFiles(".", "config*.xml");

            foreach (var configFile in configFiles)
            {
                var conf = stfConfiguration.LoadConfig(configFile);
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

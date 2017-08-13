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

            DumpTree(overLayed, @"overLayed.xml");
            DumpTree(conf12, @"expected.xml");

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
            DumpTree(overLayed, @"overLayed.xml");
            DumpTree(conf23, @"expected.xml");

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

            DumpTree(overLayed, @"overLayed.xml");
            DumpTree(conf2, @"expected.xml");
            
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

            DumpTree(overLayed, @"overLayed.xml");
            DumpTree(conf2, @"expected.xml");

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

            DumpTree(overLayed, @"overLayed.xml");
            DumpTree(conf2, @"expected.xml");
            
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

            DumpTree(overLayed, @"overLayed.xml");
            DumpTree(conf2, @"expected.xml");

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

            DumpTree(overLayed, @"overLayed.xml");
            DumpTree(conf2, @"expected.xml");
            
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

                DumpTree(conf, "Original" + configFile.Replace(@".\", string.Empty));
                DumpTree(confCopy, "Copy" + configFile.Replace(@".\", string.Empty));

                Assert.IsTrue(conf.Identical(conf, confCopy));
            }
        }

        /// <summary>
        /// The over lay config with duplicated key.
        /// </summary>
        [TestMethod]
        public void OverLayConfigWithDuplicatedKey()
        {
            var stfConfiguration = new StfConfiguration();
            var conf1 = stfConfiguration.LoadConfig(@"TestData\ConfigOverlay\ConfigWithDuplicatedKey.xml");
            var conf2 = stfConfiguration.LoadConfig(@"TestData\ConfigOverlay\ConfigWithDuplicatedKeyOverlay.xml");
            var overLayed = stfConfiguration.OverLay(conf1, conf2);

            DumpTree(conf1, @"conf1.xml");
            DumpTree(conf2, @"conf2.xml");
            DumpTree(overLayed, @"overLayed.xml");

            StfAssert.IsTrue("Comparing", overLayed.Identical(overLayed, overLayed));
        }

        /// <summary>
        /// The over lay config with duplicated section.
        /// </summary>
        [TestMethod]
        public void OverLayConfigWithDuplicatedSection()
        {
            var stfConfiguration = new StfConfiguration();
            var conf1 = stfConfiguration.LoadConfig(@"TestData\ConfigOverlay\ConfigWithDuplicatedSection.xml");
            var conf2 = stfConfiguration.LoadConfig(@"TestData\ConfigOverlay\ConfigWithDuplicatedSectionOverlay.xml");
            var overLayed = stfConfiguration.OverLay(conf1, conf2);
            var confExpected = stfConfiguration.LoadConfig(@"TestData\ConfigOverlay\ConfigWithDuplicatedSectionExpected.xml");

            DumpTree(overLayed, @"overLayed.xml");
            DumpTree(confExpected, @"confExpected.xml");

            StfAssert.IsTrue("Comparing", confExpected.Identical(confExpected, overLayed));
        }

        /// <summary>
        /// The dump tree.
        /// </summary>
        /// <param name="section2Dump">
        /// The section 2 dump.
        /// </param>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        private void DumpTree(Section section2Dump, string fileName)
        {
            var dumpFile = Path.Combine(@"c:\temp\", TestContext.TestName + "_" + fileName);

            section2Dump.DumpSection(Section.DumpAs.AsXml, dumpFile);
        }
    }
}

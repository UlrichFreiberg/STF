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
    public class ReflectionTest : StfTestScriptBase
    {
        /// <summary>
        /// The reflection test method 1.
        /// </summary>
        [TestMethod]
        public void ReflectionTestMethod1()
        {
            var tmc1 = new TestMethod1Class
                           {
                               PropPublicString1 = "pupStr1", 
                               PropPublicString2 = "pupStr2"
                           };

            // Load a configuration in StfConfiguration
            var stfConfiguration = new StfConfiguration(@"TestData\Reflection\Reflection1.xml");

            // Load that (resolved) configuration into the User Object
            var props = stfConfiguration.LoadUserConfiguration(tmc1);

            Assert.IsTrue(props.Count > 0, "Props collection is empty");
            Assert.AreEqual("config_key1value", tmc1.PropPublicString1);
            Assert.AreEqual("config_key2value", tmc1.PropPublicString2);
        }

        /// <summary>
        /// The reflection test method 1.
        /// </summary>
        [TestMethod]
        public void ReflectionTestMethod2()
        {
            var tmc1 = new TestMethodClassWithNonStfProperties
            {
                PropPublicString1 = "pupStr1",
                PropPublicString2 = "pupStr2"
            };

            // Load a configuration in StfConfiguration
            var stfConfiguration = new StfConfiguration(@"TestData\Reflection\Reflection1.xml");

            // Load that (resolved) configuration into the User Object
            var props = stfConfiguration.LoadUserConfiguration(tmc1);

            Assert.IsTrue(props.Count > 0, "Props collection is empty");
            Assert.AreEqual("config_key1value", tmc1.PropPublicString1);
            Assert.AreEqual("config_key2value", tmc1.PropPublicString2);
            StfAssert.AreEqual("Bent", string.Empty, tmc1.Bent);
        }

        /// <summary>
        /// The test method load user configuration with no default value.
        /// </summary>
        [TestMethod]
        public void TestMethodLoadUserConfigurationWithNoDefaultValue()
        {
            var tmc1 = new TestMethodClassWithNonStringProperties
            {
                PropPublicString1 = "pupStr1",
                PropPublicString2 = "pupStr2"
            };

            // Load a configuration in StfConfiguration
            var stfConfiguration = new StfConfiguration(@"TestData\Reflection\Reflection1.xml");

            // Load that (resolved) configuration into the User Object
            var props = stfConfiguration.LoadUserConfiguration(tmc1);

            Assert.IsTrue(props.Count > 0, "Props collection is empty");
            Assert.AreEqual("config_key1value", tmc1.PropPublicString1);
            Assert.AreEqual("config_key2value", tmc1.PropPublicString2);
            StfAssert.AreEqual("Bent", 0, tmc1.Bent);
        }

        /// <summary>
        /// The test method 1 class.
        /// </summary>
        private class TestMethod1Class
        {
            /// <summary>
            /// Gets or sets the prop public string 1.
            /// </summary>
            [StfConfiguration("SectionName.k1")]
            public string PropPublicString1 { get; set; }

            /// <summary>
            /// Gets or sets the prop public string 2.
            /// </summary>
            [StfConfiguration("SectionName.SubSectionName.k2")]
            public string PropPublicString2 { get; set; }
        }

        /// <summary>
        /// The test method 1 class.
        /// </summary>
        private class TestMethodClassWithNonStfProperties
        {
            /// <summary>
            /// Gets or sets the prop public string 1.
            /// </summary>
            [StfConfiguration("SectionName.k1")]
            public string PropPublicString1 { get; set; }

            /// <summary>
            /// Gets or sets bent.
            /// </summary>
            public string Bent { get; set; }

            /// <summary>
            /// Gets or sets the prop public string 2.
            /// </summary>
            [StfConfiguration("SectionName.SubSectionName.k2")]
            public string PropPublicString2 { get; set; }
        }

        /// <summary>
        /// The test method class with non string properties.
        /// </summary>
        private class TestMethodClassWithNonStringProperties
        {
            /// <summary>
            /// Gets or sets the prop public string 1.
            /// </summary>
            [StfConfiguration("SectionName.k1")]
            public string PropPublicString1 { get; set; }

            /// <summary>
            /// Gets or sets bent.
            /// </summary>
            [StfConfiguration("SectionName.NotExisting.BentValue")]
            public int Bent { get; set; }

            /// <summary>
            /// Gets or sets the prop public string 2.
            /// </summary>
            [StfConfiguration("SectionName.SubSectionName.k2")]
            public string PropPublicString2 { get; set; }
        }
    }
}

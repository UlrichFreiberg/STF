// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReflectionTest.cs" company="Mir Software">
//   2015
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
    [TestClass]
    public class ReflectionTest : StfTestScriptBase
    {
        class TestMethod1Class
        {
            [ConfigAttributes.ConfigInfo("SectionName.k1")]
            public string PropPublicString1 { get; set; }

            [ConfigAttributes.ConfigInfo("SectionName.SubSectionName.k2")]
            public string PropPublicString2 { get; set; }
        }

        [TestMethod]
        public void ReflectionTestMethod1()
        {
            var tmc1 = new TestMethod1Class
                           {
                               PropPublicString1 = "pupStr1", 
                               PropPublicString2 = "pupStr2"
                           };

            // Load a configuration in StfConfiguration
            var yacf = new StfConfiguration(@"TestData\Reflection\Reflection1.xml");

            // Load that (resolved) configuration into the User Object
            var props = yacf.LoadUserConfiguration(tmc1);

            Assert.IsTrue(props.Count > 0, "Props collection is empty");
            Assert.AreEqual("config_key1value", tmc1.PropPublicString1);
            Assert.AreEqual("config_key2value", tmc1.PropPublicString2);
        }
    }
}

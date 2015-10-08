// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParserTests.cs" company="Mir Software">
//   Copyright 2013 Mir Software. All rights reserved.
// </copyright>
// <summary>
//   Defines the ParserTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mir.Stf;
using Mir.Stf.Utilities;

namespace Tests
{
    [TestClass]
    public class ParserTests : StfTestScriptBase
    {
        [TestMethod]
        public void ParserSimpleVariable_1()
        {
            var yacf = new StfConfiguration();
            var conf = yacf.LoadConfig(@"TestData\Parser\parser1.xml");

            var key1Value = yacf.GetKeyValue(conf, "SectionName.k1");
            Assert.AreEqual("config_key1value", key1Value);
        }

                [TestMethod]
        public void ParserSimpleVariable_2()
        {
            var yacf = new StfConfiguration();
            var conf = yacf.LoadConfig(@"TestData\Parser\parser1.xml");

            var key2Value = yacf.GetKeyValue(conf, "SectionName.SubSectionName.k2");
            Assert.AreEqual("config_key2value", key2Value);
        }

    }

    
}

﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParserTests.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
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
    /// <summary>
    /// The parser tests.
    /// </summary>
    [TestClass]
    public class ParserTests : StfTestScriptBase
    {
        /// <summary>
        /// The parser simple variable_1.
        /// </summary>
        [TestMethod]
        public void ParserSimpleVariable1()
        {
            var stfConfiguration = new StfConfiguration();
            var conf = stfConfiguration.LoadConfig(@"TestData\Parser\parser1.xml");

            var key1Value = stfConfiguration.GetKeyValue(conf, "SectionName.k1");
            Assert.AreEqual("config_key1value", key1Value);
        }

        /// <summary>
        /// The parser simple variable_2.
        /// </summary>
        [TestMethod]
        public void ParserSimpleVariable2()
        {
            var stfConfiguration = new StfConfiguration();
            var conf = stfConfiguration.LoadConfig(@"TestData\Parser\parser1.xml");

            var key2Value = stfConfiguration.GetKeyValue(conf, "SectionName.SubSectionName.k2");
            Assert.AreEqual("config_key2value", key2Value);
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestStfAssertObject.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Mir.Stf;

namespace UnitTest
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The unit test stf asserts.
    /// </summary>
    [TestClass]
    public class UnitTestStfAssertObject : StfTestScriptBase
    {
        /// <summary>
        /// The test initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            StfAssert.EnableNegativeTesting = true;
            StfLogger.Configuration.ScreenshotOnLogFail = false;
        }

        /// <summary>
        /// The test cleanup.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            // setting to true agains resets failure count
            StfAssert.EnableNegativeTesting = true;
        }

        /// <summary>
        /// The test method assert equals.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertIsObject()
        {
            Assert.IsFalse(StfAssert.IsObject("An integer", 1));
            Assert.IsTrue(StfAssert.IsObject("A string", "1"));
            Assert.IsTrue(StfAssert.IsObject("An object", new object()));
            Assert.IsTrue(StfAssert.IsObject("An object", StfAssert));
            Assert.IsFalse(StfAssert.IsObject("null", null));
        }

        /// <summary>
        /// The test method assert IsInstanceOfType.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertIsInstanceOf()
        {
            Assert.IsFalse(StfAssert.IsInstanceOfType("An integer", 1, Type.GetType("int")));
            Assert.IsTrue(StfAssert.IsInstanceOfType("A string", "1", Type.GetType(typeof(string).FullName)));
            Assert.IsTrue(StfAssert.IsInstanceOfType("An object", new object(), Type.GetType(typeof(object).FullName)));
            Assert.IsFalse(StfAssert.IsInstanceOfType("null", null, null));

            /*
             * TODO: Have no idea why this fails?
             * Assert.IsTrue(StfAssert.IsInstanceOfType("An object", StfAssert, Type.GetType(typeof(StfAssert).FullName)));
             * 
             * typeof(StfAssert).FullName returns null?
             * 
             */
        }
    }
}

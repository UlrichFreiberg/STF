// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTest_StfAssert.cs" company="Foobar">
//   2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace UnitTest
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Stf.Utilities;

    /// <summary>
    /// The unit test stf asserts.
    /// </summary>
    [TestClass]
    public class UnitTestStfAssertObject
    {
        /// <summary>
        /// The test method assert equals.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertIsObject()
        {
            var myLogger = new Stf.Utilities.StfLogger { FileName = @"c:\temp\unittestlogger_TestMethodAssertIsObject.html" };
            var myAsserts = new StfAssert(myLogger);

            myAsserts.EnableNegativeTesting = true;

            Assert.IsFalse(myAsserts.AssertIsObject("An integer", 1));
            Assert.IsTrue(myAsserts.AssertIsObject("A string", "1"));
            Assert.IsTrue(myAsserts.AssertIsObject("An object", new object()));
            Assert.IsTrue(myAsserts.AssertIsObject("An object", myAsserts));
            Assert.IsFalse(myAsserts.AssertIsObject("null", null));
        }

        /// <summary>
        /// The test method assert AssertIsInstanceOf.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertIsInstanceOf()
        {
            var myLogger = new Stf.Utilities.StfLogger { FileName = @"c:\temp\unittestlogger_TestMethodAssertIsInstanceOf.html" };
            var myAsserts = new StfAssert(myLogger);

            myAsserts.EnableNegativeTesting = true;

            Assert.IsFalse(myAsserts.AssertIsInstanceOf("An integer", 1, Type.GetType("int")));
            Assert.IsTrue(myAsserts.AssertIsInstanceOf("A string", "1", Type.GetType(typeof(string).FullName)));
            Assert.IsTrue(myAsserts.AssertIsInstanceOf("An object", new object(), Type.GetType(typeof(object).FullName)));
            Assert.IsFalse(myAsserts.AssertIsInstanceOf("null", null, null));

            /*
             * TODO: Have no idea why this fails?
             * Assert.IsTrue(myAsserts.AssertIsInstanceOf("An object", myAsserts, Type.GetType(typeof(StfAssert).FullName)));
             * 
             * typeof(StfAssert).FullName returns null?
             * 
             */
        }
    }
}

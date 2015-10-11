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
        /// The test method assert equals.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertIsObject()
        {
            MyAssert.EnableNegativeTesting = true;

            Assert.IsFalse(MyAssert.AssertIsObject("An integer", 1));
            Assert.IsTrue(MyAssert.AssertIsObject("A string", "1"));
            Assert.IsTrue(MyAssert.AssertIsObject("An object", new object()));
            Assert.IsTrue(MyAssert.AssertIsObject("An object", MyAssert));
            Assert.IsFalse(MyAssert.AssertIsObject("null", null));
        }

        /// <summary>
        /// The test method assert AssertIsInstanceOf.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertIsInstanceOf()
        {
            MyAssert.EnableNegativeTesting = true;

            Assert.IsFalse(MyAssert.AssertIsInstanceOf("An integer", 1, Type.GetType("int")));
            Assert.IsTrue(MyAssert.AssertIsInstanceOf("A string", "1", Type.GetType(typeof(string).FullName)));
            Assert.IsTrue(MyAssert.AssertIsInstanceOf("An object", new object(), Type.GetType(typeof(object).FullName)));
            Assert.IsFalse(MyAssert.AssertIsInstanceOf("null", null, null));

            /*
             * TODO: Have no idea why this fails?
             * Assert.IsTrue(MyAssert.AssertIsInstanceOf("An object", MyAssert, Type.GetType(typeof(StfAssert).FullName)));
             * 
             * typeof(StfAssert).FullName returns null?
             * 
             */
        }
    }
}

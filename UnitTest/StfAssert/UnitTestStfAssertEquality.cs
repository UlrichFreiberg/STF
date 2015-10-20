// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestStfAssertEquality.cs" company="Mir Software">
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
    public class UnitTestStfAssertEquality : StfTestScriptBase
    {
        /// <summary>
        /// The test method assert equals.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertEquals()
        {
            var obj1 = new DateTime(42);
            var obj2 = new DateTime(4242);

            MyAssert.EnableNegativeTesting = true;
            MyLogger.Configuration.ScreenshotOnLogFail = false;

            Assert.IsTrue(MyAssert.AreEqual("1 = 1", 1, 1));
            Assert.IsFalse(MyAssert.AreEqual("1 = 1.0", 1, 1.0));
            Assert.IsFalse(MyAssert.AreEqual("1 = \"1\"", 1, "1"));
            Assert.IsFalse(MyAssert.AreEqual("1 = \"1.0\"", 1, "1.0"));

            Assert.IsTrue(MyAssert.AreEqual("\"\" == \"\"", string.Empty, string.Empty));
            Assert.IsFalse(MyAssert.AreEqual("\"\" == \" \"", string.Empty, " "));
            Assert.IsFalse(MyAssert.AreEqual("\" \" == \" \"", " ", string.Empty));
            Assert.IsFalse(MyAssert.AreEqual("\"A\" == \"a\"", "A", "a"));
            Assert.IsTrue(MyAssert.AreEqual("\"string\" == \"string\"", "string", "string"));

            Assert.IsTrue(MyAssert.AreEqual("obj1 = obj1", obj1, obj1));
            Assert.IsFalse(MyAssert.AreEqual("obj1 = obj2", obj1, obj2));

            // fail scenarios
            Assert.IsFalse(MyAssert.AreEqual("obj1 = 1", obj1, 1));
            Assert.IsFalse(MyAssert.AreEqual("obj1 = \"string\"", obj1, "string"));
        }

        /// <summary>
        /// The test method assert greaterThan.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertGreaterThan()
        {
            var obj1 = new DateTime(42);
            var obj2 = new DateTime(4242);

            MyAssert.EnableNegativeTesting = true;

            Assert.IsTrue(MyAssert.GreaterThan("2 > 1", 2, 1));
            Assert.IsTrue(MyAssert.GreaterThan("2.0 > 1", 2.0, 1));
            Assert.IsFalse(MyAssert.GreaterThan("1 > \"2\"", 1, "2"));
            Assert.IsTrue(MyAssert.GreaterThan("1 > \"1.0\"", 1, "1.0"));

            Assert.IsFalse(MyAssert.GreaterThan("\"\" > \"\"", string.Empty, string.Empty));
            Assert.IsFalse(MyAssert.GreaterThan("\"\" > \" \"", string.Empty, " "));
            Assert.IsTrue(MyAssert.GreaterThan("\" \" > \" \"", " ", string.Empty));
            Assert.IsFalse(MyAssert.GreaterThan("\"a\" > \"A\"", "a", "A"));
            Assert.IsTrue(MyAssert.GreaterThan("\"A\" > \"a\"", "A", "a"));
            Assert.IsFalse(MyAssert.GreaterThan("\"string\" > \"string\"", "string", "string"));

            Assert.IsFalse(MyAssert.GreaterThan("obj1 = obj1", obj1, obj1));
            Assert.IsFalse(MyAssert.GreaterThan("obj1 = obj2", obj1, obj2));
            Assert.IsTrue(MyAssert.GreaterThan("obj2 = obj1", obj2, obj1));

            // fail scenarios
            Assert.IsFalse(MyAssert.GreaterThan("obj1 = 1", obj1, 1));

            // a bit funky - the object obj1 is converted to string, and then the strings are compared.
            Assert.IsTrue(MyAssert.GreaterThan("obj1 = \"string\"", obj1, "string"));
        }

        /// <summary>
        /// The test method assert LessThan.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertLessThan()
        {
            var obj1 = new DateTime(42);
            var obj2 = new DateTime(4242);

            MyAssert.EnableNegativeTesting = true;

            Assert.IsFalse(MyAssert.LessThan("2 < 1", 2, 1));
            Assert.IsFalse(MyAssert.LessThan("2.0 < 1", 2.0, 1));
            Assert.IsTrue(MyAssert.LessThan("1 < \"2\"", 1, "2"));
            Assert.IsFalse(MyAssert.LessThan("1 < \"1.0\"", 1, "1.0"));

            Assert.IsFalse(MyAssert.LessThan("\"\" < \"\"", string.Empty, string.Empty));
            Assert.IsTrue(MyAssert.LessThan("\"\" < \" \"", string.Empty, " "));
            Assert.IsFalse(MyAssert.LessThan("\" \" < \" \"", " ", string.Empty));
            Assert.IsTrue(MyAssert.LessThan("\"a\" < \"A\"", "a", "A"));
            Assert.IsFalse(MyAssert.LessThan("\"A\" < \"a\"", "A", "a"));
            Assert.IsFalse(MyAssert.LessThan("\"string\" < \"string\"", "string", "string"));

            Assert.IsFalse(MyAssert.LessThan("obj1 < obj1", obj1, obj1));
            Assert.IsTrue(MyAssert.LessThan("obj1 < obj2", obj1, obj2));
            Assert.IsFalse(MyAssert.LessThan("obj2 < obj1", obj2, obj1));

            // fail scenarios
            Assert.IsFalse(MyAssert.LessThan("obj1 = 1", obj1, 1));

            // a bit funky - the object obj1 is converted to string, and then the strings are compared.
            Assert.IsFalse(MyAssert.LessThan("obj1 = \"string\"", obj1, "string"));
        }
    }
}

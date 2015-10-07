// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTest_StfAssert.cs" company="Foobar">
//   2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Mir.Stf.Utilities;

namespace UnitTest
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Stf.Utilities;

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

            Assert.IsTrue(MyAssert.AssertEquals("1 = 1", 1, 1));
            Assert.IsFalse(MyAssert.AssertEquals("1 = 1.0", 1, 1.0));
            Assert.IsFalse(MyAssert.AssertEquals("1 = \"1\"", 1, "1"));
            Assert.IsFalse(MyAssert.AssertEquals("1 = \"1.0\"", 1, "1.0"));

            Assert.IsTrue(MyAssert.AssertEquals("\"\" == \"\"", string.Empty, string.Empty));
            Assert.IsFalse(MyAssert.AssertEquals("\"\" == \" \"", string.Empty, " "));
            Assert.IsFalse(MyAssert.AssertEquals("\" \" == \" \"", " ", string.Empty));
            Assert.IsFalse(MyAssert.AssertEquals("\"A\" == \"a\"", "A", "a"));
            Assert.IsTrue(MyAssert.AssertEquals("\"string\" == \"string\"", "string", "string"));

            Assert.IsTrue(MyAssert.AssertEquals("obj1 = obj1", obj1, obj1));
            Assert.IsFalse(MyAssert.AssertEquals("obj1 = obj2", obj1, obj2));

            // fail scenarios
            Assert.IsFalse(MyAssert.AssertEquals("obj1 = 1", obj1, 1));
            Assert.IsFalse(MyAssert.AssertEquals("obj1 = \"string\"", obj1, "string"));
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

            Assert.IsTrue(MyAssert.AssertGreaterThan("2 > 1", 2, 1));
            Assert.IsTrue(MyAssert.AssertGreaterThan("2.0 > 1", 2.0, 1));
            Assert.IsFalse(MyAssert.AssertGreaterThan("1 > \"2\"", 1, "2"));
            Assert.IsTrue(MyAssert.AssertGreaterThan("1 > \"1.0\"", 1, "1.0"));

            Assert.IsFalse(MyAssert.AssertGreaterThan("\"\" > \"\"", string.Empty, string.Empty));
            Assert.IsFalse(MyAssert.AssertGreaterThan("\"\" > \" \"", string.Empty, " "));
            Assert.IsTrue(MyAssert.AssertGreaterThan("\" \" > \" \"", " ", string.Empty));
            Assert.IsFalse(MyAssert.AssertGreaterThan("\"a\" > \"A\"", "a", "A"));
            Assert.IsTrue(MyAssert.AssertGreaterThan("\"A\" > \"a\"", "A", "a"));
            Assert.IsFalse(MyAssert.AssertGreaterThan("\"string\" > \"string\"", "string", "string"));

            Assert.IsFalse(MyAssert.AssertGreaterThan("obj1 = obj1", obj1, obj1));
            Assert.IsFalse(MyAssert.AssertGreaterThan("obj1 = obj2", obj1, obj2));
            Assert.IsTrue(MyAssert.AssertGreaterThan("obj2 = obj1", obj2, obj1));

            // fail scenarios
            Assert.IsFalse(MyAssert.AssertGreaterThan("obj1 = 1", obj1, 1));

            // a bit funky - the object obj1 is converted to string, and then the strings are compared.
            Assert.IsTrue(MyAssert.AssertGreaterThan("obj1 = \"string\"", obj1, "string"));
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

            Assert.IsFalse(MyAssert.AssertLessThan("2 < 1", 2, 1));
            Assert.IsFalse(MyAssert.AssertLessThan("2.0 < 1", 2.0, 1));
            Assert.IsTrue(MyAssert.AssertLessThan("1 < \"2\"", 1, "2"));
            Assert.IsFalse(MyAssert.AssertLessThan("1 < \"1.0\"", 1, "1.0"));

            Assert.IsFalse(MyAssert.AssertLessThan("\"\" < \"\"", string.Empty, string.Empty));
            Assert.IsTrue(MyAssert.AssertLessThan("\"\" < \" \"", string.Empty, " "));
            Assert.IsFalse(MyAssert.AssertLessThan("\" \" < \" \"", " ", string.Empty));
            Assert.IsTrue(MyAssert.AssertLessThan("\"a\" < \"A\"", "a", "A"));
            Assert.IsFalse(MyAssert.AssertLessThan("\"A\" < \"a\"", "A", "a"));
            Assert.IsFalse(MyAssert.AssertLessThan("\"string\" < \"string\"", "string", "string"));

            Assert.IsFalse(MyAssert.AssertLessThan("obj1 < obj1", obj1, obj1));
            Assert.IsTrue(MyAssert.AssertLessThan("obj1 < obj2", obj1, obj2));
            Assert.IsFalse(MyAssert.AssertLessThan("obj2 < obj1", obj2, obj1));

            // fail scenarios
            Assert.IsFalse(MyAssert.AssertLessThan("obj1 = 1", obj1, 1));

            // a bit funky - the object obj1 is converted to string, and then the strings are compared.
            Assert.IsFalse(MyAssert.AssertLessThan("obj1 = \"string\"", obj1, "string"));
        }
    }
}

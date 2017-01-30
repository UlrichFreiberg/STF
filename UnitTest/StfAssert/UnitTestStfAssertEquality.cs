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
            StfAssert.ResetStatistics();
        }

        /// <summary>
        /// The test method assert equals.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertEquals()
        {
            var obj1 = new DateTime(42);
            var obj2 = new DateTime(4242);

            Assert.IsTrue(StfAssert.AreEqual("1 = 1", 1, 1));
            Assert.IsFalse(StfAssert.AreEqual("1 = 1.0", 1, 1.0));
            Assert.IsFalse(StfAssert.AreEqual("1 = \"1\"", 1, "1"));
            Assert.IsFalse(StfAssert.AreEqual("1 = \"1.0\"", 1, "1.0"));

            Assert.IsTrue(StfAssert.AreEqual("\"\" == \"\"", string.Empty, string.Empty));
            Assert.IsFalse(StfAssert.AreEqual("\"\" == \" \"", string.Empty, " "));
            Assert.IsFalse(StfAssert.AreEqual("\" \" == \" \"", " ", string.Empty));
            Assert.IsFalse(StfAssert.AreEqual("\"A\" == \"a\"", "A", "a"));
            Assert.IsTrue(StfAssert.AreEqual("\"string\" == \"string\"", "string", "string"));

            Assert.IsTrue(StfAssert.AreEqual("obj1 = obj1", obj1, obj1));
            Assert.IsFalse(StfAssert.AreEqual("obj1 = obj2", obj1, obj2));

            // fail scenarios
            Assert.IsFalse(StfAssert.AreEqual("obj1 = 1", obj1, 1));
            Assert.IsFalse(StfAssert.AreEqual("obj1 = \"string\"", obj1, "string"));
        }

        /// <summary>
        /// The test method assert greaterThan.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertGreaterThan()
        {
            var obj1 = new DateTime(42);
            var obj2 = new DateTime(4242);

            Assert.IsTrue(StfAssert.GreaterThan("2 > 1", 2, 1));
            Assert.IsTrue(StfAssert.GreaterThan("2.0 > 1", 2.0, 1));
            Assert.IsFalse(StfAssert.GreaterThan("1 > \"2\"", 1, "2"));
            Assert.IsTrue(StfAssert.GreaterThan("1 > \"1.0\"", 1, "1.0"));

            Assert.IsFalse(StfAssert.GreaterThan("\"\" > \"\"", string.Empty, string.Empty));
            Assert.IsFalse(StfAssert.GreaterThan("\"\" > \" \"", string.Empty, " "));
            Assert.IsTrue(StfAssert.GreaterThan("\" \" > \" \"", " ", string.Empty));
            Assert.IsFalse(StfAssert.GreaterThan("\"a\" > \"A\"", "a", "A"));
            Assert.IsTrue(StfAssert.GreaterThan("\"A\" > \"a\"", "A", "a"));
            Assert.IsFalse(StfAssert.GreaterThan("\"string\" > \"string\"", "string", "string"));

            Assert.IsFalse(StfAssert.GreaterThan("obj1 = obj1", obj1, obj1));
            Assert.IsFalse(StfAssert.GreaterThan("obj1 = obj2", obj1, obj2));
            Assert.IsTrue(StfAssert.GreaterThan("obj2 = obj1", obj2, obj1));

            // fail scenarios
            Assert.IsFalse(StfAssert.GreaterThan("obj1 = 1", obj1, 1));

            // a bit funky - the object obj1 is converted to string, and then the strings are compared.
            Assert.IsTrue(StfAssert.GreaterThan("obj1 = \"string\"", obj1, "string"));
        }

        /// <summary>
        /// The test method assert equalgreaterThan.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertEqualGreaterThan()
        {
            var obj1 = new DateTime(42);
            var obj2 = new DateTime(4242);
            var obj3 = new DateTime(42);

            Assert.IsTrue(StfAssert.GreaterThanOrEqual("2 > 1", 2, 1));
            Assert.IsTrue(StfAssert.GreaterThanOrEqual("2.0 > 1", 2.0, 1));
            Assert.IsFalse(StfAssert.GreaterThanOrEqual("1 > \"2\"", 1, "2"));
            Assert.IsTrue(StfAssert.GreaterThanOrEqual("1 > \"1.0\"", 1, "1.0"));

            Assert.IsTrue(StfAssert.GreaterThanOrEqual("\"\" > \"\"", string.Empty, string.Empty));
            Assert.IsFalse(StfAssert.GreaterThanOrEqual("\"\" > \" \"", string.Empty, " "));
            Assert.IsTrue(StfAssert.GreaterThanOrEqual("\" \" > \" \"", " ", string.Empty));
            Assert.IsFalse(StfAssert.GreaterThanOrEqual("\"a\" > \"A\"", "a", "A"));
            Assert.IsTrue(StfAssert.GreaterThanOrEqual("\"A\" > \"a\"", "A", "a"));
            Assert.IsTrue(StfAssert.GreaterThanOrEqual("\"string\" > \"string\"", "string", "string"));

            Assert.IsTrue(StfAssert.GreaterThanOrEqual("obj1 = obj1", obj1, obj1));
            Assert.IsTrue(StfAssert.GreaterThanOrEqual("obj1 = obj3", obj1, obj3));
            Assert.IsFalse(StfAssert.GreaterThanOrEqual("obj1 = obj2", obj1, obj2));
            Assert.IsTrue(StfAssert.GreaterThanOrEqual("obj2 = obj1", obj2, obj1));

            // fail scenarios
            Assert.IsFalse(StfAssert.GreaterThanOrEqual("obj1 = 1", obj1, 1));

            // a bit funky - the object obj1 is converted to string, and then the strings are compared.
            Assert.IsTrue(StfAssert.GreaterThanOrEqual("obj1 = \"string\"", obj1, "string"));
        }

        /// <summary>
        /// The test method assert LessThan.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertLessThan()
        {
            var obj1 = new DateTime(42);
            var obj2 = new DateTime(4242);

            Assert.IsFalse(StfAssert.LessThan("2 < 1", 2, 1));
            Assert.IsFalse(StfAssert.LessThan("2.0 < 1", 2.0, 1));
            Assert.IsTrue(StfAssert.LessThan("1 < \"2\"", 1, "2"));
            Assert.IsFalse(StfAssert.LessThan("1 < \"1.0\"", 1, "1.0"));

            Assert.IsFalse(StfAssert.LessThan("\"\" < \"\"", string.Empty, string.Empty));
            Assert.IsTrue(StfAssert.LessThan("\"\" < \" \"", string.Empty, " "));
            Assert.IsFalse(StfAssert.LessThan("\" \" < \" \"", " ", string.Empty));
            Assert.IsTrue(StfAssert.LessThan("\"a\" < \"A\"", "a", "A"));
            Assert.IsFalse(StfAssert.LessThan("\"A\" < \"a\"", "A", "a"));
            Assert.IsFalse(StfAssert.LessThan("\"string\" < \"string\"", "string", "string"));

            Assert.IsFalse(StfAssert.LessThan("obj1 < obj1", obj1, obj1));
            Assert.IsTrue(StfAssert.LessThan("obj1 < obj2", obj1, obj2));
            Assert.IsFalse(StfAssert.LessThan("obj2 < obj1", obj2, obj1));

            // fail scenarios
            Assert.IsFalse(StfAssert.LessThan("obj1 = 1", obj1, 1));

            // a bit funky - the object obj1 is converted to string, and then the strings are compared.
            Assert.IsFalse(StfAssert.LessThan("obj1 = \"string\"", obj1, "string"));
        }

        /// <summary>
        /// The test method assert LessThanOrEqual.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertEqualLessThan()
        {
            var obj1 = new DateTime(42);
            var obj2 = new DateTime(4242);

            Assert.IsFalse(StfAssert.LessThanOrEqual("2 < 1", 2, 1));
            Assert.IsFalse(StfAssert.LessThanOrEqual("2.0 < 1", 2.0, 1));
            Assert.IsTrue(StfAssert.LessThanOrEqual("1 < \"2\"", 1, "2"));
            Assert.IsFalse(StfAssert.LessThanOrEqual("1 < \"1.0\"", 1, "1.0"));
            Assert.IsTrue(StfAssert.LessThanOrEqual("1 < 1.0", 1, 1.0));
            Assert.IsTrue(StfAssert.LessThanOrEqual("1.0 < 1", 1.0, 1));

            Assert.IsTrue(StfAssert.LessThanOrEqual("\"\" < \"\"", string.Empty, string.Empty));
            Assert.IsTrue(StfAssert.LessThanOrEqual("\"\" < \" \"", string.Empty, " "));
            Assert.IsFalse(StfAssert.LessThanOrEqual("\" \" < \" \"", " ", string.Empty));
            Assert.IsTrue(StfAssert.LessThanOrEqual("\"a\" < \"A\"", "a", "A"));
            Assert.IsFalse(StfAssert.LessThanOrEqual("\"A\" < \"a\"", "A", "a"));
            Assert.IsTrue(StfAssert.LessThanOrEqual("\"string\" < \"string\"", "string", "string"));

            Assert.IsTrue(StfAssert.LessThanOrEqual("obj1 < obj1", obj1, obj1));
            Assert.IsTrue(StfAssert.LessThanOrEqual("obj1 < obj2", obj1, obj2));
            Assert.IsFalse(StfAssert.LessThanOrEqual("obj2 < obj1", obj2, obj1));

            // fail scenarios
            Assert.IsFalse(StfAssert.LessThanOrEqual("obj1 = 1", obj1, 1));

            // a bit funky - the object obj1 is converted to string, and then the strings are compared.
            Assert.IsFalse(StfAssert.LessThanOrEqual("obj1 = \"string\"", obj1, "string"));
        }
    }
}

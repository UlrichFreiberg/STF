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
    public class UnitTestStfAssert_Equality
    {
        /// <summary>
        /// The test method assert equals.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertEquals()
        {
            var myLogger = new Stf.Utilities.StfLogger { FileName = @"c:\temp\unittestlogger_TestMethodAssertEquals.html" };
            var myAsserts = new StfAssert(myLogger);
            var obj1 = new DateTime(42);
            var obj2 = new DateTime(4242);

            myAsserts.EnableNegativeTesting = true;

            Assert.IsTrue(myAsserts.AssertEquals("1 = 1", 1, 1));
            Assert.IsFalse(myAsserts.AssertEquals("1 = 1.0", 1, 1.0));
            Assert.IsFalse(myAsserts.AssertEquals("1 = \"1\"", 1, "1"));
            Assert.IsFalse(myAsserts.AssertEquals("1 = \"1.0\"", 1, "1.0"));

            Assert.IsTrue(myAsserts.AssertEquals("\"\" == \"\"", string.Empty, string.Empty));
            Assert.IsFalse(myAsserts.AssertEquals("\"\" == \" \"", string.Empty, " "));
            Assert.IsFalse(myAsserts.AssertEquals("\" \" == \" \"", " ", string.Empty));
            Assert.IsFalse(myAsserts.AssertEquals("\"A\" == \"a\"", "A", "a"));
            Assert.IsTrue(myAsserts.AssertEquals("\"string\" == \"string\"", "string", "string"));

            Assert.IsTrue(myAsserts.AssertEquals("obj1 = obj1", obj1, obj1));
            Assert.IsFalse(myAsserts.AssertEquals("obj1 = obj2", obj1, obj2));

            // fail scenarios
            Assert.IsFalse(myAsserts.AssertEquals("obj1 = 1", obj1, 1));
            Assert.IsFalse(myAsserts.AssertEquals("obj1 = \"string\"", obj1, "string"));
        }

        /// <summary>
        /// The test method assert greaterThan.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertGreaterThan()
        {
            var myLogger = new Stf.Utilities.StfLogger { FileName = @"c:\temp\unittestlogger_TestMethodAssertGreaterThan.html" };
            var myAsserts = new StfAssert(myLogger);
            var obj1 = new DateTime(42);
            var obj2 = new DateTime(4242);

            myAsserts.EnableNegativeTesting = true;

            Assert.IsTrue(myAsserts.AssertGreaterThan("2 > 1", 2, 1));
            Assert.IsTrue(myAsserts.AssertGreaterThan("2.0 > 1", 2.0, 1));
            Assert.IsFalse(myAsserts.AssertGreaterThan("1 > \"2\"", 1, "2"));
            Assert.IsTrue(myAsserts.AssertGreaterThan("1 > \"1.0\"", 1, "1.0"));

            Assert.IsFalse(myAsserts.AssertGreaterThan("\"\" > \"\"", string.Empty, string.Empty));
            Assert.IsFalse(myAsserts.AssertGreaterThan("\"\" > \" \"", string.Empty, " "));
            Assert.IsTrue(myAsserts.AssertGreaterThan("\" \" > \" \"", " ", string.Empty));
            Assert.IsFalse(myAsserts.AssertGreaterThan("\"a\" > \"A\"", "a", "A"));
            Assert.IsTrue(myAsserts.AssertGreaterThan("\"A\" > \"a\"", "A", "a"));
            Assert.IsFalse(myAsserts.AssertGreaterThan("\"string\" > \"string\"", "string", "string"));

            Assert.IsFalse(myAsserts.AssertGreaterThan("obj1 = obj1", obj1, obj1));
            Assert.IsFalse(myAsserts.AssertGreaterThan("obj1 = obj2", obj1, obj2));
            Assert.IsTrue(myAsserts.AssertGreaterThan("obj2 = obj1", obj2, obj1));

            // fail scenarios
            Assert.IsFalse(myAsserts.AssertGreaterThan("obj1 = 1", obj1, 1));

            // a bit funky - the object obj1 is converted to string, and then the strings are compared.
            Assert.IsTrue(myAsserts.AssertGreaterThan("obj1 = \"string\"", obj1, "string"));
        }

        /// <summary>
        /// The test method assert LessThan.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertLessThan()
        {
            var myLogger = new Stf.Utilities.StfLogger { FileName = @"c:\temp\unittestlogger_TestMethodAssertLessThan.html" };
            var myAsserts = new StfAssert(myLogger);
            var obj1 = new DateTime(42);
            var obj2 = new DateTime(4242);

            myAsserts.EnableNegativeTesting = true;

            Assert.IsFalse(myAsserts.AssertLessThan("2 < 1", 2, 1));
            Assert.IsFalse(myAsserts.AssertLessThan("2.0 < 1", 2.0, 1));
            Assert.IsTrue(myAsserts.AssertLessThan("1 < \"2\"", 1, "2"));
            Assert.IsFalse(myAsserts.AssertLessThan("1 < \"1.0\"", 1, "1.0"));

            Assert.IsFalse(myAsserts.AssertLessThan("\"\" < \"\"", string.Empty, string.Empty));
            Assert.IsTrue(myAsserts.AssertLessThan("\"\" < \" \"", string.Empty, " "));
            Assert.IsFalse(myAsserts.AssertLessThan("\" \" < \" \"", " ", string.Empty));
            Assert.IsTrue(myAsserts.AssertLessThan("\"a\" < \"A\"", "a", "A"));
            Assert.IsFalse(myAsserts.AssertLessThan("\"A\" < \"a\"", "A", "a"));
            Assert.IsFalse(myAsserts.AssertLessThan("\"string\" < \"string\"", "string", "string"));

            Assert.IsFalse(myAsserts.AssertLessThan("obj1 < obj1", obj1, obj1));
            Assert.IsTrue(myAsserts.AssertLessThan("obj1 < obj2", obj1, obj2));
            Assert.IsFalse(myAsserts.AssertLessThan("obj2 < obj1", obj2, obj1));

            // fail scenarios
            Assert.IsFalse(myAsserts.AssertLessThan("obj1 = 1", obj1, 1));

            // a bit funky - the object obj1 is converted to string, and then the strings are compared.
            Assert.IsFalse(myAsserts.AssertLessThan("obj1 = \"string\"", obj1, "string"));
        }

    }
}

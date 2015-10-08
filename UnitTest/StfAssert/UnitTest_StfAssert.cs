// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTest_StfAssert.cs" company="Foobar">
//   2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Mir.Stf;

namespace UnitTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The unit test stf asserts.
    /// </summary>
    [TestClass]
    public class UnitTestStfAsserts : StfTestScriptBase
    {
        /// <summary>
        /// The test initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            MyAssert.EnableNegativeTesting = true;
            this.MyLogger.LogInfo("UnitTestStfAsserts TestInitialize");
        }

        /// <summary>
        /// The test method assert strings.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertTrue()
        {
            Assert.IsTrue(this.MyAssert.AssertTrue("true", true));
            Assert.IsFalse(this.MyAssert.AssertTrue("false", false));
        }

        /// <summary>
        /// The test method assert strings.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertFalse()
        {
            Assert.IsFalse(this.MyAssert.AssertFalse("true", true));
            Assert.IsTrue(this.MyAssert.AssertFalse("false", false));
        }

        /// <summary>
        /// The test assert throws true.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertThrows()
        {
            Assert.IsTrue(this.MyAssert.AssertThrows<NotImplementedException>("true", ThrowNotImplementedException));
            Assert.IsFalse(this.MyAssert.AssertThrows<ArgumentException>("false", ThrowNotImplementedException));
        }

        /// <summary>
        /// The test method assert throws lambda.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertThrowsLambda()
        {
            Assert.IsTrue(this.MyAssert.AssertThrows<ApplicationException>(
                "true",
                () => MethodWithParamCanThrow<ApplicationException>(true)));
            
            Assert.IsFalse(this.MyAssert.AssertThrows<ApplicationException>(
                "false",
                () => MethodWithParamCanThrow<ApplicationException>(false)));
        }

        /// <summary>
        /// The test cleanup.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            this.MyLogger.LogInfo("UnitTestStfAsserts TestCleanup");
        }

        /// <summary>
        /// The throw not implemented exception.
        /// </summary>
        private void ThrowNotImplementedException()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The method with param throws.
        /// </summary>
        /// <param name="throwIt">
        /// The throw it.
        /// </param>
        /// <typeparam name="T">
        /// Type of exception
        /// </typeparam>
        /// <exception>
        /// Exception to throw
        /// </exception>
        // ReSharper disable once UnusedParameter.Local
        private void MethodWithParamCanThrow<T>(bool throwIt) where T : Exception, new()
        {
            if (throwIt)
            {
                throw new T();
            }
        }
    }
}

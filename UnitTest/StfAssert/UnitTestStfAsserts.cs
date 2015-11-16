// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestStfAsserts.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
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
            StfAssert.EnableNegativeTesting = true;
            this.StfLogger.LogInfo("UnitTestStfAsserts TestInitialize");
        }

        /// <summary>
        /// The test method assert strings.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertTrue()
        {
            Assert.IsTrue(this.StfAssert.IsTrue("true", true));
            Assert.IsFalse(this.StfAssert.IsTrue("false", false));
        }

        /// <summary>
        /// The test method assert strings.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertFalse()
        {
            Assert.IsFalse(this.StfAssert.IsFalse("true", true));
            Assert.IsTrue(this.StfAssert.IsFalse("false", false));
        }

        /// <summary>
        /// The test method assert missing implementation.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertMissingImplementation()
        {
            Assert.IsFalse(StfAssert.MissingImplementation(string.Empty));
            Assert.IsFalse(StfAssert.MissingImplementation(null));
            Assert.IsFalse(StfAssert.MissingImplementation("There's a missing implementation"));
        }

        /// <summary>
        /// The test assert throws true.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertThrows()
        {
            Assert.IsTrue(this.StfAssert.AssertThrows<NotImplementedException>("true", ThrowNotImplementedException));
            Assert.IsFalse(this.StfAssert.AssertThrows<ArgumentException>("false", ThrowNotImplementedException));
        }

        /// <summary>
        /// The test method assert throws lambda.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertThrowsLambda()
        {
            Assert.IsTrue(this.StfAssert.AssertThrows<ApplicationException>(
                "true",
                () => MethodWithParamCanThrow<ApplicationException>(true)));
            
            Assert.IsFalse(this.StfAssert.AssertThrows<ApplicationException>(
                "false",
                () => MethodWithParamCanThrow<ApplicationException>(false)));
        }

        /// <summary>
        /// The test cleanup.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            this.StfLogger.LogInfo("UnitTestStfAsserts TestCleanup");
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

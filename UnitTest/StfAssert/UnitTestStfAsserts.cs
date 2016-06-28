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
            StfLogger.LogInfo("UnitTestStfAsserts TestInitialize");
        }

        /// <summary>
        /// The test method assert strings.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertTrue()
        {
            Assert.IsTrue(StfAssert.IsTrue("true", true));
            Assert.IsFalse(StfAssert.IsTrue("false", false));
        }

        /// <summary>
        /// The test method assert strings.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertFalse()
        {
            Assert.IsFalse(StfAssert.IsFalse("true", true));
            Assert.IsTrue(StfAssert.IsFalse("false", false));
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
            Assert.IsTrue(StfAssert.AssertThrows<NotImplementedException>("true", ThrowNotImplementedException));
            Assert.IsFalse(StfAssert.AssertThrows<ArgumentException>("false", ThrowNotImplementedException));
        }

        /// <summary>
        /// The test method assert inconclusive.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertInconclusive()
        {
            Assert.IsTrue(StfAssert.IsInconclusive("Check is inconclusive", "Inconclusive"));
            StfAssert.AreEqual("Current inconclusives", 1, StfAssert.CurrentInconclusives);
            StfAssert.AreEqual("Current failures", 0, StfAssert.CurrentFailures);
            StfAssert.AreEqual("Current passes", 2, StfAssert.CurrentPasses);
        }

        /// <summary>
        /// The test method assert throws lambda.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertThrowsLambda()
        {
            Assert.IsTrue(StfAssert.AssertThrows<ApplicationException>(
                "true",
                () => MethodWithParamCanThrow<ApplicationException>(true)));
            
            Assert.IsFalse(StfAssert.AssertThrows<ApplicationException>(
                "false",
                () => MethodWithParamCanThrow<ApplicationException>(false)));
        }

        /// <summary>
        /// The test cleanup.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            // Setting to true again resets the failure count
            StfAssert.EnableNegativeTesting = true;
            StfLogger.LogInfo("UnitTestStfAsserts TestCleanup");
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

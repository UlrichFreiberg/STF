// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestsRetryer.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the UnitTestsRetryer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.RetryerUtils
{
    using Mir.Stf.Utilities.RetryerUtilities;

    /// <summary>
    /// The unit tests retryer.
    /// </summary>
    [TestClass]
    public class UnitTestsRetryer
    {
        /// <summary>
        /// The simple success retryer test.
        /// </summary>
        [TestMethod]
        public void SimpleSuccessRetryerActionTest()
        {
            var retryerUtils = new RetryerUtils(5, TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(1));
            var success = retryerUtils.Retry(() => SimpleTestAction("Test"));

            Assert.IsTrue(success);
        }

        /// <summary>
        /// The simple success retryer function test.
        /// </summary>
        [TestMethod]
        public void SimpleSuccessRetryerFunctionTest()
        {
            var retryerUtils = new RetryerUtils(5, TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(1));
            var success = retryerUtils.Retry(() => SimpleTestFunction("wait", 3));

            Assert.IsTrue(success);
        }

        /// <summary>
        /// The simple negative retryer test.
        /// </summary>
        [TestMethod]
        public void SimpleNegativeRetryerTest()
        {
            var retryerUtils = new RetryerUtils(5, TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(1));
            var success = retryerUtils.Retry(() => SimpleTestAction("NegativeTest"));

            Assert.IsFalse(success);
        }

        /// <summary>
        /// The simple test action.
        /// </summary>
        /// <param name="testParam">
        /// The test param.
        /// </param>
        /// <exception cref="Exception">
        /// Just an empty exception.
        /// </exception>
        private void SimpleTestAction(string testParam)
        {
            if (testParam == null)
            {
                throw new ArgumentNullException(nameof(testParam));
            }

            if (testParam == "NegativeTest")
            {
                throw new Exception();
            }
        }

        private bool SimpleTestFunction(string testParam, int seconds)
        {
            switch (testParam)
            {
                case "wait":
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(seconds));
                    break;
                case "true":
                    return true;
                case "false":
                    return false;
                case "throw":
                    throw new Exception();
            }

            return true;
        }
    }
}

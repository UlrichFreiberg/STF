using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.RetryerUtils
{
    [TestClass]
    public class UnitTestsRetryer
    {
        [TestMethod]
        public void SimpleSuccessRetryerTest()
        {
            var retryerUtils = new Mir.Stf.Utilities.RetryerUtilities.RetryerUtils(5, TimeSpan.FromSeconds(3),
                TimeSpan.FromSeconds(1));

            var success = retryerUtils.Retry(() => SimpleTestAction("Test"));

            Assert.IsTrue(success);
        }

        [TestMethod]
        public void SimpleNegativeRetryerTest()
        {
            var retryerUtils = new Mir.Stf.Utilities.RetryerUtilities.RetryerUtils(5, TimeSpan.FromSeconds(3),
                TimeSpan.FromSeconds(1));

            var success = retryerUtils.Retry(() => SimpleTestAction("NegativeTest"));

            Assert.IsFalse(success);
        }

        private void SimpleTestAction(string testParam)
        {
            if (testParam == "NegativeTest")
            {
                throw new Exception();
            }
        }
    }
}

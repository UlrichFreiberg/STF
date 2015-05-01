// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DatadrivenStfLoggerTest.cs" company="Foobar">
//   2015
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stf.Utilities;

namespace UnitTest
{
    [TestClass]
    public class DatadrivenStfLoggerTest : StfTestScriptBase
    {
        /// <summary>
        /// Backing field
        /// </summary>
        private static IList<string> _logMessages;

        /// <summary>
        /// Log messages recorded during DD test
        /// </summary>
        private IList<string> LogMessages
        {
            get { return _logMessages ?? (_logMessages = new List<string>()); }
        }

        /// <summary>
        /// The test initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            this.MyLogger.LogInfo("DatadrivenStfLoggerTest TestInitialize");
            MyAssert.EnableNegativeTesting = true;
        }

        /// <summary>
        /// Test of DatadrivenLoggerTest. Data has three lines - all pass
        /// </summary>
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\Data_SummeryLog_3lines_allpass.csv", "Data_SummeryLog_3lines_allpass#csv", DataAccessMethod.Sequential)]
        public void DatadrivenLoggerTest_3lines_allpass()
        {
            var iteration = (int)TestContext.DataRow["Iteration"];
            var message = (string)TestContext.DataRow["Message"];
            var failPass = ConvertToBool((string)TestContext.DataRow["FailPass"]);

            MyLogger.LogInfo(string.Format("Iteration [{0}]: {1}", iteration, message));
            MyAssert.AssertTrue("FailPass", failPass);
        }

        /// <summary>
        /// Test of DatadrivenLoggerTest. Data has three lines - first line fail - others pass
        /// </summary>
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\Data_SummeryLog_3lines_line1fail.csv", "Data_SummeryLog_3lines_line1fail#csv", DataAccessMethod.Sequential)]
        public void DatadrivenLoggerTest_3lines_line1fail()
        {
            var iteration = (int)TestContext.DataRow["Iteration"];
            var message = (string)TestContext.DataRow["Message"];
            var failPass = ConvertToBool((string)TestContext.DataRow["FailPass"]);

            MyLogger.LogInfo(string.Format("Iteration [{0}]: {1}", iteration, message));
            MyAssert.AssertTrue("FailPass", failPass);
        }

        /// <summary>
        /// Test of DatadrivenLoggerTest. Data has three lines - second line fail - others pass
        /// </summary>
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\Data_SummeryLog_3lines_line2fail.csv", "Data_SummeryLog_3lines_line2fail#csv", DataAccessMethod.Sequential)]
        public void DatadrivenLoggerTest_3lines_line2fail()
        {
            var iteration = (int)TestContext.DataRow["Iteration"];
            var message = (string)TestContext.DataRow["Message"];
            var failPass = ConvertToBool((string)TestContext.DataRow["FailPass"]);

            MyLogger.LogInfo(string.Format("Iteration [{0}]: {1}", iteration, message));
            MyAssert.AssertTrue("FailPass", failPass);
        }

        /// <summary>
        /// Test of DatadrivenLoggerTest. Data has three lines - third line fail - others pass
        /// </summary>
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\Data_SummeryLog_3lines_line3fail.csv", "Data_SummeryLog_3lines_line3fail#csv", DataAccessMethod.Sequential)]
        public void DatadrivenLoggerTest_3lines_line3fail()
        {
            var iteration = (int)TestContext.DataRow["Iteration"];
            var message = (string)TestContext.DataRow["Message"];
            var failPass = ConvertToBool((string)TestContext.DataRow["FailPass"]);

            MyLogger.LogInfo(string.Format("Iteration [{0}]: {1}", iteration, message));
            MyAssert.AssertTrue("FailPass", failPass);
        }

        private bool ConvertToBool(string boolValue)
        {
            if (string.Compare(boolValue.ToLower(), "true") == 0
             || string.Compare(boolValue.ToLower(), "pass") == 0
             || string.Compare(boolValue.ToLower(), "1") == 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// The test method assert strings.
        /// </summary>
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "Data\\Data.csv", "Data#csv", DataAccessMethod.Sequential)]
        public void DatadrivenLoggerTest()
        {
            var iteration = (int)TestContext.DataRow["Iteration"];
            var message = (string)TestContext.DataRow["Message"];

            LogMessages.Add(message);

            MyLogger.LogInfo(string.Format("Iteration [{0}]: {1}", iteration, message));
            ////foreach (var logMessage in LogMessages)
            ////{
            ////    MyAssert.AssertFileContains(iteration.ToString(), MyLogger.FileName, logMessage);
            ////}
        }

        /// <summary>
        /// The test cleanup.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            this.MyLogger.LogInfo("DatadrivenStfLoggerTest TestCleanup");
        }
    }
}

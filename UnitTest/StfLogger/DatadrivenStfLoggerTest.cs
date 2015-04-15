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

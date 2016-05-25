// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DatadrivenStfLoggerTest.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace UnitTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Mir.Stf;

    /// <summary>
    /// The data driven stf logger test.
    /// </summary>
    [TestClass]
    public class DatadrivenStfLoggerTest : StfTestScriptBase
    {
        /// <summary>
        /// The test initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            this.StfLogger.LogInfo("DatadrivenStfLoggerTest TestInitialize");
            StfAssert.EnableNegativeTesting = true;
        }

        /// <summary>
        /// Test of data driven Test. Data has three lines - all pass
        /// </summary>
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\Data_SummeryLog_3lines_allpass.csv", "Data_SummeryLog_3lines_allpass#csv", DataAccessMethod.Sequential)]
        public void DatadrivenLoggerTest3LinesAllpass()
        {
            var iteration = (int)TestContext.DataRow["Iteration"];
            var message = (string)TestContext.DataRow["Message"];
            var failPass = ConvertToBool((string)TestContext.DataRow["FailPass"]);

            StfLogger.LogInfo(string.Format("Iteration [{0}]: {1}", iteration, message));
            StfAssert.IsTrue("FailPass", failPass);
        }

        /// <summary>
        /// Test of data driven Test. Data has three lines - first line fail - others pass
        /// </summary>
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\Data_SummeryLog_3lines_line1fail.csv", "Data_SummeryLog_3lines_line1fail#csv", DataAccessMethod.Sequential)]
        public void DatadrivenLoggerTest3LinesLine1Fail()
        {
            var iteration = (int)TestContext.DataRow["Iteration"];
            var message = (string)TestContext.DataRow["Message"];
            var failPass = ConvertToBool((string)TestContext.DataRow["FailPass"]);

            StfLogger.LogInfo(string.Format("Iteration [{0}]: {1}", iteration, message));
            Assert.IsTrue(StfAssert.IsTrue("FailPass", failPass) == failPass);
        }

        /// <summary>
        /// Test of data driven Test. Data has three lines - second line fail - others pass
        /// </summary>
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\Data_SummeryLog_3lines_line2fail.csv", "Data_SummeryLog_3lines_line2fail#csv", DataAccessMethod.Sequential)]
        public void DatadrivenLoggerTest3LinesLine2Fail()
        {
            var iteration = (int)TestContext.DataRow["Iteration"];
            var message = (string)TestContext.DataRow["Message"];
            var failPass = ConvertToBool((string)TestContext.DataRow["FailPass"]);

            StfLogger.LogInfo(string.Format("Iteration [{0}]: {1}", iteration, message));
            Assert.IsTrue(StfAssert.IsTrue("FailPass", failPass) == failPass);
        }

        /// <summary>
        /// Test of data driven Test. Data has three lines - third line fail - others pass
        /// </summary>
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\Data_SummeryLog_3lines_line3fail.csv", "Data_SummeryLog_3lines_line3fail#csv", DataAccessMethod.Sequential)]
        public void DatadrivenLoggerTest3LinesLine3Fail()
        {
            var iteration = (int)TestContext.DataRow["Iteration"];
            var message = (string)TestContext.DataRow["Message"];
            var failPass = ConvertToBool((string)TestContext.DataRow["FailPass"]);

            StfLogger.LogInfo(string.Format("Iteration [{0}]: {1}", iteration, message));
            Assert.IsTrue(StfAssert.IsTrue("FailPass", failPass) == failPass);
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

            StfLogger.LogInfo(string.Format("Iteration [{0}]: {1}", iteration, message));

            // need to close the logfile, in order to check the content of the logfile...
            StfLogger.CloseLogFile();

            // we want to fail, if FileContains fails
            StfAssert.EnableNegativeTesting = false;

            StfAssert.FileContains(iteration.ToString(), StfLogger.FileName, message);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "Data\\DataDriven.csv", "DataDriven#csv", DataAccessMethod.Sequential)]
        public void DataDrivenSummaryLogTestEscapeCurlyParenthesisTest()
        {
            var testDescription = (string) TestContext.DataRow["Test Description"];
            StfAssert.StringNotEmpty("Description not empty", testDescription);
        }

        /// <summary>
        /// The test cleanup.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            StfLogger.LogInfo("DatadrivenStfLoggerTest TestCleanup");
            StfAssert.EnableNegativeTesting = true;
        }

        /// <summary>
        /// The convert to <see langword="bool"/>.
        /// </summary>
        /// <param name="boolValue">
        /// The bool value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool ConvertToBool(string boolValue)
        {
            if (string.CompareOrdinal(boolValue.ToLower(), "true") == 0
             || string.CompareOrdinal(boolValue.ToLower(), "pass") == 0
             || string.CompareOrdinal(boolValue.ToLower(), "1") == 0)
            {
                return true;
            }

            return false;
        }
    }
}

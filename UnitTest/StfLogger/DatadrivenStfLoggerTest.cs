﻿// --------------------------------------------------------------------------------------------------------------------
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
    using System;

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
            StfLogger.LogInfo("DatadrivenStfLoggerTest TestInitialize");
            StfAssert.EnableNegativeTesting = true;
        }

        /// <summary>
        /// Test of data driven Test. Data has three lines - all pass
        /// </summary>
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\Data_SummaryLog_3lines_allpass.csv", "Data_SummaryLog_3lines_allpass#csv", DataAccessMethod.Sequential)]
        public void DatadrivenLoggerTest3LinesAllpass()
        {
            var iteration = (int)TestContext.DataRow["Iteration"];
            var message = (string)TestContext.DataRow["Message"];
            var failPass = ConvertToBool((string)TestContext.DataRow["FailPass"]);

            StfLogger.LogInfo($"Iteration [{iteration}]: {message}");
            StfAssert.IsTrue("FailPass", failPass);
        }

        /// <summary>
        /// Test of data driven Test. Data has three lines - all pass
        /// </summary>
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\DataDriven.csv", "DataDriven#csv", DataAccessMethod.Sequential)]
        public void TestLogAllTypesOfOutcome()
        {
            var iteration = StfIterationNo;

            switch (iteration)
            {
                case 1:
                    StfAssert.IsTrue("Pass", true);
                    break;
                case 2:
                    StfAssert.IsTrue("Fail", false);
                    break;
                case 3:
                    StfAssert.IsInconclusive("Inconclusive", "Inconclusive");
                    break;
                case 4:
                    StfLogger.LogWarning("Warning", "Warning");
                    break;
                case 5:
                    StfLogger.LogError("Error", "Error");
                    break;
            }
        }

        /// <summary>
        /// Test of data driven Test. Data has three lines - first line fail - others pass
        /// </summary>
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\Data_SummaryLog_3lines_line1fail.csv", "Data_SummaryLog_3lines_line1fail#csv", DataAccessMethod.Sequential)]
        public void DatadrivenLoggerTest3LinesLine1Fail()
        {
            var iteration = (int)TestContext.DataRow["Iteration"];
            var message = (string)TestContext.DataRow["Message"];
            var failPass = ConvertToBool((string)TestContext.DataRow["FailPass"]);

            StfLogger.LogInfo($"Iteration [{iteration}]: {message}");
            Assert.IsTrue(StfAssert.IsTrue("FailPass", failPass) == failPass);
        }

        /// <summary>
        /// Test of data driven Test. Data has three lines - first line fail - others pass
        /// </summary>
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\Data_SummaryLog_10lines.csv", "Data_SummaryLog_10lines#csv", DataAccessMethod.Sequential)]
        public void DatadrivenLoggerTest10Lines()
        {
            StfLogger.LogInfo($"Iteration [{DateTime.Now}]");
        }

        /// <summary>
        /// Test of data driven Test. Data has three lines - second line fail - others pass
        /// </summary>
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\Data_SummaryLog_3lines_line2fail.csv", "Data_SummaryLog_3lines_line2fail#csv", DataAccessMethod.Sequential)]
        public void DatadrivenLoggerTest3LinesLine2Fail()
        {
            var iteration = (int)TestContext.DataRow["Iteration"];
            var message = (string)TestContext.DataRow["Message"];
            var failPass = ConvertToBool((string)TestContext.DataRow["FailPass"]);

            StfLogger.LogInfo($"Iteration [{iteration}]: {message}");
            Assert.IsTrue(StfAssert.IsTrue("FailPass", failPass) == failPass);
        }

        /// <summary>
        /// Test of data driven Test. Data has three lines - third line fail - others pass
        /// </summary>
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\Data_SummaryLog_3lines_line3fail.csv", "Data_SummaryLog_3lines_line3fail#csv", DataAccessMethod.Sequential)]
        public void DatadrivenLoggerTest3LinesLine3Fail()
        {
            var iteration = (int)TestContext.DataRow["Iteration"];
            var message = (string)TestContext.DataRow["Message"];
            var failPass = ConvertToBool((string)TestContext.DataRow["FailPass"]);

            StfLogger.LogInfo($"Iteration [{iteration}]: {message}");
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

            StfLogger.LogInfo($"Iteration [{iteration}]: {message}");

            // need to close the logfile, in order to check the content of the logfile...
            StfLogger.CloseLogFile();

            // we want to fail, if FileContains fails
            StfAssert.EnableNegativeTesting = false;

            StfAssert.FileContains(iteration.ToString(), StfLogger.FileName, message);
        }

        /// <summary>
        /// The data driven summary log test escape curly parenthesis test.
        /// </summary>
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "Data\\DataDriven.csv", "DataDriven#csv", DataAccessMethod.Sequential)]
        public void DataDrivenSummaryLogTestEscapeCurlyParenthesisTest()
        {
            var testDescription = (string)TestContext.DataRow["Test Description"];
            StfAssert.StringNotEmpty("Description not empty", testDescription);
        }

        /// <summary>
        /// The datadriven with testdata object.
        /// </summary>
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "Data\\Data.csv", "Data#csv", DataAccessMethod.Sequential)]
        public void TestInitTestDataWithDataSource()
        {
            HelperDatadrivenWithTestdataObject();
        }

        /// <summary>
        /// The datadriven with testdata object.
        /// </summary>
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "Data\\DataMapped.csv", "DataMapped#csv", DataAccessMethod.Sequential)]
        public void TestInitTestDataWithDataSourceMapped()
        {
            var testData = HelperDatadrivenWithTestdataObject();

            if (testData.StfIteration == 1)
            {
                StfAssert.AreEqual("Mapped Column Value match", "Column421", testData.ColumnTest);
            }
            else
            {
                StfAssert.StringEmpty("Mapped Column Value Empty", testData.ColumnTest);
            }            
        }

        /// <summary>
        /// The test stf ignore data driven no stf ignore.
        /// </summary>
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\TestStfIgnoreDataDrivenNoStfIgnore.csv", "TestStfIgnoreDataDrivenNoStfIgnore#csv", DataAccessMethod.Sequential)]
        public void TestStfIgnoreDataDrivenNoStfIgnore()
        {
            if (StfIgnoreRow)
            {
                StfAssert.IsTrue("Must be false", false);
                return;
            }

            StfAssert.AreEqual("StfRowNowIgnored", DataRowColumnValue("Bob"), 42);
        }

        /// <summary>
        /// The test stf ignore data driven with stf ignore.
        /// </summary>
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\TestStfIgnoreDataDrivenWithStfIgnore.csv", "TestStfIgnoreDataDrivenWithStfIgnore#csv", DataAccessMethod.Sequential)]
        public void TestStfIgnoreDataDrivenWithStfIgnore()
        {
            if (StfIgnoreRow)
            {
                StfAssert.AreEqual("StfRowNowIgnored", DataRowColumnValue("Bob"), 21);
                return;
            }

            StfAssert.AreEqual("StfRowNowIgnored", DataRowColumnValue("Bob"), 42);
        }

        /// <summary>
        /// The test stf ignore not data driven.
        /// </summary>
        [TestMethod]
        public void TestStfIgnoreNotDataDriven()
        {
            if (StfIgnoreRow)
            {
                StfAssert.IsTrue("Must be false", false);
                return;
            }

            StfAssert.IsTrue("Must be true", true);
        }

        /// <summary>
        /// The test init test data without data source.
        /// </summary>
        [TestMethod]
        public void TestInitTestDataWithoutDataSource()
        {
            HelperDatadrivenWithTestdataObject();
        }

        /// <summary>
        /// The test cleanup.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            StfLogger.LogInfo("DatadrivenStfLoggerTest TestCleanup");
            StfAssert.ResetStatistics();
        }

        /// <summary>
        /// The helper datadriven with testdata object.
        /// </summary>
        /// <returns>
        /// The <see cref="UnitTestTestDataObject"/>.
        /// </returns>
        private UnitTestTestDataObject HelperDatadrivenWithTestdataObject()
        {
            var testdata = new UnitTestTestDataObject { Iteration = "-1" };

            testdata = InitTestData(testdata);

            StfLogger.LogInfo($"Iteration [{testdata.Iteration}]: {testdata.Message}");
            StfAssert.AreEqual("Iteration", testdata.Iteration, testdata.StfIteration.ToString());

            return testdata;
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

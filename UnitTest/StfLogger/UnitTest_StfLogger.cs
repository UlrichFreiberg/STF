// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTest_TestResultHtmlLogger.cs" company="Foobar">
//   2015
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace UnitTest
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Stf.Utilities;

    /// <summary>
    /// The unit test 1.
    /// </summary>
    [TestClass]
    public class UnitTestStfLogger : StfTestScriptBase
    {
        /// <summary>
        /// The test method_ init.
        /// </summary>
        [TestMethod]
        public void TestMethodInit()
        {
            var myLogger = new Stf.Utilities.StfLogger { FileName = @"c:\temp\unittestlogger_TestMethodInit2.html" };
            myLogger.CloseLogFile();
        }

        /// <summary>
        /// All log levels
        /// </summary>
        [TestMethod]
        public void TestMethodAllLogType()
        {
            MyLogger.LogLevel = StfLogLevel.Internal;

            MyLogger.LogError("LogError");
            MyLogger.LogWarning("LogWarning");
            MyLogger.LogInfo("LogInfo");
            MyLogger.LogDebug("LogDebug");

            // normal logging functions - models and adapters
            MyLogger.LogTrace("LogTrace");
            MyLogger.LogInternal("LogInternal");

            // Header logging functions - testscripts
            MyLogger.LogHeader("LogHeader");
            MyLogger.LogSubHeader("LogSubHeader");

            MyLogger.LogFunctionEnter(StfLogLevel.Info, "Int", "NameOfFunction", new[] { "arg1", "arg2" }, new object[] { null });
            MyLogger.LogFunctionExit(StfLogLevel.Info, "NameOfFunction", 42);

            MyLogger.LogFunctionEnter(StfLogLevel.Info, "Int", "NameOfFunctionShort");
            MyLogger.LogFunctionExit(StfLogLevel.Info, "NameOfFunctionShort");

            // used solely by Assert functions
            MyLogger.LogPass("testStepName LogPass", "LogPass");
            MyLogger.LogFail("testStepName LogFail", "LogFail");

            MyLogger.LogKeyValue("SomeKey", "SomeValue", "LogKeyValue");

            MyLogger.LogGet(StfLogLevel.Info, "MyTestProperty", MyLogger);
            MyLogger.LogSet(StfLogLevel.Info, "MyTestProperty", MyLogger);

            MyLogger.LogAutomationIdObject(StfLogLevel.Internal, MyLogger, "Using MyLogger as AID for test");

            MyLogger.SetRunStatus();
        }

        /// <summary>
        /// The test method_ lots of entries.
        /// </summary>
        [TestMethod]
        public void TestMethodLotsOfEntries()
        {
            MyLogger.LogLevel = StfLogLevel.Internal;

            for (int i = 0; i < 75; i++)
            {
                MyLogger.LogInfo(string.Format("LogInfo Nr {0}", i));
            }

            MyLogger.CloseLogFile();
        }

        /// <summary>
        /// The test method_ call stack.
        /// </summary>
        [TestMethod]
        public void TestMethodCallStack()
        {
            MyLogger.LogLevel = StfLogLevel.Internal;

            MyLogger.LogInfo("NameOfFunction_L0A");
            MyLogger.LogInfo("NameOfFunction_L0B");

            MyLogger.LogFunctionEnter(StfLogLevel.Info, "Int", "NameOfFunction_L1");
            MyLogger.LogInfo("NameOfFunction_L1A");
            MyLogger.LogInfo("NameOfFunction_L1B");

            MyLogger.LogFunctionEnter(StfLogLevel.Info, "Int", "NameOfFunction_L2");
            MyLogger.LogInfo("NameOfFunction_L2A");
            MyLogger.LogInfo("NameOfFunction_L2B");

            MyLogger.LogFunctionEnter(StfLogLevel.Info, "Int", "NameOfFunction_L3");
            MyLogger.LogInfo("NameOfFunction_L3A");
            MyLogger.LogInfo("NameOfFunction_L3B");

            MyLogger.LogFunctionExit(StfLogLevel.Info, "NameOfFunction_L3");
            MyLogger.LogInfo("NameOfFunction_L2A");
            MyLogger.LogInfo("NameOfFunction_L2B");

            MyLogger.LogFunctionExit(StfLogLevel.Info, "NameOfFunction_L2");
            MyLogger.LogInfo("NameOfFunction_L1A");
            MyLogger.LogInfo("NameOfFunction_L1B");

            MyLogger.LogFunctionEnter(StfLogLevel.Info, "Int", "NameOfFunction_L2");
            MyLogger.LogInfo("NameOfFunction_L2A");
            MyLogger.LogInfo("NameOfFunction_L2B");

            MyLogger.LogFunctionExit(StfLogLevel.Info, "NameOfFunction_L2");
            MyLogger.LogInfo("NameOfFunction_L1A");
            MyLogger.LogInfo("NameOfFunction_L1B");

            MyLogger.LogFunctionExit(StfLogLevel.Info, "NameOfFunction_L1");
            MyLogger.LogInfo("NameOfFunction_L0A");
            MyLogger.LogInfo("NameOfFunction_L0B");
        }

        /// <summary>
        /// The test log screen shot.
        /// </summary>
        [TestMethod]
        public void TestLogScreenshot()
        {
            MyLogger.LogLevel = StfLogLevel.Internal;

            MyLogger.LogTrace("Just before a screenshot is taken");
            MyLogger.LogScreenshot(StfLogLevel.Info, "Grabbed screenshot");
            MyLogger.LogTrace("right after a screenshot is taken");
        }

        /// <summary>
        /// The test log all windows.
        /// </summary>
        [TestMethod, ExpectedException(typeof(NotImplementedException))]
        public void TestLogAllWindows()
        {
            MyLogger.LogLevel = StfLogLevel.Internal;

            MyLogger.LogTrace("Just before logging all windows");
            MyLogger.LogAllWindows(StfLogLevel.Info, "Grabbed all windows");
            MyLogger.LogTrace("right after logging all windows");
        }

        /// <summary>
        /// The test log screen shot.
        /// </summary>
        [TestMethod]
        public void TestLogFileWriter()
        {
            MyLogger.FileName = @"c:\temp\unittestlogger.html";
            MyLogger.FileName = @"c:\temp\unittestlogger2.html";
            MyLogger.FileName = @"c:\temp\unittestlogger3.html";

            MyLogger.CloseLogFile();
        }

        /// <summary>
        /// The test method_ asserts.
        /// </summary>
        [TestMethod]
        public void TestMethodAsserts()
        {
            MyLogger.LogLevel = StfLogLevel.Internal;
            MyAssert.EnableNegativeTesting = true;

            MyAssert.AssertTrue("True Value for AssertTrue", true);
            MyAssert.AssertTrue("False Value for AssertTrue", false);
            MyAssert.AssertTrue("2 > 3 Value for AssertTrue", 2 > 3);
            MyAssert.AssertTrue("3 > 2 Value for AssertTrue", 3 > 2);

            MyAssert.AssertFalse("True Value for AssertFalse", true);
            MyAssert.AssertFalse("False Value for AssertFalse", false);
            MyAssert.AssertFalse("2 > 3 Value for AssertFalse", 2 > 3);
            MyAssert.AssertFalse("3 > 2 Value for AssertFalse", 3 > 2);
        }

        /// <summary>
        /// The test for keyvalues
        /// </summary>
        [TestMethod]
        public void TestMethodKeyValues()
        {
            MyLogger.LogLevel = StfLogLevel.Internal;

            MyLogger.LogKeyValue("Bent", "42", "First value for Bent");
            MyLogger.LogKeyValue("Bent", "43", "Second value for Bent - this is the only one that should be shown in the list");
        }

        /// <summary>
        /// The test for keyvalues
        /// </summary>
        [TestMethod]
        public void TestMethodSummaryLogger()
        {
            var logDir = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @".\Data"));
            var summaryFilename = Path.Combine(LogDirRoot, "TestMethodSummaryLogger_SummeryLog.html");

            MyLogger.CreateSummaryLog(summaryFilename, logDir, "DatadrivenLoggerTest_*.html");
            MyAssert.AssertFileExists("MyLogger.CreateSummaryLog", summaryFilename);
        }
    }
}

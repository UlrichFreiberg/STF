// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestStfLogger.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Mir.Stf;
using Mir.Stf.Utilities;

namespace UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            var myLogger = new StfLogger { FileName = @"c:\temp\unittestlogger_TestMethodInit2.html" };
            myLogger.CloseLogFile();
        }

        /// <summary>
        /// All log levels
        /// </summary>
        [TestMethod]
        public void TestMethodAllLogType()
        {
            StfLogger.LogLevel = StfLogLevel.Internal;

            StfLogger.LogError("LogError");
            StfLogger.LogWarning("LogWarning");
            StfLogger.LogInfo("LogInfo");
            StfLogger.LogDebug("LogDebug");

            // normal logging functions - models and adapters
            StfLogger.LogTrace("LogTrace");
            StfLogger.LogInternal("LogInternal");

            // Header logging functions - testscripts
            StfLogger.LogHeader("LogHeader");
            StfLogger.LogSubHeader("LogSubHeader");

            StfLogger.LogFunctionEnter(StfLogLevel.Info, "Int", "NameOfFunction", new object[] { "arg1", "arg2", "arg3" });
            StfLogger.LogFunctionExit(StfLogLevel.Info, "NameOfFunction", 42);

            StfLogger.LogFunctionEnter(StfLogLevel.Info, "Int", "NameOfFunctionShort");
            StfLogger.LogFunctionExit(StfLogLevel.Info, "NameOfFunctionShort");

            // used solely by Assert functions
            StfLogger.LogPass("testStepName LogPass", "LogPass");
            StfLogger.LogFail("testStepName LogFail", "LogFail");

            StfLogger.LogKeyValue("SomeKey", "SomeValue", "LogKeyValue");

            StfLogger.LogGetEnter(StfLogLevel.Info, "MyTestProperty");
            StfLogger.LogGetExit(StfLogLevel.Info, "MyTestProperty", StfLogger);
            StfLogger.LogSetEnter(StfLogLevel.Info, "MyTestProperty", StfLogger);
            StfLogger.LogSetExit(StfLogLevel.Info, "MyTestProperty", StfLogger);

            StfLogger.LogAutomationIdObject(StfLogLevel.Internal, StfLogger, "Using StfLogger as AID for test");

            StfLogger.SetRunStatus();
        }

        /// <summary>
        /// The test method all log type with formatting.
        /// </summary>
        [TestMethod]
        public void TestMethodAllLogTypeWithFormatting()
        {
            StfLogger.LogLevel = StfLogLevel.Internal;

            StfLogger.LogError("{0}", "LogError");
            StfLogger.LogWarning("{0}", "LogWarning");
            StfLogger.LogInfo("{0}", "LogInfo");
            StfLogger.LogDebug("{0}", "LogDebug");
            StfLogger.LogTrace("{0}", "LogTrace");
            StfLogger.LogInternal("{0}", "LogInternal");
            StfLogger.LogHeader("{0}", "LogHeader");
            StfLogger.LogSubHeader("{0}", "LogSubHeader");

            StfLogger.LogError("{0} - {1}", "LogError", new List<string>());
            StfLogger.LogWarning("{0} - {1}", "LogWarning", new { Test = "Test" });
            StfLogger.LogInfo("{0} - {1} - {2}", "LogInfo", 42, 84);

            StfLogger.LogFunctionEnter(StfLogLevel.Info, "Int", "NameOfFunction", new object[] { "arg1", "arg2", "arg3" });
            StfLogger.LogFunctionExit(StfLogLevel.Info, "NameOfFunction", 42);

            StfLogger.LogFunctionEnter(StfLogLevel.Info, "Int", "NameOfFunctionShort");
            StfLogger.LogFunctionExit(StfLogLevel.Info, "NameOfFunctionShort");

            // used solely by Assert functions
            StfLogger.LogPass("testStepName LogPass", "{0}", "LogPass");
            StfLogger.LogFail("testStepName LogFail", "{0}", "LogFail");
            StfLogger.LogInconclusive("testStepName LogInconclusive", "Inconclusive result: {0}", 1);

            StfLogger.LogKeyValue("SomeKey", "SomeValue", "{0}", "LogKeyValue");

            StfLogger.LogGetEnter(StfLogLevel.Info, "MyTestProperty");
            StfLogger.LogGetExit(StfLogLevel.Info, "MyTestProperty", StfLogger);
            StfLogger.LogSetEnter(StfLogLevel.Info, "MyTestProperty", StfLogger);
            StfLogger.LogSetExit(StfLogLevel.Info, "MyTestProperty", StfLogger);

            StfLogger.LogAutomationIdObject(StfLogLevel.Internal, StfLogger, "Using StfLogger as AID for test");

            StfLogger.SetRunStatus();
        }

        /// <summary>
        /// The test method_ lots of entries.
        /// </summary>
        [TestMethod]
        public void TestMethodLotsOfEntries()
        {
            StfLogger.LogLevel = StfLogLevel.Internal;

            for (int i = 0; i < 75; i++)
            {
                StfLogger.LogInfo($"LogInfo Nr {i}");
            }

            StfLogger.CloseLogFile();
        }

        /// <summary>
        /// The test method_ call stack.
        /// </summary>
        [TestMethod]
        public void TestMethodCallStack()
        {
            StfLogger.LogLevel = StfLogLevel.Internal;

            StfLogger.LogInfo("NameOfFunction_L0A");
            StfLogger.LogInfo("NameOfFunction_L0B");

            StfLogger.LogFunctionEnter(StfLogLevel.Info, "Int", "NameOfFunction_L1");
            StfLogger.LogInfo("NameOfFunction_L1A");
            StfLogger.LogInfo("NameOfFunction_L1B");

            StfLogger.LogFunctionEnter(StfLogLevel.Info, "Int", "NameOfFunction_L2");
            StfLogger.LogInfo("NameOfFunction_L2A");
            StfLogger.LogInfo("NameOfFunction_L2B");

            StfLogger.LogFunctionEnter(StfLogLevel.Info, "Int", "NameOfFunction_L3");
            StfLogger.LogInfo("NameOfFunction_L3A");
            StfLogger.LogInfo("NameOfFunction_L3B");

            StfLogger.LogFunctionExit(StfLogLevel.Info, "NameOfFunction_L3");
            StfLogger.LogInfo("NameOfFunction_L2A");
            StfLogger.LogInfo("NameOfFunction_L2B");

            StfLogger.LogFunctionExit(StfLogLevel.Info, "NameOfFunction_L2");
            StfLogger.LogInfo("NameOfFunction_L1A");
            StfLogger.LogInfo("NameOfFunction_L1B");

            StfLogger.LogFunctionEnter(StfLogLevel.Info, "Int", "NameOfFunction_L2");
            StfLogger.LogInfo("NameOfFunction_L2A");
            StfLogger.LogInfo("NameOfFunction_L2B");

            StfLogger.LogFunctionExit(StfLogLevel.Info, "NameOfFunction_L2");
            StfLogger.LogInfo("NameOfFunction_L1A");
            StfLogger.LogInfo("NameOfFunction_L1B");

            StfLogger.LogFunctionExit(StfLogLevel.Info, "NameOfFunction_L1");
            StfLogger.LogInfo("NameOfFunction_L0A");
            StfLogger.LogInfo("NameOfFunction_L0B");
        }

        /// <summary>
        /// The test log screen shot.
        /// </summary>
        [TestMethod]
        public void TestLogScreenshot()
        {
            StfLogger.LogLevel = StfLogLevel.Internal;

            StfLogger.LogTrace("Just before a screenshot is taken");
            StfLogger.LogScreenshot(StfLogLevel.Debug, "Grabbed screenshot");
            StfLogger.LogTrace("right after a screenshot is taken");
        }

        /// <summary>
        /// The test log all windows.
        /// </summary>
        [TestMethod, ExpectedException(typeof(NotImplementedException))]
        public void TestLogAllWindows()
        {
            StfLogger.LogLevel = StfLogLevel.Internal;

            StfLogger.LogTrace("Just before logging all windows");
            StfLogger.LogAllWindows(StfLogLevel.Info, "Grabbed all windows");
            StfLogger.LogTrace("right after logging all windows");
        }

        /// <summary>
        /// The test log screen shot.
        /// </summary>
        [TestMethod]
        public void TestLogFileWriter()
        {
            StfLogger.FileName = @"c:\temp\unittestlogger.html";
            StfLogger.FileName = @"c:\temp\unittestlogger2.html";
            StfLogger.FileName = @"c:\temp\unittestlogger3.html";

            StfLogger.CloseLogFile();
        }

        /// <summary>
        /// The test method_ asserts.
        /// </summary>
        [TestMethod]
        public void TestMethodAsserts()
        {
            StfLogger.LogLevel = StfLogLevel.Internal;
            StfAssert.EnableNegativeTesting = true;

            StfAssert.IsTrue("True Value for IsTrue", true);
            StfAssert.IsTrue("False Value for IsTrue", false);
            StfAssert.IsTrue("2 > 3 Value for IsTrue", 2 > 3);
            StfAssert.IsTrue("3 > 2 Value for IsTrue", 3 > 2);

            StfAssert.IsFalse("True Value for IsFalse", true);
            StfAssert.IsFalse("False Value for IsFalse", false);
            StfAssert.IsFalse("2 > 3 Value for IsFalse", 2 > 3);
            StfAssert.IsFalse("3 > 2 Value for IsFalse", 3 > 2);

            // setting to true resets count of failures
            StfAssert.ResetStatistics();
        }

        /// <summary>
        /// The test for keyvalues
        /// </summary>
        [TestMethod]
        public void TestMethodKeyValues()
        {
            StfLogger.LogLevel = StfLogLevel.Internal;

            StfLogger.LogKeyValue("Bent", "42", "First value for Bent");
            StfLogger.LogKeyValue("Bent", "43", "Second value for Bent - this is the only one that should be shown in the list");

            StfLogger.LogKeyValue("File Url", @"File://c:/Temp/Stf", @"An explorer should pop up show c:\temp\Stf");
            StfLogger.LogKeyValue("Web Url", "Http://www.testautomation.dk", "A browser should pop up show testautomation.dk");
        }

        /// <summary>
        /// The test screenshot on log fail.
        /// </summary>
        [TestMethod]
        public void TestScreenshotOnLogFail()
        {
            StfLogger.LogLevel = StfLogLevel.Internal;
            StfLogger.LogFail("Screenshot on log fail on by default", string.Empty);

            StfLogger.Configuration.ScreenshotOnLogFail = false;
            StfLogger.LogFail("Screenshot on log fail turned off. No screenshot logged.", string.Empty);
        }

        /// <summary>
        /// The test screenshot on log fail.
        /// </summary>
        [TestMethod]
        public void TestLogTextWithNewlines()
        {
            StfLogger.LogLevel = StfLogLevel.Info;
            StfLogger.LogInfo("Single line 1");
            StfLogger.LogInfo(string.Format("Entering Multi line{0}Second row{0}Third row{0}{0}Blank line above{0}Last line", Environment.NewLine));
            StfLogger.LogInfo("Single line 2");
        }

        /// <summary>
        /// The test screenshot on log fail.
        /// </summary>
        [TestMethod]
        public void TestLogTextWithNewlinesAndRegex()
        {
            StfLogger.LogLevel = StfLogLevel.Info;
            StfLogger.LogInfo("Single line 1");
            StfLogger.LogInfo(string.Format("Entering [Multi line1{0}Second row{0}Third row{0}{0}Blank line above{0}Last line", Environment.NewLine));
            StfLogger.LogInfo("Single line 2");
            StfLogger.LogInfo(string.Format("Entering [Multi line2{0}Second row{0}Third row{0}{0}Blank line above{0}Last line", Environment.NewLine));
        }

        /// <summary>
        /// The test screenshot on log fail.
        /// </summary>
        [TestMethod]
        public void TestLogTextWithXmlContent()
        {
            var xmlToLog = string.Format(
                    @"<xmltags>{0}<xmltag>{0}Line 1{0}      Six Space indent{0}</xmltag>{0}</xmltags>{0}",
                    Environment.NewLine);

            StfLogger.LogLevel = StfLogLevel.Info;

            StfLogger.LogSubHeader("xml without anything");
            StfLogger.LogInfo(xmlToLog);

            StfLogger.LogSubHeader("Using Escape");
            StfLogger.LogInfo(System.Security.SecurityElement.Escape(xmlToLog));

            StfLogger.LogSubHeader("Using logXml");
            StfLogger.LogXmlMessage(xmlToLog);
        }

        /// <summary>
        /// The test screenshot on log fail.
        /// </summary>
        [TestMethod]
        public void TestLogTextWithInvalidXmlContent()
        {
            var xmlToLog = string.Format(@"<xmltags>{0}<xmltag>{0}Line 1{0}      Six Space indent{0}</xmltag>{0}</WRONG_CLOSING_TAG>{0}", Environment.NewLine);

            StfLogger.LogLevel = StfLogLevel.Info;

            StfLogger.LogSubHeader("xml without anything");
            StfLogger.LogInfo(xmlToLog);

            StfLogger.LogSubHeader("Using Escape");
            StfLogger.LogInfo(System.Security.SecurityElement.Escape(xmlToLog));

            StfLogger.LogSubHeader("Using logXml");
            StfLogger.LogXmlMessage(xmlToLog);
        }

        /// <summary>
        /// The test screenshot on log fail.
        /// </summary>
        [TestMethod]
        public void TestLogXmlMessage()
        {
            StfLogger.LogInfo("Using PrettyXml - per LogXmlMessage");
            StfLogger.LogXmlMessage(@"<xmltags><xmltag>Line 1 </xmltag><xmltag>Six Space indent</xmltag></xmltags>");
        }

        /// <summary>
        /// The test log text with new many lines.
        /// </summary>
        [TestMethod]
        public void TestLogTextWithNewManyLines()
        {
            StfLogger.LogLevel = StfLogLevel.Info;
            StfLogger.LogInfo("Single line 1");
            StfLogger.LogInfo(string.Format("Entering [Multi line1{0}Second row{0}Third row{0}{0}Blank line above{0}Last line", Environment.NewLine));
            StfLogger.LogInfo("Single line 2");
            StfLogger.LogInfo(string.Format("Entering [Multi line2{0}Second row{0}Third row{0}{0}Blank line above{0}Last line", Environment.NewLine));
            StfLogger.LogInfo("Single line 3");
            StfLogger.LogInfo(string.Format("Entering [Multi line3{0}Second row{0}Third row{0}{0}Blank line above{0}Last line", Environment.NewLine));
            StfLogger.LogInfo("Single line 4");
            StfLogger.LogInfo(string.Format("Entering [Multi line4{0}Second row{0}Third row{0}{0}Blank line above{0}Last line", Environment.NewLine));
        }

        /// <summary>
        /// The test for keyvalues
        /// </summary>
        [TestMethod]
        public void TestMethodSummaryLogger()
        {
            var logDir = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @".\Data"));
            var summaryFilename = Path.Combine(StfTextUtils.GetVariable("Stf_LogDir"), "TestMethodSummaryLogger_SummaryLog.html");
            var oneSummaryLogger = new StfSummaryLogger();

            StfAssert.IsTrue("StfLogger.CreateSummaryLog", oneSummaryLogger.CreateSummaryLog(summaryFilename, logDir, "DatadrivenLoggerTest_*.html"));
            StfAssert.FileExists("StfLogger.CreateSummaryLog", summaryFilename);
        }

        /// <summary>
        /// The test method exception thrown in test is caught in cleanup.
        /// </summary>
        [TestMethod, ExpectedException(typeof(AssertFailedException))]
        public void TestMethodExceptionThrownInTestIsCaughtInCleanup()
        {
            StfLogger.LogInfo("This is a message before assert failed exception error");
            throw new AssertFailedException();
        }
    }
}

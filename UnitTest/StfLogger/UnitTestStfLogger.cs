// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestStfLogger.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.CodeDom;
using Mir.Stf;
using Mir.Stf.Utilities;

namespace UnitTest
{
    using System;
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
        /// The test method_ lots of entries.
        /// </summary>
        [TestMethod]
        public void TestMethodLotsOfEntries()
        {
            StfLogger.LogLevel = StfLogLevel.Internal;

            for (int i = 0; i < 75; i++)
            {
                StfLogger.LogInfo(string.Format("LogInfo Nr {0}", i));
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
            StfLogger.LogLevel = StfLogLevel.Info;
            StfLogger.LogInfo("Using Escape");
            StfLogger.LogInfo(System.Security.SecurityElement.Escape(string.Format("<xmltags>{0}<xmltag>{0}return <br/> newline{0}{0}<xmltag>{0}<xmltags>{0}", Environment.NewLine)));
            StfLogger.LogInfo("Using PrettyXml");

            // TODO: Missing playing a bit with the XmlUtils
            var prettyXml = "<xmltags><xmltag>return newline                  </xmltag></xmltags>";
            StfLogger.LogInfo(prettyXml);

            StfLogger.LogInfo("Using PrettyXml and Escape");
            StfLogger.LogInfo(System.Security.SecurityElement.Escape(string.Format("<xmltags>{0}<xmltag>{0}return <br/> newline{0}{0}</xmltag>{0}</xmltags>{0}", Environment.NewLine)));
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
            var summaryFilename = Path.Combine(StfTextUtils.GetVariable("Stf_LogDir"), "TestMethodSummaryLogger_SummeryLog.html");
            var oneSummeryLogger = new StfSummeryLogger();

            StfAssert.IsTrue("StfLogger.CreateSummeryLog", oneSummeryLogger.CreateSummeryLog(summaryFilename, logDir, "DatadrivenLoggerTest_*.html"));
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

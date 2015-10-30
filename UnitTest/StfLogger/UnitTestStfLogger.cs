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

            MyLogger.LogFunctionEnter(StfLogLevel.Info, "Int", "NameOfFunction", new object[] { "arg1", "arg2", "arg3" });
            MyLogger.LogFunctionExit(StfLogLevel.Info, "NameOfFunction", 42);

            MyLogger.LogFunctionEnter(StfLogLevel.Info, "Int", "NameOfFunctionShort");
            MyLogger.LogFunctionExit(StfLogLevel.Info, "NameOfFunctionShort");

            // used solely by Assert functions
            MyLogger.LogPass("testStepName LogPass", "LogPass");
            MyLogger.LogFail("testStepName LogFail", "LogFail");

            MyLogger.LogKeyValue("SomeKey", "SomeValue", "LogKeyValue");

            MyLogger.LogGetEnter(StfLogLevel.Info, "MyTestProperty");
            MyLogger.LogGetExit(StfLogLevel.Info, "MyTestProperty", MyLogger);
            MyLogger.LogSetEnter(StfLogLevel.Info, "MyTestProperty", MyLogger);
            MyLogger.LogSetExit(StfLogLevel.Info, "MyTestProperty", MyLogger);

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
            MyLogger.LogScreenshot(StfLogLevel.Debug, "Grabbed screenshot");
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

            MyAssert.IsTrue("True Value for IsTrue", true);
            MyAssert.IsTrue("False Value for IsTrue", false);
            MyAssert.IsTrue("2 > 3 Value for IsTrue", 2 > 3);
            MyAssert.IsTrue("3 > 2 Value for IsTrue", 3 > 2);

            MyAssert.IsFalse("True Value for IsFalse", true);
            MyAssert.IsFalse("False Value for IsFalse", false);
            MyAssert.IsFalse("2 > 3 Value for IsFalse", 2 > 3);
            MyAssert.IsFalse("3 > 2 Value for IsFalse", 3 > 2);
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

            MyLogger.LogKeyValue("File Url", @"File://c:/Temp/Stf", @"An explorer should pop up show c:\temp\Stf");
            MyLogger.LogKeyValue("Web Url", "Http://www.testautomation.dk", "A browser should pop up show testautomation.dk");
        }

        /// <summary>
        /// The test screenshot on log fail.
        /// </summary>
        [TestMethod]
        public void TestScreenshotOnLogFail()
        {
            MyLogger.LogLevel = StfLogLevel.Internal;
            MyLogger.LogFail("Screenshot on log fail on by default", string.Empty);

            MyLogger.Configuration.ScreenshotOnLogFail = false;
            MyLogger.LogFail("Screenshot on log fail turned off. No screenshot logged.", string.Empty);
        }

        /// <summary>
        /// The test screenshot on log fail.
        /// </summary>
        [TestMethod]
        public void TestLogTextWithNewlines()
        {
            MyLogger.LogLevel = StfLogLevel.Info;
            MyLogger.LogInfo("Single line 1");
            MyLogger.LogInfo(string.Format("Entering Multi line{0}Second row{0}Third row{0}{0}Blank line above{0}Last line", Environment.NewLine));
            MyLogger.LogInfo("Single line 2");
        }

        /// <summary>
        /// The test screenshot on log fail.
        /// </summary>
        [TestMethod]
        public void TestLogTextWithNewlinesAndRegex()
        {
            MyLogger.LogLevel = StfLogLevel.Info;
            MyLogger.LogInfo("Single line 1");
            MyLogger.LogInfo(string.Format("Entering [Multi line1{0}Second row{0}Third row{0}{0}Blank line above{0}Last line", Environment.NewLine));
            MyLogger.LogInfo("Single line 2");
            MyLogger.LogInfo(string.Format("Entering [Multi line2{0}Second row{0}Third row{0}{0}Blank line above{0}Last line", Environment.NewLine));
        }

        /// <summary>
        /// The test screenshot on log fail.
        /// </summary>
        [TestMethod]
        public void TestLogTextWithXmlContent()
        {
            MyLogger.LogLevel = StfLogLevel.Info;
            MyLogger.LogInfo("Using Escape");
            MyLogger.LogInfo(System.Security.SecurityElement.Escape(string.Format("<xmltags>{0}<xmltag>{0}return <br/> newline{0}{0}<xmltag>{0}<xmltags>{0}", Environment.NewLine)));
            MyLogger.LogInfo("Using PrettyXml");

            // TODO: Missing playing a bit with the XmlUtils
            var prettyXml = "<xmltags><xmltag>return newline                  </xmltag></xmltags>";
            MyLogger.LogInfo(prettyXml);

            MyLogger.LogInfo("Using PrettyXml and Escape");
            MyLogger.LogInfo(System.Security.SecurityElement.Escape(string.Format("<xmltags>{0}<xmltag>{0}return <br/> newline{0}{0}</xmltag>{0}</xmltags>{0}", Environment.NewLine)));
        }

   
        /// <summary>
        /// The test log text with new many lines.
        /// </summary>
        [TestMethod]
        public void TestLogTextWithNewManyLines()
        {
            MyLogger.LogLevel = StfLogLevel.Info;
            MyLogger.LogInfo("Single line 1");
            MyLogger.LogInfo(string.Format("Entering [Multi line1{0}Second row{0}Third row{0}{0}Blank line above{0}Last line", Environment.NewLine));
            MyLogger.LogInfo("Single line 2");
            MyLogger.LogInfo(string.Format("Entering [Multi line2{0}Second row{0}Third row{0}{0}Blank line above{0}Last line", Environment.NewLine));
            MyLogger.LogInfo("Single line 3");
            MyLogger.LogInfo(string.Format("Entering [Multi line3{0}Second row{0}Third row{0}{0}Blank line above{0}Last line", Environment.NewLine));
            MyLogger.LogInfo("Single line 4");
            MyLogger.LogInfo(string.Format("Entering [Multi line4{0}Second row{0}Third row{0}{0}Blank line above{0}Last line", Environment.NewLine));
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

            MyAssert.IsTrue("MyLogger.CreateSummeryLog", oneSummeryLogger.CreateSummeryLog(summaryFilename, logDir, "DatadrivenLoggerTest_*.html"));
            MyAssert.FileExists("MyLogger.CreateSummaryLog", summaryFilename);
        }
    }
}

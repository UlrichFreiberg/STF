// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogfileManagement.cs" company="Foobar">
//   2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Stf.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    using Stf.Utilities.Interfaces;
    using Stf.Utilities.Properties;
    using Stf.Utilities.Utils;

    /// <summary>
    /// The test result html logger. the <see cref="ILogfileManagement"/> part
    /// </summary>
    public partial class StfLogger : ILogfileManagement
    {
        /// <summary>
        /// The log file name.
        /// </summary>
        private string fileName;

        /// <summary>
        /// The _m log level.
        /// </summary>
        private StfLogLevel logLevel;

        /// <summary>
        /// Initializes a new instance of the <see cref="StfLogger"/> class.
        /// </summary>
        /// <param name="logfileName">
        /// The archive log file.
        /// </param>
        public StfLogger(string logfileName)
            : this()
        {
            FileName = logfileName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StfLogger"/> class.
        /// </summary>
        public StfLogger()
        {
            LogInfoDetails = new Dictionary<string, string>();
            AddLoglevelToRunReport = new Dictionary<StfLogLevel, bool>();
            NumberOfLoglevelMessages = new Dictionary<StfLogLevel, int>();

            foreach (StfLogLevel loglevel in Enum.GetValues(typeof(StfLogLevel)))
            {
                NumberOfLoglevelMessages.Add(loglevel, 0);
                AddLoglevelToRunReport.Add(loglevel, true);
            }

            LogFileHandle = new LogfileWriter { OverwriteLogFile = this.OverwriteLogFile };

            // Set according to the configuration
            Configuration = new LogConfiguration();
            FileName = Configuration.LogFileName;
            this.LogLevel = Configuration.LogLevel;
            OverwriteLogFile = Configuration.OverwriteLogFile;
            LogToFile = Configuration.LogToFile;
            LogTitle = Configuration.LogTitle;
            AlertLongInterval = Configuration.AlertLongInterval;
            PathToLogoImageFile = Configuration.PathToLogoImageFile;

            // setting the default AID logging function
            LogAutomationIdObjectUserFunction =
                (level, obj, message) => LogOneHtmlMessage(level, string.Format("AutomationId: [{0}] - Message: [{1}]", obj.ToString(), message));

            LogFileInitialized = false;
        }

        /// <summary>
        /// Gets the Number Messages per Loglevel - to the finishing TestStatus
        /// </summary>
        public Dictionary<StfLogLevel, int> NumberOfLoglevelMessages { get; private set; }

        /// <summary>
        /// Gets the configuration for the logfile.
        /// </summary>
        public LogConfiguration Configuration { get; private set; }

        /// <summary>
        /// Gets Details about Environment, Test Agent (the current machine - OS, versions of Software), Date
        /// </summary>
        public Dictionary<string, string> LogInfoDetails { get; private set; }

        /// <summary>
        /// Gets or sets the current log level.
        /// </summary>
        public int CurrentLogLevel { get; set; }

        /// <summary>
        /// Gets or sets the build archive log file path.
        /// </summary>
        public int BuildArchiveLogFilePath { get; set; }

        /// <summary>
        /// Gets or sets the Title used in the header of the logfile
        /// </summary>
        public string LogTitle { get; set; }

        /// <summary>
        /// Gets or sets the path to the resulting logfile
        /// </summary>
        public string FileName
        {
            get
            {
                return fileName;
            }

            set
            {
                fileName = value;
                CloseLogFile();
                messageId = 0;
            }
        }

        /// <summary>
        /// Gets or sets the level the logger accepts logging for. 
        /// Lower levels than this will be ignored.
        /// Eg "trace" will be ignored is level is set to "debug"
        /// </summary>
        public StfLogLevel LogLevel
        {
            get
            {
                return this.logLevel;
            }

            set
            {
                this.logLevel = value;
                timeOfLastMessage = DateTime.Now;

                foreach (StfLogLevel loglevel in Enum.GetValues(typeof(StfLogLevel)))
                {
                    AddLoglevelToRunReport[loglevel] = true;
                }

                switch (value)
                {
                    case StfLogLevel.Error:
                        AddLoglevelToRunReport[StfLogLevel.Warning] = false;
                        AddLoglevelToRunReport[StfLogLevel.Info] = false;
                        AddLoglevelToRunReport[StfLogLevel.Debug] = false;
                        AddLoglevelToRunReport[StfLogLevel.Trace] = false;
                        AddLoglevelToRunReport[StfLogLevel.Internal] = false;
                        break;
                    case StfLogLevel.Warning:
                        AddLoglevelToRunReport[StfLogLevel.Info] = false;
                        AddLoglevelToRunReport[StfLogLevel.Debug] = false;
                        AddLoglevelToRunReport[StfLogLevel.Trace] = false;
                        AddLoglevelToRunReport[StfLogLevel.Internal] = false;
                        break;
                    case StfLogLevel.Info:
                        AddLoglevelToRunReport[StfLogLevel.Debug] = false;
                        AddLoglevelToRunReport[StfLogLevel.Trace] = false;
                        AddLoglevelToRunReport[StfLogLevel.Internal] = false;
                        break;
                    case StfLogLevel.Debug:
                        AddLoglevelToRunReport[StfLogLevel.Trace] = false;
                        AddLoglevelToRunReport[StfLogLevel.Internal] = false;
                        break;
                    case StfLogLevel.Trace:
                        AddLoglevelToRunReport[StfLogLevel.Internal] = false;
                        break;
                    case StfLogLevel.Internal:
                        break;
                    default:
                        Console.WriteLine(@"Internal Error: Unknown loglevel meet");
                        break;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the path to the Logo file.
        /// </summary>
        public string PathToLogoImageFile { get; set; }

        /// <summary>
        /// Gets or sets the directory for the logfile to be archived - to where should it be archived
        /// </summary>
        public string ArchiveDirecory { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the logfile should be archive 
        /// </summary>
        public bool ArchiveLogFile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether log file initialized.
        /// </summary>
        private bool LogFileInitialized { get; set; }

        /// <summary>
        /// Gets or sets the _log file handle.
        /// </summary>
        private LogfileWriter LogFileHandle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Logging is disabled. 
        /// It gets enabled when LogfileName property is set. />
        /// </summary>
        private bool LogToFile { get; set; }

        /// <summary>
        /// Gets or sets whether we do log for a given log level?
        /// </summary>
        private Dictionary<StfLogLevel, bool> AddLoglevelToRunReport { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether whether if an existing logfile should be overwriten.
        /// </summary>
        private bool OverwriteLogFile { get; set; }

        /// <summary>
        /// Have we logged a Error or Fail? 
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int ErrorOrFail()
        {
            return NumberOfLoglevelMessages[StfLogLevel.Error] + NumberOfLoglevelMessages[StfLogLevel.Fail];
        }

        /// <summary>
        /// The close log file.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public bool CloseLogFile()
        {
            if (!LogFileInitialized)
            {
                return true;
            }

            var retVal = SetRunStatus();
            retVal = retVal && EndHtmlLogFile();
            retVal = retVal && LogFileHandle.Close();
            LogFileInitialized = false;
            return retVal;
        }

        /// <summary>
        /// The archive this log file.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool ArchiveThisLogFile()
        {
            return false;
        }

        /// <summary>
        ///   reads in the JavaScript functions for the logfile buttons etc
        /// </summary>
        /// <returns>A string representing the JS that controls the logger</returns>
        private string GetJavaScript()
        {
            var loggerJs = Resources.ResourceManager.GetObject("logger");
            var retVal = loggerJs == null ? "<error>No Logger JS section defined</error>" : loggerJs.ToString();

            return retVal;
        }

        /// <summary>
        ///   reads in the HTML that constitutes the top LogHeader
        /// </summary>
        /// <returns>A Html string representing the start of the Body for the logger</returns>
        private string GetOpenBody()
        {
            string retVal;
            var openBody = Resources.ResourceManager.GetObject("OpenBody");

            if (openBody == null)
            {
                retVal = "<error>No OpenBody section file found</error>";
            }
            else
            {
                retVal = openBody.ToString();
                retVal = retVal.Replace("LOGFILETITLE", LogTitle);
            }

            return retVal;
        }

        /// <summary>
        ///   reads in the CSS 
        /// </summary>
        /// <returns>A string representing the CSS that controls the logger</returns>
        private string GetStyleSheet()
        {
            var styleSheet = Resources.ResourceManager.GetObject("style");
            var retVal = styleSheet == null ? "<error>No styleSheet file found</error>" : styleSheet.ToString();

            return retVal;
        }

        /// <summary>
        /// Open and Start the logger
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// private
        private bool BeginHtmlLogFile()
        {
            LogFileHandle.Open(fileName);

            var htmlLine = "<!DOCTYPE html>\n";
            htmlLine += "<html>\n";
            htmlLine += "  <head>\n";
            htmlLine += "    <meta charset=\"utf-8\" />\n";
            htmlLine += string.Format("    <title>{0}</title>\n", LogTitle);
            htmlLine += "    <style type=\"text/css\">\n";
            htmlLine += GetStyleSheet();
            htmlLine += "    </style>\n<script type=\"text/javascript\">";
            htmlLine += GetJavaScript();
            htmlLine += "  </script>\n</head>\n";
            htmlLine += GetOpenBody();

            LogFileHandle.Write(htmlLine);
            return true;
        }

        /// <summary>
        /// End html log file.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool EndHtmlLogFile()
        {
            LogKeyValue("Passed", NumberOfLoglevelMessages[StfLogLevel.Pass].ToString(), "Passed Tests");
            LogKeyValue("Failed", NumberOfLoglevelMessages[StfLogLevel.Fail].ToString(), "Failed Tests");
            LogKeyValue("Errors", NumberOfLoglevelMessages[StfLogLevel.Error].ToString(), "Errors logged");
            LogKeyValue("Warnings", NumberOfLoglevelMessages[StfLogLevel.Warning].ToString(), "Warnings logged");

            var htmlLine = "    </div>\n";
            htmlLine += "  </body>\n";
            htmlLine += "</html>\n";

            LogFileHandle.Write(htmlLine);
            return true;
        }

        /// <summary>
        /// initialize a logger
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool Init()
        {
            var userName = string.Format("{0}\\{1}", Environment.UserDomainName, Environment.UserName);

            if (LogFileInitialized)
            {
                return true;
            }

            OverwriteLogFile = Configuration.OverwriteLogFile;
            TestName = "TestName_Not_Set";
            LogToFile = Configuration.LogToFile;
            LogTitle = Configuration.LogTitle;
            this.LogLevel = Configuration.LogLevel;

            if (LogFileHandle.Initialized)
            {
                CloseLogFile();
            }

            if (!LogFileHandle.Open(FileName))
            {
                return false;
            }

            if (!BeginHtmlLogFile())
            {
                return false;
            }

            LogFileInitialized = true;
            LogTrace(string.Format("Log Initiated at [{0}]", FileName));

            LogKeyValue("Environment", "TODO_ENVIRONMENT", "Configuration.EnvironmentName");
            LogKeyValue("OS", Environment.OSVersion.ToString(), string.Empty);
            LogKeyValue("User", userName, string.Empty);
            LogKeyValue("InstDir", Environment.CurrentDirectory, "TODO_InstDir");
            LogKeyValue("ResultDir", Environment.CurrentDirectory, "TODO_ResultDir");
            LogKeyValue("Controller", Environment.MachineName, "TODO_Controller");
            LogKeyValue("Hostname", Environment.MachineName, "TODO_Hostname");
            LogKeyValue("TestDir", Environment.CurrentDirectory, "TODO_TestDir");
            LogKeyValue("Test Iteration", "TODO_Test Iteration", "TODO_Test Iteration");
            LogKeyValue("Testname", TestName, "TODO_Testname");
            LogKeyValue("Date", DateTime.Now.ToShortDateString(), string.Empty);

            return true;
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogfileManagement.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Mir.Stf.Utilities.Interfaces;
using Mir.Stf.Utilities.Properties;
using Mir.Stf.Utilities.Utils;

namespace Mir.Stf.Utilities
{
    using Configuration;

    /// <summary>
    /// The test result html logger. the <see cref="IStfLoggerLogfileManagement"/> part
    /// </summary>
    public partial class StfLogger : IStfLogger
    {
        /// <summary>
        /// The log file name.
        /// </summary>
        private string fileName;

        /// <summary>
        /// The log level.
        /// </summary>
        private StfLogLevel logLevel;

        /// <summary>
        /// Should the logfile be overwritten or not
        /// </summary>
        private bool overwriteLogFile;

        /// <summary>
        /// Initializes a new instance of the <see cref="StfLogger"/> class.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        public StfLogger(IStfLoggerConfiguration config)
        {
            Configuration = config;
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StfLogger"/> class.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <param name="logfileName">
        /// The logfile name.
        /// </param>
        public StfLogger(IStfLoggerConfiguration config, string logfileName)
        {
            Configuration = config;
            Init();
            FileName = logfileName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StfLogger"/> class.
        /// </summary>
        /// <param name="logfileName">
        /// The archive log file.
        /// </param>
        public StfLogger(string logfileName)
        {
            Init();
            FileName = logfileName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StfLogger"/> class.
        /// </summary>
        public StfLogger()
        {
            Init();
        }

        /// <summary>
        /// Gets the Number Messages per Loglevel - to the finishing TestStatus
        /// </summary>
        public Dictionary<StfLogLevel, int> NumberOfLoglevelMessages { get; private set; }

        /// <summary>
        /// Gets Details about Environment, Test Agent (the current machine - OS, versions of Software), Date
        /// </summary>
        public Dictionary<string, string> LogInfoDetails { get; private set; }

        /// <summary>
        /// Gets or sets the current log level.
        /// </summary>
        public int CurrentLogLevel { get; set; }

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
        /// Gets or sets whether we do log for a given log level?
        /// </summary>
        private Dictionary<StfLogLevel, bool> AddLoglevelToRunReport { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether whether if an existing logfile should be overwriten.
        /// </summary>
        private bool OverwriteLogFile
        {
            get { return this.overwriteLogFile; }
            set { LogFileHandle.OverwriteLogFile = this.overwriteLogFile = value; }
        }

        /// <summary>
        /// Gets or sets the time of first log message.
        /// </summary>
        private DateTime TimeOfFirstLogMessage { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StfLogger"/> class.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Init()
        {
            LogInfoDetails = new Dictionary<string, string>();
            AddLoglevelToRunReport = new Dictionary<StfLogLevel, bool>();
            NumberOfLoglevelMessages = new Dictionary<StfLogLevel, int>();
            ThreadUtils = new ThreadUtils();

            foreach (StfLogLevel loglevel in Enum.GetValues(typeof(StfLogLevel)))
            {
                NumberOfLoglevelMessages.Add(loglevel, 0);
                AddLoglevelToRunReport.Add(loglevel, true);
            }

            LogFileHandle = new LogfileWriter { OverwriteLogFile = this.OverwriteLogFile };

            SetDefaultConfiguration();

            FileName = Configuration.LogFileName;
            this.LogLevel = Configuration.LogLevel;
            OverwriteLogFile = Configuration.OverwriteLogFile;

            // setting the default AID logging function
            LogAutomationIdObjectUserFunction =
                (level, obj, message) => LogOneHtmlMessage(level, string.Format("AutomationId: [{0}] - Message: [{1}]", obj.ToString(), message));

            LogFileInitialized = false;

            return true;
        }

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
        /// If no configuration is provided, then create a default one.
        /// </summary>
        private void SetDefaultConfiguration()
        {
            if (Configuration != null)
            {
                return;
            }

            Configuration = new StfLoggerConfiguration();
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
                retVal = retVal.Replace("LOGFILETITLE", Configuration.LogTitle);
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

            if (styleSheet == null)
            {
                return "<error>No styleSheet file found</error>";
            }

            var retVal = styleSheet.ToString();
            retVal = retVal.Replace("#IMAGELOGO#", GetBase64ImageForLogo());

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
            var htmlLine = "<!DOCTYPE html>\n";
            htmlLine += "<html>\n";
            htmlLine += "  <head>\n";
            htmlLine += "    <meta charset=\"utf-8\" />\n";
            htmlLine += string.Format("    <title>{0}</title>\n", Configuration.LogTitle);
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
            LogTestDuration();

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
        private bool InitLogfile()
        {
            var userName = string.Format("{0}\\{1}", Environment.UserDomainName, Environment.UserName);

            if (LogFileInitialized)
            {
                return true;
            }

            OverwriteLogFile = Configuration.OverwriteLogFile;
            TestName = "TestName_Not_Set";
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

            TimeOfFirstLogMessage = DateTime.Now;

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

        /// <summary>
        /// The get base 64 image for logo.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetBase64ImageForLogo()
        {
            byte[] imageAsBytes;
            if (!TryGetImageAsBytes(out imageAsBytes, Configuration.PathToLogoImageFile))
            {
                return "No image for logo available";
            }

            return Convert.ToBase64String(imageAsBytes);
        }

        /// <summary>
        /// The try get bytes from default logo.
        /// </summary>
        /// <param name="imageBytes">
        /// The image bytes.
        /// </param>
        /// <param name="imageFilePath">
        /// The image File Path.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool TryGetImageAsBytes(out byte[] imageBytes, string imageFilePath)
        {
            imageBytes = new byte[64];
            Array.Clear(imageBytes, 0, imageBytes.Length);

            Bitmap image;
            if (!TryGetImageFromFile(out image, imageFilePath))
            {
                image = Resources.StfLogo;
            }
            
            if (image == null)
            {
                return false;
            }

            try
            {
                using (var memStream = new MemoryStream())
                {
                    image.Save(memStream, ImageFormat.Png);
                    imageBytes = memStream.ToArray();
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                image.Dispose();
            }

            return true;
        }

        /// <summary>
        /// The try get bytes from image file.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <param name="imageFilePath">
        /// The image file path.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool TryGetImageFromFile(out Bitmap image, string imageFilePath)
        {
            image = null;
            if (!File.Exists(imageFilePath))
            {
                return false;
            }

            try
            {
                image = new Bitmap(imageFilePath);
            }
            catch (Exception)
            {
                return false;
            }
            
            return true;
        }

        /// <summary>
        /// The get testduration.
        /// </summary>
        /// <param name="duration">
        /// The duration.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool GetTestduration(out TimeSpan duration)
        {
            duration = TimeSpan.FromSeconds(0);

            if (TimeOfFirstLogMessage == default(DateTime))
            {
                return false;
            }

            duration = DateTime.Now - TimeOfFirstLogMessage;

            return true;
        }

        /// <summary>
        /// The log test duration.
        /// </summary>
        private void LogTestDuration()
        {
            TimeSpan duration;
            if (!GetTestduration(out duration))
            {
                return;
            }

            var length = string.Format("{0:%h} hour(s), {0:%m} minute(s), {0:%s} second(s)", duration);
            LogKeyValue("Test duration", length, "Test duration");
        }
    }
}

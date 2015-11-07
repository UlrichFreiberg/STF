// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggingFunctions.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using Mir.Stf.Utilities.Interfaces;

namespace Mir.Stf.Utilities
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// The test result html logger. The <see cref="IStfLoggerLoggingFunctions"/> part.
    /// </summary>
    public partial class StfLogger
    {
        /// <summary>
        /// The _message id.
        /// </summary>
        private int messageId;

        #region "normal logging functions - test scripts/models/adapters"

        /// <summary>
        /// Logging one error <paramref name="message"/>. Something bad has happened.
        /// </summary>
        /// <param name="message">
        /// The message describing the error
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogError(string message)
        {
            return LogOneHtmlMessage(StfLogLevel.Error, message);
        }

        /*
        public int Error(string message)
        {
            return LogError(message);
        }
*/

        /// <summary>
        /// Logging one warning <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogWarning(string message)
        {
            return LogOneHtmlMessage(StfLogLevel.Warning, message);
        }

        /*
        public int Warning(string message)
        {
            return LogWarning(message);
        }
*/

        /// <summary>
        /// Logging one info <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogInfo(string message)
        {
            return LogOneHtmlMessage(StfLogLevel.Info, message);
        }

        /*
        public int Info(string message)
        {
            return LogInfo(message);
        }
*/

        /// <summary>
        /// Logging one debug <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogDebug(string message)
        {
            return LogOneHtmlMessage(StfLogLevel.Debug, message);
        }

        /*
        public int Debug(string message)
        {
            return LogDebug(message);
        }
*/

        /// <summary>
        /// Logging one trace <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogTrace(string message)
        {
            return LogOneHtmlMessage(StfLogLevel.Trace, message);
        }

        /*
                public int Trace(string message)
                {
                    return LogTrace(message);
                }
        */
        #endregion

        #region "Header logging functions - test scripts"

        /// <summary>
        /// Insert a header in the log file - makes it easier to overview the logfile
        /// </summary>
        /// <param name="headerMessage">
        /// The header Message.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogHeader(string headerMessage)
        {
            return LogOneHtmlMessage(StfLogLevel.Header, headerMessage);
        }

        /*
        public int Header(string message)
        {
            return LogHeader(message);
        }
*/

        /// <summary>
        /// Insert a sub header in the log file - makes it easier to overview the logfile
        /// </summary>
        /// <param name="subHeaderMessage">
        /// The sub header message.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogSubHeader(string subHeaderMessage)
        {
            return LogOneHtmlMessage(StfLogLevel.SubHeader, subHeaderMessage);
        }

        /*
        public int SubHeader(string message)
        {
            return LogSubHeader(message);
        }
*/
        #endregion

        #region "normal logging functions - models and adapters"

        /// <summary>
        /// Logging one <c>internal</c> <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogInternal(string message)
        {
            return LogOneHtmlMessage(StfLogLevel.Internal, message);
        }

        /*
        public int Internal(string message)
        {
            return LogInternal(message);
        }
*/
        #endregion

        #region "used solely by Assert functions"

        /// <summary>
        /// Logging a test step assertion did pass.
        /// </summary>
        /// <param name="testStepName">
        /// The test step name.
        /// </param>
        /// <param name="message">
        /// Further information related to the passing test step - if any.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogPass(string testStepName, string message)
        {
            var tempNeedsToBeReworkedMessage = string.Format("TestStepName=[{0}], message=[{1}]", testStepName, message);

            return LogOneHtmlMessage(StfLogLevel.Pass, tempNeedsToBeReworkedMessage);
        }

        /*
        public int Pass(string testStepName, string message)
        {
            return LogPass(testStepName, message);
        }
*/

        /// <summary>
        /// Logging a test step assertion did fail.
        /// </summary>
        /// <param name="testStepName">
        /// The test step name.
        /// </param>
        /// <param name="message">
        /// Further information related to the failing test step - if any.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogFail(string testStepName, string message)
        {
            var tempNeedsToBeReworkedMessage = string.Format("TestStepName=[{0}], message=[{1}]", testStepName, message);
            const StfLogLevel TheLogLevel = StfLogLevel.Fail;
            var length = 0;

            if (Configuration.ScreenshotOnLogFail)
            {
                length = LogScreenshot(TheLogLevel, string.Empty);
            }

            return length + LogOneHtmlMessage(TheLogLevel, tempNeedsToBeReworkedMessage);
        }

        /*
        public int Fail(string testStepName, string message)
        {
            return LogFail(testStepName, message);
        }
*/
        #endregion

        /// <summary>
        /// Logging a KeyValue pair - as you see fit. The logger will log some, that are generally useful - like OS
        /// Machine, CurrentDirectory etc..
        /// </summary>
        /// <param name="key">
        /// The <c>key</c> part of the pair.
        /// </param>
        /// <param name="value">
        /// The <c>value</c> part of the pair.
        /// </param>
        /// <param name="message">
        /// Further information related to the KeyValue pair - if any.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogKeyValue(string key, string value, string message)
        {
            string htmlLine;

            htmlLine = string.Format("<div class=\"line keyvalue\">\n");
            htmlLine += string.Format("   <div class=\"el key\">{0}</div>\n", key);
            htmlLine += string.Format("   <div class=\"el value\">{0}</div>\n", value);
            htmlLine += string.Format("   <div class=\"el msg\">{0}</div>\n", message);
            htmlLine += string.Format("</div>\n");

            this.LogFileHandle.Write(htmlLine);
            return htmlLine.Length;
        }

        /// <summary>
        /// The get next message id.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetNextMessageId()
        {
            return string.Format("m{0}", this.messageId++);
        }

        /// <summary>
        /// The log one html Message.
        /// </summary>
        /// <param name="loglevel">
        /// The log level.
        /// </param>
        /// <param name="message">
        /// The Message.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private int LogOneHtmlMessage(StfLogLevel loglevel, string message)
        {
            ResetIdleTimer();

            string htmlLine;

            if (!this.AddLoglevelToRunReport[loglevel])
            {
                return -1;
            }

            if (messageId == 0)
            {
                if (!InitLogfile())
                {
                    Console.WriteLine(@"Coulnd't initialise the logfile");
                }
            }

            if (!LogFileHandle.Initialized)
            {
                return -2;
            }

            var messageIdString = GetNextMessageId();
            var logLevelString = Enum.GetName(typeof(StfLogLevel), loglevel) ?? "Unknown StfLogLevel";

            logLevelString = logLevelString.ToLower();

            CheckForPerformanceAlert();

            if (Configuration.MapNewlinesToBr)
            {
                var count = message.Count(f => f == '\n');
                if (count > 1)
                {
                    var indexOfFirstLine = message.IndexOf(Environment.NewLine, StringComparison.Ordinal);
                    if (indexOfFirstLine > 0)
                    {
                        var multilineId = string.Format("multiLineId_{0}", messageIdString);
                        var firstLine = message.Substring(0, indexOfFirstLine);
                        var restOfMessage = message.Substring(indexOfFirstLine);

                        var multiLineSection =
                            string.Format(
                                "<a class=\"left\" href=\"javascript:toggle_messege('{0}')\" id='href_about'> {1} </a>",
                                multilineId,
                                firstLine);
                        multiLineSection += string.Format("    <div id='{0}' class='hide' style=\"display:none;\">", multilineId);
                        multiLineSection += restOfMessage.Replace("\n", "<br/>");
                        multiLineSection += "    </div>";

                        message = multiLineSection;
                    }
                }
            }

            switch (loglevel)
            {
                case StfLogLevel.Header:
                    htmlLine = string.Format("<div class=\"line logheader\">{0}</div>\n", message);
                    break;
                case StfLogLevel.SubHeader:
                    htmlLine = string.Format("<div class=\"line logsubheader\">{0}</div>\n", message);
                    break;

                default:
                    htmlLine = string.Format(
                        "<div onclick=\"sa('{0}')\" id=\"{0}\" class=\"line {1} \">\n",
                        messageIdString,
                        logLevelString);
                    htmlLine += string.Format(
                        "    <div class=\"el time\">{0}</div>\n",
                        this.timeOfLastMessage.ToString("HH:mm:ss"));
                    htmlLine += string.Format("    <div class=\"el level\">{0}</div>\n", logLevelString);
                    htmlLine += string.Format("    <div class=\"el pad\">{0}</div>\n", IndentString());
                    htmlLine += string.Format("    <div class=\"el msg\">{0}</div>\n", message);
                    htmlLine += string.Format("</div>\n");
                    break;
            }

            NumberOfLoglevelMessages[loglevel]++;
            this.LogFileHandle.Write(htmlLine);
            return htmlLine.Length;
        }
    }
}

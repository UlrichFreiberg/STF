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
    using System.IO;
    using System.Text;
    using System.Xml;

    using Mir.Stf.Utilities.Utils;

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
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogError(string message, params object[] args)
        {
            return LogOneHtmlMessage(StfLogLevel.Error, message, args);
        }

        /// <summary>
        /// Logging one warning <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogWarning(string message, params object[] args)
        {
            return LogOneHtmlMessage(StfLogLevel.Warning, message, args);
        }

        /// <summary>
        /// Logging one info <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogInfo(string message, params object[] args)
        {
            return LogOneHtmlMessage(StfLogLevel.Info, message, args);
        }

        /// <summary>
        /// The log xml message.
        /// </summary>
        /// <param name="xmlMessage">
        /// The xml message.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogXmlMessage(string xmlMessage)
        {
            var formatedXml = PrettyPrint(xmlMessage);

            return LogOneHtmlMessage(StfLogLevel.Info, formatedXml, null);                        
        }

        /// <summary>
        /// Logging one debug <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogDebug(string message, params object[] args)
        {
            return LogOneHtmlMessage(StfLogLevel.Debug, message, args);
        }

        /// <summary>
        /// Logging one trace <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogTrace(string message, params object[] args)
        {
            return LogOneHtmlMessage(StfLogLevel.Trace, message, args);
        }

        #endregion

        #region "Header logging functions - test scripts"

        /// <summary>
        /// Insert a header in the log file - makes it easier to overview the logfile
        /// </summary>
        /// <param name="headerMessage">
        /// The header Message.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogHeader(string headerMessage, params object[] args)
        {
            return LogOneHtmlMessage(StfLogLevel.Header, headerMessage, args);
        }

        /// <summary>
        /// Insert a sub header in the log file - makes it easier to overview the logfile
        /// </summary>
        /// <param name="subHeaderMessage">
        /// The sub header message.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogSubHeader(string subHeaderMessage, params object[] args)
        {
            return LogOneHtmlMessage(StfLogLevel.SubHeader, subHeaderMessage, args);
        }

        #endregion

        #region "normal logging functions - models and adapters"

        /// <summary>
        /// Logging one <c>internal</c> <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogInternal(string message, params object[] args)
        {
            return LogOneHtmlMessage(StfLogLevel.Internal, message, args);
        }

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
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogPass(string testStepName, string message, params object[] args)
        {
            message = message.StfFormatString(args);

            var tempNeedsToBeReworkedMessage = $"TestStepName=[{testStepName}], message=[{message}]";

            return LogOneHtmlMessage(StfLogLevel.Pass, tempNeedsToBeReworkedMessage);
        }

        /// <summary>
        /// Logging a test step assertion did fail.
        /// </summary>
        /// <param name="testStepName">
        /// The test step name.
        /// </param>
        /// <param name="message">
        /// Further information related to the failing test step - if any.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogFail(string testStepName, string message, params object[] args)
        {
            message = message.StfFormatString(args);

            const StfLogLevel TheLogLevel = StfLogLevel.Fail;
            var tempNeedsToBeReworkedMessage = $"TestStepName=[{testStepName}], message=[{message}]";
            var length = LogOneHtmlMessage(TheLogLevel, tempNeedsToBeReworkedMessage);

            if (Configuration.ScreenshotOnLogFail)
            {
                length = LogScreenshot(TheLogLevel, string.Empty);
            }

            return length;
        }

        /// <summary>
        /// The log inconclusive.
        /// </summary>
        /// <param name="testStepName">
        /// The test Step Name.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogInconclusive(string testStepName, string message, params object[] args)
        {
            message = message.StfFormatString(args);

            var tempNeedsToBeReworkedMessage = $"TestStepName=[{testStepName}], message=[{message}]";

            return LogOneHtmlMessage(StfLogLevel.Inconclusive, tempNeedsToBeReworkedMessage);
        }

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
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogKeyValue(string key, string value, string message, params object[] args)
        {
            message = message.StfFormatString(args);
            
            var htmlLine = "<div class=\"line keyvalue\">\n";
            htmlLine += $"   <div class=\"el key\">{key}</div>\n";
            htmlLine += $"   <div class=\"el value\">{value}</div>\n";
            htmlLine += $"   <div class=\"el msg\">{message}</div>\n";
            htmlLine += "</div>\n";

            LogFileHandle.Write(htmlLine);
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
            return $"m{messageId++}";
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
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private int LogOneHtmlMessage(StfLogLevel loglevel, string message, params object[] args)
        {
            ResetIdleTimer();

            string htmlLine;

            if (!AddLoglevelToRunReport[loglevel])
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
            var convertedToFoldableMessage = false;

            logLevelString = logLevelString.ToLower();

            CheckForPerformanceAlert();

            message = message.StfFormatString(args);

            if (Configuration.MapNewlinesToBr)
            {
                var count = message.Count(f => f == '\n');
                if (count > 1)
                {
                    var firstLineIndexOf = message.IndexOf(Environment.NewLine, StringComparison.Ordinal);

                    if (firstLineIndexOf > 0)
                    {
                        var firstLineOfMessage = message.Substring(0, firstLineIndexOf);
                        var firstLoggedLine =
                            $"{Environment.NewLine}{System.Security.SecurityElement.Escape(firstLineOfMessage)}";

                        message = GetMultilineMessage(messageIdString, firstLoggedLine, message);
                        convertedToFoldableMessage = true;
                    }
                }
            }

            if (ShouldMessageBeConvertedToClickable(message) && !convertedToFoldableMessage)
            {
                // todo: Implement :-)
            }

            switch (loglevel)
            {
                case StfLogLevel.Header:
                    htmlLine = $"<div class=\"line logheader\">{message}</div>\n";
                    break;
                case StfLogLevel.SubHeader:
                    htmlLine = $"<div class=\"line logsubheader\">{message}</div>\n";
                    break;

                default:
                    htmlLine = string.Format(
                        "<div onclick=\"sa('{0}')\" id=\"{0}\" class=\"line {1} \">\n",
                        messageIdString,
                        logLevelString);
                    htmlLine += $"    <div class=\"el time\">{timeOfLastMessage:HH:mm:ss}</div>\n";
                    htmlLine += $"    <div class=\"el level\">{logLevelString}</div>\n";
                    htmlLine += $"    <div class=\"el pad\">{IndentString()}</div>\n";
                    htmlLine += $"    <div class=\"el msg\">{message}</div>\n";
                    htmlLine += "</div>\n";
                    break;
            }

            NumberOfLoglevelMessages[loglevel]++;
            LogFileHandle.Write(htmlLine);
            return htmlLine.Length;
        }

        /// <summary>
        /// The pretty print.
        /// See http://stackoverflow.com/questions/1123718/format-xml-string-to-print-friendly-xml-string
        /// </summary>
        /// <param name="xml">
        /// The xml.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string PrettyPrint(string xml)
        {
            var result = string.Empty;

            var memoryStream = new MemoryStream();
            var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.Unicode);
            var xmlDocument = new XmlDocument();

            try
            {
                // Load the XmlDocument with the XML.
                xmlDocument.LoadXml(xml);

                xmlTextWriter.Formatting = Formatting.Indented;

                // Write the XML into a formatting XmlTextWriter
                xmlDocument.WriteContentTo(xmlTextWriter);
                xmlTextWriter.Flush();
                memoryStream.Flush();

                // Have to rewind the MemoryStream in order to read
                // its contents.
                memoryStream.Position = 0;

                // Read MemoryStream contents into a StreamReader.
                var streamReader = new StreamReader(memoryStream);

                // Extract the text from the StreamReader.
                var formattedXml = streamReader.ReadToEnd();

                result = formattedXml;
            }
            catch (XmlException ex)
            {
                LogError("You tried to log an XML message - something is not right: [{0}]", ex.Message);
            }

            memoryStream.Close();
            xmlTextWriter.Close();

            return result;
        }

        /// <summary>
        /// The get multiline message.
        /// </summary>
        /// <param name="messageIdString">
        /// The message Id String.
        /// </param>
        /// <param name="firstLoggedLine">
        /// The first Logged Line.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetMultilineMessage(string messageIdString, string firstLoggedLine, string message)
        {
            var multilineId = $"multiLineId_{messageIdString}";
            var retVal =
                string.Format(
                    "<a class=\"left\" href=\"javascript:toggle_messege('{0}')\" id='href_about'> {1} " 
                    + "</a>"
                    + "    <div id='{0}' class='hide' style=\"display:none;\">" 
                    + "       </br>" 
                    + "       <xmp>"
                    + "{2}" 
                    + "       </xmp>" 
                    + "    </div>",
                    multilineId,
                    firstLoggedLine,
                    message);

            return retVal;
        }

        /// <summary>
        /// The should message be converted to clickable.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool ShouldMessageBeConvertedToClickable(string message)
        {
            if (Configuration.ConvertToFoldableThreshold == -1 || string.IsNullOrEmpty(message))
            {
                return false;
            }

            var retval = message.Length > Configuration.ConvertToFoldableThreshold;

            return retval;
        }
    }
}

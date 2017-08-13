// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScreenShots.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Windows.Forms;
using Mir.Stf.Utilities.Utils;

namespace Mir.Stf.Utilities
{
    /// <summary>
    /// The test result html logger.
    /// </summary>
    public partial class StfLogger
    {
        /// <summary>
        /// The utilities.
        /// </summary>
        private ScreenshotUtilities utilities;

        /// <summary>
        /// Gets the utilities.
        /// </summary>
        private ScreenshotUtilities Utilities => utilities ?? (utilities = new ScreenshotUtilities(this));

        /// <summary>
        /// The log all windows.
        /// </summary>
        /// <param name="stfLogLevel">
        /// The log level.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogAllWindows(StfLogLevel stfLogLevel, string message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The log screenshot.
        /// </summary>
        /// <param name="stfLogLevel">
        /// The log level.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogScreenshot(StfLogLevel stfLogLevel, string message)
        {
            var length = 0;

            foreach (var screen in Screen.AllScreens)
            {
                length += LogOneImage(stfLogLevel, Utilities.DoScreenshot(screen.Bounds), message);

                if (length < 0)
                {
                    break;
                }
            }

            return length;
        }

        /// <summary>
        /// The log image.
        /// </summary>
        /// <param name="stfLogLevel">
        /// The log level.
        /// </param>
        /// <param name="imageFile">
        /// The image file.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogImage(StfLogLevel stfLogLevel, string imageFile, string message)
        {
            return LogOneImage(stfLogLevel, imageFile, message);
        }

        /// <summary>
        /// The log one image.
        /// </summary>
        /// <param name="level">
        /// The level.
        /// </param>
        /// <param name="imageFile">
        /// The image file.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private int LogOneImage(StfLogLevel level, string imageFile, string message)
        {
            if (!AddLoglevelToRunReport[level])
            {
                return -1;
            }

            var messageIdString = GetNextMessageId();
            var logLevelString = Enum.GetName(typeof(StfLogLevel), level) ?? "Unknown StfLogLevel";

            logLevelString = logLevelString.ToLower();

            var html = string.Format("<div onclick=\"sa('{0}')\" id=\"{0}\" class=\"line {1} image\">\n", messageIdString, logLevelString);

            html += $"    <div class=\"el time\">{DateTime.Now:HH:mm:ss}</div>\n";
            html += $"    <div class=\"el level\">{logLevelString}</div>\n";
            html += $"    <div class=\"el msg\">{message}</div>\n";
            html += $"    <p><img  onclick=\"showImage(this)\" class=\"embeddedimage\" alt=\"{message}\" src=\"data:image/png;base64, {imageFile}\" /></p>";
            html += "</div>\n";

            LogFileHandle.Write(html);

            return html.Length;
        }
    }
}

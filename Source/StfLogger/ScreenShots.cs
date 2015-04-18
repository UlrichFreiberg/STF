// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScreenShots.cs" company="Foobar">
//   2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Stf.Utilities
{
    using System;
    using System.Windows.Forms;

    using Stf.Utilities.Interfaces;
    using Stf.Utilities.Utils;

    /// <summary>
    /// The test result html logger.
    /// </summary>
    public partial class StfLogger : IScreenshots
    {
        /// <summary>
        /// The utilities.
        /// </summary>
        private ScreenshotUtilities utilities;

        /// <summary>
        /// Gets the utilities.
        /// </summary>
        private ScreenshotUtilities Utilities 
        {
            get { return utilities ?? (utilities = new ScreenshotUtilities(this)); }
        }

        /// <summary>
        /// The log all windows.
        /// </summary>
        /// <param name="level">
        /// The log level.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogAllWindows(StfLogLevel level, string message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The log screenshot.
        /// </summary>
        /// <param name="level">
        /// The log level.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int LogScreenshot(StfLogLevel level, string message)
        {
            var length = 0;
            foreach (var screen in Screen.AllScreens)
            {
                length += LogOneImage(level, Utilities.DoScreenshot(screen.Bounds), message);
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
        /// <param name="level">
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
        public int LogImage(StfLogLevel level, string imageFile, string message)
        {
            return LogOneImage(level, imageFile, message);
        }

        /// <summary>
        /// The log one image.
        /// </summary>
        /// <param name="level">
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
        private int LogOneImage(StfLogLevel level, string imageFile, string message)
        {
            if (!this.AddLoglevelToRunReport[level])
            {
                return -1;
            }

            var messageIdString = GetNextMessageId();
            var logLevelString = Enum.GetName(typeof(StfLogLevel), level);

            var html = string.Format("<div onclick=\"sa('{0}')\" id=\"{0}\" class=\"line info image\">", messageIdString);
            html += string.Format("    <div class=\"el time\">{0}</div>", DateTime.Now.ToString("HH:mm:ss"));
            html += string.Format("    <div class=\"el level\">{0}</div>", logLevelString);
            html += string.Format("    <div class=\"el msg\">{0}</div>", message);
            html += string.Format("    <p><img  onclick=\"showImage(this)\" class=\"embeddedimage\" alt=\"{0}\" src=\"data:image/png;base64, {1}\"</p>", message, imageFile);
            html += "</div>";

            this.LogFileHandle.Write(html);

            return html.Length;
        }
    }
}

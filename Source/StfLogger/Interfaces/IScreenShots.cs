﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IScreenShots.cs" company="Foobar">
//   2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Stf.Utilities.Interfaces
{
    /// <summary>
    /// The Screenshots interface.
    /// </summary>
    public interface IScreenshots
    {
        /// <summary>
        /// The log all windows.
        /// </summary>
        /// <param name="logLevel">
        /// The log level.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogAllWindows(LogLevel logLevel, string message);

        /// <summary>
        /// The log screenshot.
        /// </summary>
        /// <param name="logLevel">
        /// The log level.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogScreenshot(LogLevel logLevel, string message);

        /// <summary>
        /// The log image.
        /// </summary>
        /// <param name="logLevel">
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
        int LogImage(LogLevel logLevel, string imageFile, string message);
    }
}

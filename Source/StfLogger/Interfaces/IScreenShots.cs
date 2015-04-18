// --------------------------------------------------------------------------------------------------------------------
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
        /// <param name="stfLogLevel">
        /// The log level.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int LogAllWindows(StfLogLevel stfLogLevel, string message);

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
        int LogScreenshot(StfLogLevel stfLogLevel, string message);

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
        int LogImage(StfLogLevel stfLogLevel, string imageFile, string message);
    }
}

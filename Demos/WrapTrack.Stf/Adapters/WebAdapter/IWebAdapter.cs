// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWebAdapter.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The WebAdapter interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WrapTrack.Stf.Adapters.WebAdapter
{
    using System;
    using System.Collections.Generic;
    using Configuration;

    using Mir.Stf.Utilities;
    using Mir.Stf.Utilities.Attributes;

    using OpenQA.Selenium;

    /// <summary>
    /// The WebAdapter interface.
    /// </summary>
    [StfInterfaceLogLevel(StfLogLevel.Internal)]
    public interface IWebAdapter : IStfPlugin
    {
        /// <summary>
        /// Gets or sets the configuration
        /// </summary>
        WebAdapterConfiguration Configuration { get; set; }

        /// <summary>
        /// The open url.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool OpenUrl(string url);

        /// <summary>
        /// The find element.
        /// </summary>
        /// <param name="by">
        /// The by.
        /// </param>
        /// <returns>
        /// The <see cref="IWebElement"/>.
        /// </returns>
        IWebElement FindElement(By by);

        /// <summary>
        /// The find element using an explicit wait timeout
        /// </summary>
        /// <param name="by">
        /// The by.
        /// </param>
        /// <param name="secondsToWait">
        /// The max number of seconds to wait.
        /// </param>
        /// <returns>
        /// The <see cref="IWebElement"/>.
        /// </returns>
        IWebElement FindElement(By by, int secondsToWait);

        /// <summary>
        /// The find elements.
        /// </summary>
        /// <param name="by">
        /// The by.
        /// </param>
        /// <returns>
        /// The collection of elements that matches the search term.
        /// </returns>
        IReadOnlyCollection<IWebElement> FindElements(By by);

        /// <summary>
        /// The get text by by. Returns the text - if element not found string.empty is returned
        /// </summary>
        /// <param name="by">
        /// The by.
        /// </param>
        /// <param name="secondsToWait">
        /// The seconds To Wait.
        /// </param>
        /// <param name="depth">
        /// Only used internally to stir out of endless recursion
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetText(By by, int secondsToWait = 1, int depth = 0);

        /// <summary>
        /// The click.
        /// </summary>
        /// <param name="by">
        /// The by.
        /// </param>
        /// <param name="depth">
        /// Only used internally to stir out of endless recursion
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool Click(By by, int depth = 0);

        /// <summary>
        /// The set implicitly wait.
        /// </summary>
        /// <param name="seconds">
        /// The seconds.
        /// </param>
        void SetImplicitlyWait(int seconds);

        /// <summary>
        /// The set implicitly wait.
        /// </summary>
        /// <param name="timeSpan">
        /// The time span.
        /// </param>
        void SetImplicitlyWait(TimeSpan timeSpan);

        /// <summary>
        /// The reset implicitly wait. Sets the value according to the configuration
        /// </summary>
        void ResetImplicitlyWait();

        /// <summary>
        /// The close down.
        /// </summary>
        void CloseDown();

        /// <summary>
        /// The wait for complete.
        /// </summary>
        /// <param name="seconds">
        /// The seconds.
        /// </param>
        void WaitForComplete(int seconds);
    }
}
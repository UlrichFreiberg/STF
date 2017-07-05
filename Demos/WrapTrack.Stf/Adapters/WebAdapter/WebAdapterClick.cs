// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebAdapterClick.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the WebAdapter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WrapTrack.Stf.Adapters.WebAdapter
{
    using System;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;

    /// <summary>
    /// The web adapter.
    /// </summary>
    public partial class WebAdapter
    {
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
        public bool Click(By by, int depth = 0)
        {
            var button = FindElement(by);
            bool retVal;

            if (button == null || depth >= 5)
            {
                return false;
            }

            try
            {
                button.Click();
                retVal = true;
            }
            catch (Exception ex)
            {
                StfLogger.LogInternal("Click failed - Have to reclick - got exception [{0}]", ex.Message);

                // if we cant get to click the button, then the UI has changed (like we did press search, but the page wasn't rendered fully yet)
                retVal = Click(by, depth + 1);
            }

            return retVal;
        }

        /// <summary>
        /// The double click element.
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool DoubleClickElement(IWebElement element)
        {
            var retVal = false;

            if (element == null)
            {
                return false;
            }

            try
            {
                var actions = new Actions(WebDriver);

                actions.DoubleClick(element).Build().Perform();

                retVal = true;
            }
            catch (Exception ex)
            {
                StfLogger.LogDebug("Caught error double clicking at element. Error: [{0}]", ex.Message);
            }

            return retVal;
        }
    }
}
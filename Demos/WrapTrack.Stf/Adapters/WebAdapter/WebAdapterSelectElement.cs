// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebAdapterSelectElement.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the WebAdapter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WrapTrack.Stf.Adapters.WebAdapter
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    /// <summary>
    /// The web adapter.
    /// </summary>
    public partial class WebAdapter
    {
        /// <summary>
        /// The select element.
        /// </summary>
        /// <param name="by">
        /// The by.
        /// </param>
        /// <returns>
        /// The <see cref="SelectElement"/>.
        /// </returns>
        public SelectElement SelectElement(By by)
        {
            var elem = FindElement(by);
            if (elem == null)
            {
                return null;
            }

            var retVal = new SelectElement(elem);

            return retVal;
        }

        /// <summary>
        /// Get the text for a SelectElement
        /// </summary>
        /// <param name="by">
        /// The By
        /// </param>
        /// <returns>
        /// The text
        /// </returns>
        public string SelectElementGetText(By by)
        {
            var elem = SelectElement(by);
            var retVal = elem != null ? elem.ToString() : string.Empty;

            return retVal;
        }

        /// <summary>
        /// Sets a selectElement using a text/string
        /// </summary>
        /// <param name="by">
        /// the By
        /// </param>
        /// <param name="value">
        /// The option text
        /// </param>
        /// <returns>
        /// indication whether if went well or not
        /// </returns>
        public bool SelectElementSetText(By by, string value)
        {
            var elem = SelectElement(by);

            if (elem == null)
            {
                return false;
            }

            elem.SelectByText(value);
            return true;
        }
   }
}
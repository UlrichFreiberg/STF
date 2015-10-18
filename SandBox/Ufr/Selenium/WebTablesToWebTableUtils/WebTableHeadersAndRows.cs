// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebTableHeadersAndRows.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebTablesToWebTableUtils
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    using OpenQA.Selenium;
    using OpenQA.Selenium.IE;

    /// <summary>
    /// Util to get header and rows for a specific web table layout
    /// </summary>
    public class WebTableHeadersAndRows
    {
        /// <summary>
        /// Gets or sets the Selenium Driver
        /// </summary>
        private IWebDriver Driver { get; set; }

        /// <summary>
        /// Init the class with a URL to the table
        /// </summary>
        /// <param name="url">
        /// The URL to the table
        /// </param>
        /// <returns>
        /// Success or not
        /// </returns>
        public bool Init(string url)
        {
            Driver = new InternetExplorerDriver();
            Driver.Navigate().GoToUrl(url);

            return true;
        }

        /// <summary>
        /// Find the headers
        /// </summary>
        /// <returns>
        /// a list of elements for a header
        /// </returns>
        public List<string> FindHeaders()
        {
            var xpathHeaders = Driver.FindElements(By.XPath("//div[@role = 'columnheader']/span"));
            var retVal = new List<string>();
            const string RegExpr = ">(?<header>[^<]+)<";

            foreach (var header in xpathHeaders)
            {
                var text = header.Text.Trim();
                var match = Regex.Match(text, RegExpr);

                if (!match.Success)
                {
                    continue;
                }

                var headerText = match.Groups["header"].Value;
                headerText = headerText.Replace("\r\n", string.Empty).Replace(" ", string.Empty);
                retVal.Add(headerText);
            }

            return retVal;
        }

        /// <summary>
        /// Find all rows for the table
        /// </summary>
        /// <returns>
        /// a list of rows. One row is a list of elements
        /// </returns>
        public List<List<string>> FindAllRows()
        {
            var xpathRows = Driver.FindElements(By.XPath("//tbody[@role = 'presentation']"));
            var retVal = new List<List<string>>();
            const string RegExpr = ">(?<cell>[^<]+)<";
 
            foreach (var xpathRow in xpathRows)
            {
                // a DIV node without any descendents
                var rowElements = xpathRow.FindElements(By.XPath("tr/td/div/self::node()[not(child::div)]"));
                var row = new List<string>();

                foreach (var rowElement in rowElements)
                {
                    var text = rowElement.Text.Trim();
                    var match = Regex.Match(text, RegExpr);
                    var cellText = string.Empty;

                    if (match.Success)
                    {
                        cellText = match.Groups["cell"].Value;
                        cellText = cellText.Replace("\r\n", string.Empty);
                    }

                    row.Add(cellText);
                }
                
                retVal.Add(row);
            }

            return retVal;
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DemoCorpWebShell.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the DemoCorpWebShell type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace DemoCorp.Stf.DemoCorpWeb
{
    using DemoCorp.Stf.Adapters.WebAdapter;

    using Mir.Stf.Utilities;
    using Mir.Stf.Utilities.Interfaces;

    using OpenQA.Selenium;

    /// <summary>
    /// The demo corp web shell.
    /// </summary>
    public class DemoCorpWebShell : IDemoCorpWebShell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DemoCorpWebShell"/> class. 
        /// </summary>
        public DemoCorpWebShell()
        {
            Name = "DemoCorpWebShell";
            VersionInfo = new Version(1, 0, 0, 0);
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the version info.
        /// </summary>
        public Version VersionInfo { get; private set; }

        /// <summary>
        /// Gets or sets the stf container.
        /// </summary>
        public IStfContainer StfContainer { get; set; }

        /// <summary>
        /// Gets or sets the stf logger.
        /// </summary>
        public IStfLogger StfLogger { get; set; }

        /// <summary>
        /// Gets or sets the web adapter.
        /// </summary>
        private IWebAdapter WebAdapter { get; set; }

        /// <summary>
        /// The learn more.
        /// </summary>
        /// <returns>
        /// The <see cref="ILearnMore"/>.
        /// </returns>
        public ILearnMore LearnMore()
        {
            // Press learn more
            var but = WebAdapter.FindElement(By.Id("LearnMore"));
            but.Click();

            var retVal = StfContainer.Get<ILearnMore>();

            return retVal;
        }

        /// <summary>
        /// The init.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Init()
        {
            // register my needed types
            StfContainer.RegisterType<ILearnMore, LearnMore>();

            // get what I need - a WebAdapter:-)
            WebAdapter = StfContainer.Get<IWebAdapter>();

            WebAdapter.OpenUrl("http://democorpweb.azurewebsites.net/");

            return true;
        }
    }
}

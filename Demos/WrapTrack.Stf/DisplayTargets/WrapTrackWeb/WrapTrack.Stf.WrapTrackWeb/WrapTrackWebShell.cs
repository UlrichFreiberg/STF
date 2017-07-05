// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WrapTrackWebShell.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the WrapTrackWebShell type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace WrapTrack.Stf.WrapTrackWeb
{
    using Mir.Stf.Utilities;
    using Mir.Stf.Utilities.Interfaces;

    using OpenQA.Selenium;

    using WrapTrack.Stf.Adapters.WebAdapter;

    /// <summary>
    /// The demo corp web shell.
    /// </summary>
    public class WrapTrackWebShell : IWrapTrackWebShell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WrapTrackWebShell"/> class. 
        /// </summary>
        public WrapTrackWebShell()
        {
            Name = "WrapTrackWebShell";
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
        /// The <see cref="IMinSamling"/>.
        /// </returns>
        /// <summary>
        /// The login.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        public bool Login(string userName, string password)
        {
             var loginTabElem = WebAdapter.FindElement(By.Id("nav_login"));

            loginTabElem.Click();

            var userNameElem = WebAdapter.FindElement(By.Id("input_username"));

            userNameElem.SendKeys(userName);

            var passwordElem = WebAdapter.FindElement(By.Id("input_pw"));

            passwordElem.SendKeys(password);

            var loginButtonElem = WebAdapter.FindElement(By.Id("nav_"));

            loginButtonElem.Click();

            return true;
        }

        /// <summary>
        /// The min samling.
        /// </summary>
        /// <returns>
        /// The <see cref="IMinSamling"/>.
        /// </returns>
        public IMinSamling MinSamling()
        {
            // Press learn more
            var but = WebAdapter.FindElement(By.Id("## MISSING ##"));
            but.Click();

            var retVal = StfContainer.Get<IMinSamling>();

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
            StfContainer.RegisterType<IMinSamling, MinSamling>();

            // get what I need - a WebAdapter:-)
            WebAdapter = StfContainer.Get<IWebAdapter>();
            WebAdapter.OpenUrl("http://WrapTrack.org/");

            var currentDomainBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            StfLogger.LogKeyValue("Current Directory", currentDomainBaseDirectory, "Current Directory");
            Login("ida88", "lk8dsafpUqwe");

            return true;
        }

        /// <summary>
        /// The learn more.
        /// </summary>
        /// <returns>
        /// The <see cref="IMinSamling"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public IMinSamling LearnMore()
        {
            throw new NotImplementedException();
        }
    }
}

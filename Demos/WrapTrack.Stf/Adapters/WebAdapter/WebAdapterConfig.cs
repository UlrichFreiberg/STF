// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebAdapterConfig.cs" company="Mir Software">
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

    using Configuration;

    using Mir.Stf.Utilities;

    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.IE;

    /// <summary>
    /// The web adapter.
    /// </summary>
    public partial class WebAdapter
    {
        /// <summary>
        /// Gets or sets the configuration
        /// </summary>
        public WebAdapterConfiguration Configuration { get; set; }

        /// <summary>
        /// Get the web adapter configuration
        /// </summary>
        /// <returns>
        /// Instance of web adapter configuration 
        /// </returns>
        private WebAdapterConfiguration GetConfiguration()
        {
            WebAdapterConfiguration retVal;

            try
            {
                var stfConfiguration = StfContainer.Get<StfConfiguration>();

                retVal = new WebAdapterConfiguration();

                stfConfiguration.LoadUserConfiguration(retVal);
            }
            catch (Exception ex)
            {
                StfLogger.LogInternal($"Couldn't GetConfiguration - got error [{ex.Message}]");
                retVal = null;
            }

            return retVal;
        }

        /// <summary>
        /// The get internet explorer options.
        /// </summary>
        /// <returns>
        /// The <see cref="InternetExplorerOptions"/>.
        /// </returns>
        private InternetExplorerOptions GetInternetExplorerOptions()
        {
            var retVal = new InternetExplorerOptions { IntroduceInstabilityByIgnoringProtectedModeSettings = Configuration.IgnoreProtectedModeSettings };

            LogUnsafeDriverSettings(retVal);

            return retVal;
        }

        private ChromeOptions GetChromeOptionsOptions()
        {
            var retVal = new ChromeOptions();

            return retVal;
        }
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebAdapterConfig.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the WebAdapter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DemoCorp.Stf.Adapters.WebAdapter
{
    using System;

    using Configuration;

    using Mir.Stf.Utilities;

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

            //// TODO: We should receive a browsername from the displaytarget using this webadapter
            ////       so we can create the correct configuration object. Currently we support IE and IE only

            try
            {
                var stfConfiguration = StfContainer.Get<StfConfiguration>();
                retVal = new WebAdapterConfiguration();

                stfConfiguration.LoadUserConfiguration(retVal);
            }
            catch (Exception ex)
            {
                StfLogger.LogInternal(string.Format("Couldn't GetConfiguration - got error [{0}]", ex.Message));
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
   }
}
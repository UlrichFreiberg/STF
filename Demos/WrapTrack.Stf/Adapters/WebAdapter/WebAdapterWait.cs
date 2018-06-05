// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebAdapterWait.cs" company="Mir Software">
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
    using System.Threading;

    /// <summary>
    /// The web adapter.
    /// </summary>
    public partial class WebAdapter
    {
        /// <summary>
        /// The set implicitly wait.
        /// </summary>
        /// <param name="seconds">
        /// The seconds.
        /// </param>
        public void SetImplicitlyWait(int seconds)
        {
            if (seconds == -1)
            {
                seconds = Configuration.WaitForControlReadyTimeout;
            }

            // read more here: http://docs.seleniumhq.org/docs/04_webdriver_advanced.jsp
            SetImplicitlyWait(TimeSpan.FromSeconds(double.Parse(seconds.ToString())));
        }

        /// <summary>
        /// The set implicitly wait.
        /// </summary>
        /// <param name="timeSpan">
        /// The time span.
        /// </param>
        public void SetImplicitlyWait(TimeSpan timeSpan)
        {
            WebDriver.Manage().Timeouts().ImplicitWait = timeSpan;
        }

        /// <summary>
        /// The reset implicitly wait. Sets the value according to the configuration
        /// </summary>
        public void ResetImplicitlyWait()
        {
            SetImplicitlyWait(-1);
        }

        /// <summary>
        /// The wait for complete.
        /// </summary>
        /// <param name="seconds">
        /// The seconds.
        /// </param>
        public void WaitForComplete(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }
   }
}
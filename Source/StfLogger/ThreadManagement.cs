// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThreadManagement.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The stf logger.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Mir.Stf.Utilities.Utils;

namespace Mir.Stf.Utilities
{
    /// <summary>
    /// The stf logger.
    /// </summary>
    public partial class StfLogger
    {
        /// <summary>
        /// Gets or sets the thread utils.
        /// </summary>
        private ThreadUtils ThreadUtils { get; set; }

        /// <summary>
        /// Gets or sets the next idle timer reset.
        /// </summary>
        private DateTime NextIdleTimerReset { get; set; }

        /// <summary>
        /// The reset idle timer.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool ResetIdleTimer()
        {
            if (Configuration.KeepAliveInterval <= 0)
            {
                return false;
            }

            if (NextIdleTimerReset > DateTime.Now)
            {
                return false;
            }

            var retVal = ThreadUtils.ResetIdleTimerForThread();

            NextIdleTimerReset = DateTime.Now.AddMinutes(Convert.ToDouble(Configuration.KeepAliveInterval));

            return retVal;
        }
    }
}

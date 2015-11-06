﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThreadUtils.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The execution state.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;

namespace Mir.Stf.Utilities.Utils
{
    /// <summary>
    /// The execution state.
    /// </summary>
    [Flags]
    public enum EXECUTION_STATE : uint
    {
        /// <summary>
        /// The e s_ awaymod e_ required.
        /// </summary>
        ES_AWAYMODE_REQUIRED = 0x00000040,

        /// <summary>
        /// The e s_ continuous.
        /// </summary>
        ES_CONTINUOUS = 0x80000000,

        /// <summary>
        /// The e s_ displa y_ required.
        /// </summary>
        ES_DISPLAY_REQUIRED = 0x00000002,

        /// <summary>
        /// The e s_ syste m_ required.
        /// </summary>
        ES_SYSTEM_REQUIRED = 0x00000001
    }

    /// <summary>
    /// The thread utils.
    /// </summary>
    public class ThreadUtils
    {
        /// <summary>
        /// The try keep computer alive.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool ResetIdleTimerForThread()
        {
            EXECUTION_STATE state;
            try
            {
                state = SetThreadExecutionState(EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
            }
            catch (Exception)
            {
                return false;
            }

            return state != 0;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);
    }
}
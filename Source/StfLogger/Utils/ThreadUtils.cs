// --------------------------------------------------------------------------------------------------------------------
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
    public enum ExecutionState : uint
    {
        /// <summary>
        /// The e s_ awaymod e_ required.
        /// </summary>
        EsAwaymodeRequired = 0x00000040,

        /// <summary>
        /// The e s_ continuous.
        /// </summary>
        EsContinuous = 0x80000000,

        /// <summary>
        /// The e s_ displa y_ required.
        /// </summary>
        EsDisplayRequired = 0x00000002,

        /// <summary>
        /// The e s_ syste m_ required.
        /// </summary>
        EsSystemRequired = 0x00000001
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
            ExecutionState state;
            try
            {
                state = SetThreadExecutionState(ExecutionState.EsDisplayRequired | ExecutionState.EsSystemRequired | ExecutionState.EsContinuous);
            }
            catch (Exception)
            {
                return false;
            }

            return state != 0;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern ExecutionState SetThreadExecutionState(ExecutionState esFlags);
    }
}

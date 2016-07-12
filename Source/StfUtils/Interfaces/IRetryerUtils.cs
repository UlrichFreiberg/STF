// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRetryerUtils.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the IRetryer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.Interfaces
{
    using System;

    /// <summary>
    /// The Retryer interface.
    /// </summary>
    public interface IRetryerUtils
    {
        /// <summary>
        /// Retry an action for a specific duration
        /// </summary>
        /// <param name="action">
        /// The action to retry
        /// </param>
        /// <param name="duration">
        /// The duration for which to keep trying the action
        /// </param>
        /// <param name="attempts">
        /// The attempts.
        /// </param>
        /// <returns>
        /// Whether the action succeeded or not <see cref="bool"/>.
        /// </returns>
        bool Retry(Action action, TimeSpan duration, int attempts);

        /// <summary>
        /// The retry.
        /// </summary>
        /// <param name="function">
        /// A function delegate that return false, while not done, and true when done
        /// </param>
        /// <param name="duration">
        /// The duration.
        /// </param>
        /// <param name="attempts">
        /// The attempts.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool Retry(Func<bool> function, TimeSpan duration, int attempts);

        /// <summary>
        /// The retry.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <param name="duration">
        /// The duration.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool Retry(Action action, TimeSpan duration);

        /// <summary>
        /// The retry.
        /// </summary>
        /// <param name="function">
        /// A function delegate that return false, while not done, and true when done
        /// </param>
        /// <param name="duration">
        /// The duration.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool Retry(Func<bool> function, TimeSpan duration);

        /// <summary>
        /// The retry.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool Retry(Action action);

        /// <summary>
        /// The retry.
        /// </summary>
        /// <param name="function">
        /// A function delegate that return false, while not done, and true when done
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool Retry(Func<bool> function);
    }
}

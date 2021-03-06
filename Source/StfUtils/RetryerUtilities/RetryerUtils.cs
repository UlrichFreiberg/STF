﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RetryerUtils.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the RetryerUtils type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Mir.Stf.Utilities.RetryerUtilities
{
    using System.Threading;

    using Mir.Stf.Utilities.Interfaces;

    /// <summary>
    /// The retryer utils.
    /// </summary>
    public class RetryerUtils : IRetryerUtils
    {
        /// <summary>
        /// The action. Used when retying using a action
        /// </summary>
        private Action retrierAction;

        /// <summary>
        /// The function. Used when retying using a function
        /// </summary>
        private Func<bool> retrierFunction;

        /// <summary>
        /// Initializes a new instance of the <see cref="RetryerUtils"/> class.
        /// </summary>
        /// <param name="attempts">
        /// The attempts.
        /// </param>
        /// <param name="duration">
        /// The duration.
        /// </param>
        /// <param name="attemptTime">
        /// The wait time.
        /// </param>
        public RetryerUtils(int attempts, TimeSpan duration, TimeSpan attemptTime)
        {
            Attempts = attempts;
            Duration = duration;
            AttemptTime = attemptTime;
            retrierAction = null;
            retrierFunction = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RetryerUtils"/> class.
        /// </summary>
        public RetryerUtils()
        {
            Attempts = 3;
            Duration = TimeSpan.FromSeconds(10);
            AttemptTime = TimeSpan.FromSeconds(3);
            retrierAction = null;
            retrierFunction = null;
        }

        /// <summary>
        /// Gets or sets the duration. How long time (mSec) should pass before we give up
        /// </summary>
        private TimeSpan Duration { get; set; }

        /// <summary>
        /// Gets or sets the number of attempts. How many times should we initiate the action before we give up
        /// </summary>
        private int Attempts { get; set; }

        /// <summary>
        /// Gets or sets the wait time. How long time should pass between each attempt. 
        /// If duration of an attempt is less than wait time, the retryer will wait until WaitTime has passed before initiating the next attempt
        /// </summary>
        private TimeSpan AttemptTime { get; set; }

        /// <summary>
        /// The retry.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <param name="duration">
        /// The duration. How long time (mSec) should pass before we give up
        /// </param>
        /// <param name="attempts">
        /// The attempts. How many times should we initiate the action before we give up
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Retry(Action action, TimeSpan duration, int attempts)
        {
            retrierAction = action;
            Duration = duration;
            Attempts = attempts;
            AttemptTime = BestGuessForAttemptTime();

            var retVal = InternalRetry();
            return retVal;
        }

        /// <summary>
        /// The retry.
        /// </summary>
        /// <param name="function">
        /// The function.
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
        public bool Retry(Func<bool> function, TimeSpan duration, int attempts)
        {
            retrierFunction = function;
            Duration = duration;
            Attempts = attempts;
            AttemptTime = BestGuessForAttemptTime();

            var retVal = InternalRetry();
            return retVal;
        }

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
        public bool Retry(Action action, TimeSpan duration)
        {
            var attempts = BestGuessForAttempts();
            var retVal = Retry(action, duration, attempts);

            return retVal;
        }

        /// <summary>
        /// The retry.
        /// </summary>
        /// <param name="function">
        /// The function.
        /// </param>
        /// <param name="duration">
        /// The duration.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Retry(Func<bool> function, TimeSpan duration)
        {
            var attempts = BestGuessForAttempts();
            var retVal = Retry(function, duration, attempts);

            return retVal;
        }

        /// <summary>
        /// The retry.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Retry(Action action)
        {
            var duration = BestGuessForDuration();
            var retVal = Retry(action, duration);

            return retVal;
        }

        /// <summary>
        /// The retry.
        /// </summary>
        /// <param name="function">
        /// The function.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Retry(Func<bool> function)
        {
            var duration = BestGuessForDuration();
            var retVal = Retry(function, duration);

            return retVal;
        }

        /// <summary>
        /// The internal retry.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool InternalRetry()
        {
            var startTime = DateTime.Now;

            for (var iteration = 1; iteration <= Attempts; iteration++)
            {
                var elapsed = DateTime.Now.Subtract(startTime);

                if (elapsed > Duration)
                {
                    return false;
                }

                var iterationStartTime = DateTime.Now;

                try
                {
                    if (retrierAction != null)
                    {
                        // we can only communicate with an action only by throwing an exception 
                        // if something is not right - meaning for us to go for the next iteration
                        retrierAction();

                        return true;
                    }

                    if (retrierFunction != null)
                    {
                        var retVal = retrierFunction();

                        if (retVal)
                        {
                            // Mom I'm done...
                            return true;
                        }
                    }
                }
                catch (Exception)
                {
                    // Ignore
                }

                var iterationTime = DateTime.Now - iterationStartTime;
                var sleepTime = AttemptTime - iterationTime;

                // the action might have taken more time, than one iteration try (AttemptTime)
                if (sleepTime.TotalMilliseconds > 0)
                {
                    Thread.Sleep(sleepTime);
                }
            }

            return false;
        }

        /// <summary>
        /// The calculate duration. Given whatEver information we got, we set the best suitable duration.
        /// </summary>
        /// <returns>
        /// The <see cref="TimeSpan"/>.
        /// </returns>
        private TimeSpan BestGuessForDuration()
        {
            var ticks = Attempts * AttemptTime.Ticks;

            return TimeSpan.FromTicks(ticks);
        }

        /// <summary>
        /// The calculate attempts. Given whatEver information we got, we set the best suitable number of attempts.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private int BestGuessForAttempts()
        {
            if (Duration < TimeSpan.FromMinutes(1))
            {
                return 10;
            }

            if (Duration < TimeSpan.FromMinutes(10))
            {
                return 50;
            }

            return 200;
        }

        /// <summary>
        /// The calculate wait time. Given whatEver information we got, we set the best suitable waittime.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private TimeSpan BestGuessForAttemptTime()
        {
            if (Attempts <= 0)
            {
                return TimeSpan.Zero;
            }

            var ticks = Math.Abs(Duration.Ticks / Attempts);

            return TimeSpan.FromTicks(ticks);
        }
    }
}

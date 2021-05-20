// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfAssert.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities
{
    using System;
    using Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Utilities;

    /// <summary>
    /// The stf assert.
    /// </summary>
    public partial class StfAssert : IStfAssert
    {
        /// <summary>
        /// Backing field: enable negative testing.
        /// </summary>
        private bool enableNegativeTesting;

        /// <summary>
        /// Backing field: treat errors as warning.
        /// </summary>
        private bool treatFailsAsWarning;

        /// <summary>
        /// Initializes a new instance of the <see cref="StfAssert"/> class.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        public StfAssert(IStfLogger logger) : this()
        {
            AssertLogger = logger;
            LastMessage = "Initialized with logger";
            Stats = new AssertStats();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StfAssert"/> class.
        /// </summary>
        public StfAssert()
        {
            AssertLogger = null;
            LastMessage = "Not Initialized";
        }

        /// <summary>
        /// Gets or sets the assert logger.
        /// </summary>
        public IStfLogger AssertLogger { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether enable negative testing.
        /// </summary>
        public bool EnableNegativeTesting
        {
            get
            {
                return enableNegativeTesting;
            }

            set
            {
                AssertLogger.LogTrace("EnableNegativeTesting set to [{0}]", value);
                enableNegativeTesting = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether treat fails as warning.
        /// Sometimes, we want to fail without failing for real. Like when using the 
        /// Asserter in a retrier context - we want to log errors, 
        /// but errors should not count as errors - the whole point of the retrier
        /// </summary>
        public bool TreatFailsAsWarning
        {
            get
            {
                return treatFailsAsWarning;
            }

            set
            {
                AssertLogger.LogTrace("TreatFailsAsWarning set to [{0}]", value);
                treatFailsAsWarning = value;
            }
        }

        /// <summary>
        /// Gets the current failures.
        /// </summary>
        public int CurrentFailures => Stats.AssertFailedCount;

        /// <summary>
        /// Gets the current passes.
        /// </summary>
        public int CurrentPasses => Stats.AssertPassCount;

        /// <summary>
        /// Gets the current inconclusives.
        /// </summary>
        public int CurrentInconclusives => Stats.AssertInconclusiveCount;

        /// <summary>
        /// Gets the last message.
        /// </summary>
        public string LastMessage { get; private set; }

        /// <summary>
        /// Gets or sets the stats.
        /// </summary>
        private AssertStats Stats { get; set; }

        /// <summary>
        /// Reset all statistics for Error, Warning, pass, fail, and inconclusive. Typically used when retry'ing stuff
        /// </summary>
        /// <returns>Success if able to reset stats</returns>
        public bool ResetStatistics()
        {
            Stats = new AssertStats();
            AssertLogger.LogTrace("Statistics resat as requested");
            AssertLogger.NumberOfLoglevelMessages[StfLogLevel.Error] = 0;
            AssertLogger.NumberOfLoglevelMessages[StfLogLevel.Warning] = 0;
            AssertLogger.NumberOfLoglevelMessages[StfLogLevel.Inconclusive] = 0;
            AssertLogger.NumberOfLoglevelMessages[StfLogLevel.Fail] = 0;
            AssertLogger.NumberOfLoglevelMessages[StfLogLevel.Pass] = 0;

            return true;
        }

        /// <summary>
        /// Asserts that a expression is True
        /// </summary>
        /// <param name="testStep">
        /// Name of the test step in the test script
        /// </param>
        /// <param name="value">
        /// The Value to investigate
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsTrue(string testStep, bool value)
        {
            var retVal = value;
            string msg;

            if (retVal)
            {
                msg = "IsTrue: value True";
                AssertPass(testStep, msg);
            }
            else
            {
                msg = "IsTrue: value False";
                AssertFail(testStep, msg);
            }

            return retVal;
        }

        /// <summary>
        /// Asserts that a expression is True
        /// </summary>
        /// <param name="testStep">
        /// Name of the test step in the test script
        /// </param>
        /// <param name="value">
        /// The Value to investigate
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsFalse(string testStep, bool value)
        {
            var retVal = !value;
            string msg;

            if (retVal)
            {
                msg = "IsFalse: value False";
                AssertPass(testStep, msg);
            }
            else
            {
                msg = "IsFalse: value True";
                AssertFail(testStep, msg);
            }

            return retVal;
        }

        /// <summary>
        /// The is inconclusive.
        /// </summary>
        /// <param name="testStep">
        /// The test Step.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsInconclusive(string testStep, string message)
        {
            AssertLogger.LogInconclusive(testStep, message);
            Stats.AssertInconclusiveCount++;

            if (!enableNegativeTesting)
            {
                throw new AssertInconclusiveException(message);
            }

            return true;
        }

        /// <summary>
        /// Asserts that the supplied throws an exception of a specific type.
        /// </summary>
        /// <param name="testStep">
        /// Name of the test step in the test script.
        /// </param>
        /// <param name="action">
        /// The action that throws an exception.
        /// </param>
        /// <typeparam name="T">
        /// The expected exception.
        /// </typeparam>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool AssertThrows<T>(string testStep, Action action) where T : Exception
        {
            var isExpected = false;
            var expectedTypeName = string.Empty;

            try
            {
                action();
            }
            catch (Exception exception)
            {
                isExpected = exception is T;
                expectedTypeName = exception.GetType().Name;
            }

            var msg = $"AssertThrows: Actual exception [{expectedTypeName}] is";

            if (isExpected)
            {
                msg = $"{msg} of expected type [{typeof(T).Name}]";
                AssertPass(testStep, msg);
            }
            else
            {
                msg = $"{msg} not of expected type [{typeof(T).Name}]";
                AssertFail(testStep, msg);
            }

            return isExpected;
        }

        /// <summary>
        /// Used in testscripts to indicate missing implementation of models or testscripts.
        /// </summary>
        /// <param name="msg">
        /// Details on what is missing
        /// </param>
        /// <returns>
        /// Always returns false.
        /// </returns>
        public bool MissingImplementation(string msg)
        {
            AssertFail("Missing implementation", msg);

            return false;
        }

        /// <summary>
        /// The assert pass.
        /// </summary>
        /// <param name="testStep">
        /// The test step.
        /// </param>
        /// <param name="message">
        /// The Message.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool AssertPass(string testStep, string message)
        {
            AssertLogger.LogPass(testStep, message);
            Stats.AssertPassCount++;

            return true;
        }

        /// <summary>
        /// The assert fail.
        /// </summary>
        /// <param name="testStep">
        /// The test step.
        /// </param>
        /// <param name="message">
        /// The Message.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool AssertFail(string testStep, string message)
        {
            if (TreatFailsAsWarning)
            {
                AssertLogger.LogWarning(testStep, "TreatFailsAsWarning is true, so this AssertFail is not included in the error stats");
            }
            else
            {
                Stats.AssertFailedCount++;
            }

            AssertLogger.LogFail(testStep, message);

            if (!EnableNegativeTesting)
            {
                throw new AssertFailedException(message);
            }

            return true;
        }
    }
}

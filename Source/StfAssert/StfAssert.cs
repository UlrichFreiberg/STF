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

    /// <summary>
    /// The stf assert.
    /// </summary>
    public partial class StfAssert : IStfAssert
    {
        /// <summary>
        /// The m enable negative testing.
        /// </summary>
        private bool enableNegativeTesting;

        /// <summary>
        /// Initializes a new instance of the <see cref="StfAssert"/> class.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        public StfAssert(IStfLogger logger) : this()
        {
            AssertLogger = logger;
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
                AssertLogger.LogTrace(string.Format("EnableNegativeTesting set to [{0}]", value));
                enableNegativeTesting = value;
            }
        }

        /// <summary>
        /// Gets the last message.
        /// </summary>
        public string LastMessage { get; private set; }

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

            var msg = string.Format("AssertThrows: Actual exception [{0}] is", expectedTypeName);
            if (isExpected)
            {
                msg = string.Format("{0} of expected type [{1}]", msg, typeof(T).Name);
                AssertPass(testStep, msg);
            }
            else
            {
                msg = string.Format("{0} not of expected type [{1}]", msg, typeof(T).Name);
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
            AssertLogger.LogFail(testStep, message);

            if (!enableNegativeTesting)
            {
                throw new AssertFailedException(message);
            }

            return true;
        }
    }
}

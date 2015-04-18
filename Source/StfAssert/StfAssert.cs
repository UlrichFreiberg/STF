// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfAssert.cs" company="Foobar">
//   2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Stf.Utilities
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Stf.Utilities.Interfaces;

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
        public StfAssert(StfLogger logger) : this()
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
        public StfLogger AssertLogger { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether enable negative testing.
        /// </summary>
        public bool EnableNegativeTesting
        {
            get
            {
                return this.enableNegativeTesting;
            }

            set
            {
                this.AssertLogger.LogTrace(string.Format("EnableNegativeTesting set to [{0}]", value));
                this.enableNegativeTesting = value;
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
        public bool AssertTrue(string testStep, bool value)
        {
            bool retVal = value;
            string msg;

            if (retVal)
            {
                msg = string.Format("AssertTrue: value True");
                this.AssertPass(testStep, msg);
            }
            else
            {
                msg = string.Format("AssertTrue: value False");
                this.AssertFail(testStep, msg);
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
        public bool AssertFalse(string testStep, bool value)
        {
            bool retVal = !value;
            string msg;

            if (retVal)
            {
                msg = string.Format("AssertFalse: value False");
                this.AssertPass(testStep, msg);
            }
            else
            {
                msg = string.Format("AssertFalse: value True");
                this.AssertFail(testStep, msg);
            }

            return retVal;
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
            this.AssertLogger.LogPass(testStep, message);
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
            this.AssertLogger.LogFail(testStep, message);

            if (!enableNegativeTesting)
            {
                throw new AssertFailedException(message);
            }

            return true;
        }
    }
}

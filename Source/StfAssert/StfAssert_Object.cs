// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfAssert_Object.cs" company="Foobar">
//   2015
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Stf.Utilities
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Stf.Utilities.Interfaces;

    /// <summary>
    /// The stf assert.
    /// </summary>
    public partial class StfAssert : IStfAssert
    {
        /// <summary>
        /// Asserts that a value is a Object type and not a reference type
        /// </summary>
        /// <param name="testStep">
        /// Name of the test step in the test script
        /// </param>
        /// <param name="actual">
        /// Value that was actually experienced
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool AssertIsObject(string testStep, object actual)
        {
            string msg;

            if (actual == null)
            {
                msg = string.Format("AssertIsObject: Null is Not an object ");
                this.AssertFail(testStep, msg);
                return false;
            }

            var typeOfActual = actual.GetType();
            var retVal = typeOfActual.IsClass;

            if (retVal)
            {
                msg = string.Format("AssertIsObject: is an object ");
                this.AssertPass(testStep, msg);
            }
            else
            {
                msg = string.Format("AssertIsObject: is Not an object ");
                this.AssertFail(testStep, msg);
            }

            return retVal;
        }

        /// <summary>
        /// Asserts a variable is of a specific type
        /// </summary>
        /// <param name="testStep">
        /// Name of the test step in the test script
        /// </param>
        /// <param name="value">
        /// The variable to investigate
        /// </param>
        /// <param name="expectedType">
        /// The expected Type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool AssertIsInstanceOf(string testStep, object value, Type expectedType)
        {
            bool retVal;
            string msg;

            try
            {
                Assert.IsInstanceOfType(value, expectedType);
                msg = string.Format("AssertIsInstanceOf: [{0}] is of type [{1}]", value, expectedType);
                retVal = this.AssertPass(testStep, msg);
            }
            catch (AssertFailedException ex)
            {
                retVal = false;
                msg = ex.Message;
                this.AssertFail(testStep, msg);
            }

            return retVal;
        }

        /// <summary>
        /// Asserts whether a variable is NOT Null
        /// </summary>
        /// <param name="testStep">
        /// Name of the test step in the test script
        /// </param>
        /// <param name="actual">
        /// Value that was actually experienced
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool AssertNotNull(string testStep, object actual)
        {
            bool retVal = actual != null;
            string msg;

            if (retVal)
            {
                msg = string.Format("AssertNotNull: 'actual' Is not null");
                this.AssertPass(testStep, msg);
            }
            else
            {
                msg = string.Format("AssertNotNull: 'actual' Is null");
                this.AssertFail(testStep, msg);
            }

            return retVal;
        }

        /// <summary>
        /// Asserts whether a variable is Null
        /// </summary>
        /// <param name="testStep">
        /// Name of the test step in the test script
        /// </param>
        /// <param name="actual">
        /// Value that was actually experienced
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool AssertNull(string testStep, object actual)
        {
            bool retVal = actual == null;
            string msg;

            if (retVal)
            {
                msg = string.Format("AssertNull: object Is null");
                this.AssertPass(testStep, msg);
            }
            else
            {
                msg = string.Format("AssertNull:'{0}' Is not null", actual);
                this.AssertFail(testStep, msg);
            }

            return retVal;
        }

        /// <summary>
        /// Asserts whether a variable has a value - e.g. not Null or Empty
        /// </summary>
        /// <param name="testStep">
        /// Name of the test step in the test script
        /// </param>
        /// <param name="actual">
        /// Value that was actually experienced
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool AssertHasValue(string testStep, object actual)
        {
            bool retVal = actual != null;
            string msg;

            if (retVal)
            {
                msg = string.Format("AssertHasValue: Has a value");
                this.AssertPass(testStep, msg);
            }
            else
            {
                msg = string.Format("AssertHasValue: Has no value");
                this.AssertFail(testStep, msg);
            }

            return retVal;
        }

        /// <summary>
        /// Asserts whether a variable has NO value - e.g. Null or Empty
        /// </summary>
        /// <param name="testStep">
        /// Name of the test step in the test script
        /// </param>
        /// <param name="actual">
        /// Value that was actually experienced
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool AssertHasNoValue(string testStep, object actual)
        {
            bool retVal = actual == null;
            string msg;

            if (retVal)
            {
                msg = string.Format("AssertHasNoValue: Has no value");
                this.AssertPass(testStep, msg);
            }
            else
            {
                msg = string.Format("AssertHasNoValue: Has a value");
                this.AssertFail(testStep, msg);
            }

            return retVal;
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfAssert_Object.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mir.Stf.Utilities
{
    /// <summary>
    /// The stf assert.
    /// </summary>
    public partial class StfAssert
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
        public bool IsObject(string testStep, object actual)
        {
            string msg;

            if (actual == null)
            {
                msg = "IsObject: Null is Not an object ";
                this.AssertFail(testStep, msg);
                return false;
            }

            var typeOfActual = actual.GetType();
            var retVal = typeOfActual.IsClass;

            if (retVal)
            {
                msg = "IsObject: is an object ";
                this.AssertPass(testStep, msg);
            }
            else
            {
                msg = "IsObject: is Not an object ";
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
        public bool IsInstanceOfType(string testStep, object value, Type expectedType)
        {
            bool retVal;
            string msg;

            try
            {
                Assert.IsInstanceOfType(value, expectedType);
                msg = string.Format("IsInstanceOfType: [{0}] is of type [{1}]", value, expectedType);
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
        /// Asserts a variable is NOT of a specific type
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
        public bool IsNotInstanceOfType(string testStep, object value, Type expectedType)
        {
            bool retVal;
            string msg;

            try
            {
                Assert.IsInstanceOfType(value, expectedType);
                msg = string.Format("IsInstanceOfType: [{0}] is of type [{1}]", value, expectedType);
                retVal = this.AssertFail(testStep, msg);
            }
            catch (AssertFailedException)
            {
                msg = string.Format("IsInstanceOfType: [{0}] is NOT of type [{1}]", value, expectedType);
                retVal = this.AssertPass(testStep, msg);
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
        public bool IsNotNull(string testStep, object actual)
        {
            bool retVal = actual != null;
            string msg;

            if (retVal)
            {
                msg = "IsNotNull: \'actual\' Is not null";
                this.AssertPass(testStep, msg);
            }
            else
            {
                msg = "IsNotNull: \'actual\' Is null";
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
        public bool IsNull(string testStep, object actual)
        {
            bool retVal = actual == null;
            string msg;

            if (retVal)
            {
                msg = "IsNull: object Is null";
                this.AssertPass(testStep, msg);
            }
            else
            {
                msg = string.Format("IsNull:'{0}' Is not null", actual);
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
        public bool HasValue(string testStep, object actual)
        {
            bool retVal = actual != null;
            string msg;

            if (retVal)
            {
                msg = "HasValue: Has a value";
                this.AssertPass(testStep, msg);
            }
            else
            {
                msg = "HasValue: Has no value";
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
        public bool HasNoValue(string testStep, object actual)
        {
            bool retVal = actual == null;
            string msg;

            if (retVal)
            {
                msg = "HasNoValue: Has no value";
                this.AssertPass(testStep, msg);
            }
            else
            {
                msg = "HasNoValue: Has a value";
                this.AssertFail(testStep, msg);
            }

            return retVal;
        }
    }
}

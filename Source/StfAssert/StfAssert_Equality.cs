// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfAssert_Equality.cs" company="Mir Software">
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
        /// Assert if two values are the same. Values and objects can be compared.
        /// </summary>
        /// <typeparam name="T1">
        /// Type of expected
        /// </typeparam>
        /// <typeparam name="T2">
        /// Type of actual
        /// </typeparam>
        /// <param name="testStep">
        /// Name of the test step in the test script
        /// </param>
        /// <param name="expected">
        /// Value <c>expected</c> for the assert
        /// </param>
        /// <param name="actual">
        /// Value that was actually experienced
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool AreEqual<T1, T2>(string testStep, T1 expected, T2 actual)
        {
            var retVal = true;

            try
            {
                var msg = $"AreEqual: [{expected}] Are Equal to [{actual}]";

                Assert.AreEqual(expected, actual);
                AssertPass(testStep, msg);
            }
            catch (AssertFailedException)
            {
                var msg = $"AreEqual: [{expected}] Are Not Equal to [{actual}]";

                retVal = false;
                AssertFail(testStep, msg);
            }

            return retVal;
        }

        /// <summary>
        /// Asserts that two values are the same. Values and objects can be compared.
        /// </summary>
        /// <param name="testStep">
        /// Name of the test step in the test script
        /// </param>
        /// <param name="expected">
        /// Value <c>expected</c> for the assert
        /// </param>
        /// <param name="actual">
        /// Value that was actually experienced
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool AreNotEqual(string testStep, object expected, object actual)
        {
            bool retVal;

            try
            {
                var msg = $"AreNotEqual: [{expected}] Not Equal to [{actual}]";

                Assert.AreNotEqual(expected, actual);
                retVal = AssertPass(testStep, msg);
            }
            catch (AssertFailedException)
            {
                var msg = $"AreNotEqual: [{expected}] Are Equal to [{actual}]";

                retVal = false;
                AssertFail(testStep, msg);
            }

            return retVal;
        }

        /// <summary>
        /// Asserts whether the left hand side is greater than the right hand side
        /// </summary>
        /// <typeparam name="T1">
        /// The type of the left value in a compare expression
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the right value in a compare expression
        /// </typeparam>
        /// <param name="testStep">
        /// Name of the test step in the test script
        /// </param>
        /// <param name="leftHandSide">
        /// The value to the left in a compare expression
        /// </param>
        /// <param name="rightHandSide">
        /// The value to the right in a compare expression
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool GreaterThan<T1, T2>(string testStep, T1 leftHandSide, T2 rightHandSide)
            where T1 : IConvertible, IComparable
            where T2 : IConvertible, IComparable
        {
            var msg = string.Empty;
            var compareVal = 0;

            // TODO:what to return if we cannot compare the objects?
            if (!AssertCompareTo(leftHandSide, rightHandSide, ref msg, ref compareVal))
            {
                return false;
            }

            var retVal = compareVal > 0;

            if (retVal)
            {
                msg = $"GreaterThan: [{leftHandSide}] is greater than [{rightHandSide}]";
                AssertPass(testStep, msg);
            }
            else
            {
                msg = $"GreaterThan: [{leftHandSide}] is Not greater than [{rightHandSide}]";
                AssertFail(testStep, msg);
            }

            return retVal;
        }

        /// <summary>
        /// Asserts whether the left hand side is equal to or greater than the right hand side
        /// </summary>
        /// <typeparam name="T1">
        /// The type of the left value in a compare expression
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the right value in a compare expression
        /// </typeparam>
        /// <param name="testStep">
        /// Name of the test step in the test script
        /// </param>
        /// <param name="leftHandSide">
        /// The value to the left in a compare expression
        /// </param>
        /// <param name="rightHandSide">
        /// The value to the right in a compare expression
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool GreaterThanOrEqual<T1, T2>(string testStep, T1 leftHandSide, T2 rightHandSide) where T1 : IConvertible, IComparable where T2 : IConvertible, IComparable
        {
            var msg = string.Empty;
            var compareVal = 0;

            // TODO:what to return if we cannot compare the objects?
            if (!AssertCompareTo(leftHandSide, rightHandSide, ref msg, ref compareVal))
            {
                return false;
            }

            var retVal = compareVal >= 0;

            if (retVal)
            {
                msg = $"GreaterThanOrEqual: [{leftHandSide}] is equal to or greater than [{rightHandSide}]";
                AssertPass(testStep, msg);
            }
            else
            {
                msg = $"GreaterThanOrEqual: [{leftHandSide}] is not equal to or greater than [{rightHandSide}]";
                AssertFail(testStep, msg);
            }

            return retVal;
        }

        /// <summary>
        /// Asserts whether the left hand side is less than the right hand side
        /// </summary>
        /// <typeparam name="T1">
        /// The type of the left value in a compare expression
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the right value in a compare expression
        /// </typeparam>
        /// <param name="testStep">
        /// Name of the test step in the test script
        /// </param>
        /// <param name="leftHandSide">
        /// The value to the left in a compare expression
        /// </param>
        /// <param name="rightHandSide">
        /// The value to the right in a compare expression
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool LessThan<T1, T2>(string testStep, T1 leftHandSide, T2 rightHandSide)
            where T1 : IConvertible, IComparable
            where T2 : IConvertible, IComparable
        {
            var msg = string.Empty;
            var compareVal = 0;

            // TODO:what to return if we cannot compare the objects?
            if (!AssertCompareTo(leftHandSide, rightHandSide, ref msg, ref compareVal))
            {
                return false;
            }

            var retVal = compareVal < 0;

            if (retVal)
            {
                msg = $"LessThan: [{leftHandSide}] is less than [{rightHandSide}]";
                AssertPass(testStep, msg);
            }
            else
            {
                msg = $"LessThan: [{leftHandSide}] is Not less than [{rightHandSide}]";
                AssertFail(testStep, msg);
            }

            return retVal;
        }

        /// <summary>
        /// Asserts whether the left hand side is equal to or less than the right hand side
        /// </summary>
        /// <typeparam name="T1">
        /// The type of the left value in a compare expression
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the right value in a compare expression
        /// </typeparam>
        /// <param name="testStep">
        /// Name of the test step in the test script
        /// </param>
        /// <param name="leftHandSide">
        /// The value to the left in a compare expression
        /// </param>
        /// <param name="rightHandSide">
        /// The value to the right in a compare expression
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool LessThanOrEqual<T1, T2>(string testStep, T1 leftHandSide, T2 rightHandSide) where T1 : IConvertible, IComparable where T2 : IConvertible, IComparable
        {
            var msg = string.Empty;
            var compareVal = 0;

            // TODO:what to return if we cannot compare the objects?
            if (!AssertCompareTo(leftHandSide, rightHandSide, ref msg, ref compareVal))
            {
                return false;
            }

            var retVal = compareVal <= 0;

            if (retVal)
            {
                msg = $"LessThanOrEqual: [{leftHandSide}] is equal to or less than [{rightHandSide}]";
                AssertPass(testStep, msg);
            }
            else
            {
                msg = $"LessThan: [{leftHandSide}] is not equal to or less than [{rightHandSide}]";
                AssertFail(testStep, msg);
            }

            return retVal;
        }

        /// <summary>
        /// The assert comare to.
        /// </summary>
        /// <param name="leftHandSide">
        /// The left hand side.
        /// </param>
        /// <param name="rightHandSide">
        /// The right hand side.
        /// </param>
        /// <param name="msg">
        /// The msg.
        /// </param>
        /// <param name="compareVal">
        /// The compare val.
        /// </param>
        /// <typeparam name="T1">
        /// Type of the lefthandside
        /// </typeparam>
        /// <typeparam name="T2">
        /// Type of the righthandside
        /// </typeparam>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool AssertCompareTo<T1, T2>(T1 leftHandSide, T2 rightHandSide, ref string msg, ref int compareVal)
            where T1 : IConvertible, IComparable
            where T2 : IConvertible, IComparable
        {
            if (msg == null)
            {
                return false;
            }

            try
            {
                // convert val2 to type of val1.
                var rhsAsLhs = (T1)Convert.ChangeType(rightHandSide, typeof(T1));

                compareVal = leftHandSide.CompareTo(rhsAsLhs);
                msg = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                AssertLogger.LogTrace(ex.Message);
            }

            // if T2 cant convert to T1 perhaps T1 can convert to T2
            try
            {
                // convert val1 to type of val2.
                var lhsAsRhs = (T2)Convert.ChangeType(leftHandSide, typeof(T2));

                compareVal = rightHandSide.CompareTo(lhsAsRhs);
                msg = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return false;
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfStringAssert.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mir.Stf.Utilities
{
    using Interfaces;

    /// <summary>
    /// The stf assert.
    /// </summary>
    public partial class StfAssert : IStfStringAssert
    {
        /// <summary>
        /// The string assert function.
        /// </summary>
        private enum StringAssertFunction
        {
            /// <summary>
            /// The contains.
            /// </summary>
            Contains,

            /// <summary>
            /// The does not match.
            /// </summary>
            DoesNotMatch,

            /// <summary>
            /// The starts with.
            /// </summary>
            StartsWith,

            /// <summary>
            /// The ends with.
            /// </summary>
            EndsWith,

            /// <summary>
            /// The matches.
            /// </summary>
            Matches,
        }

        /// <summary>
        /// The string contains.
        /// </summary>
        /// <param name="testStep">
        /// The test step.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="substring">
        /// The substring.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool StringContains(string testStep, string value, string substring)
        {
            var retVal = WrapperStringAsserts(StringAssertFunction.Contains, value, substring);

            if (!wrapperRetVal.HasValue)
            {
                AssertFail(testStep, assertionMessage);
                return false;
            }

            var retVal = wrapperRetVal.Value;

            if (retVal)
            {
                AssertPass(testStep, $"[{value}] contains [{substring}]");
            }
            else
            {
                AssertFail(testStep, assertionMessage);
            }

            return retVal;
        }

        /// <summary>
        /// The string does not contain.
        /// </summary>
        /// <param name="testStep">
        /// The test step.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="substring">
        /// The substring.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool StringDoesNotContain(string testStep, string value, string substring)
        {
            string assertionMessage;
            var wrapperRetVal = !WrapperStringAsserts(StringAssertFunction.Contains, value, substring, out assertionMessage);

            if (!wrapperRetVal.HasValue)
            {
                AssertFail(testStep, assertionMessage);
                return false;
            }

            var retVal = wrapperRetVal.Value;

            if (retVal)
            {
                var message = $"[{value}] don't contain [{substring}]";

                AssertPass(testStep, message);
            }
            else
            {
                var message = $"[{value}] do contain [{substring}]";

                AssertFail(testStep, message);
            }

            return retVal;
        }

        /// <summary>
        /// The string matches.
        /// </summary>
        /// <param name="testStep">
        /// The test step.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="pattern">
        /// The pattern.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool StringMatches(string testStep, string value, string pattern)
        {
            string assertionMessage;
            var wrapperRetVal = WrapperStringAsserts(StringAssertFunction.Matches, value, pattern, out assertionMessage);

            if (!wrapperRetVal.HasValue)
            {
                AssertFail(testStep, assertionMessage);
                return false;
            }

            var retVal = wrapperRetVal.Value;

            if (retVal)
            {
                var message = $"[{value}] is matched by [{pattern}]";

                AssertPass(testStep, message);
            }
            else
            {
                AssertFail(testStep, assertionMessage);
            }

            return retVal;
        }

        /// <summary>
        /// The string does not match.
        /// </summary>
        /// <param name="testStep">
        /// The test step.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="pattern">
        /// The pattern.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool StringDoesNotMatch(string testStep, string value, string pattern)
        {
            string assertionMessage;
            var wrapperRetVal = WrapperStringAsserts(StringAssertFunction.DoesNotMatch, value, pattern, out assertionMessage);

            if (!wrapperRetVal.HasValue)
            {
                AssertFail(testStep, assertionMessage);
                return false;
            }

            var retVal = wrapperRetVal.Value;

            if (!retVal)
            {
                var message = $"[{value}] is not matched by [{pattern}]";

                AssertPass(testStep, message);
            }
            else
            {
                AssertFail(testStep, assertionMessage);
            }

            return retVal;
        }

        /// <summary>
        /// The string starts with.
        /// </summary>
        /// <param name="testStep">
        /// The test step.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="substring">
        /// The substring.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool StringStartsWith(string testStep, string value, string substring)
        {
            string assertionMessage;
            var wrapperRetVal = WrapperStringAsserts(StringAssertFunction.StartsWith, value, substring, out assertionMessage);

            if (!wrapperRetVal.HasValue)
            {
                AssertFail(testStep, assertionMessage);
                return false;
            }

            var retVal = wrapperRetVal.Value;

            if (retVal)
            {
                var message = $"[{value}] StartsWith [{substring}]";

                AssertPass(testStep, message);
            }
            else
            {
                AssertFail(testStep, assertionMessage);
            }

            return retVal;
        }

        /// <summary>
        /// The string does not start with.
        /// </summary>
        /// <param name="testStep">
        /// The test step.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="substring">
        /// The substring.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool StringDoesNotStartWith(string testStep, string value, string substring)
        {
            string assertionMessage;
            var wrapperRetVal = !WrapperStringAsserts(StringAssertFunction.StartsWith, value, substring, out assertionMessage);

            if (!wrapperRetVal.HasValue)
            {
                AssertFail(testStep, assertionMessage);
                return false;
            }

            var retVal = wrapperRetVal.Value;

            if (retVal)
            {
                var message = $"[{value}] do start with [{substring}]";

                AssertFail(testStep, message);
            }
            else
            {
                var message = $"[{value}] doesn't start with [{substring}]";

                AssertPass(testStep, message);
            }

            return retVal;
        }

        /// <summary>
        /// The string ends with.
        /// </summary>
        /// <param name="testStep">
        /// The test step.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="substring">
        /// The substring.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool StringEndsWith(string testStep, string value, string substring)
        {
            string assertionMessage;
            var wrapperRetVal = WrapperStringAsserts(StringAssertFunction.EndsWith, value, substring, out assertionMessage);

            if (!wrapperRetVal.HasValue)
            {
                AssertFail(testStep, assertionMessage);
                return false;
            }

            var retVal = wrapperRetVal.Value;

            if (retVal)
            {
                var message = $"[{value}] EndsWith [{substring}]";

                AssertPass(testStep, message);
            }
            else
            {
                AssertFail(testStep, assertionMessage);
            }

            return retVal;
        }

        /// <summary>
        /// The string does not ends with.
        /// </summary>
        /// <param name="testStep">
        /// The test step.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="substring">
        /// The substring.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool StringDoesNotEndsWith(string testStep, string value, string substring)
        {
            string assertionMessage;
            var wrapperRetVal = !WrapperStringAsserts(StringAssertFunction.EndsWith, value, substring, out assertionMessage);

            if (!wrapperRetVal.HasValue)
            {
                AssertFail(testStep, assertionMessage);
                return false;
            }

            var retVal = wrapperRetVal.Value;

            if (!retVal)
            {
                AssertFail(testStep, $"[{value}] do ends with [{substring}]");
            }
            else
            {
                AssertPass(testStep, $"[{value}] doesn't ends with [{substring}]");
            }

            return retVal;
        }

        /// <summary>
        /// Asserts that two values are equal - Case Insignificant
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
        public bool StringEquals(string testStep, string expected, string actual)
        {
            var retVal = string.Compare(expected, actual, StringComparison.InvariantCulture) == 0;

            if (retVal)
            {
                AssertPass(testStep, $"[{expected}] is equal to [{actual}]");
            }
            else
            {
                AssertFail(testStep, $"[{expected}] is NOT equal to [{actual}]");
            }

            return retVal;
        }

        /// <summary>
        /// Asserts that two strings are equal - Case Insignificant
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
        public bool StringEqualsCi(string testStep, string expected, string actual)
        {
            var retVal = string.Compare(expected, actual, StringComparison.InvariantCultureIgnoreCase) == 0;

            if (retVal)
            {
                AssertPass(testStep, $"[{expected}] is equal to [{actual}]");
            }
            else
            {
                AssertFail(testStep, $"[{expected}] is NOT equal to [{actual}]");
            }

            return retVal;
        }

        /// <summary>
        /// Asserts that two values not are equal
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
        public bool StringNotEquals(string testStep, string expected, string actual)
        {
            var retVal = string.Compare(expected, actual, StringComparison.InvariantCulture) != 0;

            if (retVal)
            {
                AssertPass(testStep, $"[{expected}] is NOT equal to [{actual}]");
            }
            else
            {
                AssertFail(testStep, $"[{expected}] is equal to [{actual}]");
            }

            return retVal;
        }

        /// <summary>
        /// Asserts that two strings are not equal - Case Insignificant
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
        public bool StringNotEqualsCi(string testStep, string expected, string actual)
        {
            var retVal = string.Compare(expected, actual, StringComparison.InvariantCultureIgnoreCase) != 0;

            if (retVal)
            {
                AssertPass(testStep, $"[{expected}] is not equal to [{actual}]");
            }
            else
            {
                AssertFail(testStep, $"[{expected}] IS equal to [{actual}]");
            }

            return retVal;
        }

        /// <summary>
        /// Asserts that a string is Null or Empty
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
        public bool StringIsNullOrEmpty(string testStep, string actual)
        {
            var retVal = string.IsNullOrEmpty(actual);

            if (retVal)
            {
                AssertPass(testStep, "String is NullOrEmpty");
            }
            else
            {
                AssertFail(testStep, $"[{actual}] is NOT NullOrEmpty");
            }

            return retVal;
        }

        /// <summary>
        /// Asserts that a string is Not (Null or Empty)
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
        public bool StringIsNotNullOrEmpty(string testStep, string actual)
        {
            var retVal = !string.IsNullOrEmpty(actual);

            if (retVal)
            {
                AssertPass(testStep, "String is not NullOrEmpty");
            }
            else
            {
                AssertFail(testStep, $"[{actual}] IS NullOrEmpty");
            }

            return retVal;
        }

        /// <summary>
        /// Asserts that a string is Empty
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
        public bool StringEmpty(string testStep, string actual)
        {
            var retVal = actual != null && string.IsNullOrEmpty(actual);

            if (retVal)
            {
                AssertPass(testStep, "String is NullOrEmpty");
            }
            else
            {
                AssertFail(testStep, $"[{actual}] is NOT NullOrEmpty");
            }

            return retVal;
        }

        /// <summary>
        /// Asserts that a string is Not or Empty
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
        public bool StringNotEmpty(string testStep, string actual)
        {
            var retVal = !string.IsNullOrEmpty(actual);

            if (retVal)
            {
                AssertPass(testStep, "String is not NullOrEmpty");
            }
            else
            {
                AssertFail(testStep, $"[{actual}] IS NullOrEmpty");
            }

            return retVal;
        }

        /// <summary>
        /// The wrapper string asserts.
        /// </summary>
        /// <param name="function">
        /// The function.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="argstring">
        /// The argstring.
        /// </param>
        /// <param name="assertionMessage">
        /// setting the message from Assert
        /// </param>
        /// <returns>
        /// Wether or not the assertion failed
        /// </returns>
        private bool? WrapperStringAsserts(StringAssertFunction function, string value, string argstring, out string assertionMessage)
        {
            var retVal = true;

            assertionMessage = null;

            if (string.IsNullOrEmpty(value))
            {
                assertionMessage = $"Can not evaluate {function}, as actual is null or empty";

                return null;
            }

            if (string.IsNullOrEmpty(argstring))
            {
                assertionMessage = $"Can not evaluate {function}, as expected is null or empty";

                return null;
            }

            try
            {
                switch (function)
                {
                    case StringAssertFunction.Contains:
                        StringAssert.Contains(value, argstring);
                        break;
                    case StringAssertFunction.Matches:
                        StringAssert.Matches(value, new Regex(argstring));
                        break;
                    case StringAssertFunction.DoesNotMatch:
                        StringAssert.DoesNotMatch(value, new Regex(argstring));
                        break;
                    case StringAssertFunction.StartsWith:
                        StringAssert.StartsWith(value, argstring);
                        break;
                    case StringAssertFunction.EndsWith:
                        StringAssert.EndsWith(value, argstring);
                        break;
                }
            }
            catch (AssertFailedException ex)
            {
                assertionMessage = ex.Message;
                retVal = false;
            }

            return retVal;
        }
    }
}

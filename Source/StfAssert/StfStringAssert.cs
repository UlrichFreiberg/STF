// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfStringAssert.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

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
            /// The ends with.
            /// </summary>
            EndsWith, 

            /// <summary>
            /// The matches.
            /// </summary>
            Matches, 

            /// <summary>
            /// The starts with.
            /// </summary>
            StartsWith
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

            if (retVal == null)
            {
                var message = $"[{value}] contains [{substring}]";
                AssertPass(testStep, message);
            }
            else
            {
                AssertFail(testStep, retVal);
            }

            return retVal == null;
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
            var retVal = WrapperStringAsserts(StringAssertFunction.Contains, value, substring);

            if (retVal != null)
            {
                var message = $"[{value}] don't contain [{substring}]";
                AssertPass(testStep, message);
            }
            else
            {
                var message = $"[{value}] do contain [{substring}]";
                AssertFail(testStep, message);
            }

            return retVal != null;
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
            var retVal = WrapperStringAsserts(StringAssertFunction.Matches, value, pattern);

            if (retVal == null)
            {
                var message = $"[{value}] is matched by [{pattern}]";
                AssertPass(testStep, message);
            }
            else
            {
                AssertFail(testStep, retVal);
            }

            return retVal == null;
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
            var retVal = WrapperStringAsserts(StringAssertFunction.DoesNotMatch, value, pattern);

            if (retVal == null)
            {
                var message = $"[{value}] is not matched by [{pattern}]";
                AssertPass(testStep, message);
            }
            else
            {
                AssertFail(testStep, retVal);
            }

            return retVal == null;
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
            var retVal = WrapperStringAsserts(StringAssertFunction.StartsWith, value, substring);

            if (retVal == null)
            {
                var message = $"[{value}] StartsWith [{substring}]";
                AssertPass(testStep, message);
            }
            else
            {
                AssertFail(testStep, retVal);
            }

            return retVal == null;
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
            var retVal = WrapperStringAsserts(StringAssertFunction.StartsWith, value, substring);

            if (retVal != null)
            {
                var message = $"[{value}] doesn't start with [{substring}]";
                AssertPass(testStep, message);
            }
            else
            {
                var message = $"[{value}] do start with [{substring}]";
                AssertFail(testStep, message);
            }

            return retVal != null;
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
            var retVal = WrapperStringAsserts(StringAssertFunction.EndsWith, value, substring);

            if (retVal == null)
            {
                var message = $"[{value}] EndsWith [{substring}]";
                AssertPass(testStep, message);
            }
            else
            {
                AssertFail(testStep, retVal);
            }

            return retVal == null;
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
            var retVal = WrapperStringAsserts(StringAssertFunction.EndsWith, value, substring);

            if (retVal != null)
            {
                var message = $"[{value}] doesn't ends with [{substring}]";
                AssertPass(testStep, message);
            }
            else
            {
                var message = $"[{value}] do ends with [{substring}]";
                AssertFail(testStep, message);
            }

            return retVal != null;
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
            var retVal = expected == actual;

            if (retVal)
            {
                var message = $"[{expected}] is equal to [{actual}]";
                AssertPass(testStep, message);
            }
            else
            {
                var message = $"[{expected}] is NOT equal to [{actual}]";
                AssertFail(testStep, message);
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
            return StringEquals(testStep, expected.ToLower(), actual.ToLower());
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
            bool retVal = expected != actual;

            if (retVal)
            {
                var message = $"[{expected}] is NOT equal to [{actual}]";
                AssertPass(testStep, message);
            }
            else
            {
                var message = $"[{expected}] is equal to [{actual}]";
                AssertFail(testStep, message);
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
            return StringNotEquals(testStep, expected.ToLower(), actual.ToLower());
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
        public bool StringEmpty(string testStep, string actual)
        {
            bool retVal = string.IsNullOrEmpty(actual);

            if (retVal)
            {
                var message = "String is NullOrEmpty";
                AssertPass(testStep, message);
            }
            else
            {
                var message = $"[{actual}] is NOT NullOrEmpty";
                AssertFail(testStep, message);
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
        public bool StringNotEmpty(string testStep, string actual)
        {
            var retVal = !string.IsNullOrEmpty(actual);

            if (retVal)
            {
                var message = "String is not NullOrEmpty";
                AssertPass(testStep, message);
            }
            else
            {
                var message = $"[{actual}] IS NullOrEmpty";
                AssertFail(testStep, message);
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
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string WrapperStringAsserts(StringAssertFunction function, string value, string argstring)
        {
            string retVal = null;
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
                retVal = ex.Message;
            }

            return retVal;
        }
    }
}

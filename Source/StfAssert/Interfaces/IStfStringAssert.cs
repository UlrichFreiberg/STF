// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStfStringAssert.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.Interfaces
{
    /// <summary>
    /// The StfStringAssert interface.
    /// </summary>
    public interface IStfStringAssert
    {
        #region Some wrapping of https://msdn.microsoft.com/en-us/library/microsoft.visualstudio.testtools.unittesting.stringassert.aspx

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
        bool StringContains(string testStep, string value, string substring);

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
        bool StringDoesNotContain(string testStep, string value, string substring);

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
        bool StringMatches(string testStep, string value, string pattern);

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
        bool StringDoesNotMatch(string testStep, string value, string pattern);

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
        bool StringStartsWith(string testStep, string value, string substring);

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
        bool StringDoesNotStartWith(string testStep, string value, string substring);

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
        bool StringEndsWith(string testStep, string value, string substring);

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
        bool StringDoesNotEndsWith(string testStep, string value, string substring);
        #endregion

        /// <summary>
        /// Assert if two strings are the same - Case Insignificant
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
        bool StringEqualsCi(string testStep, string expected, string actual);

        /// <summary>
        /// Asserts that two values are not equal - Case Insignificant
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
        bool StringNotEqualsCi(string testStep, string expected, string actual);

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
        bool StringEmpty(string testStep, string actual);

        /// <summary>
        /// Asserts that a string is Not Empty
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
        bool StringNotEmpty(string testStep, string actual);

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

        bool StringIsNullOrEmpty(string testStep, string actual);
        /// <summary>
        /// Asserts that a string is Not Null nor Empty
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
        bool StringIsNotNullOrEmpty(string testStep, string actual);
    }
}

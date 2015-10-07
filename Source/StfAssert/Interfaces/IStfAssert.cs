// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStfAssert.cs" company="Foobar">
//   2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Mir.Stf.Utilities;

namespace Stf.Utilities.Interfaces
{
    using System;

    /// <summary>
    /// The <see cref="StfAssert"/> interface.
    /// </summary>
    public interface IStfAssert
    {
        /// <summary>
        /// Gets or sets a value indicating whether negative testing is enabled - 
        /// As in LogFail and errors don't count as errors
        /// </summary>
        bool EnableNegativeTesting { get; set; }

        /// <summary>
        /// Gets the last message reported - used by Unit tests 
        /// </summary>
        string LastMessage { get; }

        /// <summary>
        /// Assert if two values are the same. Values and objects can be compared.
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
        bool AssertEquals<T1, T2>(string testStep, T1 expected, T2 actual);

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
        bool AssertNotEquals(string testStep, object expected, object actual);

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
        bool AssertIsObject(string testStep, object actual);

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
        /// The expected type of the variable
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool AssertIsInstanceOf(string testStep, object value, Type expectedType);

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
        bool AssertNull(string testStep, object actual);

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
        bool AssertNotNull(string testStep, object actual);

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
        bool AssertHasValue(string testStep, object actual);

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
        bool AssertHasNoValue(string testStep, object actual);

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
        bool AssertGreaterThan<T1, T2>(string testStep, T1 leftHandSide, T2 rightHandSide)
            where T1 : IConvertible, IComparable where T2 : IConvertible, IComparable;


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
        bool AssertLessThan<T1, T2>(string testStep, T1 leftHandSide, T2 rightHandSide)
            where T1 : IConvertible, IComparable
            where T2 : IConvertible, IComparable;

        /// <summary>
        /// Asserts that a file exists
        /// </summary>
        /// <param name="testStep">
        /// Name of the test step in the test script
        /// </param>
        /// <param name="filenameAndPath">
        /// Absolute path to the file of interest
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool AssertFileExists(string testStep, string filenameAndPath);

        /// <summary>
        /// Asserts that a file doesn't exists
        /// </summary>
        /// <param name="testStep">
        /// Name of the test step in the test script
        /// </param>
        /// <param name="filenameAndPath">
        /// Absolute path to the file of interest
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool AssertFileNotExists(string testStep, string filenameAndPath);

        /// <summary>
        /// Asserts that a folder (directory) exists
        /// </summary>
        /// <param name="testStep">
        /// Name of the test step in the test script
        /// </param>
        /// <param name="foldernameAndPath">
        /// Path to the folder
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool AssertFolderExists(string testStep, string foldernameAndPath);

        /// <summary>
        /// Asserts that a folder (directory) does NOT exists
        /// </summary>
        /// <param name="testStep">
        /// Name of the test step in the test script
        /// </param>
        /// <param name="foldernameAndPath">
        /// Path to the folder
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool AssertFolderNotExists(string testStep, string foldernameAndPath);

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
        bool AssertTrue(string testStep, bool value);

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
        bool AssertFalse(string testStep, bool value);
    }
}

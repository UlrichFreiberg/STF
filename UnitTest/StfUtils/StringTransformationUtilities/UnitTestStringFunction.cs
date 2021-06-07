// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestStringFunction.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the UnitTest1 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.StringTransformationUtilities
{
    using Mir.Stf.Utilities.StringTransformationUtilities;

    /// <summary>
    /// The unit test unique functions.
    /// </summary>
    [TestClass]
    public class UnitTestStringFunction : UnitTestScriptBase
    {
        /// <summary>
        /// The string transformation utils.
        /// </summary>
        private readonly StringTransformationUtils stringTransformationUtils = new StringTransformationUtils();

        /// <summary>
        /// The test stu unique function for PadRight.
        /// </summary>
        [TestMethod]
        public void TestStuStringFunctionPadRight()
        {
            // Positive
            HelperTestPad("PadRight", "A source", "14", "X", "A sourceXXXXXX");
            HelperTestPad("PadRight", "A source", "8", "X", "A source");

            // Negative
            HelperTestPad("PadRight", string.Empty, "14", "X", null);
            HelperTestPad("PadRight", null, "14", "X", null);

            HelperTestPad("PadRight", "A source", string.Empty, "X", null);
            HelperTestPad("PadRight", "A source", null, "X", null);

            HelperTestPad("PadRight", "A source", "14", string.Empty, "A source      ");
            HelperTestPad("PadRight", "A source", "14", null, "A source      ");

            HelperTestPad("PadRight", "A source", "Q", "X", null);
            HelperTestPad("PadRight", "A source", "-1", "X", null);
            HelperTestPad("PadRight", "A source", "0", "X", null);
            HelperTestPad("PadRight", "A source", "6", "X", null);
        }

        /// <summary>
        /// The test stu unique function for PadLeft.
        /// </summary>
        [TestMethod]
        public void TestStuStringFunctionPadLeft()
        {
            // Positive
            HelperTestPad("PadLeft", "A source", "14", "X", "XXXXXXA source");
            HelperTestPad("PadLeft", "A source", "8", "X", "A source");

            // Negative
            HelperTestPad("PadLeft", string.Empty, "14", "X", null);
            HelperTestPad("PadLeft", null, "14", "X", null);

            HelperTestPad("PadLeft", "A source", string.Empty, "X", null);
            HelperTestPad("PadLeft", "A source", null, "X", null);

            HelperTestPad("PadLeft", "A source", "14", string.Empty, "      A source");
            HelperTestPad("PadLeft", "A source", "14", null, "      A source");

            HelperTestPad("PadLeft", "A source", "Q", "X", null);
            HelperTestPad("PadLeft", "A source", "-1", "X", null);
            HelperTestPad("PadLeft", "A source", "0", "X", null);
            HelperTestPad("PadLeft", "A source", "6", "X", null);
        }

        /// <summary>
        /// The test stu unique function for ToUpper.
        /// </summary>
        [TestMethod]
        public void TestStuStringFunctionToUpper()
        {
            // Positive
            HelperTestToCase("ToUpper", "A 12soU!# rce", "A 12SOU!# RCE");

            HelperTestToCase("ToUpper", string.Empty, string.Empty);
            HelperTestToCase("ToUpper", null, string.Empty);
        }

        /// <summary>
        /// The test stu unique function for ToLower.
        /// </summary>
        [TestMethod]
        public void TestStuStringFunctionToLower()
        {
            // Positive
            HelperTestToCase("ToLower", "A 12soU!# rcE", "a 12sou!# rce");

            HelperTestToCase("ToLower", string.Empty, string.Empty);
            HelperTestToCase("ToLower", null, string.Empty);
        }

        /// <summary>
        /// The test stu unique function for StartsWith.
        /// </summary>
        [TestMethod]
        public void TestStuStringFunctionStartsWith()
        {
            // Positive
            HelperTestStartsOrEndsWith(
                "StartsWith",
                "The Source string of 1 this test",
                "The S",
                "CS",
                "The Source string of 1 this test");

            HelperTestStartsOrEndsWith(
                "StartsWith",
                "The S",
                "The Source string of this test",
                "CS",
                string.Empty);

            HelperTestStartsOrEndsWith(
                "StartsWith", 
                "The Source string of this test", 
                "The s", 
                "CS", 
                string.Empty);

            HelperTestStartsOrEndsWith(
                "StartsWith",
                "The Source string of this test",
                "The s",
                "CI",
                "The Source string of this test");

            HelperTestStartsOrEndsWith(
                "StartsWith",
                "The Source string of this test",
                "The s",
                string.Empty,
                string.Empty);

            HelperTestStartsOrEndsWith(
                "StartsWith",
                "The Source string of this test",
                "The s",
                null,
                string.Empty);

            HelperTestStartsOrEndsWith(
                "StartsWith",
                "The Source string of this test",
                "The S",
                string.Empty,
                "The Source string of this test");

            HelperTestStartsOrEndsWith(
                "StartsWith",
                "The Source string of this test",
                "The S",
                null,
                "The Source string of this test");

            // Negative
            HelperTestStartsOrEndsWith(
                "StartsWith",
                string.Empty,
                "The s",
                "CI",
                null);

            HelperTestStartsOrEndsWith(
                "StartsWith",
                null,
                "The s",
                "CI",
                null);

            HelperTestStartsOrEndsWith(
                "StartsWith",
                "The Source string of this test",
                string.Empty,
                "CI",
                null);

            HelperTestStartsOrEndsWith(
                "StartsWith",
                "The Source string of this test",
                null,
                "CI",
                null);

            HelperTestStartsOrEndsWith(
                "StartsWith",
                "The Source string of this test",
                "The S",
                "CC",
                null);
        }

        /// <summary>
        /// The test stu unique function for EndsWith.
        /// </summary>
        [TestMethod]
        public void TestStuStringFunctionEndsWith()
        {
            // Positive
            HelperTestStartsOrEndsWith(
                "EndsWith",
                "The Source string of this Test",
                "s Test",
                "CS",
                "The Source string of this Test");

            HelperTestStartsOrEndsWith(
                "EndsWith",
                "s Test",
                "The Source string of this Test",
                "CS",
                string.Empty);

            HelperTestStartsOrEndsWith(
                "EndsWith",
                "The Source string of this Test",
                "s test",
                "CS",
                string.Empty);

            HelperTestStartsOrEndsWith(
                "EndsWith",
                "The Source string of this Test",
                "s test",
                "CI",
                "The Source string of this Test");

            HelperTestStartsOrEndsWith(
                "EndsWith",
                "The Source string of this Test",
                "s test",
                string.Empty,
                string.Empty);

            HelperTestStartsOrEndsWith(
                "EndsWith",
                "The Source string of this Test",
                "s test",
                null,
                string.Empty);

            HelperTestStartsOrEndsWith(
                "EndsWith",
                "The Source string of this Test",
                "s Test",
                string.Empty,
                "The Source string of this Test");

            HelperTestStartsOrEndsWith(
                "EndsWith",
                "The Source string of this Test",
                "s Test",
                null,
                "The Source string of this Test");

            // Negative
            HelperTestStartsOrEndsWith(
                "EndsWith",
                string.Empty,
                "s test",
                "CI",
                null);

            HelperTestStartsOrEndsWith(
                "EndsWith",
                null,
                "s test",
                "CI",
                null);

            HelperTestStartsOrEndsWith(
                "EndsWith",
                "The Source string of this Test",
                string.Empty,
                "CI",
                null);

            HelperTestStartsOrEndsWith(
                "EndsWith",
                "The Source string of this Test",
                null,
                "CI",
                null);

            HelperTestStartsOrEndsWith(
                "EndsWith",
                "The Source string of this Test",
                "s Test",
                "CC",
                null);
        }

        /// <summary>
        /// The helper test Pad.... functions.
        /// </summary>
        /// <param name="direction">
        /// The direction of the padding PadRight or PadLeft
        /// </param>
        /// <param name="source">
        /// The source string
        /// </param>
        /// <param name="totalWidth">
        /// The total width if the transformed string
        /// </param>
        /// <param name="paddingChar">
        /// The character with which to pad the string
        /// </param>
        /// <param name="expected">
        /// The expected value of the transformed string
        /// </param>
        private void HelperTestPad(string direction, string source, string totalWidth, string paddingChar, string expected)
        {
            var arg = $@"""{direction}"" ""{source}"" ""{totalWidth}"" ""{paddingChar}""";
            var actual = stringTransformationUtils.EvaluateFunction("STRING", arg);

            StfAssert.AreEqual($"Unittest {direction} test actual / expected", expected, actual);
        }

        /// <summary>
        /// The helper test ToUpper.... function.
        /// </summary>
        /// <param name="toCase">
        /// The toCase string ToUpper or ToLower
        /// </param>
        /// <param name="source">
        /// The source string
        /// </param>
        /// <param name="expected">
        /// The expected string
        /// </param>
        private void HelperTestToCase(string toCase, string source, string expected)
        {
            var arg = $@"""{toCase}"" ""{source}""";
            var actual = stringTransformationUtils.EvaluateFunction("STRING", arg);

            StfAssert.AreEqual($"Unittest {toCase} test actual / expected", expected, actual);
        }

        /// <summary>
        /// The helper test StartsWith and EndsWith.... function.
        /// </summary>
        /// <param name="startsOrEndsWith">
        /// The startsOrEndsWith string StartsWith or EndsWith
        /// </param>
        /// <param name="source">
        /// The source string
        /// </param>
        /// <param name="testString">
        /// The test string
        /// </param>
        /// <param name="stringComparison">
        /// The stringComparison string
        /// </param>
        /// <param name="expected">
        /// The expected string
        /// </param>
        private void HelperTestStartsOrEndsWith(
                                                string startsOrEndsWith,
                                                string source,
                                                string testString,
                                                string stringComparison,
                                                string expected)
        {
            var arg = $@"""{startsOrEndsWith}"" ""{source}"" ""{testString}"" ""{stringComparison}""";
            var actual = stringTransformationUtils.EvaluateFunction("STRING", arg);

            StfAssert.AreEqual($"Unittest {startsOrEndsWith},{source},{testString},{stringComparison} test actual / expected", expected, actual);
        }
    }
}

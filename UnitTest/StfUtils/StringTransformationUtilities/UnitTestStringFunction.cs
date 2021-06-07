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
            HelperTestPad("PADRIGHT", "A source", "14", "X", "A sourceXXXXXX");
            HelperTestPad("PADRIGHT", "A source", "8", "X", "A source");

            // Negative
            HelperTestPad("PADRIGHT", string.Empty, "14", "X", null);
            HelperTestPad("PADRIGHT", null, "14", "X", null);

            HelperTestPad("PADRIGHT", "A source", string.Empty, "X", null);
            HelperTestPad("PADRIGHT", "A source", null, "X", null);

            HelperTestPad("PADRIGHT", "A source", "14", string.Empty, "A source      ");
            HelperTestPad("PADRIGHT", "A source", "14", null, "A source      ");

            HelperTestPad("PADRIGHT", "A source", "Q", "X", null);
            HelperTestPad("PADRIGHT", "A source", "-1", "X", null);
            HelperTestPad("PADRIGHT", "A source", "0", "X", null);
            HelperTestPad("PADRIGHT", "A source", "6", "X", null);
        }

        /// <summary>
        /// The test stu unique function for PadLeft.
        /// </summary>
        [TestMethod]
        public void TestStuStringFunctionPadLeft()
        {
            // Positive
            HelperTestPad("PADLEFT", "A source", "14", "X", "XXXXXXA source");
            HelperTestPad("PADLEFT", "A source", "8", "X", "A source");

            // Negative
            HelperTestPad("PADLEFT", string.Empty, "14", "X", null);
            HelperTestPad("PADLEFT", null, "14", "X", null);

            HelperTestPad("PADLEFT", "A source", string.Empty, "X", null);
            HelperTestPad("PADLEFT", "A source", null, "X", null);

            HelperTestPad("PADLEFT", "A source", "14", string.Empty, "      A source");
            HelperTestPad("PADLEFT", "A source", "14", null, "      A source");

            HelperTestPad("PADLEFT", "A source", "Q", "X", null);
            HelperTestPad("PADLEFT", "A source", "-1", "X", null);
            HelperTestPad("PADLEFT", "A source", "0", "X", null);
            HelperTestPad("PADLEFT", "A source", "6", "X", null);
        }

        /// <summary>
        /// The test stu unique function for ToUpper.
        /// </summary>
        [TestMethod]
        public void TestStuStringFunctionToUpper()
        {
            // Positive
            HelperTestToCase("TOUPPER", "A 12soU!# rce", "A 12SOU!# RCE");

            HelperTestToCase("TOUPPER", string.Empty, string.Empty);
            HelperTestToCase("TOUPPER", null, string.Empty);
        }

        /// <summary>
        /// The test stu unique function for ToLower.
        /// </summary>
        [TestMethod]
        public void TestStuStringFunctionToLower()
        {
            // Positive
            HelperTestToCase("TOLOWER", "A 12soU!# rcE", "a 12sou!# rce");

            HelperTestToCase("TOLOWER", string.Empty, string.Empty);
            HelperTestToCase("TOLOWER", null, string.Empty);
        }

        /// <summary>
        /// The test stu unique function for StartsWith.
        /// </summary>
        [TestMethod]
        public void TestStuStringFunctionStartsWith()
        {
            // Positive
            HelperTestStartsOrEndsWith(
                "STARTSWITH",
                "The Source string of this test",
                "The S",
                "CS",
                "The Source string of this test");

            HelperTestStartsOrEndsWith(
                "STARTSWITH",
                "The S",
                "The Source string of this test",
                "CS",
                string.Empty);

            HelperTestStartsOrEndsWith(
                "STARTSWITH", 
                "The Source string of this test", 
                "The s", 
                "CS", 
                string.Empty);

            HelperTestStartsOrEndsWith(
                "STARTSWITH",
                "The Source string of this test",
                "The s",
                "CI",
                "The Source string of this test");

            HelperTestStartsOrEndsWith(
                "STARTSWITH",
                "The Source string of this test",
                "The s",
                string.Empty,
                string.Empty);

            HelperTestStartsOrEndsWith(
                "STARTSWITH",
                "The Source string of this test",
                "The s",
                null,
                string.Empty);

            HelperTestStartsOrEndsWith(
                "STARTSWITH",
                "The Source string of this test",
                "The S",
                string.Empty,
                "The Source string of this test");

            HelperTestStartsOrEndsWith(
                "STARTSWITH",
                "The Source string of this test",
                "The S",
                null,
                "The Source string of this test");

            // Negative
            HelperTestStartsOrEndsWith(
                "STARTSWITH",
                string.Empty,
                "The s",
                "CI",
                null);

            HelperTestStartsOrEndsWith(
                "STARTSWITH",
                null,
                "The s",
                "CI",
                null);

            HelperTestStartsOrEndsWith(
                "STARTSWITH",
                "The Source string of this test",
                string.Empty,
                "CI",
                null);

            HelperTestStartsOrEndsWith(
                "STARTSWITH",
                "The Source string of this test",
                null,
                "CI",
                null);

            HelperTestStartsOrEndsWith(
                "STARTSWITH",
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
                "ENDSWITH",
                "The Source string of this Test",
                "s Test",
                "CS",
                "The Source string of this Test");

            HelperTestStartsOrEndsWith(
                "ENDSWITH",
                "s Test",
                "The Source string of this Test",
                "CS",
                string.Empty);

            HelperTestStartsOrEndsWith(
                "ENDSWITH",
                "The Source string of this Test",
                "s test",
                "CS",
                string.Empty);

            HelperTestStartsOrEndsWith(
                "ENDSWITH",
                "The Source string of this Test",
                "s test",
                "CI",
                "The Source string of this Test");

            HelperTestStartsOrEndsWith(
                "ENDSWITH",
                "The Source string of this Test",
                "s test",
                string.Empty,
                string.Empty);

            HelperTestStartsOrEndsWith(
                "ENDSWITH",
                "The Source string of this Test",
                "s test",
                null,
                string.Empty);

            HelperTestStartsOrEndsWith(
                "ENDSWITH",
                "The Source string of this Test",
                "s Test",
                string.Empty,
                "The Source string of this Test");

            HelperTestStartsOrEndsWith(
                "ENDSWITH",
                "The Source string of this Test",
                "s Test",
                null,
                "The Source string of this Test");

            // Negative
            HelperTestStartsOrEndsWith(
                "ENDSWITH",
                string.Empty,
                "s test",
                "CI",
                null);

            HelperTestStartsOrEndsWith(
                "ENDSWITH",
                null,
                "s test",
                "CI",
                null);

            HelperTestStartsOrEndsWith(
                "ENDSWITH",
                "The Source string of this Test",
                string.Empty,
                "CI",
                null);

            HelperTestStartsOrEndsWith(
                "ENDSWITH",
                "The Source string of this Test",
                null,
                "CI",
                null);

            HelperTestStartsOrEndsWith(
                "ENDSWITH",
                "The Source string of this Test",
                "s Test",
                "CC",
                null);
        }

        /// <summary>
        /// The helper test Pad.... functions.
        /// </summary>
        /// <param name="direction">
        /// The direction of the padding PADRIGHT or PADLEFT
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
        /// The toCase string TOUPPER or TOLOWER
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
        /// The helper test startsWith and endsWith.... function.
        /// </summary>
        /// <param name="startsOrEndsWith">
        /// The startsOrEndsWith string STARTSWITH or ENDSWITH
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

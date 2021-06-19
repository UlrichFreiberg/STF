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
        /// The test stu function for IndexOf.
        /// </summary>
        [TestMethod]
        public void TestStuStringFunctionIndexOf()
        {
            // Positive
            HelperTestIndexOf("The quick Brown Fox jumps", "qui", "1", "CS", "4");
            HelperTestIndexOf("The quick Brown Fox jumps", "Qui", "1", "CS", "-1");
            HelperTestIndexOf("The quick Brown Fox jumps", "Qui", "1", "CI", "4");
            HelperTestIndexOf("The quick Brown Fox jumps", "qui", "8", "CS", "-1");
            HelperTestIndexOf("The quick Brown Fox jumps", null, "3", "CI", "3");
            HelperTestIndexOf("The quick Brown Fox jumps", string.Empty, "5", "CI", "5");

            // Negative
            HelperTestIndexOf("The quick Brown Fox jumps", "qui", "-1", "CS", "-1");
            HelperTestIndexOf("The quick Brown Fox jumps", "qui", "26", "CS", "-1");
            HelperTestIndexOf("The quick Brown Fox jumps", "qui", "A", "CS", "-1");
        }

        /// <summary>
        /// The test stu function for IndexOf.
        /// </summary>
        [TestMethod]
        public void TestStuStringFunctionSubstring()
        {
            // Positive
            HelperTestSubstring("The quick Brown Fox jumps", "1", "1", "h");
            HelperTestSubstring("The quick Brown Fox jumps", "1", "4", "he q");
            HelperTestSubstring("The quick Brown Fox jumps", "10", null, "Brown Fox jumps");
            HelperTestSubstring(" The quick Brown Fox jumps ", "11", null, "Brown Fox jumps ");
            HelperTestSubstring("The quick Brown Fox jumps", "10", "0", string.Empty);
            HelperTestSubstring("The quick Brown Fox jumps", "10", string.Empty, "Brown Fox jumps");
            HelperTestSubstring(" The quick Brown Fox jumps ", "15", "12", "n Fox jumps ");

            // Negative
            HelperTestSubstring(" The quick Brown Fox jumps ", "211", null, null);
            HelperTestSubstring(" The quick Brown Fox jumps ", "15", "13", null);
            HelperTestSubstring(" The quick Brown Fox jumps ", "-1", "1", null);
            HelperTestSubstring(" The quick Brown Fox jumps ", "A", "1", null);
            HelperTestSubstring(" The quick Brown Fox jumps ", "5", "-1", null);
        }

        /// <summary>
        /// The test stu function for Compare.
        /// </summary>
        [TestMethod]
        public void TestStuStringFunctionCompare()
        {
            // Positive
            HelperTestCompare("BBBaaa", "BBBaaa", "CS", "0");
            HelperTestCompare("BBBaaa", "BABaaa", "CS", "1");
            HelperTestCompare("BBBaaa", "BCBaaa", "CS", "-1");
            HelperTestCompare(string.Empty, "BBBaaa", "CS", "-1");
            HelperTestCompare(null, "BBBaaa", "CS", "-1");
            HelperTestCompare(string.Empty, string.Empty, "CS", "0");
            HelperTestCompare(null, null, "CS", "0");
            HelperTestCompare("BBBaaa", string.Empty, "CS", "1");
            HelperTestCompare("BBBaaa", null, "CS", "1");
            HelperTestCompare("BBBaaa", "BbBaaa", "CS", "1");
            HelperTestCompare("BBBaaa", "BbBaaa", "CI", "0");

            // Negative
            HelperTestCompare("BBBaaa", "BCBaaa", "CA", null);
        }

        /// <summary>
        /// The test stu function for Length.
        /// </summary>
        [TestMethod]
        public void TestStuStringFunctionLength()
        {
            // Positive
            HelperTestLength("BBBaaa", "6");
            HelperTestLength(" BBBaaa ", "8");
            HelperTestLength(string.Empty, "0");
            HelperTestLength(null, "0");
        }

        /// <summary>
        /// The test stu  function for PadRight.
        /// </summary>
        [TestMethod]
        public void TestStuStringFunctionPadRight()
        {
            // Positive
            HelperTestPad("PadRight", "Aaaa", "7", "X", "AaaaXXX", true);
            HelperTestPad("PadRight", "Aaaa", "4", "X", "Aaaa", true);
            HelperTestPad("PadRight", "A aa", "3", "X", "A aa");
            HelperTestPad("PadRight", "Aa!a", "0", "X", "Aa!a");
            HelperTestPad("PadRight", string.Empty, "0", "X", string.Empty, true);
            HelperTestPad("PadRight", null, "0", "X", string.Empty, true);
            HelperTestPad("PadRight", string.Empty, "4", "X", "XXXX", true);
            HelperTestPad("PadRight", null, "5", "X", "XXXXX", true);
            HelperTestPad("PadRight", "Aaaa", "7", " ", "Aaaa   ", true);

            HelperTestPad("PadRight", " bbb", "4", "X", " bbb");
            HelperTestPad("PadRight", " bbb", "7", "Q", " bbbQQQ");

            HelperTestPad("PadRight", "ccc ", "4", "X", "ccc ");
            HelperTestPad("PadRight", "ccc ", "7", "!", "ccc !!!");
            HelperTestPad("PadRight", "c cc ", "7", " ", "c cc   ");

            HelperTestPad("PadRight", "aaaa", "7", string.Empty, "aaaa   ", true);
            HelperTestPad("PadRight", "aaaa", "7", null, "aaaa   ", true);

            // Negative
            HelperTestPad("PadRight", "aaaa", "-1", "X", null);
            HelperTestPad("PadRight", "aaaa", "Q", "X", null);
            HelperTestPad("PadRight", "aaaa", "2147483647", "X", null);
            HelperTestPad("PadRight", "aaaa", string.Empty, "X", null);
            HelperTestPad("PadRight", "aaaa", null, "X", null);
        }

        /// <summary>
        /// The test stu unique function for PadLeft.
        /// </summary>
        [TestMethod]
        public void TestStuStringFunctionPadLeft()
        {
            // Positive
            HelperTestPad("PadLeft", "Aaaa", "7", "X", "XXXAaaa", true);
            HelperTestPad("PadLeft", "Aaaa", "4", "X", "Aaaa", true);
            HelperTestPad("PadLeft", "A aa", "3", "X", "A aa");
            HelperTestPad("PadLeft", "Aa!a", "0", "X", "Aa!a");
            HelperTestPad("PadLeft", string.Empty, "0", "X", string.Empty, true);
            HelperTestPad("PadLeft", null, "0", "X", string.Empty, true);
            HelperTestPad("PadLeft", string.Empty, "4", "X", "XXXX", true);
            HelperTestPad("PadLeft", null, "5", "X", "XXXXX", true);
            HelperTestPad("PadLeft", "Aaaa", "7", " ", "   Aaaa", true);

            HelperTestPad("PadLeft", " bbb", "4", "X", " bbb");
            HelperTestPad("PadLeft", " bbb", "7", "Q", "QQQ bbb");

            HelperTestPad("PadLeft", "ccc ", "4", "X", "ccc ");
            HelperTestPad("PadLeft", "ccc ", "7", "!", "!!!ccc ");
            HelperTestPad("PadLeft", "c cc ", "7", " ", "  c cc ");

            HelperTestPad("PadLeft", "aaaa", "7", string.Empty, "   aaaa", true);
            HelperTestPad("PadLeft", "aaaa", "7", null, "   aaaa", true);

            // Negative
            HelperTestPad("PadLeft", "aaaa", "-1", "X", null);
            HelperTestPad("PadLeft", "aaaa", "Q", "X", null);
            HelperTestPad("PadLeft", "aaaa", "2147483647", "X", null);
            HelperTestPad("PadLeft", "aaaa", string.Empty, "X", null);
            HelperTestPad("PadLeft", "aaaa", null, "X", null);
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
                StuBoolean.True.ToString());

            HelperTestStartsOrEndsWith(
                "StartsWith",
                "The s",
                "The Source string of this test",
                "CS",
                StuBoolean.False.ToString());

            HelperTestStartsOrEndsWith(
                "StartsWith",
                "The Source string of this test",
                "The S",
                "CI",
                StuBoolean.True.ToString());

            HelperTestStartsOrEndsWith(
                "StartsWith",
                "The Source string of this test",
                "The s",
                "CI",
                StuBoolean.True.ToString());

            HelperTestStartsOrEndsWith(
                "StartsWith",
                "The Source string of this test",
                "The s",
                null,
                StuBoolean.False.ToString());

            HelperTestStartsOrEndsWith(
                "StartsWith",
                "The Source string of this test",
                "The S",
                string.Empty,
                StuBoolean.True.ToString());

            HelperTestStartsOrEndsWith(
                "StartsWith",
                "The Source string of this test",
                "The S",
                null,
                StuBoolean.True.ToString());

            // Negative
            HelperTestStartsOrEndsWith(
                "StartsWith",
                string.Empty,
                "The s",
                "CI",
                StuBoolean.False.ToString());

            HelperTestStartsOrEndsWith(
                "StartsWith",
                null,
                "The s",
                "CI",
                StuBoolean.False.ToString());

            HelperTestStartsOrEndsWith(
                "StartsWith",
                "The Source string of this test",
                string.Empty,
                "CI",
                StuBoolean.False.ToString());

            HelperTestStartsOrEndsWith(
                "StartsWith",
                "The Source string of this test",
                null,
                "CI",
                StuBoolean.False.ToString());

            HelperTestStartsOrEndsWith(
                "StartsWith",
                "The Source string of this test",
                "The S",
                "CC",
                StuBoolean.False.ToString());
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
                StuBoolean.True.ToString());

            HelperTestStartsOrEndsWith(
                "EndsWith",
                "The Source string of this Test",
                "s test",
                "CS",
                StuBoolean.False.ToString());

            HelperTestStartsOrEndsWith(
                "EndsWith",
                "The Source string of this Test",
                "s Test",
                "CI",
                StuBoolean.True.ToString());

            HelperTestStartsOrEndsWith(
                "EndsWith",
                "The Source string of this Test",
                "s test",
                "CI",
                StuBoolean.True.ToString());

            HelperTestStartsOrEndsWith(
                "EndsWith",
                "The Source string of this Test",
                "s test",
                string.Empty,
                StuBoolean.False.ToString());

            HelperTestStartsOrEndsWith(
                "EndsWith",
                "The Source string of this Test",
                "s test",
                null,
                StuBoolean.False.ToString());

            HelperTestStartsOrEndsWith(
                "EndsWith",
                "The Source string of this Test",
                "s Test",
                string.Empty,
                StuBoolean.True.ToString());

            HelperTestStartsOrEndsWith(
                "EndsWith",
                "The Source string of this Test",
                "s Test",
                null,
                StuBoolean.True.ToString());

            // Negative
            HelperTestStartsOrEndsWith(
                "EndsWith",
                string.Empty,
                "s test",
                "CI",
                StuBoolean.False.ToString());

            HelperTestStartsOrEndsWith(
                "EndsWith",
                null,
                "s test",
                "CI",
                StuBoolean.False.ToString());

            HelperTestStartsOrEndsWith(
                "EndsWith",
                "The Source string of this Test",
                string.Empty,
                "CI",
                StuBoolean.False.ToString());

            HelperTestStartsOrEndsWith(
                "EndsWith",
                "The Source string of this Test",
                null,
                "CI",
                StuBoolean.False.ToString());

            HelperTestStartsOrEndsWith(
                "EndsWith",
                "The Source string of this Test",
                "s Test",
                "CC",
                StuBoolean.False.ToString());
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
        /// <param name="testLengthAgainstTotalWidth">
        /// The test Length Against Total Width.
        /// </param>
        private void HelperTestPad(string direction, string source, string totalWidth, string paddingChar, string expected, bool testLengthAgainstTotalWidth = false)
        {
            var arg = $@"""{direction}"" ""{source}"" ""{totalWidth}"" ""{paddingChar}""";
            var actual = stringTransformationUtils.EvaluateFunction("STRING", arg);

            StfAssert.AreEqual($"Unittest {direction} test actual / expected", expected, actual);
            if (testLengthAgainstTotalWidth)
            {
                StfAssert.AreEqual($"Unittest {direction} test actual length / total width", expected.Length.ToString(), totalWidth);
            }
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

        /// <summary>
        /// The helper test IndexOf function.
        /// </summary>
        /// <param name="source">
        /// The source string
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="startIndex">
        /// The start Index.
        /// </param>
        /// <param name="stringComparison">
        /// The stringComparison string
        /// </param>
        /// <param name="expected">
        /// The expected string
        /// </param>
        private void HelperTestIndexOf(
            string source,
            string value,
            string startIndex,
            string stringComparison,
            string expected)
        {
            var arg = $@"""IndexOf"" ""{source}"" ""{value}"" ""{startIndex}"" ""{stringComparison}""";
            var actual = stringTransformationUtils.EvaluateFunction("STRING", arg);

            StfAssert.AreEqual($"Unittest IndexOf,{source},{value}, {startIndex},{stringComparison} test actual / expected", expected, actual);
        }

        /// <summary>
        /// The helper test Substring function.
        /// </summary>
        /// <param name="source">
        /// The source string
        /// </param>
        /// <param name="startIndex">
        /// The value.
        /// </param>
        /// <param name="length">
        /// The start Index.
        /// </param>
        /// <param name="expected">
        /// The expected string
        /// </param>
        private void HelperTestSubstring(
            string source,
            string startIndex,
            string length,
            string expected)
        {
            var arg = $@"""Substring"" ""{source}"" ""{startIndex}"" ""{length}""";
            var actual = stringTransformationUtils.EvaluateFunction("STRING", arg);

            StfAssert.AreEqual($"Unittest Substring,{source}, {startIndex}, {length} test actual / expected", expected, actual);
        }

        /// <summary>
        /// The helper test Compare function.
        /// </summary>
        /// <param name="sourceA">
        /// The sourceA string
        /// </param>
        /// <param name="sourceB">
        /// The source B.
        /// </param>
        /// <param name="stringComparison">
        /// The string Comparison.
        /// </param>
        /// <param name="expected">
        /// The expected string
        /// </param>
        private void HelperTestCompare(
            string sourceA,
            string sourceB,
            string stringComparison,
            string expected)
        {
            var arg = $@"""Compare"" ""{sourceA}"" ""{sourceB}"" ""{stringComparison}""";
            var actual = stringTransformationUtils.EvaluateFunction("STRING", arg);

            StfAssert.AreEqual($"Unittest Compare,{sourceA}, {sourceB}, {stringComparison} test actual / expected", expected, actual);
        }

        /// <summary>
        /// The helper test Length function.
        /// </summary>
        /// <param name="source">
        /// The source string
        /// </param>
        /// <param name="expected">
        /// The expected string
        /// </param>
        private void HelperTestLength(
            string source,
            string expected)
        {
            var arg = $@"""Length"" ""{source}"" ";
            var actual = stringTransformationUtils.EvaluateFunction("STRING", arg);

            StfAssert.AreEqual($"Unittest Length,{source} test actual / expected", expected, actual);
        }
    }
}

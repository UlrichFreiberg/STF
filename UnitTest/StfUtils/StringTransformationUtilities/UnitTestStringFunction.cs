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

    }
}

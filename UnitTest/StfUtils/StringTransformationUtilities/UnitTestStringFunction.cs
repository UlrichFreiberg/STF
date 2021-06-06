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
        /// The test stu unique functions.
        /// </summary>
        [TestMethod]
        public void TestStuStringFunction()
        {
            // Positive
            HelperTestPadRight("A source", "14", "X", "A sourceXXXXXX");

            // Negative
            HelperTestPadRight(string.Empty, "14", "X", null);
            HelperTestPadRight(null, "14", "X", null);

            HelperTestPadRight("A source", string.Empty, "X", null);
            HelperTestPadRight("A source", null, "X", null);

            HelperTestPadRight("A source", "14", string.Empty, "A source      ");
            HelperTestPadRight("A source", "14", null, "A source      ");
        }

        /// <summary>
        /// The helper test GUID functions.
        /// </summary>
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
        private void HelperTestPadRight(string source, string totalWidth, string paddingChar, string expected)
        {
            var arg = $@"""PADRIGHT"" ""{source}"" ""{totalWidth}"" ""{paddingChar}""";
            var actual = stringTransformationUtils.EvaluateFunction("STRING", arg);

            StfAssert.AreEqual("UnittestPadRight test actual / expected", expected, actual);
        }

    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestFormatFunction.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The unit test select function.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.StringTransformationUtilities
{
    using Mir.Stf.Utilities.StringTransformationUtilities;

    /// <summary>
    /// The unit test format function.
    /// </summary>
    [TestClass]
    public class UnitTestFormatFunction : UnitTestScriptBase
    {
        /// <summary>
        /// The string transformation utils.
        /// </summary>
        private readonly StringTransformationUtils stringTransformationUtils = new StringTransformationUtils();

        /// <summary>
        /// The test stu select function.
        /// </summary>
        [TestMethod]
        public void TestStuFormatFunction()
        {
            HelperTestFormat(@"DATE 2021-12-31 yyyy-MM-dd ""dd/MM/yyyy""", "31/12/2021");
            HelperTestFormat("FLOAT 23.34 en-US da-DK", "23,34");
            HelperTestFormat("FLOAT 23,34 da-DK en-US", "23.34");

            // Ensure we do handle usual string weirdos
            HelperTestFormat(string.Empty, null);
            HelperTestFormat(null, null);
        }

        /// <summary>
        /// The helper test format.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <param name="expected">
        /// The expected.
        /// </param>
        private void HelperTestFormat(string arg, string expected)
        {
            var actual = stringTransformationUtils.EvaluateFunction("FORMAT", arg);

            StfAssert.AreEqual(arg, expected, actual);
        }
    }
}

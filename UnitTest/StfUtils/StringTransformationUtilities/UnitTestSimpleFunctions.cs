// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestSimpleFunctions.cs" company="Mir Software">
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
    /// The unit test simple functions.
    /// </summary>
    [TestClass]
    public class UnitTestSimpleFunctions : UnitTestScriptBase
    {
        /// <summary>
        /// The string transformation utils.
        /// </summary>
        private readonly StringTransformationUtils stringTransformationUtils = new StringTransformationUtils();

        /// <summary>
        /// The test stu simple functions.
        /// </summary>
        [TestMethod]
        public void TestStuSimpleFunctions()
        {
            HelperTestSimple("EMPTY", "HEJ", string.Empty);
            HelperTestSimple("EMPTY", string.Empty, string.Empty);
            HelperTestSimple("EMPTY", null, string.Empty);

            HelperTestSimple("SPACE", "5", "     ");
            HelperTestSimple("SPACE", "HEJ", " ");
            HelperTestSimple("SPACE", string.Empty, " ");
            HelperTestSimple("SPACE", null, " ");
        }

        /// <summary>
        /// The helper test simple functions.
        /// </summary>
        /// <param name="functionName">
        /// The function name.
        /// </param>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <param name="expected">
        /// The expected.
        /// </param>
        private void HelperTestSimple(string functionName, string arg, string expected)
        {
            var actual = stringTransformationUtils.EvaluateFunction(functionName, arg);

            StfAssert.AreEqual(arg, expected, actual);
        }
    }
}

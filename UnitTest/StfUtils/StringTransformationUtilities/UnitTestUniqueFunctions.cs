// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestUniqueFunctions.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The unit test unique functions.
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
    public class UnitTestUniqueFunctions : UnitTestScriptBase
    {
        /// <summary>
        /// The string transformation utils.
        /// </summary>
        private readonly StringTransformationUtils stringTransformationUtils = new StringTransformationUtils();

        /// <summary>
        /// The test stu unique functions.
        /// </summary>
        [TestMethod]
        public void TestStuUniqueFunctions()
        {
            HelperTestGuid("GUID", "N", true);
            HelperTestGuid("GUID", "D", true);
            HelperTestGuid("GUID", "B", true);
            HelperTestGuid("GUID", "P", true);
            HelperTestGuid("GUID", "X", true);

            HelperTestGuid("GUID", string.Empty, true);
            HelperTestGuid("GUID", null, true);

            HelperTestGuid("GUID", "Q", false);
            HelperTestGuid("GUID", "BX", false);

            // HelperTestGuid("GUID", "Q", false);
        }

        /// <summary>
        /// The helper test GUID functions.
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
        private void HelperTestGuid(string functionName, string arg, bool expected)
        {
            var guid = stringTransformationUtils.EvaluateFunction(functionName, arg);
            var formatArg = string.IsNullOrEmpty(arg) ? "D" : arg;
            var actual = System.Guid.TryParseExact(guid, formatArg, out _);

            StfAssert.AreEqual($"{functionName} - {arg}", expected, actual);
        }
    }
}

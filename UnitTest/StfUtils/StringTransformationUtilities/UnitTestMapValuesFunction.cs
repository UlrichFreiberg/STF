// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestMapValuesFunction.cs" company="Mir Software">
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
    public class UnitTestMapValuesFunction : UnitTestScriptBase
    {
        /// <summary>
        /// The string transformation utils.
        /// </summary>
        private readonly StringTransformationUtils stringTransformationUtils = new StringTransformationUtils();

        /// <summary>
        /// The test stu map values function.
        /// </summary>
        [TestMethod]
        public void TestStuMapValuesFunction()
        {
            HelperTestSimple("41 [41;42;43] [one;two;three]", "one");
            HelperTestSimple("42 [41;42;43] [one;two;three]", "two");
            HelperTestSimple("43 [41;42;43] [one;two;three]", "three");

            // Ensure we do handle usual string weirdos
            HelperTestSimple(string.Empty, null);
            HelperTestSimple(null, null);
        }

        /// <summary>
        /// The helper test simple functions.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <param name="expected">
        /// The expected.
        /// </param>
        private void HelperTestSimple(string arg, string expected)
        {
            var actual = stringTransformationUtils.EvaluateFunction("MAPVALUES", arg);

            StfAssert.AreEqual(arg, expected, actual);
        }
    }
}

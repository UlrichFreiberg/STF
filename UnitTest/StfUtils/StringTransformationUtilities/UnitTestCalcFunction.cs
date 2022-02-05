﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestCalcFunction.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the UnitTestCalcFunction type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.StringTransformationUtilities
{
    using Mir.Stf;
    using Mir.Stf.Utilities.StfTestUtilities;
    using Mir.Stf.Utilities.StringTransformationUtilities;

    /// <summary>
    /// The unit test calc function.
    /// </summary>
    [TestClass]
    public class UnitTestCalcFunction : StfTestScriptBase
    { 
        /// <summary>
        /// Gets or sets the stf test utils.
        /// </summary>
        private StfTestUtils StfTestUtils { get; set; }

        /// <summary>
        /// The test initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            StfTestUtils = new StfTestUtils(0);
        }

        /// <summary>
        /// The test stu Calc function.
        /// </summary>
        [TestMethod]
        public void TestStuCalcFunction()
        {
            HelperTestCalc("41", "41");
            HelperTestCalc("41+1", "42");
            HelperTestCalc("41 +1", "42");
            HelperTestCalc("41 + 1", "42");

            HelperTestCalc("(7-4) * (2+3)", "15");

            // Ensure we do handle usual string weirdos
            HelperTestCalc(string.Empty, null);
            HelperTestCalc(null, null);
        }

        /// <summary>
        /// The helper test calc.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <param name="expected">
        /// The expected.
        /// </param>
        private void HelperTestCalc(string arg, string expected)
        {
            var actual = StfTestUtils.StringTransformationUtils.EvaluateFunction("CALC", arg);

            StfAssert.AreEqual(arg, expected, actual);
        }
    }
}

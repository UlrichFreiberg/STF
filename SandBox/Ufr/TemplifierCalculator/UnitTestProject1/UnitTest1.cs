// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTest1.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the UnitTest1 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    using Mir.Stf;

    using Templifier;

    /// <summary>
    /// The unit test 1.
    /// </summary>
    [TestClass]
    public class UnitTest1 : StfTestScriptBase
    {
        /// <summary>
        /// The test method Simple.
        /// </summary>
        [TestMethod]
        public void TestMethodSimple()
        {
            var templifierCalculator = new TemplifierCalculator();

            templifierCalculator.Init("{CALC 2 + 2}");
            StfAssert.StringEquals(templifierCalculator.CurrentTemplifierStatement, "4", templifierCalculator.CalculateExpression());

            templifierCalculator.Init("{CALC 6 / 2}");
            StfAssert.StringEquals(templifierCalculator.CurrentTemplifierStatement, "3", templifierCalculator.CalculateExpression());
        }

        /// <summary>
        /// The test method advanced.
        /// </summary>
        [TestMethod]
        public void TestMethodAdvanced()
        {
            var templifierCalculator = new TemplifierCalculator();

            templifierCalculator.Init("BasePrice = {CALC {BasePrice} + {Provision}}");
            templifierCalculator.LeftOperator = "23";
            templifierCalculator.RightOperator = "45";
            StfAssert.StringEquals(templifierCalculator.CurrentTemplifierStatement, "68", templifierCalculator.CalculateExpression());
        }
    }
}

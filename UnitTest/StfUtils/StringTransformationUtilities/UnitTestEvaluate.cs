// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestEvaluate.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The unit test evaluate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace UnitTest.StringTransformationUtilities
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Mir.Stf;
    using Mir.Stf.Utilities.StringTransformationUtilities;

    /// <summary>
    /// The unit test evaluate.
    /// </summary>
    [TestClass]
    public class UnitTestEvaluate : StfTestScriptBase
    {
        /// <summary>
        /// The string transformation utils.
        /// </summary>
        private readonly StringTransformationUtils stringTransformationUtils = new StringTransformationUtils();

        /// <summary>
        /// The test stu unique functions.
        /// </summary>
        [TestMethod]
        public void TestStuEvaluate()
        {
            HelperTestEvaluate("Bob", "Bob");
            HelperTestEvaluate("{CALC 3 + 5}", "8");
        }

        /// <summary>
        /// The helper test evaluate.
        /// </summary>
        /// <param name="statement">
        /// The statement.
        /// </param>
        /// <param name="expected">
        /// The expected.
        /// </param>
        private void HelperTestEvaluate(string statement, string expected)
        {
            var actual = stringTransformationUtils.Evaluate(statement);

            StfAssert.StringEqualsCi(statement, expected, actual);
        }
    }
}

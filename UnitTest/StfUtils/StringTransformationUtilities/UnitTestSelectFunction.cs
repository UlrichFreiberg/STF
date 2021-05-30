// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestSelectFunction.cs" company="Mir Software">
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
    using System.Collections.Generic;

    using Mir.Stf.Utilities.StringTransformationUtilities;

    /// <summary>
    /// The unit test select function.
    /// </summary>
    [TestClass]
    public class UnitTestSelectFunction
    {
        /// <summary>
        /// The string transformation utils.
        /// </summary>
        private readonly StringTransformationUtils stringTransformationUtils = new StringTransformationUtils();

        /// <summary>
        /// The test stu select function.
        /// </summary>
        [TestMethod]
        public void TestStuSelectFunction()
        {
            HelperTestSelect("41", "41");

            // Ensure we do handle usual string weirdos
            HelperTestSelect(string.Empty, null);
            HelperTestSelect(null, null);

            HelperMultiTestSelect("41;42", 2);
        }

        /// <summary>
        /// The helper multi test select.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <param name="numberOfValues">
        /// The number of values.
        /// </param>
        private void HelperMultiTestSelect(string arg, int numberOfValues)
        {
            var dictOutcomes = new Dictionary<string, int>();

            for (var i = 0; i < 50 * numberOfValues; i++)
            {
                var actual = stringTransformationUtils.EvaluateFunction("SELECT", arg);

                if (!dictOutcomes.ContainsKey(actual))
                {
                    dictOutcomes[actual] = 0;
                }

                dictOutcomes[actual]++;
            }

            // TODO: Missing a evaluation of the distribution - for now we check all values are chooen
            Assert.AreEqual(dictOutcomes.Count, numberOfValues);
        }

        /// <summary>
        /// The helper test select.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <param name="expected">
        /// The expected.
        /// </param>
        private void HelperTestSelect(string arg, string expected)
        {
            var actual = stringTransformationUtils.EvaluateFunction("SELECT", arg);

            Assert.AreEqual(actual, expected);
        }
    }
}

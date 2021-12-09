// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestStfTestUtilities.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the UnitTestCalcFunction type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.StfTestUtilities
{
    using Mir.Stf.Utilities.StfTestUtilities;

    /// <summary>
    /// The unit test Stf Test Utilities function.
    /// </summary>
    [TestClass]
    public class UnitTestStfTestUtilities : UnitTestScriptBase
    {
        /// <summary>
        /// The test Stf Test Utilities.
        /// </summary>
        [TestMethod]
        public void TestStfTestTestCases()
        {
            HelperStfTestUtilities("number of unit test cases", 8);
            HelperStfTestUtilities("number of unit test cases for specific rootfolder ",
                @".\TestData\StfTestUtilities", 
                4);
        }

        /// <summary>
        /// The helper stf test Utilities.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <param name="expected">
        /// The expected.
        /// </param>
        private void HelperStfTestUtilities(string arg, int expected)
        {
            var stfTestUtilities = new StfTestUtilities();
            var actual = stfTestUtilities.GetTestCaseFolderPathsFromCache();

            StfAssert.AreEqual(arg, expected, actual.Count);
        }

        /// <summary>
        /// The helper stf test Utilities.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <param name="expected">
        /// The expected.
        /// </param>
        private void HelperStfTestUtilities(string arg, string rootFolder, int expected)
        {
            var stfTestUtilities = new StfTestUtilities(rootFolder);
            var actual = stfTestUtilities.GetTestCaseFolderPathsFromCache();

            StfAssert.AreEqual(arg, expected, actual.Count);
        }

    }
}

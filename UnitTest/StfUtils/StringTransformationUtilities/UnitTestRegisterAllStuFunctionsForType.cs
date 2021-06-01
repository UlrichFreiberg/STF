// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestRegisterAllStuFunctionsForType.cs" company="Mir Software">
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
    using System.Diagnostics.CodeAnalysis;

    using Mir.Stf.Utilities.StringTransformationUtilities;

    /// <summary>
    /// The unit test register all stu functions for type.
    /// </summary>
    [TestClass]
    public class UnitTestRegisterAllStuFunctionsForType
    {
        /// <summary>
        /// The string transformation utils.
        /// </summary>
        private readonly StringTransformationUtils stringTransformationUtils = new StringTransformationUtils();

        /// <summary>
        /// The test register all stu functions for type.
        /// </summary>
        [TestMethod]
        public void TestRegisterAllStuFunctionsForType()
        {
            stringTransformationUtils.RegisterAllStuFunctionsForType(new BobClassNotTheFunctionName());

            HelperTestBob("BOB", "HEJ", "BOB [HEJ]");
            HelperTestBob("BOB", string.Empty, "BOB []");
            HelperTestBob("BOB", null, "BOB []");
        }

        /// <summary>
        /// The helper test bob.
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
        private void HelperTestBob(string functionName, string arg, string expected)
        {
            var actual = stringTransformationUtils.EvaluateFunction(functionName, arg);

            Assert.AreEqual(actual, expected);
        }
    }

    /// <summary>
    /// The bob class not the function name.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed. Suppression is OK here.")]
    public class BobClassNotTheFunctionName
    {
        /// <summary>
        /// The test function BOB.
        /// </summary>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        [StringTransformationUtilFunction("BOB")]
        public string ImplmentationOfBob(string arg)
        {
            var retVal = $"BOB [{arg}]";
            return retVal;
        }
    }
}

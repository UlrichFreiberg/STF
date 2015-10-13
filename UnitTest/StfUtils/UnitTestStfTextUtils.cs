// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestStfTextUtils.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Mir.Stf.KernelUtils;

namespace UnitTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// UnitTests for StfTextUtils
    /// </summary>
    [TestClass]
    public class UnitTestStfTextUtils
    {
        /// <summary>
        /// Test for Expanding Dos Environment Variables
        /// </summary>
        [TestMethod]
        public void TestMethodExpandDosEnvironmentVariables()
        {
            var textUtils = new StfTextUtils();
            const string Expected = "SystemDrive=C:";

            var expandedLine = textUtils.ExpandVariables("SystemDrive=%SystemDrive%");

            Assert.IsTrue(expandedLine == Expected);
        }

        /// <summary>
        /// Test for Registering Varibles
        /// </summary>
        [TestMethod]
        public void TestRegisterVar()
        {
            var textUtils = new StfTextUtils();

            textUtils.Register("UnitTestVar", @"c:\temp");

            CheckExpandedExpression(textUtils, "UnitTestVar=%UnitTestVar%", @"UnitTestVar=c:\temp");
            CheckExpandedExpression(textUtils, "UnitTestVar=%UnitTestVar%", @"UnitTestVar=c:\temp");
            CheckExpandedExpression(textUtils, "UnitTestVar=%UnitTestVar% - UnitTestVar=%UnitTestVar%", @"UnitTestVar=c:\temp - UnitTestVar=c:\temp");

            CheckExpandedExpression(textUtils, "UnitTestVar=%UnitTestVar% - SystemDrive=%SystemDrive%", @"UnitTestVar=c:\temp - SystemDrive=C:");
        }

        /// <summary>
        /// Test for Registering and chaning values for Varibles 
        /// </summary>
        [TestMethod]
        public void TestRegisterVarChangeValue()
        {
            var textUtils = new StfTextUtils();

            textUtils.Register("UnitTestVar", @"c:\temp");
            CheckExpandedExpression(textUtils, "UnitTestVar=%UnitTestVar%", @"UnitTestVar=c:\temp");

            textUtils.Register("UnitTestVar", @"c:\temp2");
            CheckExpandedExpression(textUtils, "UnitTestVar=%UnitTestVar%", @"UnitTestVar=c:\temp2");
        }

        /// <summary>
        /// Expand and verify 
        /// </summary>
        /// <param name="stfTextUtils">
        /// The text util 
        /// </param>
        /// <param name="expression">
        /// Expression to expand
        /// </param>
        /// <param name="expected">
        /// What it should be expanded to
        /// </param>
        // ReSharper disable once UnusedParameter.Local
        private static void CheckExpandedExpression(StfTextUtils stfTextUtils, string expression, string expected)
        {
            var expandedLine = stfTextUtils.ExpandVariables(expression);
            Assert.IsTrue(expandedLine == expected);
        }
    }
}

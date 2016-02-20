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

namespace DataDriven
{
    using System.Diagnostics;

    /// <summary>
    /// The unit test 1.
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Gets or sets the test context.
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// The test method ms test style.
        /// </summary>
        [TestMethod,
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", 
                    @".\DataDrive.csv",
                    "DataDrive#csv", 
                    DataAccessMethod.Sequential),
        DeploymentItem(@".\DataDrive.csv")]
        public void TestMethodMsTestStyle()
        {
            var a = TestContext.DataRow["A"];
            var b = TestContext.DataRow["B"];
            int oneNumber;

            if (!int.TryParse(TestContext.DataRow["OneNumber"].ToString(), out oneNumber))
            {
                oneNumber = -1;
            }

            Debug.WriteLine("A={0}, b={1}, c+1={2} ", a, b, oneNumber + 1);
        }

        /// <summary>
        /// The test method stf style.
        /// </summary>
        [TestMethod,
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    @".\DataDrive.csv",
                    "DataDrive#csv",
                    DataAccessMethod.Sequential),
        DeploymentItem(@".\DataDrive.csv")]
        public void TestMethodStfStyle()
        {
            var myTestData = StfTestDataInitialize.InitObject<UnitTestTestDataInput>(TestContext.DataRow);

            Debug.WriteLine("A={0}, b={1}", myTestData.A, myTestData.B);
        }
    }
}

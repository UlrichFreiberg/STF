// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfArchiverTests.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StfArchiverTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Mir.Stf.Utilities;

    /// <summary>
    /// </summary>
    [TestClass]
    public class StfArchiverTests
    {
        /// <summary>
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestPerformArchiveZeroAndOneFile()
        {
            var s = new StfArchiver(TestContext.TestName);

            var emptyStatusTxt = s.Status();
            Assert.AreEqual("Nothing to Archive", emptyStatusTxt);

            s.AddFile(@"C:\Temp\Stf\Config\StfConfiguration.xml");
            var oneFileStatusTxt = s.Status();
            const string ExpectedStatusTxt = "Files to archive\n\tC:\\Temp\\Stf\\Config\\StfConfiguration.xml\n";
            Assert.AreEqual(ExpectedStatusTxt, oneFileStatusTxt);

            s.PerformArchive();
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestPerformArchiveOneDirectory()
        {
            var s = new StfArchiver(TestContext.TestName);

            s.AddDirectory(@"C:\Temp\Stf\Config");

            var oneFileStatusTxt = s.Status();
            const string ExpectedStatusTxt = "Directories to archive\n\tC:\\Temp\\Stf\\Config\n";
            Assert.AreEqual(ExpectedStatusTxt, oneFileStatusTxt);

            s.PerformArchive();
        }
    }
}

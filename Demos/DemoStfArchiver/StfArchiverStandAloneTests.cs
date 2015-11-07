// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfArchiverStandAloneTests.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AgoraCorp.Stf.DemoTests
{
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Mir.Stf.Utilities;

    /// <summary>
    /// Stf Archiver Tests
    /// </summary>
    [TestClass]
    public class StfArchiverStandAloneTests
    {
        /// <summary>
        /// Gets or sets the standard text context
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// Test Perform Archive with Zero And One File
        /// </summary>
        [TestMethod]
        public void TestPerformArchiveZeroAndOneFile()
        {
            var stfArchiver = new StfArchiver(TestContext.TestName);

            stfArchiver.AddFile(@"C:\Temp\Stf\Config\StfConfiguration.xml");
            stfArchiver.PerformArchive();
        }

        /// <summary>
        /// Test Perform Archive One Directory
        /// </summary>
        [TestMethod]
        public void TestPerformArchiveOneDirectory()
        {
            var stfArchiver = new StfArchiver(TestContext.TestName);

            stfArchiver.AddDirectory(@"C:\Temp\Stf\Config");
            stfArchiver.PerformArchive();
        }

        /// <summary>
        /// The test perform archive and zip.
        /// </summary>
        [TestMethod]
        public void TestPerformArchiveAndZip()
        {
            var stfArchiver = new StfArchiver(TestContext.TestName);
            const string ZipFilename = @"c:\temp\Stf\StfArchiver.zip";

            stfArchiver.AddDirectory(@"C:\Temp\Stf\Config");
            stfArchiver.Configuration.ZipFilename = ZipFilename;
            stfArchiver.PerformArchive();

            Assert.IsTrue(File.Exists(ZipFilename));
        }
    }
}

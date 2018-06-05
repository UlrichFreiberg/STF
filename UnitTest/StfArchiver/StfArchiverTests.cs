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
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Mir.Stf;

    /// <summary>
    /// Stf Archiver Tests
    /// </summary>
    [TestClass]
    public class StfArchiverTests : StfTestScriptBase
    {
        /// <summary>
        /// Test Perform Archive with Zero And One File
        /// </summary>
        [TestMethod]
        public void TestPerformArchiveZeroAndOneFile()
        {
            StfArchiver.Configuration.DoArchiveFoldersAndFiles = true;
            StfArchiver.Configuration.DoArchiveToZipfile = false;

            var emptyStatusTxt = StfArchiver.Status();
            Assert.AreEqual("Nothing to Archive", emptyStatusTxt);

            StfArchiver.AddFile(@"C:\Temp\Stf\Config\StfConfiguration.xml");
            var oneFileStatusTxt = StfArchiver.Status();
            const string ExpectedStatusTxt = "Files to archive\n\tC:\\Temp\\Stf\\Config\\StfConfiguration.xml\n";
            Assert.AreEqual(ExpectedStatusTxt, oneFileStatusTxt);

            StfArchiver.PerformArchive();
        }

        /// <summary>
        /// Test Perform Archive One Directory
        /// </summary>
        [TestMethod]
        public void TestPerformArchiveOneDirectory()
        {
            StfArchiver.AddDirectory(@"C:\Temp\Stf\Config");

            var oneFileStatusTxt = StfArchiver.Status();
            const string ExpectedStatusTxt = "Directories to archive\n\tC:\\Temp\\Stf\\Config\n";
            Assert.AreEqual(ExpectedStatusTxt, oneFileStatusTxt);

            StfArchiver.PerformArchive();
        }

        /// <summary>
        /// The test perform archive and zip.
        /// </summary>
        [TestMethod]
        public void TestPerformArchiveAndZip()
        {
            const string ZipFilename = @"c:\temp\Stf\StfArchiver.zip";

            File.Delete(ZipFilename);
            Assert.IsFalse(File.Exists(ZipFilename));

            StfArchiver.AddDirectory(@"C:\Temp\Stf\Config");
            StfArchiver.Configuration.ZipFilename = ZipFilename;
            StfArchiver.PerformArchive();

            Assert.IsTrue(File.Exists(ZipFilename));
        }
    }
}

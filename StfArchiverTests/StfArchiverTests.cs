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
            MyArchiver.Configuration.DoArchiveFoldersAndFiles = false;
            MyArchiver.Configuration.DoArchiveToZipfile = false;

            var emptyStatusTxt = MyArchiver.Status();
            Assert.AreEqual("Nothing to Archive", emptyStatusTxt);

            MyArchiver.AddFile(@"C:\Temp\Stf\Config\StfConfiguration.xml");
            var oneFileStatusTxt = MyArchiver.Status();
            const string ExpectedStatusTxt = "Files to archive\n\tC:\\Temp\\Stf\\Config\\StfConfiguration.xml\n";
            Assert.AreEqual(ExpectedStatusTxt, oneFileStatusTxt);

            MyArchiver.PerformArchive();
        }

        /// <summary>
        /// Test Perform Archive One Directory
        /// </summary>
        [TestMethod]
        public void TestPerformArchiveOneDirectory()
        {
            MyArchiver.AddDirectory(@"C:\Temp\Stf\Config");

            var oneFileStatusTxt = MyArchiver.Status();
            const string ExpectedStatusTxt = "Directories to archive\n\tC:\\Temp\\Stf\\Config\n";
            Assert.AreEqual(ExpectedStatusTxt, oneFileStatusTxt);

            MyArchiver.PerformArchive();
            MyArchiver.Configuration.DoArchiveFoldersAndFiles = false;
            MyArchiver.Configuration.DoArchiveToZipfile = false;
        }

        /// <summary>
        /// The test perform archive and zip.
        /// </summary>
        [TestMethod]
        public void TestPerformArchiveAndZip()
        {
            const string ZipFilename = @"c:\temp\Stf\StfArchiver.zip";

            MyArchiver.AddDirectory(@"C:\Temp\Stf\Config");
            MyArchiver.Configuration.ZipFilename = ZipFilename;
            MyArchiver.PerformArchive();

            Assert.IsTrue(File.Exists(ZipFilename));

            MyArchiver.Configuration.DoArchiveFoldersAndFiles = false;
            MyArchiver.Configuration.DoArchiveToZipfile = false;
        }
    }
}

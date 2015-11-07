// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfArchiverTests.cs" company="Mir Software">
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
            MyArchiver.AddFile(@"C:\Temp\Stf\Config\StfConfiguration.xml");
            MyArchiver.PerformArchive();
        }

        /// <summary>
        /// Test Perform Archive One Directory
        /// </summary>
        [TestMethod]
        public void TestPerformArchiveOneDirectory()
        {
            MyArchiver.AddDirectory(@"C:\Temp\Stf\Config");
            MyArchiver.PerformArchive();
        }

        /// <summary>
        /// The test perform archive and zip.
        /// </summary>
        [TestMethod]
        public void TestPerformArchiveAndZip()
        {
            const string ZipFilename = @"c:\temp\Stf\StfArchiver.zip";

            if (File.Exists(ZipFilename))
            {
                File.Delete(ZipFilename);
                
            }

            MyAssert.FileNotExists("Deleted file should not exist", ZipFilename);

            MyArchiver.Configuration.ZipFilename = ZipFilename;
            MyArchiver.Configuration.DoArchiveFoldersAndFiles = false;
            MyArchiver.Configuration.DoArchiveToZipfile = true;

            MyArchiver.AddDirectory(@"C:\Temp\Stf\Config");
            MyArchiver.PerformArchive();
            MyAssert.FileExists("Zip file should exist", ZipFilename);
        }
    }
}

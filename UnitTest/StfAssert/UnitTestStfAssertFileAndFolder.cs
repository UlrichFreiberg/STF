// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestStfAssertFileAndFolder.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Mir.Stf;

namespace UnitTest
{
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The unit test stf asserts.
    /// </summary>
    [TestClass]
    public class UnitTestStfAssertFileAndFolder : StfTestScriptBase
    {
        /// <summary>
        /// The test initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            StfAssert.EnableNegativeTesting = true;
            StfLogger.Configuration.ScreenshotOnLogFail = false;
        }

        /// <summary>
        /// The test cleanup.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            // setting to true agains resets failure count
            StfAssert.EnableNegativeTesting = true;
        }

        /// <summary>
        /// The test method assert FileContains.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertFileContains()
        {
            const string UnitTestFile = @"c:\temp\TestMethodAssertFileContains.txt";
            var testFile = File.CreateText(UnitTestFile);

            testFile.WriteLine("one line of test data");
            testFile.Close();

            Assert.IsFalse(StfAssert.FileContains("TestStepName 1", @"c:\DoNotExists.nope", "A string"));
            Assert.IsFalse(StfAssert.FileContains("TestStepName 2", UnitTestFile, "Nothing Like it"));
            Assert.IsTrue(StfAssert.FileContains("TestStepName 3", UnitTestFile, "test"));
            Assert.IsTrue(StfAssert.FileContains("TestStepName 4", UnitTestFile, "t[eE]st"));
        }

        /// <summary>
        /// The test method assert FileExists.
        /// </summary>
        [TestMethod]

        public void TestMethodAssertFileExists()
        {
            const string UnitTestFile = @"c:\temp\TestMethodAssertFileContains.txt";

            if (File.Exists(UnitTestFile))
            {
                File.Delete(UnitTestFile);
            }

            Assert.IsFalse(StfAssert.FileExists("TestStepName 1", @"c:\DoNotExists.nope"));
            Assert.IsFalse(StfAssert.FileExists("TestStepName 2", UnitTestFile));

            var testFile = File.CreateText(UnitTestFile);
            testFile.WriteLine("one line of test data");
            testFile.Close();

            Assert.IsTrue(StfAssert.FileExists("TestStepName 3", UnitTestFile));
        }

        /// <summary>
        /// The test method assert FileNotExists.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertFileNotExists()
        {
            const string UnitTestFile = @"c:\temp\TestMethodAssertFileNotExists.txt";

            if (File.Exists(UnitTestFile))
            {
                File.Delete(UnitTestFile);
            }

            Assert.IsTrue(StfAssert.FileNotExists("TestStepName 1", UnitTestFile));

            var testFile = File.CreateText(UnitTestFile);
            testFile.WriteLine("one line of test data");
            testFile.Close();

            Assert.IsFalse(StfAssert.FileNotExists("TestStepName 2", UnitTestFile));
        }

        /// <summary>
        /// The test method assert FolderExists.
        /// </summary>
        [TestMethod]

        public void TestMethodAssertFolderExists()
        {
            const string UnitTestDir = @"c:\temp\TestMethodAssertFolderExists";

            if (Directory.Exists(UnitTestDir))
            {
                Directory.Delete(UnitTestDir);
            }

            Assert.IsFalse(StfAssert.FolderExists("TestStepName 1", UnitTestDir));

            Directory.CreateDirectory(UnitTestDir);
            Assert.IsTrue(StfAssert.FolderExists("TestStepName 2", UnitTestDir));
        }

        /// <summary>
        /// The test method assert FolderNotExists.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertFolderNotExists()
        {
            const string UnitTestDir = @"c:\temp\TestMethodAssertFolderNotExists";

            if (Directory.Exists(UnitTestDir))
            {
                Directory.Delete(UnitTestDir);
            }

            Assert.IsTrue(StfAssert.FolderNotExists("TestStepName 1", UnitTestDir));

            Directory.CreateDirectory(UnitTestDir);
            Assert.IsFalse(StfAssert.FolderNotExists("TestStepName 2", UnitTestDir));
        }

        /// <summary>
        /// The test method assert files differ 01.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertFilesDiffer01()
        {
            const string UnitTestFile1 = @"D:\Projects\STF\UnitTest\StfAssert\TestData\TestMethodAssertFileNotExists1.xml";
            const string UnitTestFile2 = @"D:\Projects\STF\UnitTest\StfAssert\TestData\TestMethodAssertFileNotExists2.xml";

            Assert.IsTrue(StfAssert.FilesDoDiffer("TestStepName 1", UnitTestFile1, UnitTestFile2));
            Assert.IsFalse(StfAssert.FilesDoNotDiffer("TestStepName 1", UnitTestFile1, UnitTestFile2));
        }

        /// <summary>
        /// The test method assert files differ 02.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertFilesDiffer02()
        {
            const string UnitTestFile1 = @"TestData\zerofile.txt";
            const string UnitTestFile2 = @"TestData\zerofile.txt";

            Assert.IsFalse(StfAssert.FilesDoDiffer("TestStepName 1", UnitTestFile1, UnitTestFile2));
            Assert.IsTrue(StfAssert.FilesDoNotDiffer("TestStepName 1", UnitTestFile1, UnitTestFile2));
        }

        /// <summary>
        /// The test method assert files diff 03.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertFilesDiff03()
        {
            const string UnitTestFile1 = @"TestData\zerofile.txt";
            const string UnitTestFile2 = @"TestData\OneCharFile.txt";

            Assert.IsTrue(StfAssert.FilesDoDiffer("TestStepName 1", UnitTestFile1, UnitTestFile2));
            Assert.IsFalse(StfAssert.FilesDoNotDiffer("TestStepName 1", UnitTestFile1, UnitTestFile2));
        }

        /// <summary>
        /// The test method assert files diff 04.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertFilesDiff04()
        {
            const string UnitTestFile1 = @"TestData\CaptureSame01.jpg";
            const string UnitTestFile2 = @"TestData\CaptureSame02.jpg";

            Assert.IsTrue(StfAssert.FilesDoNotDiffer("TestStepName 1", UnitTestFile1, UnitTestFile2));
            Assert.IsFalse(StfAssert.FilesDoDiffer("TestStepName 1", UnitTestFile1, UnitTestFile2));
        }
    }
}

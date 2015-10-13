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
        /// The test method assert FileContains.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertFileContains()
        {
            const string UnitTestFile = @"c:\temp\TestMethodAssertFileContains.txt";
            var testFile = File.CreateText(UnitTestFile);

            MyAssert.EnableNegativeTesting = true;

            testFile.WriteLine("one line of test data");
            testFile.Close();

            Assert.IsFalse(MyAssert.FileContains("TestStepName 1", @"c:\DoNotExists.nope", "A string"));
            Assert.IsFalse(MyAssert.FileContains("TestStepName 2", UnitTestFile, "Nothing Like it"));
            Assert.IsTrue(MyAssert.FileContains("TestStepName 3", UnitTestFile, "test"));
            Assert.IsTrue(MyAssert.FileContains("TestStepName 4", UnitTestFile, "t[eE]st"));
        }



        /// <summary>
        /// The test method assert FileExists.
        /// </summary>
        [TestMethod]

        public void TestMethodAssertFileExists()
        {
            const string UnitTestFile = @"c:\temp\TestMethodAssertFileContains.txt";

            MyAssert.EnableNegativeTesting = true;

            if (File.Exists(UnitTestFile))
            {
                File.Delete(UnitTestFile);
            }

            Assert.IsFalse(MyAssert.FileExists("TestStepName 1", @"c:\DoNotExists.nope"));
            Assert.IsFalse(MyAssert.FileExists("TestStepName 2", UnitTestFile));

            var testFile = File.CreateText(UnitTestFile);
            testFile.WriteLine("one line of test data");
            testFile.Close();

            Assert.IsTrue(MyAssert.FileExists("TestStepName 3", UnitTestFile));
        }

        /// <summary>
        /// The test method assert FileNotExists.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertFileNotExists()
        {
            const string UnitTestFile = @"c:\temp\TestMethodAssertFileNotExists.txt";

            MyAssert.EnableNegativeTesting = true;

            if (File.Exists(UnitTestFile))
            {
                File.Delete(UnitTestFile);
            }

            Assert.IsTrue(MyAssert.FileNotExists("TestStepName 1", UnitTestFile));

            var testFile = File.CreateText(UnitTestFile);
            testFile.WriteLine("one line of test data");
            testFile.Close();

            Assert.IsFalse(MyAssert.FileNotExists("TestStepName 2", UnitTestFile));
        }

        /// <summary>
        /// The test method assert FolderExists.
        /// </summary>
        [TestMethod]

        public void TestMethodAssertFolderExists()
        {
            const string UnitTestDir = @"c:\temp\TestMethodAssertFolderExists";

            MyAssert.EnableNegativeTesting = true;

            if (Directory.Exists(UnitTestDir))
            {
                Directory.Delete(UnitTestDir);
            }

            Assert.IsFalse(MyAssert.FolderExists("TestStepName 1", UnitTestDir));

            var testDir = Directory.CreateDirectory(UnitTestDir);
            Assert.IsTrue(MyAssert.FolderExists("TestStepName 2", UnitTestDir));
        }

        /// <summary>
        /// The test method assert FolderNotExists.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertFolderNotExists()
        {
            const string UnitTestDir = @"c:\temp\TestMethodAssertFolderNotExists";

            MyAssert.EnableNegativeTesting = true;

            if (Directory.Exists(UnitTestDir))
            {
                Directory.Delete(UnitTestDir);
            }

            Assert.IsTrue(MyAssert.FolderNotExists("TestStepName 1", UnitTestDir));

            var testDir = Directory.CreateDirectory(UnitTestDir);
            Assert.IsFalse(MyAssert.FolderNotExists("TestStepName 2", UnitTestDir));
        }
    }
}

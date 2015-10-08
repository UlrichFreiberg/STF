// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTest_StfAssert.cs" company="Foobar">
//   2015
// </copyright>
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
        /// The test method assert AssertFileContains.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertFileContains()
        {
            const string UnitTestFile = @"c:\temp\TestMethodAssertFileContains.txt";
            var testFile = File.CreateText(UnitTestFile);

            MyAssert.EnableNegativeTesting = true;

            testFile.WriteLine("one line of test data");
            testFile.Close();

            Assert.IsFalse(MyAssert.AssertFileContains("TestStepName 1", @"c:\DoNotExists.nope", "A string"));
            Assert.IsFalse(MyAssert.AssertFileContains("TestStepName 2", UnitTestFile, "Nothing Like it"));
            Assert.IsTrue(MyAssert.AssertFileContains("TestStepName 3", UnitTestFile, "test"));
            Assert.IsTrue(MyAssert.AssertFileContains("TestStepName 4", UnitTestFile, "t[eE]st"));
        }



        /// <summary>
        /// The test method assert AssertFileExists.
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

            Assert.IsFalse(MyAssert.AssertFileExists("TestStepName 1", @"c:\DoNotExists.nope"));
            Assert.IsFalse(MyAssert.AssertFileExists("TestStepName 2", UnitTestFile));

            var testFile = File.CreateText(UnitTestFile);
            testFile.WriteLine("one line of test data");
            testFile.Close();

            Assert.IsTrue(MyAssert.AssertFileExists("TestStepName 3", UnitTestFile));
        }

        /// <summary>
        /// The test method assert AssertFileNotExists.
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

            Assert.IsTrue(MyAssert.AssertFileNotExists("TestStepName 1", UnitTestFile));

            var testFile = File.CreateText(UnitTestFile);
            testFile.WriteLine("one line of test data");
            testFile.Close();

            Assert.IsFalse(MyAssert.AssertFileNotExists("TestStepName 2", UnitTestFile));
        }

        /// <summary>
        /// The test method assert AssertFolderExists.
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

            Assert.IsFalse(MyAssert.AssertFolderExists("TestStepName 1", UnitTestDir));

            var testDir = Directory.CreateDirectory(UnitTestDir);
            Assert.IsTrue(MyAssert.AssertFolderExists("TestStepName 2", UnitTestDir));
        }

        /// <summary>
        /// The test method assert AssertFolderNotExists.
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

            Assert.IsTrue(MyAssert.AssertFolderNotExists("TestStepName 1", UnitTestDir));

            var testDir = Directory.CreateDirectory(UnitTestDir);
            Assert.IsFalse(MyAssert.AssertFolderNotExists("TestStepName 2", UnitTestDir));
        }
    }
}

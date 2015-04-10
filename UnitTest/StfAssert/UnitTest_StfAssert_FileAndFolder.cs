// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTest_StfAssert.cs" company="Foobar">
//   2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace UnitTest
{
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Stf.Utilities;

    /// <summary>
    /// The unit test stf asserts.
    /// </summary>
    [TestClass]
    public class UnitTestStfAssertFileAndFolder
    {
        /// <summary>
        /// The test method assert AssertFileContains.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertFileContains()
        {
            const string UnitTestFile = @"c:\temp\TestMethodAssertFileContains.txt";
            var myLogger = new Stf.Utilities.StfLogger { FileName = @"c:\temp\unittestlogger_AssertFileContains.html" };
            var myAsserts = new StfAssert(myLogger);
            var testFile = File.CreateText(UnitTestFile);

            myAsserts.EnableNegativeTesting = true;

            testFile.WriteLine("one line of test data");
            testFile.Close();

            Assert.IsFalse(myAsserts.AssertFileContains("TestStepName 1", @"c:\DoNotExists.nope", "A string"));
            Assert.IsFalse(myAsserts.AssertFileContains("TestStepName 2", UnitTestFile, "Nothing Like it"));
            Assert.IsTrue(myAsserts.AssertFileContains("TestStepName 3", UnitTestFile, "test"));
            Assert.IsTrue(myAsserts.AssertFileContains("TestStepName 4", UnitTestFile, "t[eE]st"));
        }



        /// <summary>
        /// The test method assert AssertFileExists.
        /// </summary>
        [TestMethod]

        public void TestMethodAssertFileExists()
        {
            const string UnitTestFile = @"c:\temp\TestMethodAssertFileContains.txt";
            var myLogger = new Stf.Utilities.StfLogger { FileName = @"c:\temp\unittestlogger_AssertFileExists.html" };
            var myAsserts = new StfAssert(myLogger);

            myAsserts.EnableNegativeTesting = true;

            if (File.Exists(UnitTestFile))
            {
                File.Delete(UnitTestFile);
            }

            Assert.IsFalse(myAsserts.AssertFileExists("TestStepName 1", @"c:\DoNotExists.nope"));
            Assert.IsFalse(myAsserts.AssertFileExists("TestStepName 2", UnitTestFile));

            var testFile = File.CreateText(UnitTestFile);
            testFile.WriteLine("one line of test data");
            testFile.Close();

            Assert.IsTrue(myAsserts.AssertFileExists("TestStepName 3", UnitTestFile));
        }

        /// <summary>
        /// The test method assert AssertFileNotExists.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertFileNotExists()
        {
            const string UnitTestFile = @"c:\temp\TestMethodAssertFileNotExists.txt";
            var myLogger = new Stf.Utilities.StfLogger { FileName = @"c:\temp\unittestlogger_AssertFileNotExists.html" };
            var myAsserts = new StfAssert(myLogger);

            myAsserts.EnableNegativeTesting = true;

            if (File.Exists(UnitTestFile))
            {
                File.Delete(UnitTestFile);
            }

            Assert.IsTrue(myAsserts.AssertFileNotExists("TestStepName 1", UnitTestFile));

            var testFile = File.CreateText(UnitTestFile);
            testFile.WriteLine("one line of test data");
            testFile.Close();

            Assert.IsFalse(myAsserts.AssertFileNotExists("TestStepName 2", UnitTestFile));
        }

        /// <summary>
        /// The test method assert AssertFolderExists.
        /// </summary>
        [TestMethod]

        public void TestMethodAssertFolderExists()
        {
            const string UnitTestDir = @"c:\temp\TestMethodAssertFolderExists";
            var myLogger = new Stf.Utilities.StfLogger { FileName = @"c:\temp\unittestlogger_AssertFolderExists.html" };
            var myAsserts = new StfAssert(myLogger);

            myAsserts.EnableNegativeTesting = true;

            if (Directory.Exists(UnitTestDir))
            {
                Directory.Delete(UnitTestDir);
            }

            Assert.IsFalse(myAsserts.AssertFolderExists("TestStepName 1", UnitTestDir));

            var testDir = Directory.CreateDirectory(UnitTestDir);
            Assert.IsTrue(myAsserts.AssertFolderExists("TestStepName 2", UnitTestDir));
        }

        /// <summary>
        /// The test method assert AssertFolderNotExists.
        /// </summary>
        [TestMethod]
        public void TestMethodAssertFolderNotExists()
        {
            const string UnitTestDir = @"c:\temp\TestMethodAssertFolderNotExists";
            var myLogger = new Stf.Utilities.StfLogger {FileName = @"c:\temp\unittestlogger_AssertFolderNotExists.html"};
            var myAsserts = new StfAssert(myLogger);

            myAsserts.EnableNegativeTesting = true;

            if (Directory.Exists(UnitTestDir))
            {
                Directory.Delete(UnitTestDir);
            }

            Assert.IsTrue(myAsserts.AssertFolderNotExists("TestStepName 1", UnitTestDir));

            var testDir = Directory.CreateDirectory(UnitTestDir);
            Assert.IsFalse(myAsserts.AssertFolderNotExists("TestStepName 2", UnitTestDir));
        }
    }
}

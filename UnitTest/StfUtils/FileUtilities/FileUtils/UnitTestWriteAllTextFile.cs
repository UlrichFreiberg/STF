﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestDeleteFile.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace UnitTest.TextUtils
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mir.Stf;
    using Mir.Stf.Utilities.FileUtilities;
    using System.IO;

    [TestClass]
    public class UnitTestWriteAllTextFile
    {
        [TestMethod]
        public void TestWriteAllTextFile()
        {
            string standardTextToWrite = "Sfu text to write";

            Helper_WriteAllTextFile(@"C:\temp\wallText1.txt", standardTextToWrite, true);
            Helper_WriteAllTextFile(@"C:\temp\emptyText1.txt", string.Empty, true);
            Helper_WriteAllTextFile(@"C:\temp\nullText1.txt", null, true);

            Helper_WriteAllTextFile(@"QQ:\temp\baddriveText1.txt", standardTextToWrite, false);
            Helper_WriteAllTextFile(@"C:\temp\folderNotExists\noFolderText1.txt", standardTextToWrite, false);
            Helper_WriteAllTextFile(@"C:\temp\!\noFolderText1.txt", standardTextToWrite, false);

            // Usual test
            Helper_WriteAllTextFile(null, "path is null", false);
            Helper_WriteAllTextFile(string.Empty, "path is empty", false);

        }

        private void Helper_WriteAllTextFile(string filename, string testComment, bool expected)
        {
            var fileUtils = new FileUtils();

            var actual = fileUtils.WriteAllTextFile(filename, testComment);

            Assert.IsTrue(expected == actual);
        }
    }
}

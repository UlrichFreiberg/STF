// --------------------------------------------------------------------------------------------------------------------
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
    public class UnitTestCreateTextFile
    {
        [TestMethod]
        public void TestCreateTextFile()
        {
            Helper_TestCreateTextFile(@"C:\temp\CreateTextFile.txt", "CreateTextFile - path correct", true);

            Helper_TestCreateTextFile(@"C:\temp\FolderNotExist\Nope.txt", "Folder not exist", false);
            Helper_TestCreateTextFile(@"QQQ:\temp\FolderNotExist\Nope.txt", "path incorrect format", false);

            Helper_TestCreateTextFile(@"C:\temp\CreateFirst.txt", "Overwrite Existing file(create first) - path correct", true, true);

            // Usual test
            Helper_TestCreateTextFile(null, "path is null", false);
            Helper_TestCreateTextFile(string.Empty, "path is empty", false);
        }

        private void Helper_TestCreateTextFile(string filename, string testComment, bool expected, bool createFileFirst = false)
        {
            var fileUtils = new FileUtils();

            if (createFileFirst)
            {
                fileUtils.WriteAllTextFile(filename, "UnitTestStuff");
            }

            var streamWriter = fileUtils.CreateTextfile(filename);
            var actual = (streamWriter != null) ? true : false;

            Assert.IsTrue(expected == actual);
        }
    }
}

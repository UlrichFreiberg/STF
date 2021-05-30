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
    public class UnitTestDeleteFile
    {
        [TestMethod]
        public void TestDeleteFile()
        {
            Helper_DeleteFile(@"C:\temp\Nope.txt", "Not existing file - path correct", true);
            Helper_DeleteFile(@"C:\temp\FolderNotExist\Nope.txt", "Not existing file - path incorrect", true);

            Helper_DeleteFile(@"C:\temp\FolderNotExist\Nope.txt", "Not existing file - path incorrect", true, true);

            // Usual test
            Helper_DeleteFile(null, "path is null", true);
            Helper_DeleteFile(string.Empty, "path is empty", true);

        }

        private void Helper_DeleteFile(string filename, string testComment, bool expected, bool createFileFirst = false)
        {
            var fileUtils = new FileUtils();

            if (createFileFirst)
            {
                fileUtils.WriteAllTextFile(filename, "UnitTestStuff");
            }

            var actual = fileUtils.DeleteFile(filename);

            Assert.IsTrue(expected == actual);
        }
    }
}

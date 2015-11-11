// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfAssert_FileAndFolder.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.IO;
using System.Text.RegularExpressions;

namespace Mir.Stf.Utilities
{
    using System;

    /// <summary>
    /// The stf assert.
    /// </summary>
    public partial class StfAssert
    {
        /// <summary>
        /// Asserts that a file exists
        /// </summary>
        /// <param name="testStep">
        /// Name of the test step in the test script
        /// </param>
        /// <param name="filenameAndPath">
        /// Absolute path to the file of interest
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool FileExists(string testStep, string filenameAndPath)
        {
            var retVal = File.Exists(filenameAndPath);
            string msg;

            if (retVal)
            {
                msg = string.Format("FileExists: [{0}] Does exist", filenameAndPath);
                this.AssertPass(testStep, msg);
            }
            else
            {
                msg = string.Format("FileExists: [{0}] Does Not exist", filenameAndPath);
                this.AssertFail(testStep, msg);
            }

            return retVal;
        }

        /// <summary>
        /// Asserts that a file exists
        /// </summary>
        /// <param name="testStep">
        /// Name of the test step in the test script
        /// </param>
        /// <param name="filenameAndPath">
        /// Absolute path to the file of interest
        /// </param>
        /// <param name="pattern">
        /// The pattern.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool FileContains(string testStep, string filenameAndPath, string pattern)
        {
            string msg;

            if (!File.Exists(filenameAndPath))
            {
                msg = string.Format("FileContains: [{0}] Does Not exist", filenameAndPath);
                this.AssertFail(testStep, msg);
                return false;
            }

            var content = File.ReadAllText(filenameAndPath);
            var match = Regex.Match(content, pattern);
            var retVal = match.Success;

            if (retVal)
            {
                msg = string.Format("FileContains: [{0}] Does contain [{1}]", filenameAndPath, pattern);
                this.AssertPass(testStep, msg);
            }
            else
            {
                msg = string.Format("FileContains: [{0}] Does Not contain [{1}]", filenameAndPath, pattern);
                this.AssertFail(testStep, msg);
            }

            return retVal;
        }

        /// <summary>
        /// Asserts that a folder (directory) exists
        /// </summary>
        /// <param name="testStep">
        /// Name of the test step in the test script
        /// </param>
        /// <param name="foldernameAndPath">
        /// Path to the folder
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool FolderExists(string testStep, string foldernameAndPath)
        {
            var retVal = Directory.Exists(foldernameAndPath);
            string msg;

            if (retVal)
            {
                msg = string.Format("FolderExists: [{0}] Does exist", foldernameAndPath);
                this.AssertPass(testStep, msg);
            }
            else
            {
                msg = string.Format("FolderExists: [{0}] Does Not exist", foldernameAndPath);
                this.AssertFail(testStep, msg);
            }

            return retVal;
        }

        /// <summary>
        /// Asserts that a folder (directory) does NOT exists
        /// </summary>
        /// <param name="testStep">
        /// Name of the test step in the test script
        /// </param>
        /// <param name="foldernameAndPath">
        /// Path to the folder
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool FolderNotExists(string testStep, string foldernameAndPath)
        {
            var retVal = !Directory.Exists(foldernameAndPath);
            string msg;

            if (retVal)
            {
                msg = string.Format("FolderNotExists: [{0}] Does Not exist", foldernameAndPath);
                this.AssertPass(testStep, msg);
            }
            else
            {
                msg = string.Format("FolderNotExists: [{0}] Does exist", foldernameAndPath);
                this.AssertFail(testStep, msg);
            }

            return retVal;
        }

        /// <summary>
        /// Asserts that a file doesn't exists
        /// </summary>
        /// <param name="testStep">
        /// Name of the test step in the test script
        /// </param>
        /// <param name="filenameAndPath">
        /// Absolute path to the file of interest
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool FileNotExists(string testStep, string filenameAndPath)
        {
            var retVal = !File.Exists(filenameAndPath);
            string msg;

            if (retVal)
            {
                msg = string.Format("FileNotExists: [{0}] Does Not exist", filenameAndPath);
                this.AssertPass(testStep, msg);
            }
            else
            {
                msg = string.Format("FileNotExists: [{0}] Does exist", filenameAndPath);
                this.AssertFail(testStep, msg);
            }

            return retVal;
        }

        /// <summary>
        /// Asserts that two files differ
        /// </summary>
        /// <param name="testStep">
        /// Name of the test step in the test script
        /// </param>
        /// <param name="filenameAndPathFirst">
        /// </param>
        /// <param name="filenameAndPathSecond">
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool FilesDoesDiffer(string testStep, string filenameAndPathFirst, string filenameAndPathSecond)
        {
            string msg;
            var fileInfoFirst = new FileInfo(filenameAndPathFirst);
            var fileInfoSecond = new FileInfo(filenameAndPathSecond);

            if (!fileInfoFirst.Exists)
            {
                msg = string.Format("FilesDoesDiffer: [{0}] Does Not exist", filenameAndPathFirst);
                this.AssertFail(testStep, msg);
                return true;
            }

            if (!fileInfoSecond.Exists)
            {
                msg = string.Format("FilesDoesDiffer: [{0}] Does Not exist", filenameAndPathSecond);
                this.AssertFail(testStep, msg);
                return true;
            }

            if (fileInfoFirst.Length != fileInfoSecond.Length)
            {
                msg = string.Format("FilesDoesDiffer: [{0}] and [{1}] does Not have the same length", filenameAndPathFirst, filenameAndPathSecond);
                this.AssertFail(testStep, msg);
                return true;
            }

            var diffPosition = FileCompare(filenameAndPathFirst, filenameAndPathSecond);
            if (diffPosition == 0)
            {
                msg = string.Format("FilesDoesNotDiffer: [{0}] and [{1}] doesn't differ byte-per-byte", filenameAndPathFirst, filenameAndPathSecond, diffPosition);
                this.AssertFail(testStep, msg);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Asserts that two files differ
        /// </summary>
        /// <param name="testStep">
        /// Name of the test step in the test script
        /// </param>
        /// <param name="filenameAndPathFirst">
        /// </param>
        /// <param name="filenameAndPathSecond">
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool FilesDoesNotDiffer(string testStep, string filenameAndPathFirst, string filenameAndPathSecond)
        {
            string msg;
            var fileInfoFirst = new FileInfo(filenameAndPathFirst);
            var fileInfoSecond = new FileInfo(filenameAndPathSecond);

            if (!fileInfoFirst.Exists)
            {
                msg = string.Format("FilesDoesNotDiffer: [{0}] Does Not exist", filenameAndPathFirst);
                this.AssertFail(testStep, msg);
                return false;
            }

            if (!fileInfoSecond.Exists)
            {
                msg = string.Format("FilesDoesNotDiffer: [{0}] Does Not exist", filenameAndPathSecond);
                this.AssertFail(testStep, msg);
                return false;
            }

            if (fileInfoFirst.Length != fileInfoSecond.Length)
            {
                msg = string.Format("FilesDoesNotDiffer: [{0}] and [{1}] does have the same length", filenameAndPathFirst, filenameAndPathSecond);
                this.AssertFail(testStep, msg);
                return false;
            }

            var diffPosition = FileCompare(filenameAndPathFirst, filenameAndPathSecond);
            if (diffPosition > 0)
            {
                msg = string.Format("FilesDoesDiffer: [{0}] and [{1}] differ at [{2}]", filenameAndPathFirst, filenameAndPathSecond, diffPosition);
                this.AssertFail(testStep, msg);
                return false;
            }

            return true;
        }

        // Inspired from http://stackoverflow.com/questions/7931304/comparing-two-files-in-c-sharp
        // This method accepts two strings the represent two files to 
        // compare. A return value of 0 indicates that the contents of the files
        // are the same. A return value of any other value indicates that the 
        // files are not the same.
        private Int64 FileCompare(string file1, string file2)
        {
            int file1Byte;
            int file2Byte;
            Int64 diffPosition = 0;

            // Determine if the same file was referenced two times.
            if (file1 == file2)
            {
                // Return true to indicate that the files are the same.
                return 0;
            }

            // Open the two files.
            var fs1 = new FileStream(file1, FileMode.Open, FileAccess.Read);
            var fs2 = new FileStream(file2, FileMode.Open, FileAccess.Read);

            // Read and compare a byte from each file until either a
            // non-matching set of bytes is found or until the end of
            // file1 is reached.
            do
            {
                // Read one byte from each file.
                file1Byte = fs1.ReadByte();
                file2Byte = fs2.ReadByte();
                diffPosition++;
            }
            while ((file1Byte == file2Byte) && (file1Byte != -1));

            // Close the files.
            fs1.Close();
            fs2.Close();

            // Return the success of the comparison. "file1byte" is 
            // equal to "file2byte" at this point only if the files are 
            // the same.
            return (file1Byte - file2Byte) != 0 ? diffPosition : 0;
        }
    }
}

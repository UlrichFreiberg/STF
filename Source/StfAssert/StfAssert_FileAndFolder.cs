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
                msg = $"FileExists: [{filenameAndPath}] Does exist";
                AssertPass(testStep, msg);
            }
            else
            {
                msg = $"FileExists: [{filenameAndPath}] Does Not exist";
                AssertFail(testStep, msg);
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

            if (string.IsNullOrEmpty(pattern))
            {
                msg = $"FileContains: pattern cannot be null nor empty";
                AssertFail(testStep, msg);
                return false;
            }

            if (string.IsNullOrEmpty(filenameAndPath))
            {
                msg = $"FileContains: filenameAndPath cannot be null nor empty";
                AssertFail(testStep, msg);
                return false;
            }

            if (!File.Exists(filenameAndPath))
            {
                msg = $"FileContains: [{filenameAndPath}] Does Not exist";
                AssertFail(testStep, msg);
                return false;
            }

            var content = File.ReadAllText(filenameAndPath);
            var match = Regex.Match(content, pattern);
            var retVal = match.Success;

            if (retVal)
            {
                msg = $"FileContains: [{filenameAndPath}] Does contain [{pattern}]";
                AssertPass(testStep, msg);
            }
            else
            {
                msg = $"FileContains: [{filenameAndPath}] Does Not contain [{pattern}]";
                AssertFail(testStep, msg);
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
                msg = $"FolderExists: [{foldernameAndPath}] Does exist";
                AssertPass(testStep, msg);
            }
            else
            {
                msg = $"FolderExists: [{foldernameAndPath}] Does Not exist";
                AssertFail(testStep, msg);
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
                msg = $"FolderNotExists: [{foldernameAndPath}] Does Not exist";
                AssertPass(testStep, msg);
            }
            else
            {
                msg = $"FolderNotExists: [{foldernameAndPath}] Does exist";
                AssertFail(testStep, msg);
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
        public bool DirectoryExists(string testStep, string foldernameAndPath)
        {
            return FolderExists(testStep, foldernameAndPath);
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
        public bool DirectoryNotExists(string testStep, string foldernameAndPath)
        {
            return FolderNotExists(testStep, foldernameAndPath);
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
                msg = $"FileNotExists: [{filenameAndPath}] Does Not exist";
                AssertPass(testStep, msg);
            }
            else
            {
                msg = $"FileNotExists: [{filenameAndPath}] Does exist";
                AssertFail(testStep, msg);
            }

            return retVal;
        }

        /// <summary>
        /// The files do differ.
        /// </summary>
        /// <param name="testStep">
        /// The test step.
        /// </param>
        /// <param name="filenameAndPathFirst">
        /// The filename and path first.
        /// </param>
        /// <param name="filenameAndPathSecond">
        /// The filename and path second.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool FilesDoDiffer(string testStep, string filenameAndPathFirst, string filenameAndPathSecond)
        {
            if (string.IsNullOrEmpty(filenameAndPathFirst) || string.IsNullOrEmpty(filenameAndPathSecond))
            {
                AssertFail(testStep, $"FilesDoDiffer: file names cannot be null nor empty");
                return false;
            }

            var msg = $"FilesDoDiffer: [{filenameAndPathFirst}] differs from [{filenameAndPathSecond}]";
            var fileInfoFirst = new FileInfo(filenameAndPathFirst);
            var fileInfoSecond = new FileInfo(filenameAndPathSecond);

            if (!fileInfoFirst.Exists)
            {
                msg = $"FilesDoDiffer: [{filenameAndPathFirst}] does not exist";
                AssertFail(testStep, msg);
                return true;
            }

            if (!fileInfoSecond.Exists)
            {
                msg = $"FilesDoDiffer: [{filenameAndPathSecond}] does not exist";
                AssertFail(testStep, msg);
                return true;
            }

            if (fileInfoFirst.Length != fileInfoSecond.Length)
            {
                msg =
                    $"FilesDoDiffer: [{filenameAndPathFirst}] and [{filenameAndPathSecond}] do not have the same length";
                AssertFail(testStep, msg);
                return true;
            }

            var diffPosition = FileCompare(filenameAndPathFirst, filenameAndPathSecond);
            if (diffPosition == 0)
            {
                msg = $"FilesDoDiffer: [{filenameAndPathFirst}] and [{filenameAndPathSecond}] do not differ byte-per-byte";
                AssertFail(testStep, msg);
                return false;
            }

            return AssertPass(testStep, msg);
        }

        /// <summary>
        /// The files do not differ.
        /// </summary>
        /// <param name="testStep">
        /// The test step.
        /// </param>
        /// <param name="filenameAndPathFirst">
        /// The filename and path first.
        /// </param>
        /// <param name="filenameAndPathSecond">
        /// The filename and path second.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool FilesDoNotDiffer(string testStep, string filenameAndPathFirst, string filenameAndPathSecond)
        {
            if (string.IsNullOrEmpty(filenameAndPathFirst) || string.IsNullOrEmpty(filenameAndPathSecond))
            {
                AssertFail(testStep, $"FilesDoNotDiffer: file names cannot be null nor empty");
                return false;
            }

            var msg = $"FilesDoNotDiffer: [{filenameAndPathFirst}] does not differ from [{filenameAndPathSecond}]";
            var fileInfoFirst = new FileInfo(filenameAndPathFirst);
            var fileInfoSecond = new FileInfo(filenameAndPathSecond);

            if (!fileInfoFirst.Exists)
            {
                msg = $"FilesDoNotDiffer: [{filenameAndPathFirst}] does not exist";
                AssertFail(testStep, msg);
                return false;
            }

            if (!fileInfoSecond.Exists)
            {
                msg = $"FilesDoNotDiffer: [{filenameAndPathSecond}] does not exist";
                AssertFail(testStep, msg);
                return false;
            }

            if (fileInfoFirst.Length != fileInfoSecond.Length)
            {
                msg = $"FilesDoNotDiffer: [{filenameAndPathFirst}] and [{filenameAndPathSecond}] does have the same length";
                AssertFail(testStep, msg);
                return false;
            }

            var diffPosition = FileCompare(filenameAndPathFirst, filenameAndPathSecond);
            if (diffPosition > 0)
            {
                msg =
                    $"FilesDoNotDiffer: [{filenameAndPathFirst}] and [{filenameAndPathSecond}] differ at [{diffPosition}]";
                AssertFail(testStep, msg);
                return false;
            }

            return AssertPass(testStep, msg);
        }

        /// <summary>
        /// The file compare. Inspired from http://stackoverflow.com/questions/7931304/comparing-two-files-in-c-sharp
        /// This method accepts two strings the represent two files to 
        /// compare. A return value of 0 indicates that the contents of the files
        /// are the same. A return value of any other value indicates that the 
        /// files are not the same.
        /// </summary>
        /// <param name="file1">
        /// The file 1.
        /// </param>
        /// <param name="file2">
        /// The file 2.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        private long FileCompare(string file1, string file2)
        {
            int file1Byte;
            int file2Byte;
            long diffPosition = 0;

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

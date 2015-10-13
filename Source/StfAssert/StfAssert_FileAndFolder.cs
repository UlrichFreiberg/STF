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
            Match match = Regex.Match(content, pattern);
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
    }
}

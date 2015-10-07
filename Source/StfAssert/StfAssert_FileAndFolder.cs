// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfAssert_FileAndFolder.cs" company="Foobar">
//   2015
// </copyright>
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
        public bool AssertFileExists(string testStep, string filenameAndPath)
        {
            var retVal = File.Exists(filenameAndPath);
            string msg;

            if (retVal)
            {
                msg = string.Format("AssertFileExists: [{0}] Does exist", filenameAndPath);
                this.AssertPass(testStep, msg);
            }
            else
            {
                msg = string.Format("AssertFileExists: [{0}] Does Not exist", filenameAndPath);
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
        public bool AssertFileContains(string testStep, string filenameAndPath, string pattern)
        {
            string msg;

            if (!File.Exists(filenameAndPath))
            {
                msg = string.Format("AssertFileContains: [{0}] Does Not exist", filenameAndPath);
                this.AssertFail(testStep, msg);
                return false;
            }

            var content = File.ReadAllText(filenameAndPath);
            Match match = Regex.Match(content, pattern);
            var retVal = match.Success;

            if (retVal)
            {
                msg = string.Format("AssertFileContains: [{0}] Does contain [{1}]", filenameAndPath, pattern);
                this.AssertPass(testStep, msg);
            }
            else
            {
                msg = string.Format("AssertFileContains: [{0}] Does Not contain [{1}]", filenameAndPath, pattern);
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
        public bool AssertFolderExists(string testStep, string foldernameAndPath)
        {
            var retVal = Directory.Exists(foldernameAndPath);
            string msg;

            if (retVal)
            {
                msg = string.Format("AssertFolderExists: [{0}] Does exist", foldernameAndPath);
                this.AssertPass(testStep, msg);
            }
            else
            {
                msg = string.Format("AssertFolderExists: [{0}] Does Not exist", foldernameAndPath);
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
        public bool AssertFolderNotExists(string testStep, string foldernameAndPath)
        {
            var retVal = !Directory.Exists(foldernameAndPath);
            string msg;

            if (retVal)
            {
                msg = string.Format("AssertFolderNotExists: [{0}] Does Not exist", foldernameAndPath);
                this.AssertPass(testStep, msg);
            }
            else
            {
                msg = string.Format("AssertFolderNotExists: [{0}] Does exist", foldernameAndPath);
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
        public bool AssertFileNotExists(string testStep, string filenameAndPath)
        {
            var retVal = !File.Exists(filenameAndPath);
            string msg;

            if (retVal)
            {
                msg = string.Format("AssertFileNotExists: [{0}] Does Not exist", filenameAndPath);
                this.AssertPass(testStep, msg);
            }
            else
            {
                msg = string.Format("AssertFileNotExists: [{0}] Does exist", filenameAndPath);
                this.AssertFail(testStep, msg);
            }

            return retVal;
        }
    }
}

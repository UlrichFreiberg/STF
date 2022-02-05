// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileUtils.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the FileUtils type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.FileUtilities
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;

    using Mir.Stf.Utilities.Interfaces;

    /// <summary>
    /// The file utils.
    /// </summary>
    public class FileUtils : StfUtilsBase
    {
        /// <summary>
        /// The exists file.
        /// </summary>
        /// <param name="filename">
        /// The filename.
        /// </param>
        /// <param name="ensureWaitSeconds">
        /// The ensureWaitSeconds.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool ExistsFile(string filename, int ensureWaitSeconds = 30)
        {
            var retVal = false;

            if (string.IsNullOrEmpty(filename))
            {
                return false;
            }

            try
            {
                var retryer = new RetryerUtilities.RetryerUtils();

                retVal = retryer.Retry(
                    () => File.Exists(filename),
                    TimeSpan.FromSeconds(ensureWaitSeconds));
            }
            catch (Exception ex)
            {
                LogError($"ExistsFile: Got exception: [{ex}]");
                return false;
            }
            finally
            {
                LogInfo($"ExistsFile: retVal is [{retVal}]");
            }

            return retVal;
        }

        /// <summary>
        /// The not exists file.
        /// </summary>
        /// <param name="filename">
        /// The filename.
        /// </param>
        /// <param name="ensureWaitSeconds">
        /// The ensureWaitSeconds.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool NotExistsFile(string filename, int ensureWaitSeconds = 30)
        {
            var retVal = false;

            try
            {
                var retryer = new RetryerUtilities.RetryerUtils();

                retVal = retryer.Retry(
                    () => !File.Exists(filename),
                    TimeSpan.FromSeconds(ensureWaitSeconds));
            }
            catch (Exception ex)
            {
                LogError($"NotExistsFile: Got exception: [{ex}]");
                return false;
            }
            finally
            {
                LogInfo($"NotExistsFile: retVal is [{retVal}]");
            }

            return retVal;
        }

        /// <summary>
        /// The delete file.
        /// </summary>
        /// <param name="filename">
        /// The filename.
        /// </param>
        /// <param name="ensureWaitSeconds">
        /// The ensureWaitSeconds.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool DeleteFile(string filename, int ensureWaitSeconds = 30)
        {
            var retVal = false;

            try
            {
                if (!File.Exists(filename))
                {
                    return true;
                }

                File.Delete(filename);
                retVal = NotExistsFile(filename, ensureWaitSeconds);
            }
            catch (Exception ex)
            {
                // TODO: Log the error with message from exception
                // TODO: Put error message in the standard ErrorMessage 
                LogError($"DeleteFile: Get Ex: [{ex}]");
            }
            finally
            {
                // log the value of retVal
                LogInfo($"DeleteFile: RetVal = [{retVal}]");
            }

            return retVal;
        }

        /// <summary>
        /// The create text file.
        /// </summary>
        /// <param name="filename">
        /// The filename.
        /// </param>
        /// <param name="ensureWaitSeconds">
        /// The ensureWaitSeconds.
        /// </param>
        /// <returns>
        /// The <see cref="StreamWriter"/>.
        /// </returns>
        public StreamWriter CreateText(string filename, int ensureWaitSeconds = 30)
        {
            StreamWriter retVal;

            try
            {
                retVal = File.CreateText(filename);

                if (!ExistsFile(filename))
                {
                    retVal = null;
                }
            }
            catch (Exception ex)
            {
                // TODO: Log the error with message from exception
                // TODO: Put error message in the standard ErrorMessage 
                LogError($"DeleteFile: Got Ex: [{ex}]");
                retVal = null;
            }
            finally
            {
                // log the value of retVal
                LogInfo("DeleteFile: RetVal is not null");
            }

            return retVal;
        }

        /// <summary>
        /// The copy file.
        /// </summary>
        /// <param name="sourceFilename">
        /// The source filename.
        /// </param>
        /// <param name="destinationFilename">
        /// The destination filename.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool CopyFile(string sourceFilename, string destinationFilename)
        {
            bool retVal;

            if (!File.Exists(sourceFilename))
            {
                LogError($"CopyFile: Source file does not exist [{sourceFilename}]");
                return false;
            }

            if (!DeleteFile(destinationFilename))
            {
                LogError($"CopyFile: Couldn't delete destination file [{destinationFilename}]");
                return false;
            }

            try
            {
                File.Copy(sourceFilename, destinationFilename);
            }
            catch (Exception ex)
            {
                LogError($"CopyFile: Got Ex: [{ex}]");
            }
            finally
            {
                retVal = ExistsFile(destinationFilename);

                // log the value of retVal
                LogInfo($"CopyFile: retVal = [{retVal}]");
            }

            return retVal;
        }


        /// <summary>
        /// Get file content.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetFileContent(string fileName)
        {
            string retVal = null;

            if (!ExistsFile(fileName))
            {
                return null;
            }

            try
            {
                retVal = File.ReadAllText(fileName);
            }
            catch (Exception ex)
            {
                LogError($"GetFileContent: Got Ex: [{ex}]");
                return null;
            }
            finally
            {
                var maxLogText = retVal?.Length ?? 0;

                if (maxLogText > 500)
                {
                    maxLogText = 500;
                }

                // log the value of retVal
                LogInfo($"GetFileContent: First 500 character of file [{retVal?.Substring(0, maxLogText)}]");
            }

            return retVal;
        }

        /// <summary>
        /// Get uncommented file content.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <param name="startOfCommentLine">
        /// The string all comments starts with - rest of the line is treated as a comment.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetCleanFileContent(string fileName, string startOfCommentLine = "//")
        {
            string retVal = null;

            if (!ExistsFile(fileName))
            {
                return null;
            }

            try
            {
                var content = File.ReadAllText(fileName);

                retVal = RemoveComments(content, startOfCommentLine);
            }
            catch (Exception ex)
            {
                LogError($"GetCleanFileContent: Got Ex: [{ex}]");
                return null;
            }
            finally
            {
                var maxLogText = retVal?.Length ?? 0;

                if (maxLogText > 500)
                {
                    maxLogText = 500;
                }

                // log the value of retVal
                LogInfo($"GetCleanFileContent: First 500 character of file [{retVal?.Substring(0, maxLogText)}]");
            }

            return retVal;
        }

        /// <summary>
        /// The remove comments.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <param name="startOfCommentLine">
        /// The string all comments starts with - rest of the line is treated as a comment.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string RemoveComments(string content, string startOfCommentLine = "//")
        {
            if (string.IsNullOrEmpty(content))
            {
                return content;
            }

            // remove comments
            content = Regex.Replace(content, $@"\s*{startOfCommentLine}.*", string.Empty);

            // remove empty lines
            content = Regex.Replace(content, @"[\r\n]+", Environment.NewLine, RegexOptions.Multiline);

            // remove starting and ending empty lines (line one empty and/or last lines)
            // we regard the content as one string
            content = content.Trim();

            return content;
        }

        /// <summary>
        /// The WriteAllText file.
        /// </summary>
        /// <param name="filename">
        /// The filename.
        /// </param>
        /// <param name="textToWrite">
        /// The text To Write.
        /// </param>
        /// <param name="ensureWaitSeconds">
        /// Time to wait for write to complete in seconds
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool WriteAllTextFile(string filename, string textToWrite, int ensureWaitSeconds = 30)
        {
            var retVal = false;

            try
            {
                var actualText = textToWrite ?? string.Empty;

                File.WriteAllText(filename, actualText);

                if (!ExistsFile(filename))
                {
                    return false;
                }

                // test size of file is correct 
                var length = new FileInfo(filename).Length;

                retVal = length == actualText.Length;
            }
            catch (Exception ex)
            {
                LogError($"WriteAllTextFile: Got Ex: [{ex}]");
            }
            finally
            {
                // log the value of retVal
                LogInfo($"WriteAllTextFile: RetVal = [{retVal}]");
            }

            return retVal;
        }
    }
}

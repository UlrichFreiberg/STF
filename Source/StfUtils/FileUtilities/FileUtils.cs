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
    public class FileUtils : IFileUtils
    {
        /// <summary>
        /// The delete file.
        /// </summary>
        /// <param name="filename">
        /// The filename.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool DeleteFile(string filename, int ensureWaitSeconds = 30)
        {
            if (!File.Exists(filename))
            {
                return true;
            }

            File.Delete(filename);

            var retrier = new RetryerUtilities.RetryerUtils();

            var ok = retrier.Retry(
                () => !File.Exists(filename),
                TimeSpan.FromSeconds(ensureWaitSeconds));

            return ok;
        }

        /// <summary>
        /// The create textfile.
        /// cas
        /// pkevkevin spacey films
        /// ETH development smart contracts c#
        /// 
        /// 
        /// </summary>
        /// <param name="filename">
        /// The filename.
        /// </param>
        /// <returns>
        /// The <see cref="StreamWriter"/>.
        /// </returns>
        public StreamWriter CreateTextfile(string filename) 
        {
            var streamWriter  = File.CreateText(filename);

            if (!File.Exists(filename))
            {
                return null;
            }

            return streamWriter;
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
            if (!File.Exists(sourceFilename))
            {
                return false;
            }

            if (!DeleteFile(destinationFilename))
            {
                return false;
            }

            File.Copy(sourceFilename, destinationFilename);

            return File.Exists(destinationFilename);
        }

        /// <summary>
        /// Get uncommented filecontent.
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
        public string GetCleanFilecontent(string fileName, string startOfCommentLine = "//")
        {
            if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName))
            {
                return string.Empty;
            }

            var content = File.ReadAllText(fileName);
            var retVal = RemoveComments(content, startOfCommentLine);

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
            // remove comments
            content = Regex.Replace(content, $@"\s*{startOfCommentLine}.*", string.Empty);

            // remove empty lines
            content = Regex.Replace(content, @"[\r\n]+", Environment.NewLine, RegexOptions.Multiline);

            // remove starting and ending empty lines (line one empty and/or last lines)
            content = content.Trim();

            return content;
        }

        /// <summary>
        /// The WriteAllText file.
        /// </summary>
        /// <param name="filename">
        /// The filename.
        /// </param>
        /// <param name="text">
        /// The text to write
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

                // TODO: retryer

                // test size of file is correct 
                var length = new System.IO.FileInfo(filename).Length;
                retVal = (length == (long)actualText.Length) ? true : false;
            }
            catch (Exception ex)
            {
                // TODO: Log the error with message from exception
                // TODO: Put error message in the standard ErrorMessage 
                retVal = false;
            }
            finally
            {
                // log the value of retVal ???
                // what else should we do here ?
            }
            return retVal;
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoboCopyWrapper.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Threading;

    // from http://codereview.stackexchange.com/questions/19386/resilient-wrapper-for-robocopy-in-c

    /// <summary>
    /// The robo copy wrapper.
    /// </summary>
    public class RoboCopyWrapper
    {
        /// <summary>
        /// The robocopy exit message.
        /// </summary>
        private const string RobocopyExitMessage = "Robocopy exited with code: {0}";

        /// <summary>
        /// The robocopy done.
        /// </summary>
        private const string RobocopyDone = "Robocopy Done!";

        /// <summary>
        /// The robocopy retry.
        /// </summary>
        private const string RobocopyRetry = "Retrying...";

        /// <summary>
        /// The robocopy killing.
        /// </summary>
        private const string RobocopyKilling = "Killing Robocopy. Took too much time. Try try again till you succeed...";

        /// <summary>
        /// The robocopy exception.
        /// </summary>
        private const string RobocopyException = "Exception: {0}";

        /// <summary>
        /// The milli seconds to sleep.
        /// </summary>
        private const int MilliSecondsToSleep = 5000;

        /// <summary>
        /// The copy files.
        /// </summary>
        /// <param name="commandLine">
        /// The command line.
        /// </param>
        /// <param name="maxSleepingIterations">
        /// The max sleeping iterations.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int CopyFiles(string commandLine, int maxSleepingIterations)
        {
            var robocopy = new Process();
            try
            {
                robocopy = Process.Start("C:\\Windows\\SysWOW64\\robocopy.exe", commandLine);
                if (robocopy == null)
                {
                    return 0;
                }

                for (var i = 0; i <= maxSleepingIterations; i++)
                {
                    Thread.Sleep(MilliSecondsToSleep);
                    if (robocopy.HasExited)
                    {
                        break;
                    }
                }

                if (robocopy.HasExited)
                {
                    Print(RobocopyExitMessage, robocopy.ExitCode);
                    if (robocopy.ExitCode > 2)
                    {
                        Print(RobocopyRetry);
                        return -1;
                    }
                    else
                    {
                        Print(RobocopyDone);
                        return robocopy.ExitCode;
                    }
                }
                else
                {
                    Print(RobocopyKilling);
                    robocopy.Kill();
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Print(RobocopyException, ex.ToString());
                return -2;
            }
            finally
            {
                if (robocopy != null)
                {
                    robocopy.Close();
                }
            }
        }

        /// <summary>
        /// The mirror dir.
        /// </summary>
        /// <param name="sourceDirectory">
        /// The source directory.
        /// </param>
        /// <param name="destinationDirectory">
        /// The destination directory.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int MirrorDir(string sourceDirectory, string destinationDirectory)
        {
            var sourceDirname = sourceDirectory;
            var dirname = new DirectoryInfo(sourceDirectory).Name;
            var destinationDirname = Path.Combine(destinationDirectory, dirname);

            // ensure the directories ends on a "\"
            if (Regex.Match(sourceDirname, @"[^\\]$").Success)
            {
                sourceDirname += @"\";
            }

            if (Regex.Match(destinationDirname, @"[^\\]$").Success)
            {
                destinationDirname += @"\";
            }

            var robocopyCmdline = string.Format(" \"{0}\\\" \"{1}\\\" /MIR ", sourceDirname, destinationDirname);
            var retVal = CopyFiles(robocopyCmdline, 5);
            return retVal;
        }

        /// <summary>
        /// The print.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private static void Print(string message, params object[] args)
        {
            var content = args.Length != 0 ? string.Format(message, args) : message;

            Console.WriteLine(content);
        }
    }
}

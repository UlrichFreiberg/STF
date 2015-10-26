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
    using System.Text;
    using System.Threading;

    // from http://codereview.stackexchange.com/questions/19386/resilient-wrapper-for-robocopy-in-c

    /// <summary>
    /// </summary>
    class RoboCopyWrapper
    {
        /// <summary>
        /// </summary>
        private const string Separator = "--------------------------------------------------------------------";

        /// <summary>
        /// </summary>
        private const string RobocopyExitMessage = "Robocopy exited with code: {0}";

        /// <summary>
        /// </summary>
        private const string RobocopyDone = "Robocopy Done!";

        /// <summary>
        /// </summary>
        private const string RobocopyRetry = "Retrying...";

        /// <summary>
        /// </summary>
        private const string RobocopyCommandline = "Robocopy Command Line: {0}";

        /// <summary>
        /// </summary>
        private const string RobocopyCalling = "Calling Robocopy";

        /// <summary>
        /// </summary>
        private const string RobocopyKilling = "Killing Robocopy. Took too much time. Try try again till you succeed...";

        /// <summary>
        /// </summary>
        private const string RobocopyException = "Exception: {0}";

        /// <summary>
        /// </summary>
        private const int MilliSecondsToSleep = 5000;

        /// <summary>
        /// </summary>
        private const int Initial5SecondIterations = 120;

        /// <summary>
        /// </summary>
        private const int Additional5SecondIterations = 60;

        /// <summary>
        /// </summary>
        /// <param name="args">
        /// </param>
        /// <returns>
        /// </returns>
        private static string ComposeCommandline(string[] args)
        {
            int count = 0;
            StringBuilder sb = new StringBuilder(1024);

            foreach (string str in args)
            {
                if ((count == 0) || (count == 1))
                {
                    sb.Append("\"").Append(str).Append("\"");
                }
                else
                {
                    sb.Append(str);
                }

                sb.Append(" ");
                count += 1;
            }

            return sb.ToString();
        }

        /// <summary>
        /// </summary>
        /// <param name="message">
        /// </param>
        /// <param name="args">
        /// </param>
        private static void Print(string message, params object[] args)
        {
            var content = args.Length != 0 ? string.Format(message, args) : message;

            Console.WriteLine(content);
        }

        /// <summary>
        /// </summary>
        /// <param name="commandLine">
        /// </param>
        /// <param name="maxSleepingIterations">
        /// </param>
        /// <returns>
        /// </returns>
        public static int CopyFiles(string commandLine, int maxSleepingIterations)
        {
            var robocopy = new Process();
            try
            {
                robocopy = Process.Start("C:\\Windows\\SysWOW64\\robocopy.exe", commandLine);
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
                robocopy.Close();
            }
        }

        public static int MirrorDir(string sourceDirectory, string destinationDirectory)
        {
            var sourceDirname = new DirectoryInfo(sourceDirectory).Name;
            var destinationDirname = Path.Combine(destinationDirectory, sourceDirname);
            var robocopyCmdline = string.Format(" \"{0}\" \"{1}\" /MIR ", sourceDirectory, destinationDirname);

            var retVal = CopyFiles(robocopyCmdline, 5);
            return retVal;
        }
    }
}

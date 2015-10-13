// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogfileWriter.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;

namespace Mir.Stf.Utilities.Utils
{
    /// <summary>
    /// The logfile writer.
    /// </summary>
    internal class LogfileWriter
    {
        /// <summary>
        /// Gets or sets the log file name.
        /// </summary>
        public string LogFileName { get; set; }

        /// <summary>
        /// Gets a value indicating whether or not the logfile is initialized.
        /// </summary>
        public bool Initialized { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not an existing logfile should be truncated
        /// </summary>
        public bool OverwriteLogFile { get; set; }

        /// <summary>
        /// Gets or sets the stream.
        /// </summary>
        private StreamWriter Stream { get; set; }

        /// <summary>
        /// Writes a buffer of characters to the logfile.
        /// </summary>
        /// <param name="stuffToWrite">
        /// The stuff to write.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Write(string stuffToWrite)
        {
            if (this.Stream == null)
            {
                return false;
            }

            // might be that a unit test closed the logfile- in the middle of everything....
            if (!Initialized)
            {
                return false;
            }

            this.Stream.Write(stuffToWrite);
            return true;
        }

        /// <summary>
        /// Open a logfile stream - respects the OverWriteFlag (soon:-)).
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Open(string fileName)
        {
            LogFileName = fileName;
            return Open();
        }

        /// <summary>
        /// Open a logfile stream - respects the OverWriteFlag (soon:-)).
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Open()
        {
            /* 
             * Normalize value filename
             * if new file, then close the old file 
             * open new file - respect the overwritefileflag
            */
            if (this.Initialized)
            {
                this.Close();
            }

            this.Stream = null;
            this.Initialized = false;

            if (File.Exists(this.LogFileName))
            {
                if (!OverwriteLogFile)
                {
                    Console.WriteLine(@"File [{0}] exists and OverwriteLogFile is false", LogFileName);
                    return false;
                }

                File.Delete(this.LogFileName);
                if (File.Exists(this.LogFileName))
                {
                    Console.WriteLine(@"File [{0}] exists and couldn't be deleted", LogFileName);
                    return false;
                }
            }

            this.Stream = new StreamWriter(this.LogFileName) { AutoFlush = true };
            this.Initialized = true;
            return true;
        }

        /// <summary>
        /// Closing the logfile stream
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Close()
        {
            if (Initialized)
            {
                Stream.Close();
            }

            Initialized = false;
            return true;

            /*       LogToFile = False */
        }
    }
}

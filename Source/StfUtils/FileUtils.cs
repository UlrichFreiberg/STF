using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mir.Stf.Utilities
{
    using System.IO;
    using System.Security.Policy;

    public class FileUtils
    {
        public bool DeleteFile(string filename)
        {
            if (!File.Exists(filename))
            {
                return true;
            }

            File.Delete(filename);
            return !File.Exists(filename);
        }

        public StreamWriter CreateTextfile(string filename) {
            var streamWriter  = File.CreateText(filename);

            if (!File.Exists(filename))
            {
                return null;
            }

            return streamWriter;
        }

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

        public string GetTempFolder()
        {
            var tempDirname = Path.Combine(Environment.ExpandEnvironmentVariables("TEMP"), Guid.NewGuid().ToString());

            Directory.CreateDirectory(tempDirname);

            return Directory.Exists(tempDirname) ? tempDirname : null;
        }

        public bool MirrorFolder(string sourceDirname, string destinationDirname)
        {
            // get to the RoboCopy Wrapper
            return false;
        }
    }
}

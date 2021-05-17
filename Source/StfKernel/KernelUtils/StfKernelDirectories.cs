// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfKernelDirectories.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the StfKernelDirectories type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Mir.Stf.KernelUtils
{
    using System.IO;
    using System.Security.AccessControl;

    using Mir.Stf.Exceptions;

    /// <summary>
    /// The stf kernel directories.
    /// </summary>
    public class StfKernelDirectories
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StfKernelDirectories"/> class.
        /// </summary>
        /// <param name="stfTextUtils">
        /// The stf text utils.
        /// </param>
        public StfKernelDirectories(StfTextUtils stfTextUtils)
        {
            StfTextUtils = stfTextUtils;
        }

        /// <summary>
        /// Gets or sets the stf text utils.
        /// </summary>
        public StfTextUtils StfTextUtils { get; set; }

        /// <summary>
        /// The get stf root.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="StfConfigurationException">
        /// Thrown if no suitable directory is found or permissions are missing for the one we found
        /// </exception>
        internal string GetStfRoot()
        {
            string stfRootCandidate;
            var tempStfRoot = Path.Combine(Path.GetTempPath(), "Stf");
            var currentDirStfRoot = Path.Combine(Environment.CurrentDirectory, "Stf");

            string TryThisForStfRoot(string dirVariableName, string defaultValue = null, bool createIfNotExist = false)
            {
                stfRootCandidate = CheckForNeededKernelDirectory(dirVariableName, defaultValue, createIfNotExist);

                // did we find a StfRoot?
                if (string.IsNullOrWhiteSpace(stfRootCandidate))
                {
                    return null;
                }

                Console.WriteLine($@"Found a STF root here [{stfRootCandidate}]");
                StfTextUtils.Register("Stf_Root", stfRootCandidate);

                return stfRootCandidate;
            }

            // first we look for STF using the Stf_Root variable - defaulting to c:\temp\STf
            if (TryThisForStfRoot(@"Stf_Root", @"C:\temp\Stf") != null)
            {
                return stfRootCandidate;
            }

            // then we look for a StfRoot at some random temp place (that doesn't always work - like on build servers)
            if (TryThisForStfRoot(null, tempStfRoot, true) != null)
            {
                return stfRootCandidate;
            }

            // Hmm, this is going to be tough... Lets see if we can use the current directory as StfRoot
            if (TryThisForStfRoot(null, currentDirStfRoot, true) != null)
            {
                return stfRootCandidate;
            }

            var msg = $"Can't find or create a StfRoot.";

            Console.WriteLine(msg);
            throw new StfConfigurationException(msg);
        }

        /// <summary>
        /// Check For Needed Kernel Directory - like configDir
        /// </summary>
        /// <param name="dirVariableName">
        /// What STF name should we remember this directory as
        /// </param>
        /// <param name="defaultValue">
        /// default Value
        /// </param>
        /// <param name="createIfNotExist">
        /// Create the directory if it doesn't exist. Default is to not create
        /// </param>
        /// <returns>
        /// The path to the directory - will be null if not exists
        /// </returns>
        internal string CheckForNeededKernelDirectory(string dirVariableName, string defaultValue = null, bool createIfNotExist = false)
        {
            string HandleDefaultValue()
            {
                if (string.IsNullOrEmpty(defaultValue))
                {
                    return null;
                }

                // The default value might be using variables (Like "%STF_ROOT%\Temp")
                var defaultValueExpanded = StfTextUtils.ExpandVariables(defaultValue);
                var retVal = StfTextUtils.GetVariableOrSetDefault(dirVariableName, defaultValueExpanded);

                return retVal;
            }

            var directoryPath = HandleDefaultValue();

            Console.WriteLine($@"CheckForNeededKernelDirectory([{dirVariableName}], [{defaultValue}], [{createIfNotExist}]");

            if (!string.IsNullOrEmpty(dirVariableName))
            {
                // we have an Environment/kernel variable to find this directory...
                // see if that one works, if not then use the default value
                var directoryNameVariable = $"%{dirVariableName}%";
                var directoryNameVariableExpanded = StfTextUtils.ExpandVariables(directoryNameVariable);

                if (!directoryNameVariableExpanded.Equals(directoryNameVariable))
                {
                    // The variable got expanded - lets use that value
                    directoryPath = directoryNameVariableExpanded;
                }
            }

            if (!Directory.Exists(directoryPath) && createIfNotExist)
            {
                try
                {
                    // We need the directory - see if we can create it
                    Directory.CreateDirectory(directoryPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($@"Couldn't create kernel directory at [{directoryPath}] - Got error: [{ex.Message}]");

                    return null;
                }
            }

            if (CheckWritePermissionForFolder(directoryPath))
            {
                StfTextUtils.Register(dirVariableName, directoryPath);

                return directoryPath;
            }

            var msg = $"Seems like we don't have full permissions for candidate Stf directoryName [{directoryPath}]";

            Console.WriteLine(msg);
            throw new StfConfigurationException(msg);
        }

        /// <summary>
        /// The check write permission for folder.
        /// </summary>
        /// <param name="folderPath">
        /// The folder path.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool CheckWritePermissionForFolder(string folderPath)
        {
            if (string.IsNullOrEmpty(folderPath) || File.Exists(folderPath))
            {
                return false;
            }

            var dirInfo = new DirectoryInfo(folderPath);

            try
            {
                // according to the Microsoft Documentation, this will
                // throw an exception if the folder is read only (UnauthorizedAccessException) 
                dirInfo.GetAccessControl(AccessControlSections.All);
                Console.WriteLine($@"Kernel directory at [{folderPath}] is not write protected");

                return true;
            }
            catch (Exception)
            {
                // Slurp 
            }

            // on my Win10 box the above throws even for a writable directory
            // so lets go simple and check if we can create and delete a file in the directory
            try
            {
                var fileName = Path.Combine(folderPath, "tempStfCheckWritePermissions.txt");

                File.Create(fileName).Close();
                File.Delete(fileName);
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }

            return true;
        }
    }
}

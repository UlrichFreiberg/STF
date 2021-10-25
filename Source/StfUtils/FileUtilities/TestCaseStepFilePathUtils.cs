// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestCaseStepFilePathUtils.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the TestCaseStepFilePathUtils type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.FileUtilities
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// The test case step file path utils.
    /// </summary>
    public class TestCaseStepFilePathUtils
    {
        /// <summary>
        /// The max number of steps.
        /// </summary>
        private const int MaxNumberOfSteps = 100;

        /// <summary>
        /// Backing field
        /// </summary>
        private string[][] existingFilePaths;

        /// <summary>
        /// Backing field
        /// </summary>
        private string[,] testCaseStepFilePaths;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseStepFilePathUtils"/> class.
        /// </summary>
        /// <param name="rootFolder">
        /// The root folder.
        /// </param>
        /// <param name="fileNameFilters">
        /// The template file filters.
        /// </param>
        public TestCaseStepFilePathUtils(string rootFolder, string[] fileNameFilters, bool ignoreFileExtensions = false)
        {
            RootFolder = rootFolder;
            FileNameFilters = fileNameFilters;
            IgnoreFileExtensions = ignoreFileExtensions;
            InitFileArrays();
        }

        /// <summary>
        /// Gets or sets a value indicating whether ignore file extensions.
        /// </summary>
        public bool IgnoreFileExtensions { get; set; }

        /// <summary>
        /// Gets the file name filters.
        /// </summary>
        public string[] FileNameFilters { get; }

        /// <summary>
        /// Gets the number of steps for this test case.
        /// </summary>
        public int NumberOfSteps { get; private set; }

        /// <summary>
        /// Gets the root folder.
        /// </summary>
        public string RootFolder { get; }

        /// <summary>
        /// The existing file paths.
        /// </summary>
        public string[][] ExistingFilePaths => existingFilePaths;

        /// <summary>
        /// The test case step file paths.
        /// </summary>
        public string[,] TestCaseStepFilePaths => testCaseStepFilePaths;

        /// <summary>
        /// The get file path for step.
        /// </summary>
        /// <param name="fileNameFilter">
        /// The file name filter.
        /// </param>
        /// <param name="step">
        /// The step.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetFilePathForStep(string fileNameFilter, int step)
        {
            var index = Array.IndexOf(FileNameFilters, fileNameFilter);

            if (index == -1)
            {
                return null;
            }

            var retVal = TestCaseStepFilePaths[index, step];

            return retVal;
        }

        /// <summary>
        /// The get file name for step. Used heavily in the unit tests
        /// </summary>
        /// <param name="fileNameFilter">
        /// The file name filter.
        /// </param>
        /// <param name="step">
        /// The step.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetFileNameForStep(string fileNameFilter, int step)
        {
            var filePath = GetFilePathForStep(fileNameFilter, step);

            if (filePath == null)
            {
                return null;
            }

            var retVal = filePath.Replace(RootFolder, string.Empty).Trim('\\', ' ');

            return retVal;
        }

        /// <summary>
        /// The get file paths for.
        /// </summary>
        /// <param name="fileNameFilter">
        /// The file name filter.
        /// </param>
        /// <returns>
        /// An array of file paths that matches the specified file pattern
        /// </returns>
        private string[] GetFilePathsFor(string fileNameFilter)
        {
            var basename = Path.GetFileNameWithoutExtension(fileNameFilter);
            var extension = Path.GetExtension(fileNameFilter);
            var wildcard = IgnoreFileExtensions ? $"{basename}*{extension}" : $"{basename}*";
            var retVal = Directory.GetFiles(RootFolder, wildcard).OrderBy(fileName => fileName).ToArray();

            return retVal;
        }

        /// <summary>
        /// The init file arrays.
        /// </summary>
        /// <returns>
        /// Indication of success
        /// </returns>
        private bool InitFileArrays()
        {
            bool CheckFirstFileIsNumberOne(int fileNo, string firstStepFilePath)
            {
                // Ensure it is a file name without digits
                // The first in a file series always are without numbers
                var fileFilterBasename = Path.GetFileNameWithoutExtension(FileNameFilters[fileNo]);
                var fileName = Path.GetFileName(firstStepFilePath);
                var candidateRegExp = $@"^{fileFilterBasename}1?([^\d]|\.)";

                return Regex.Match(fileName, candidateRegExp).Success;
            }

            existingFilePaths = new string[FileNameFilters.Length][];
            testCaseStepFilePaths = new string[FileNameFilters.Length, MaxNumberOfSteps];

            for (var fileNo = 0; fileNo < FileNameFilters.Length; fileNo++)
            {
                var filesForThisFileFilter = GetFilePathsFor(FileNameFilters[fileNo]);

                if (filesForThisFileFilter.Length == 0)
                {
                    // no files att all for this fileFilter
                    return false;
                }

                existingFilePaths[fileNo] = filesForThisFileFilter;

                if (!CheckFirstFileIsNumberOne(fileNo, filesForThisFileFilter[0]))
                {
                    // we do not have a correct first/initial file for this fileFilter 
                    return false;
                }

                // we start with the first file for step 1
                testCaseStepFilePaths[fileNo, 1] = filesForThisFileFilter[0];
            }

            for (var step = 2; step <= MaxNumberOfSteps; step++)
            {
                var foundFilePathForStep = false;

                for (var fileNo = 0; fileNo < FileNameFilters.Length; fileNo++)
                {
                    var fileNameFilter = FileNameFilters[fileNo];
                    var basename = Path.GetFileNameWithoutExtension(fileNameFilter);
                    var candidateRegExp = $@"{basename}{step}([^\d]|\.)";
                    var filterCandidate = existingFilePaths[fileNo].FirstOrDefault(p => Regex.IsMatch(p, candidateRegExp));

                    foundFilePathForStep = foundFilePathForStep || filterCandidate != null;
                    testCaseStepFilePaths[fileNo, step] = filterCandidate ?? testCaseStepFilePaths[fileNo, step - 1];
                }

                if (foundFilePathForStep)
                {
                    continue;
                }

                // we didn't find any files for this step - we are done - no more steps..
                NumberOfSteps = step - 1;
                break;
            }

            return true;
        }
    }
}

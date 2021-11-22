// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DirectoryUtils.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the DirectoryUtils type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.TestCaseDirectoryUtilities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    using Mir.Stf.Utilities.Interfaces;

    /// <summary>
    /// The directory utils.
    /// </summary>
    public class DirectoryUtils 
    {
        /// <summary>
        /// The cache info filename.
        /// </summary>
        private const string CacheInfoFilename = "FolderPathCacheInfo.txt";

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryUtils"/> class.
        /// </summary>
        /// <param name="rootFolder">
        /// The root folder.
        /// </param>
        public DirectoryUtils(string rootFolder)
        {
            RootFolder = rootFolder;

            if (!Path.IsPathRooted(rootFolder))
            {
                var newRootFolder = Path.Combine(Directory.GetCurrentDirectory(), RootFolder);

                RootFolder = Path.GetFullPath(newRootFolder);
            }

            CacheInfoFilePath = Path.Combine(RootFolder, CacheInfoFilename);
            CacheErrorFilePath = Regex.Replace(CacheInfoFilePath, ".txt$", ".err.txt");
        }

        /// <summary>
        /// The test case directory reg exp.
        /// </summary>
        public string TestCaseDirectoryRegExp => @"^(T|.*\\T)[Cc](?<TestCaseId>\d+)[^\\]*$";

        /// <summary>
        /// Gets the root folder.
        /// </summary>
        public string RootFolder { get; }

        /// <summary>
        /// Gets the cache info file path.
        /// </summary>
        public string CacheInfoFilePath { get; }

        /// <summary>
        /// Gets the cache error file path.
        /// </summary>
        public string CacheErrorFilePath { get; }

        /// <summary>
        /// The get test case id.
        /// </summary>
        /// <param name="directoryPath">
        /// The directory path.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetTestCaseId(string directoryPath)
        {
            var trimmedDirectoryPath = directoryPath.TrimEnd('\\');
            var match = Regex.Match(trimmedDirectoryPath, TestCaseDirectoryRegExp, RegexOptions.Multiline);

            if (!match.Success)
            {
                return -1;
            }

            var testCaseId = match.Groups["TestCaseId"].Value;

            if (!int.TryParse(testCaseId, out var retVal))
            {
                return -2;
            }

            return retVal;
        }

        /// <summary>
        /// The get test case directory path.
        /// </summary>
        /// <param name="testCaseId">
        /// The test case id.
        /// </param>
        /// <returns>
        /// Path for given test case id
        /// </returns>
        public string GetTestCaseDirectoryPath(int testCaseId)
        {
            var retVal = GetTestCaseDirectoryPath(testCaseId, false);

            return retVal;
        }

        /// <summary>
        /// The refresh cache.
        /// </summary>
        public void RefreshCache()
        {
            if (!Directory.Exists(RootFolder))
            {
                return;
            }

            var directoryInfo = new DirectoryInfo(RootFolder);
            var directories = directoryInfo.GetDirectories("tc*", SearchOption.AllDirectories);
            var errorContent = new StringBuilder();
            var testCaseFilePaths = new Dictionary<int, string>();

            foreach (var directory in directories)
            {
                // we only want to save relative paths -
                // In that way we avoids root contains something that could be interpreted as a Test case dir (when looking for TestCaseIds)
                var testCaseFilePath = directory.FullName.Replace(RootFolder, ".");
                var testCaseId = GetTestCaseId(testCaseFilePath);

                if (testCaseId < 0)
                {
                    continue;
                }

                try
                {
                    testCaseFilePaths.Add(testCaseId, testCaseFilePath);
                }
                catch
                {
                    errorContent.AppendLine($"TestCase {testCaseId} duplicated here [{testCaseFilePath}] and [{testCaseFilePaths[testCaseId]}]");
                    testCaseFilePaths[testCaseId] = null;
                }
            }

            var content = string.Join(Environment.NewLine, testCaseFilePaths.Values.Where(tc => tc != null));

            File.WriteAllText(CacheInfoFilePath, content);

            if (errorContent.Length == 0)
            {
                // If no errors for the directory (like duplicates) then delete the file
                File.Delete(CacheErrorFilePath);
            }
            else
            {
                File.WriteAllText(CacheErrorFilePath, errorContent.ToString());
            }
        }

        /// <summary>
        /// The check test case in cache file.
        /// </summary>
        /// <param name="testCaseId">
        /// The test case id.
        /// </param>
        /// <returns>
        /// Indication of existence
        /// </returns>
        public bool CheckIfTestCaseInCacheFile(int testCaseId)
        {
            var testCaseDirectoryPath = GetTestCaseDirectoryPath(testCaseId);
            var retVal = testCaseDirectoryPath != null;

            return retVal;
        }

        /// <summary>
        /// The check test case duplicated.
        /// </summary>
        /// <param name="testCaseId">
        /// The test case id.
        /// </param>
        /// <param name="updateCache">
        /// The update cache.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool CheckTestCaseDuplicated(int testCaseId, bool updateCache = false)
        {
            if (updateCache)
            {
                RefreshCache();
            }

            var cacheContent = GetCacheErrorFileContent();

            if (string.IsNullOrEmpty(cacheContent))
            {
                return false;
            }

            var testCaseInErrorFileRegExp = $@"TestCase\s+{testCaseId}\s+duplicated here";
            var match = Regex.Match(cacheContent, testCaseInErrorFileRegExp, RegexOptions.Multiline);
            var retVal = match.Success;

            return retVal;
        }

        /// <summary>
        /// The get test case folders paths from cache file
        /// </summary>
        /// <returns>
        /// The test case folder paths
        /// </returns>
        public List<string> GetTestCaseFolderPathsFromCache()
        {
            var retVal = new List<string>();

            if (!File.Exists(CacheInfoFilePath))
            {
                RefreshCache();

                if (!File.Exists(CacheInfoFilePath))
                {
                    return null;
                }
            }

            StreamReader reader = new StreamReader(CacheInfoFilePath);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                retVal.Add(line);
            }

            return retVal;
        }

        /// <summary>
        /// The get test case Ids from cache file
        /// </summary>
        /// <returns>
        /// The test case Ids
        /// </returns>
        public List<int> GetTestCaseIdsFromCache()
        {
            var retVal = new List<int>();

            if (!File.Exists(CacheInfoFilePath))
            {
                RefreshCache();

                if (!File.Exists(CacheInfoFilePath))
                {
                    return null;
                }
            }

            StreamReader reader = new StreamReader(CacheInfoFilePath);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var tcId = GetTestCaseId(line);
                retVal.Add(tcId);
            }

            return retVal;
        }

        /// <summary>
        /// The get cache error file content.
        /// </summary>
        /// <returns>
        /// The content
        /// </returns>
        public string GetCacheErrorFileContent()
        {
            if (!File.Exists(CacheErrorFilePath))
            {
                return null;
            }

            var retVal = File.ReadAllText(CacheErrorFilePath);

            return retVal;
        }

        /// <summary>
        /// The get test case directory path.
        /// </summary>
        /// <param name="testCaseId">
        /// The test case id.
        /// </param>
        /// <param name="secondRoundAround">
        /// The second round around. If we don not find the path, we refresh the cache - this avoids uis doing it forever :-)
        /// </param>
        /// <returns>
        /// The path - or null if not found
        /// </returns>
        private string GetTestCaseDirectoryPath(int testCaseId, bool secondRoundAround)
        {
            if (!File.Exists(CacheInfoFilePath))
            {
                RefreshCache();

                if (!File.Exists(CacheInfoFilePath))
                {
                    return null;
                }
            }

            var testCaseRegExp = $@".*\\?[Tt][Cc]0*{testCaseId}([^\\\d].*)?$";
            var cacheContent = File.ReadAllText(CacheInfoFilePath);
            var match = Regex.Match(cacheContent, testCaseRegExp, RegexOptions.Multiline);
            string retVal;

            // is the tc in the cache info file?
            if (!match.Success)
            {
                if (!secondRoundAround)
                {
                    // still not - even after refresh
                    return null;
                }

                // We didn't find the test case - it might have moved or is new ... We give it another shot
                RefreshCache();

                // Okay fresh cache - now it should be there, or it does not actually exists
                retVal = GetTestCaseDirectoryPath(testCaseId, true);

                return retVal;
            }

            retVal = Path.GetFullPath(Path.Combine(RootFolder, match.Value.Trim()));

            if (Directory.Exists(retVal))
            {
                // we found an entry that exists in reality - MOM I'm done
                return retVal;
            }

            if (secondRoundAround)
            {
                return null;
            }

            // Hmm, test case found in the cache file does not exist in reality
            // It might have moved - lets update the cache
            RefreshCache();

            // now updated, lets look again, but do not continue to look for it
            retVal = GetTestCaseDirectoryPath(testCaseId, true);

            return retVal;
        }
    }

    /// <summary>
    /// The logger - temp solution until something better comes along
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed. Suppression is OK here.")]
    internal class Logger
    {
        /// <summary>
        /// The log error.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public static void LogError(string message)
        {
            Console.WriteLine(message);
        }
    }
}

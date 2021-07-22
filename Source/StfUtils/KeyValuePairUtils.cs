// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KeyValuePairUtils.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The key value pair utils.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities
{
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Mir.Stf.Utilities.FileUtilities;

    /// <summary>
    /// The key value pair utils.
    /// </summary>
    public class KeyValuePairUtils
    {
        /// <summary>
        /// The file utils.
        /// </summary>
        private readonly FileUtils fileUtils;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyValuePairUtils"/> class.
        /// </summary>
        /// <param name="assignmentOperator">
        /// The assignment indicator.
        /// Default is '='
        /// </param>
        /// <param name="startOfCommentLine">
        /// The string all comments starts with - rest of the line is treated as a comment.
        /// Default is '//'
        /// </param>
        /// <param name="keyNameIgnoreCase">
        ///  whether or now the key used for the dictionary is case significant or not.
        /// Default is true
        /// </param>
        public KeyValuePairUtils(string assignmentOperator = "=", string startOfCommentLine = "//", bool keyNameIgnoreCase = true)
        {
            AssignmentOperator = assignmentOperator;
            StartOfCommentLine = startOfCommentLine;
            KeyNameIgnoreCase = keyNameIgnoreCase;

            fileUtils = new FileUtils();
        }

        /// <summary>
        /// Gets the string all comments starts with - rest of the line is treated as a comment.
        /// Default is '//'
        /// </summary>
        public string StartOfCommentLine { get; }

        /// <summary>
        /// Gets a value indicating whether or note the key used for the dictionary is case significant or not. 
        /// Default is insignificant - to ignore case
        /// </summary>
        public bool KeyNameIgnoreCase { get; }

        /// <summary>
        /// Gets the assignment operator used then reading or saving a dictionary to a file.
        /// </summary>
        public string AssignmentOperator { get; }

        /// <summary>
        /// The get a ordered dictionary for a file containing KeyName KeyValue pair (default separated by '=').
        /// </summary>
        /// <param name="fileName">
        /// The file Name.
        /// </param>
        /// <returns>
        /// A Dictionary of key value pairs".
        /// </returns>
        public OrderedDictionary ReadKeyValuePairsFromFile(string fileName)
        {
            var content = fileUtils.GetCleanFilecontent(fileName);
            var retVal = GetKeyValuePairs(content);

            return retVal;
        }

        /// <summary>
        /// The save key value pairs to file.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <param name="dictionary">
        /// The dictionary.
        /// </param>
        /// <param name="headerText">
        /// The header text.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool SaveKeyValuePairsToFile(string fileName, OrderedDictionary dictionary, string headerText = null)
        {
            var newLine = Environment.NewLine;

            string GetFileHeader()
            {
                const string HeaderLine = " ========================================================";
                var retVal = $@"{StartOfCommentLine}{HeaderLine}
{StartOfCommentLine} = 
{StartOfCommentLine} = {headerText}
{StartOfCommentLine} = 
{StartOfCommentLine}{HeaderLine}{newLine}{newLine}";

                return retVal;
            }

            // lets start with a header
            var content = GetFileHeader();

            foreach (var key in dictionary.Keys)
            {
                var keyName = key.ToString();
                var keyValue = dictionary[keyName];

                content += $"{keyName} {AssignmentOperator} {keyValue}{newLine}";
            }

            File.WriteAllText(fileName, content);

            return true;
        }

        /// <summary>
        /// The parse test data values.
        /// </summary>
        /// <param name="rawContent">
        /// The raw Content.
        /// </param>
        /// <returns>
        /// The <see cref="OrderedDictionary"/>.
        /// </returns>
        public OrderedDictionary GetKeyValuePairs(string rawContent)
        {
            var retVal = new OrderedDictionary();
            var keyValueRegExp = $@"^\s*(?<KeyName>.+){AssignmentOperator}(?<KeyValue>[^\n]*)$";
            var content = fileUtils.RemoveComments(rawContent);

            if (string.IsNullOrEmpty(content))
            {
                return retVal;
            }

            var lines = Regex.Matches(content, keyValueRegExp, RegexOptions.Multiline);

            foreach (Match line in lines)
            {
                var keyName = line.Groups["KeyName"].Value.Trim();
                var keyValue = line.Groups["KeyValue"].Value.Trim();

                // the same key can by used several times
                var keys = retVal.Keys;
                var compareMode = KeyNameIgnoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture;
                var keyToUse = keys.Cast<string>().FirstOrDefault(name => keyName.Equals(name, compareMode));

                if (keyToUse == null)
                {
                    // New unknown key value - just add it to the retVal dict
                    retVal.Add(keyName, keyValue);
                    continue;
                }

                // key was already in the retVal dict - overlay that one
                retVal[keyToUse] = keyValue;
            }

            return retVal;
        }

        /// <summary>
        /// Overlay two dictionaries.
        /// </summary>
        /// <param name="masterDictionary">
        /// The base dictionary.
        /// </param>
        /// <param name="overlayDictionary">
        /// The overlay dictionary.
        /// </param>
        /// <returns>
        /// The <see cref="OrderedDictionary"/>.
        /// </returns>
        public OrderedDictionary OverlayDictionary(OrderedDictionary masterDictionary, OrderedDictionary overlayDictionary)
        {
            // if the overlay one is empty, then overlaying is easy
            if (overlayDictionary == null || overlayDictionary.Count == 0)
            {
                return masterDictionary;
            }
    
            // if the master one is empty, then overlaying is easy
            if (masterDictionary == null || masterDictionary.Count == 0)
            {
                return overlayDictionary;
            }

            var retVal = new OrderedDictionary();

            // basis for retVal is the masterDictionary
            foreach (var key in masterDictionary.Keys)
            {
                var keyName = key.ToString().Trim();
                var keyValue = masterDictionary[keyName];

                retVal.Add(keyName, keyValue);
            }

            foreach (var key in overlayDictionary.Keys)
            {
                var keyName = key.ToString();
                var keyValue = overlayDictionary[keyName];
                var keys = retVal.Keys;
                var compareMode = KeyNameIgnoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture;
                var keyToUse = keys.Cast<string>().FirstOrDefault(name => keyName.Equals(name, compareMode));

                if (keyToUse == null)
                {
                    // New unknown key value - just add it to the retVal dict
                    retVal.Add(keyName, keyValue);
                    continue;
                }

                // key was already in the retVal dict - overlay that one
                retVal[keyToUse] = keyValue;
            }

            return retVal;
        }
    }
}

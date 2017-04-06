// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfTextUtils.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace Mir.Stf.KernelUtils
{
    /// <summary>
    /// Utils for handling text for the kernel 
    /// </summary>
    public class StfTextUtils
    {
        /// <summary>
        /// Dictionary to hold variables registered ad-hoc
        /// </summary>
        private readonly StringDictionary dict = new StringDictionary();

        /// <summary>
        /// Register ad-hoc variables that the kernel should remember later on...
        /// </summary>
        /// <param name="keyName">
        /// name of the key
        /// </param>
        /// <param name="keyValue">
        /// the value of the key
        /// </param>
        /// <returns>
        /// wether it succeeded or not
        /// </returns>
        public bool Register(string keyName, string keyValue)
        {
            if (string.IsNullOrWhiteSpace(keyName))
            {
                return false;
            }

            if (dict.ContainsKey(keyName))
            {
                dict[keyName] = keyValue;
                return true;
            }

            dict.Add(keyName, keyValue);
            return true;
        }

        /// <summary>
        /// Clear all notions of variables - like after an initialization
        /// </summary>
        /// <returns>
        /// wether it succeeded or not
        /// </returns>
        public bool VariablesClear()
        {
            dict.Clear();
            return true;
        }

        /// <summary>
        /// Expands Variables enclosed by % in a string. 
        /// Uses internal registered variables as well as DOS environment variables
        /// </summary>
        /// <param name="line">
        /// Line containing the string to substitute variables
        /// </param>
        /// <returns>
        /// the string where the substrings enclosed in %% are substituted with Stf variables.
        /// </returns>
        public string ExpandVariables(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                return line;
            }

            var variables = new Regex("%(?<variable>[^%]+)%");
            var matches = variables.Match(line);

            while (matches.Success)
            {
                var variable = matches.Groups["variable"].Value;

                if (!dict.ContainsKey(variable))
                {
                    matches = matches.NextMatch();
                    continue;
                }

                var replaceKey = string.Format("%{0}%", variable);
                var replaceValue = dict[variable];

                line = line.Replace(replaceKey, replaceValue);
                matches = matches.NextMatch();
            }

            var retVal = Environment.ExpandEnvironmentVariables(line);
            return retVal;
        }

        /// <summary>
        /// Gets a value for a registered variable name
        /// </summary>
        /// <param name="variableName">
        /// Name of variable
        /// </param>
        /// <returns>
        /// the registered value of the variable
        /// </returns>
        public string GetVariable(string variableName)
        {
            return dict.ContainsKey(variableName) ? dict[variableName] : null;
        }

        /// <summary>
        /// Gets a value for a variable name
        /// </summary>
        /// <param name="variableName">
        /// Name of variable
        /// </param>
        /// <param name="defaultValue">
        /// If value not found for this variable, then set it to this value
        /// </param>
        /// <returns>
        /// the registered value of the variable
        /// </returns>
        public string GetVariableOrSetDefault(string variableName, string defaultValue)
        {
            var variableExpandString = string.Format("%{0}%", variableName);
            var variableValue = ExpandVariables(variableExpandString);

            // If no variable Value found, then default
            if (variableValue == variableExpandString)
            {
                variableValue = defaultValue;
                Register(variableName, variableValue);
            }

            return variableValue;
        }
    }
}
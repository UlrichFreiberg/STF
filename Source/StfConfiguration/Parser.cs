﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Parser.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the Parser type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Text.RegularExpressions;

namespace Mir.Stf.Utilities
{
    /// <summary>
    /// Functionality to expose and navigate through the Section tree
    /// </summary>
    public class Parser
    {
        /// <summary>
        /// Gets the value of a key within a section tree.
        /// </summary>
        /// <param name="section">
        /// The section.
        /// </param>
        /// <param name="keyName">
        /// The key name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// if the key is not found this will be thrown 
        /// </exception>
        internal string GetKey(Section section, string keyName)
        {
            if (IsLeaf(keyName))
            {
                if (section.Keys.ContainsKey(keyName))
                {
                    return section.Keys[keyName].KeyValue;
                }

                if (!string.IsNullOrEmpty(section.DefaultSection))
                {
                    var tryDefaultKeyName = string.Format("{0}.{1}", section.DefaultSection, keyName);
                    var retVal = GetKey(section, tryDefaultKeyName);

                    return retVal;
                }

                var errMsg = string.Format("Section [{0}] have no matching key [{1}]", section.SectionName, keyName);
                throw new ArgumentOutOfRangeException(keyName, errMsg);
            }

            var sectionName = this.GetSectionName(keyName);

            if (string.CompareOrdinal(section.SectionName, sectionName) == 0)
            {
                // we found the section that should hold the key
                return GetKey(section, GetKeyName(keyName));
            }

            if (section.Sections.ContainsKey(sectionName))
            {
                return GetKey(section.Sections[sectionName], GetKeyName(keyName));
            }

            if (!string.IsNullOrEmpty(section.DefaultSection))
            {
                if (section.Sections.ContainsKey(section.DefaultSection))
                {
                    var retVal = GetKey(section.Sections[section.DefaultSection], keyName);
                    return retVal;
                }
            }

            throw new ArgumentOutOfRangeException(keyName, "Section not found");
        }

        /// <summary>
        /// Checks if a keyName is a leaf.
        /// </summary>
        /// <param name="keyName">
        /// The key name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool IsLeaf(string keyName)
        {
            return keyName.IndexOf('.') == -1;
        }

        /// <summary>
        /// Gets the section name from a keyName
        /// </summary>
        /// <param name="keyName">
        /// The key name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetSectionName(string keyName)
        {
            var regex = new Regex(@"^[^.]*\.");

            var nameMatch = regex.Match(keyName).Value;
            var name = nameMatch.Substring(0, nameMatch.Length - 1);
            return name;
        }

        /// <summary>
        /// Gets the key name from a keyName
        /// </summary>
        /// <param name="keyName">
        /// The key name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetKeyName(string keyName)
        {
            var regex = new Regex(@"\..*");

            var nameMatch = regex.Match(keyName).Value;
            var name = nameMatch.Substring(1, nameMatch.Length - 1);
            return name;
        }
    }
}

﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Section.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the Section type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Mir.Stf.Utilities
{
    /// <summary>
    /// The section. Contains sections or values
    /// </summary>
    public class Section : Comparer<Section>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Section"/> class.
        /// </summary>
        public Section()
        {
            Keys = new Dictionary<string, Key>();
            Sections = new Dictionary<string, Section>();
        }

        /// <summary>
        /// Enumeration to indicate how to do the dump.
        /// </summary>
        public enum DumpAs
        {
            /// <summary>
            /// Dump as xml.
            /// </summary>
            AsXml,

            /// <summary>
            /// Dump as text.
            /// </summary>
            AsText
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string SectionName { get; set; }

        /// <summary>
        /// Gets or sets the default section. If not specified a containing section, 
        /// then this will be the one you get
        /// </summary>
        public string DefaultSection { get; set; }

        /// <summary>
        /// Gets or sets the Contained sections.
        /// </summary>
        public Dictionary<string, Section> Sections { get; set; }

        /// <summary>
        /// Gets or sets the keys.
        /// </summary>
        public Dictionary<string, Key> Keys { get; set; }

        /// <summary>
        /// Implementation / override of compare. 
        /// Only match the name - not the rest of the tree
        /// </summary>
        /// <param name="x">
        /// One section to compare
        /// </param>
        /// <param name="y">
        /// The other section to compare
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public override int Compare(Section x, Section y)
        {
            if (x == y)
            {
                return 0;
            }

            if (x == null || y == null)
            {
                return -1;
            }

            return string.CompareOrdinal(x.SectionName, y.SectionName);
        }

        /// <summary>
        /// Recursively compare the two sections.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="y">
        /// The y.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Identical(Section x, Section y)
        {
            var dumpX = x.DumpSection(DumpAs.AsXml);
            var dumpY = y.DumpSection(DumpAs.AsXml);

            return string.CompareOrdinal(dumpX, dumpY) == 0;
        }

        /// <summary>
        /// TODO The make copy.
        /// </summary>
        /// <returns>
        /// The <see cref="Section"/>.
        /// </returns>
        public Section MakeCopy()
        {
            var retVal = new Section
            {
                SectionName = SectionName,
                DefaultSection = DefaultSection
            };

            foreach (var childNode in Sections)
            {
                var childNodes = childNode.Value.MakeCopy();
                retVal.Sections.Add(childNode.Key, childNodes);
            }

            foreach (var childNode in Keys)
            {
                var newNode = new Key
                {
                    KeyName = childNode.Key,
                    KeyValue = childNode.Value.KeyValue,
                    SourceConfigFile = childNode.Value.SourceConfigFile
                };
                retVal.Keys.Add(childNode.Key, newNode);
            }

            return retVal;
        }

        /// <summary>
        /// Override of ToString()
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            var txt = DumpSection(DumpAs.AsText);
            return txt;
        }

        /// <summary>
        /// Dump a section tree
        /// </summary>
        /// <param name="dumpAs">
        /// What format to dump as.
        /// </param>
        /// <param name="fileName">
        /// TODO The file name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string DumpSection(
            DumpAs dumpAs,
            string fileName = null)
        {
            string retVal;

            switch (dumpAs)
            {
                case DumpAs.AsText:
                    retVal = DumpSectionAsText();
                    break;
                case DumpAs.AsXml:
                    var doc = DumpSectionAsXml();

                    // if the filename is given, then save the file - othervise dont:-)
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        doc.Save(fileName);
                    }

                    retVal = doc.InnerXml;
                    break;
                default:
                    throw new ArgumentException("Internal Error: Unknown dumpAs");
            }

            return retVal;
        }

        /// <summary>
        /// Internal utility function to dump a section recursively as Test
        /// </summary>
        /// <param name="indent">
        /// The indentation aka level - starts with zero.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string DumpSectionAsText(int indent = 0)
        {
            var sb = new StringBuilder();
            var indentString = string.Empty.PadLeft(indent);
            sb.AppendLine(string.Format("{0}KeyName:{1}, DefaultSection:{2}", indentString, SectionName, DefaultSection));
            if (Sections.Count > 0)
            {
                sb.AppendLine(string.Format("{0}Sections", indentString));
            }

            foreach (var subSection in Sections)
            {
                sb.Append(subSection.Value.DumpSectionAsText(indent + 2));
            }

            if (Keys.Count > 0)
            {
                sb.AppendLine(string.Format("{0}Keys", indentString));
            }

            foreach (var key in Keys)
            {
                indentString = string.Empty.PadLeft(indent + 2);
                sb.AppendLine(string.Format("{0}{1}", indentString, key.Value));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Internal utility function to dump a section recursively as XML
        /// </summary>
        /// <param name="xmlDumpDoc">
        /// The xml dump doc.
        /// </param>
        /// <param name="rootNode">
        /// The root node.
        /// </param>
        /// <returns>
        /// The <see cref="XmlDocument"/>.
        /// </returns>
        private XmlDocument DumpSectionAsXml(XmlDocument xmlDumpDoc = null, XmlNode rootNode = null)
        {
            if (xmlDumpDoc == null)
            {
                xmlDumpDoc = new XmlDocument();
                rootNode = xmlDumpDoc.CreateElement("configuration");
                xmlDumpDoc.AppendChild(rootNode);
            }

            // if not the internal top section
            if (string.IsNullOrEmpty(SectionName))
            {
                if (Sections.Count == 0)
                {
                    return xmlDumpDoc;
                }

                var firstKey = Sections.Keys.First();
                var retVal = Sections[firstKey].DumpSectionAsXml(xmlDumpDoc, rootNode);

                return retVal;
            }

            if (rootNode == null)
            {
                throw new Exception("rootNode shouldnt be null here");
            }

            var sectionNode = xmlDumpDoc.CreateElement("section");
            var attribute = xmlDumpDoc.CreateAttribute("name");

            attribute.Value = SectionName;
            if (sectionNode.Attributes == null)
            {
                throw new Exception("Cant CreateAtribute");
            }

            sectionNode.Attributes.Append(attribute);
            attribute = xmlDumpDoc.CreateAttribute("defaultsection");
            if (sectionNode.Attributes == null)
            {
                throw new Exception("Cant CreateAtribute");
            }

            attribute.Value = DefaultSection;
            sectionNode.Attributes.Append(attribute);
            rootNode.AppendChild(sectionNode);

            if (Sections.Count > 0)
            {
                foreach (var subSection in Sections)
                {
                    subSection.Value.DumpSectionAsXml(xmlDumpDoc, sectionNode);
                }
            }

            if (Keys.Count <= 0)
            {
                return xmlDumpDoc;
            }

            foreach (var key in Keys)
            {
                var keyNode = xmlDumpDoc.CreateElement("add");

                attribute = xmlDumpDoc.CreateAttribute("key");
                attribute.Value = key.Value.KeyName;
                keyNode.Attributes.Append(attribute);
                attribute = xmlDumpDoc.CreateAttribute("value");
                attribute.Value = key.Value.KeyValue;
                keyNode.Attributes.Append(attribute);
                sectionNode.AppendChild(keyNode);
            }

            return xmlDumpDoc;
        }
    }
}

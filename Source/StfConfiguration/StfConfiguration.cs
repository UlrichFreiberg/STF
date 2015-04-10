// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfConfiguration.cs" company="Foobar">
//   2015
// </copyright>
// <summary>
//   The configuration tree.
//   Holding all the low level information around all the config files.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Xml;

namespace Stf.Utilities
{
    using System.Collections.Generic;

    /// <summary>
    /// The configuration tree. 
    /// Holding all the low level information around all the config files.
    /// </summary>
    public class StfConfiguration
    {
        /// <summary>
        /// A Xml reader.
        /// </summary>
        private XmlTextReader reader;

        /// <summary>
        /// The current section, the latest config loaded.
        /// </summary>
        private Section currentlyLoadedSection;

        /// <summary>
        /// Initializes a new instance of the <see cref="StfConfiguration"/> class.
        /// and loads it with the configuration tree in the file
        /// </summary>
        /// <param name="configFileName">
        /// The config file name.
        /// </param>
        public StfConfiguration(string configFileName)
        {
            this.LoadConfig(configFileName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StfConfiguration"/> class.
        /// </summary>
        public StfConfiguration()
        {
        }

        /// <summary>
        /// Loads a config file into a configuration tree.
        /// </summary>
        /// <param name="fileName">
        /// The file name of the config file.
        /// </param>
        /// <returns>
        /// The <see cref="Section"/>.
        /// </returns>
        public Section LoadConfig(string fileName)
        {
            this.reader = new XmlTextReader(fileName);
            this.currentlyLoadedSection = GetSections();
            return this.currentlyLoadedSection;
        }

        /// <summary>
        /// The over lay.
        /// </summary>
        /// <param name="core">
        /// The core aka the master, template
        /// </param>
        /// <param name="overlay">
        /// The overlaying layer.
        /// </param>
        /// <returns>
        /// The <see cref="Section"/>.
        /// </returns>
        public Section OverLay(Section core, Section overlay)
        {
            var overLayer = new OverLayer();
            return overLayer.OverLay(core, overlay);
        }

        /// <summary>
        /// Initiates a dictionary of relevant information given a endUser configuration type.
        /// </summary>
        /// <param name="userConfigurationObject">
        /// The end User ConfigurationObject.
        /// </param>
        /// <returns>
        /// The Dictionary holding information around fieldNames and its current Values.
        /// </returns>
        public Dictionary<string, string> LoadUserConfiguration(object userConfigurationObject)
        {
            var confValuesHandler = new Reflection(userConfigurationObject);
            var configEntities = confValuesHandler.GetConfigPropertiesFromType();

            confValuesHandler.SetConfig(configEntities, this);
            return configEntities;
        }

        /// <summary>
        /// Gets the key part of the Path. Overload using the latest config/section loaded
        /// </summary>
        /// <param name="keyName">
        /// The key name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetKeyValue(string keyName)
        {
            return this.GetKeyValue(this.currentlyLoadedSection, keyName);
        }

        /// <summary>
        /// Gets the value for a key.
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
        public string GetKeyValue(Section section, string keyName)
        {
            var parser = new Parser();
            return parser.GetKey(section, keyName);
        }

        /// <summary>
        /// Get sections for this root.
        /// </summary>
        /// <param name="currentSection">
        /// The section we want to get sections for.
        /// </param>
        /// <returns>
        /// The <see cref="Section"/>.
        /// </returns>
        private Section GetSections(Section currentSection = null)
        {
            if (currentSection == null)
            {
                currentSection = new Section { SectionName = string.Empty };
            }

            while (GetToNextToken())
            {
                switch (this.reader.NodeType)
                {
                    case XmlNodeType.Element:
                        switch (this.reader.Name)
                        {
                            case "key":
                                HandleAdd(currentSection);
                                break;
                            case "section":
                                var section = HandleSection(new Section());
                                currentSection.Sections.Add(section.SectionName, GetSections(section));
                                break;
                        }

                        break;

                    case XmlNodeType.EndElement:
                        return currentSection;
                }
            }

            return currentSection;
        }

        /// <summary>
        /// A tokenizer - get to next token.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool GetToNextToken()
        {
            while (this.reader.Read())
            {
                switch (this.reader.NodeType)
                {
                    case XmlNodeType.EndElement:
                    case XmlNodeType.Element:
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// for this section get all values and contained sections
        /// </summary>
        /// <param name="section">
        /// The section.
        /// </param>
        /// <returns>
        /// The <see cref="Section"/>.
        /// </returns>
        private Section HandleSection(Section section)
        {
            section.DefaultSection = string.Empty;
            while (this.reader.MoveToNextAttribute())
            {
                switch (this.reader.Name)
                {
                    case "name":
                        section.SectionName = this.reader.Value;
                        break;
                    case "defaultsection":
                        section.DefaultSection = this.reader.Value;
                        break;
                }
            }

            return section;
        }

        /// <summary>
        /// For this element, get the attributes that constitutes a key value pair.
        /// </summary>
        /// <param name="section">
        /// The section where the key value pair belongs.
        /// </param>
        private void HandleAdd(Section section)
        {
            var newKey = new Key { SourceConfigFile = this.reader.BaseURI };

            while (this.reader.MoveToNextAttribute())
            {
                switch (this.reader.Name)
                {
                    case "name":
                        newKey.KeyName = this.reader.Value;
                        break;
                    case "value":
                        newKey.KeyValue = this.reader.Value;
                        break;
                }
            }

            section.Keys.Add(newKey.KeyName, newKey);
        }
    }
}

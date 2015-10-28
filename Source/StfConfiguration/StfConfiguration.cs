// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfConfiguration.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The configuration tree.
//   Holding all the low level information around all the config files.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Xml;

namespace Mir.Stf.Utilities
{
    using System;
    using System.IO;

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
        /// The configuration for the set environment from the resolved configuration.
        /// </summary>
        private Section environmentConfiguration;

        /// <summary>
        /// Backing field for Environment
        /// </summary>
        private string environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="StfConfiguration"/> class.
        /// and loads it with the configuration tree in the file
        /// </summary>
        /// <param name="configFileName">
        /// The config file name.
        /// </param>
        public StfConfiguration(string configFileName)
        {
            LoadConfig(configFileName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StfConfiguration"/> class.
        /// </summary>
        public StfConfiguration()
        {
        }

        /// <summary>
        /// Gets or sets the current Environment.
        /// For most test configurations Environment is an intrinsic abstraction.
        /// </summary>
        public string Environment
        {
            get
            {
                if (string.IsNullOrEmpty(environment))
                {
                    Environment = DefaultEnvironment;
                }

                return environment;
            }

            set
            {
                // no need to change is same:-)
                if (environment == value)
                {
                    return;
                }

                if (string.IsNullOrEmpty(value))
                {
                    environment = null;
                    environmentConfiguration = currentlyLoadedSection;
                    return;
                }

                var configEnvironmentSection = currentlyLoadedSection.Sections["Environments"];
                var newEnvironmentName = value;
                var newEnvironmentConfig = configEnvironmentSection.Sections[newEnvironmentName];

                if (newEnvironmentConfig == null)
                {
                    return;
                }

                environment = newEnvironmentName;
                environmentConfiguration = newEnvironmentConfig;
            }
        }

        /// <summary>
        /// Gets the default environment set in configuration file
        /// </summary>
        public string DefaultEnvironment
        {
            get
            {
                if (!currentlyLoadedSection.Sections.ContainsKey("Environments"))
                {
                    return null;
                }

                var environmentSection = currentlyLoadedSection.Sections["Environments"];
                return currentlyLoadedSection.Sections["Environments"].DefaultSection;
            }
        }

        /// <summary>
        /// Get a value for a key - take into account and checks for the current Environment if set.
        /// </summary>
        /// <param name="configValuePath">
        /// Path to the KeyValue pair
        /// </param>
        /// <returns>
        /// the value of the keyValue pair
        /// </returns>
        public string GetConfigValue(string configValuePath)
        {
            var configToUse = currentlyLoadedSection;

            // lets see if we should use the Environment configuration
            if (!string.IsNullOrEmpty(Environment))
            {
                configToUse = environmentConfiguration;
            }

            var value = GetKeyValue(configToUse, configValuePath);
            return value;
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
            if (!File.Exists(fileName))
            {
                var errMsg = string.Format("Configuration File [{0}] doesn't exist", fileName);
                throw new ArgumentOutOfRangeException(fileName, errMsg);
            }

            reader = new XmlTextReader(fileName);
            currentlyLoadedSection = GetSections();
            return currentlyLoadedSection;
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

            // the overlayer handles if arguments are null
            return overLayer.OverLay(core, overlay);
        }

        /// <summary>
        /// The overlayer used by the pluginloader. Meant to take a (plugin)settings
        /// and overlay the core configuration with a (plugin)settings.
        /// </summary>
        /// <param name="filename">
        /// The filename.
        /// </param>
        /// <returns>
        /// The <see cref="Section"/>.
        /// </returns>
        public Section OverLay(string filename)
        {
            var overLayer = new OverLayer();
            var stfconfiguration = new StfConfiguration();
            var overlay = stfconfiguration.LoadConfig(filename);

            // the overlayer handles if arguments are null
            currentlyLoadedSection = overLayer.OverLay(currentlyLoadedSection, overlay);
            
            Environment = DefaultEnvironment;
            
            return currentlyLoadedSection;
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
            if (currentlyLoadedSection != null)
            {
                return GetKeyValue(currentlyLoadedSection, keyName);
            }

            var errMsg = string.Format("No section is loaded - can't find matching key [{0}]", keyName);
            throw new ArgumentOutOfRangeException(keyName, errMsg);
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
            if (currentlyLoadedSection != null)
            {
                var parser = new Parser();
                return parser.GetKey(section, keyName);
            }

            var errMsg = string.Format("No section is loaded - can't find matching key [{0}]", keyName);
            throw new ArgumentOutOfRangeException(keyName, errMsg);
        }

        /// <summary>
        /// The try get key value.
        /// </summary>
        /// <param name="keyName">
        /// The key name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool TryGetKeyValue(string keyName, out string value)
        {
            value = string.Empty;
            try
            {
                value = GetKeyValue(keyName);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
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
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        switch (reader.Name)
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
            while (reader.Read())
            {
                switch (reader.NodeType)
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
            while (reader.MoveToNextAttribute())
            {
                switch (reader.Name)
                {
                    case "name":
                        section.SectionName = reader.Value;
                        break;
                    case "defaultsection":
                        section.DefaultSection = reader.Value;
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
            var newKey = new Key { SourceConfigFile = reader.BaseURI };

            while (reader.MoveToNextAttribute())
            {
                switch (reader.Name)
                {
                    case "name":
                        newKey.KeyName = reader.Value;
                        break;
                    case "value":
                        newKey.KeyValue = reader.Value;
                        break;
                }
            }

            section.Keys.Add(newKey.KeyName, newKey);
        }
    }
}

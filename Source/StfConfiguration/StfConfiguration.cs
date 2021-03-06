﻿// --------------------------------------------------------------------------------------------------------------------
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
            EvaluateKeyValue = System.Environment.ExpandEnvironmentVariables;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StfConfiguration"/> class.
        /// </summary>
        public StfConfiguration()
        {
        }

        /// <summary>
        /// The evaluate key value delegate.
        /// </summary>
        /// <param name="keyValue">
        /// The key value.
        /// </param>
        /// <returns>
        /// Delegate used to expand defaultSection and values. 
        /// </returns>
        public delegate string EvaluateKeyValueDelegate(string keyValue);

        /// <summary>
        /// Gets or sets the evaluate key value.
        /// </summary>
        public EvaluateKeyValueDelegate EvaluateKeyValue { get; set; }

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
                // no need to change if same:-)
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
                if (currentlyLoadedSection == null || !currentlyLoadedSection.Sections.ContainsKey("Environments"))
                {
                    return null;
                }

                return currentlyLoadedSection.Sections["Environments"].DefaultSection;
            }
        }

        /// <summary>
        /// Get a value for a key - take into account and checks for the current Environment if set.
        /// </summary>
        /// <param name="configValuePath">
        /// Path to the KeyValue pair
        /// </param>
        /// <param name="defaultValue">
        /// The default Value.
        /// </param>
        /// <returns>
        /// the value of the keyValue pair
        /// </returns>
        public string GetConfigValue(string configValuePath, string defaultValue = null)
        {
            var configToUse = currentlyLoadedSection;
            string retVal;

            // lets see if we should use the Environment configuration
            var pathToUse = configValuePath;

            if (!string.IsNullOrEmpty(Environment) && !IsAbsoluteConfigurationPath(ref pathToUse))
            {
                configToUse = environmentConfiguration;
            }

            try
            {
                retVal = GetKeyValue(configToUse, pathToUse);
            }
            catch (ArgumentOutOfRangeException)
            {
                // if no default value, then we are missing something in the configuration
                if (defaultValue == null)
                {
                    throw;
                }

                retVal = defaultValue;
            }

            return retVal;
        }

        /// <summary>
        /// The set config value.
        /// </summary>
        /// <param name="configValuePath">
        /// The config value path.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool SetConfigValue(string configValuePath, string value)
        {
            var configToUse = currentlyLoadedSection;
            bool retVal;

            // lets see if we should use the Environment configuration
            var pathToUse = configValuePath;

            if (!string.IsNullOrEmpty(Environment) && !IsAbsoluteConfigurationPath(ref pathToUse))
            {
                configToUse = environmentConfiguration;
            }

            try
            {
                retVal = SetKeyValue(configToUse, pathToUse, value);
            }
            catch (ArgumentOutOfRangeException)
            {
                var errMsg = $"No section is loaded - can't find matching key [{configValuePath}]";

                throw new ArgumentOutOfRangeException(configValuePath, errMsg);
            }

            return retVal;
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
                var errMsg = $"Configuration File [{fileName}] doesn't exist";

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
        /// The overlayer used by the plugin loader. Meant to take a (plugin)settings
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
            var stfConfiguration = new StfConfiguration();
            var overlay = stfConfiguration.LoadConfig(filename);

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
        public Dictionary<string, StfConfigurationAttribute> LoadUserConfiguration(object userConfigurationObject)
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
                var retVal = GetKeyValue(currentlyLoadedSection, keyName);

                if (EvaluateKeyValue != null)
                {
                    retVal = EvaluateKeyValue(retVal);
                }

                return retVal;
            }

            var errMsg = $"No section is loaded - can't find matching key [{keyName}]";
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
                var parser = new Parser { EvaluateKeyValue = EvaluateKeyValue };

                return parser.GetKey(section, keyName);
            }

            var errMsg = $"No section is loaded - can't find matching key [{keyName}]";
            throw new ArgumentOutOfRangeException(keyName, errMsg);
        }

        /// <summary>
        /// The set key value.
        /// </summary>
        /// <param name="section">
        /// The section.
        /// </param>
        /// <param name="keyName">
        /// The key name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Exception if no section is currently loaded
        /// </exception>
        public bool SetKeyValue(Section section, string keyName, string value)
        {
            if (currentlyLoadedSection != null)
            {
                var parser = new Parser { EvaluateKeyValue = EvaluateKeyValue };

                return parser.SetValue(section, keyName, value);
            }

            var errMsg = $"No section is loaded - can't find matching key [{keyName}]";
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
                value = GetConfigValue(keyName);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            var retVal = environmentConfiguration.DumpSection(Section.DumpAs.AsXml);

            return retVal;
        }

        /// <summary>
        /// Save the configuration to file.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <param name="dumpAs">
        /// The dump As.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool SaveToFile(string fileName, Section.DumpAs dumpAs = Section.DumpAs.AsXml)
        {
            try
            {
                environmentConfiguration.DumpSection(dumpAs, fileName);
            }
            catch (Exception)
            {
                return false;
            }

            return true; // TODO: better return value
        }

        /// <summary>
        /// The dump stf configuration.
        /// </summary>
        /// <param name="dumpFilename">
        /// The dump filename.
        /// </param>
        public void DumpStfConfiguration(string dumpFilename)
        {
            var dumpFilenameNoExtension = Path.ChangeExtension(dumpFilename, string.Empty);

            DumpStfConfigurationAsXml(dumpFilenameNoExtension + "xml");
            DumpStfConfigurationAsText(dumpFilenameNoExtension + "txt");
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

                                SectionAddSection(currentSection, section);
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
        /// Add a section to the current section collection - check for duplicate "add sections" - last "add section" wins.
        /// </summary>
        /// <param name="section">
        /// The current/destination section.
        /// </param>
        /// <param name="sectionToAdd">
        /// The source section to add.
        /// </param>
        private void SectionAddSection(Section section, Section sectionToAdd)
        {
            var sections = GetSections(sectionToAdd);
            var sectionName = sectionToAdd.SectionName;

            if (section.Sections.ContainsKey(sectionName))
            {
                section.Sections[sectionName] = sections;
                return;
            }

            section.Sections.Add(sectionName, sections);
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
                switch (reader.Name.ToLower())
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
                switch (reader.Name.ToLower())
                {
                    case "name":
                        newKey.KeyName = reader.Value;
                        break;
                    case "value":
                        newKey.KeyValue = reader.Value;
                        break;
                }
            }

            SectionAddKey(section, newKey);
        }

        /// <summary>
        /// The current section add key.
        /// </summary>
        /// <param name="section">
        /// The current section.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        private void SectionAddKey(Section section, Key key)
        {
            var keyName = key.KeyName;

            if (section.Keys.ContainsKey(keyName))
            {
                section.Keys[keyName] = key;
                return;
            }

            section.Keys.Add(keyName, key);
        }

        /// <summary>
        /// The is absolute configuration path.
        /// </summary>
        /// <param name="configPath">
        /// The config path.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool IsAbsoluteConfigurationPath(ref string configPath)
        {
            const string StartOfAbsolutePath = "Configuration.";

            if (string.IsNullOrEmpty(configPath))
            {
                return false;
            }

            if (!configPath.StartsWith(StartOfAbsolutePath, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            configPath = configPath.Replace(StartOfAbsolutePath, string.Empty);
            return true;
        }

        /// <summary>
        /// The dump stf configuration.
        /// </summary>
        /// <param name="dumpFilename">
        /// The configuration dump Filename.
        /// </param>
        private void DumpStfConfigurationAsXml(string dumpFilename)
        {
            var content = $"<body><xmp>{environmentConfiguration.DumpSection(Section.DumpAs.AsXml)}</xmp></body>";

            if (File.Exists(dumpFilename))
            {
                File.Delete(dumpFilename);
            }

            var xDoc = new XmlDocument();

            xDoc.LoadXml(content);
            xDoc.Save(dumpFilename);
        }

        /// <summary>
        /// The dump stf configuration as text.
        /// </summary>
        /// <param name="dumpFilename">
        /// The dump filename.
        /// </param>
        private void DumpStfConfigurationAsText(string dumpFilename)
        {
            var content = environmentConfiguration.DumpSection(Section.DumpAs.AsText);

            if (File.Exists(dumpFilename))
            {
                File.Delete(dumpFilename);
            }

            File.WriteAllText(dumpFilename, content);
        }
    }
}

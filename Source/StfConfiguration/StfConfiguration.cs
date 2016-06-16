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
                if (!currentlyLoadedSection.Sections.ContainsKey("Environments"))
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
                var parser = new Parser { EvaluateKeyValue = EvaluateKeyValue };

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
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool SaveToFile(string fileName)
        {
            var retVal = currentlyLoadedSection.DumpSection(Section.DumpAs.AsXml, fileName);

            return true; // TODO: better retrun value
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

            section.Keys.Add(newKey.KeyName, newKey);
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
    }
}

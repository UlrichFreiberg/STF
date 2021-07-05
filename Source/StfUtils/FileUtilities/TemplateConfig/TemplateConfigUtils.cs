// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemplateConfigUtils.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the TemplateConfigUtils type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.FileUtilities.TemplateConfig
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// The template config utils.
    /// </summary>
    public class TemplateConfigUtils
    {
        /// <summary>
        /// The steps.
        /// </summary>
        private StepInfo[] steps;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateConfigUtils"/> class.
        /// </summary>
        /// <param name="rootFolder">
        /// The root folder.
        /// </param>
        /// <param name="templateFileFilter">
        /// The template file filter.
        /// </param>
        /// <param name="configFileFilter">
        /// The config file filter.
        /// </param>
        public TemplateConfigUtils(
            string rootFolder,
            string templateFileFilter = "Template.txt",
            string configFileFilter = "Config.txt")
        {
            RootFolder = rootFolder;
            TemplateFileFilter = templateFileFilter;
            ConfigFileFilter = configFileFilter;
            InitFileArrays();
        }

        /// <summary>
        /// Gets or sets the steps.
        /// </summary>
        public int NumberOfSteps { get; set; }

        /// <summary>
        /// Gets or sets the template file filter.
        /// </summary>
        public string TemplateFileFilter { get; set; }

        /// <summary>
        /// Gets or sets the config file filter.
        /// </summary>
        public string ConfigFileFilter { get; set; }

        /// <summary>
        /// Gets or sets the root folder.
        /// </summary>
        public string RootFolder { get; set; }

        /// <summary>
        /// The init file arrays.
        /// </summary>
        private void InitFileArrays()
        {
            var templateFilesBasename = Path.GetFileNameWithoutExtension(TemplateFileFilter);
            var templateFilesExtension = Path.GetExtension(TemplateFileFilter);
            var templateFilesWildcard = $"{templateFilesBasename}*{templateFilesExtension}";
            var configFilesBasename = Path.GetFileNameWithoutExtension(ConfigFileFilter);
            var configFilesExtension = Path.GetExtension(ConfigFileFilter);
            var configFilesWildcard = $"{configFilesBasename}*{configFilesExtension}";
            var templateFiles = Directory.GetFiles(RootFolder, templateFilesWildcard);
            var configFiles = Directory.GetFiles(RootFolder, configFilesWildcard);

            NumberOfSteps = Math.Max(templateFiles.Length, configFiles.Length);

            // we skip the first entry '0' to avoid lots of +/- 1 for step. Now step is the index into the array
            steps = new StepInfo[NumberOfSteps + 1];

            var templateFilePath = templateFiles.FirstOrDefault(p => Regex.IsMatch(p, $@"\\{templateFilesBasename}([^\d]|.)", RegexOptions.IgnoreCase));
            var configFilePath = configFiles.FirstOrDefault(p => Regex.IsMatch(p, $@"\\{configFilesBasename}[^\d]([^\d]|.)", RegexOptions.IgnoreCase));

            // TODO: Error handling for missing first template or config
            if (templateFilePath == null || configFilePath == null)
            {
                return;
            }

            steps[1] = new StepInfo(1, templateFilePath, configFilePath);

            var latestTemplatePath = steps[1].TemplateFilePath;
            var latestConfigPath = steps[1].ConfigFilePath;

            for (var step = 2; step <= NumberOfSteps; step++)
            {
                var templateCandidateRegExp = $@"{templateFilesBasename}{step}([^\d]|.)";
                var configCandidateRegExp = $@"{configFilesBasename}{step}([^\d]|.)";
                var templateCandidate = templateFiles.FirstOrDefault(p => Regex.IsMatch(p, templateCandidateRegExp));
                var configCandidate = configFiles.FirstOrDefault(p => Regex.IsMatch(p, configCandidateRegExp));

                // lets find some candidates, and insert the right one
                templateFilePath = templateCandidate ?? latestTemplatePath;
                configFilePath = configCandidate ?? latestConfigPath;
                steps[step] = new StepInfo(step, templateFilePath, configFilePath);

                // remember the latest inserted
                latestTemplatePath = steps[step].TemplateFilePath;
                latestConfigPath = steps[step].ConfigFilePath;
            }
        }
    }
}

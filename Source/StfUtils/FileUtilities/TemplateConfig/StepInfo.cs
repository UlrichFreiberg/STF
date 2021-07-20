// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StepInfo.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the StepInfo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.FileUtilities.TemplateConfig
{
    /// <summary>
    /// The step info.
    /// </summary>
    public class StepInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StepInfo"/> class.
        /// </summary>
        /// <param name="stepNumber">
        /// The step number.
        /// </param>
        /// <param name="configFilePath">
        /// The config file path.
        /// </param>
        /// <param name="templateFilePath">
        /// The template file path.
        /// </param>
        public StepInfo(int stepNumber, string configFilePath, string templateFilePath)
        {
            StepNumber = stepNumber;
            ConfigFilePath = configFilePath;
            TemplateFilePath = templateFilePath;
        }

        /// <summary>
        /// Gets or sets the step number.
        /// </summary>
        public int StepNumber { get; set; }

        /// <summary>
        /// Gets or sets the Template file path.
        /// </summary>
        public string TemplateFilePath { get; set; }

        /// <summary>
        /// Gets or sets the config file path.
        /// </summary>
        public string ConfigFilePath { get; set; }
    }
}
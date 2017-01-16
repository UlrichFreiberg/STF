// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfConfigurationAttribute.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the ConfigAttributes type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Mir.Stf
{
    /// <summary>
    /// The config info.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class StfTestDataAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StfTestDataAttribute"/> class.
        /// </summary>
        /// <param name="columnName">
        /// Name of the Test Data Column to use
        /// </param>
        public StfTestDataAttribute(string columnName)
        {
            ColumnName = columnName;
            DefaultValue = string.Empty;
        }

        public string ColumnName { get; set; }

        /// <summary>
        /// Gets or sets the default value.
        /// </summary>
        public string DefaultValue { get; set; }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfConfigurationAttributeExtensions.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The stf configuration attribute extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;

namespace Mir.Stf.Utilities
{
    /// <summary>
    /// The stf configuration attribute extensions.
    /// </summary>
    public static class StfConfigurationAttributeExtensions
    {
        /// <summary>
        /// The get default value.
        /// </summary>
        /// <param name="instance">
        /// The instance.
        /// </param>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetDefaultValue(this object instance, string propertyName)
        {
            var attrType = typeof(StfConfigurationAttribute);
            var property = instance.GetType().GetProperty(propertyName);

            return ((StfConfigurationAttribute)property.GetCustomAttributes(attrType, false).First()).DefaultValue;
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Reflection.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mir.Stf.Utilities
{
    /// <summary>
    /// The reflection.
    /// </summary>
    public class Reflection
    {
        /// <summary>
        /// The current field set.
        /// </summary>
        private object currentFieldSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Reflection"/> class.
        /// </summary>
        /// <param name="fieldSet">
        /// The field set.
        /// </param>
        public Reflection(object fieldSet)
        {
            this.currentFieldSet = fieldSet;
        }

        /// <summary>
        /// The get config properties from type.
        /// Later on perhaps also fields (var fields = t.GetFields();)
        /// </summary>
        /// <returns>
        /// The <see cref="Dictionary{TKey,TValue}"/>.
        /// </returns>
        internal Dictionary<string, ConfigInfo> GetConfigPropertiesFromType()
        {
            if (this.currentFieldSet == null)
            {
                return new Dictionary<string, ConfigInfo>();
            }

            var currentFieldSetype = currentFieldSet.GetType();
            var props = currentFieldSetype.GetProperties();
            var dict = new Dictionary<string, ConfigInfo>();

            foreach (var property in props)
            {
                var value = property.GetValue(this.currentFieldSet);
                var configAttributes = property.GetCustomAttributes<ConfigInfo>(true).FirstOrDefault();

                dict.Add(property.Name, configAttributes);
            }

            return dict;
        }

        /// <summary>
        /// The set config.
        /// </summary>
        /// <param name="userConfig">
        /// The user config.
        /// </param>
        /// <param name="stfConfiguration">
        /// The stf configuration.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        internal object SetConfig(Dictionary<string, ConfigInfo> userConfig, StfConfiguration stfConfiguration)
        {
            var t = this.currentFieldSet.GetType();

            foreach (var uc in userConfig)
            {
                var property = t.GetProperty(uc.Key);
                var currentValue = property.GetValue(currentFieldSet);
                var newValue = stfConfiguration.GetConfigValue(uc.Value.ConfigKeyPath, uc.Value.DefaultValue);
                property.SetValue(currentFieldSet, newValue);
            }

            return currentFieldSet;
        }
    }
}

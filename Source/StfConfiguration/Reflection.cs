// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Reflection.cs" company="Foobar">
//   2015
// </copyright>
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
        internal Dictionary<string, string> GetConfigPropertiesFromType()
        {
            if (this.currentFieldSet == null)
            {
                return new Dictionary<string, string>();
            }

            var t = this.currentFieldSet.GetType();
            var props = t.GetProperties(); 
            var dict = new Dictionary<string, string>();

            foreach (var property in props)
            {
                var value = property.GetValue(this.currentFieldSet);
                ConfigAttributes.ConfigInfo configAttributes = property.GetCustomAttributes<ConfigAttributes.ConfigInfo>(true).FirstOrDefault();

                // var propertyConfigInfo = GetConfigInfo(property);
                dict.Add(property.Name, configAttributes.ConfigKeyPath);
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
        internal object SetConfig(Dictionary<string, string> userConfig, StfConfiguration stfConfiguration)
        {
            var t = this.currentFieldSet.GetType();

            foreach (var uc in userConfig)
            {
                var property = t.GetProperty(uc.Key);
                var currentValue = property.GetValue(this.currentFieldSet);
                var newValue = stfConfiguration.GetKeyValue(uc.Value);
                property.SetValue(currentFieldSet, newValue);
            }

            return currentFieldSet;
        }
    }
}

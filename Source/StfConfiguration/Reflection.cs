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
    using System;
    using System.ComponentModel;

    /// <summary>
    /// The reflection.
    /// </summary>
    public class Reflection
    {
        /// <summary>
        /// The current field set.
        /// </summary>
        private readonly object currentFieldSet;

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
        internal Dictionary<string, StfConfigurationAttribute> GetConfigPropertiesFromType()
        {
            var retVal = new Dictionary<string, StfConfigurationAttribute>();

            if (currentFieldSet == null)
            {
                return retVal;
            }

            var currentFieldSetype = currentFieldSet.GetType();
            var props = currentFieldSetype.GetProperties();

            foreach (var property in props)
            {
                var configAttributes = property.GetCustomAttributes<StfConfigurationAttribute>(true).FirstOrDefault();

                if (configAttributes != null)
                {
                    retVal.Add(property.Name, configAttributes);
                }
            }

            return retVal;
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
        internal object SetConfig(Dictionary<string, StfConfigurationAttribute> userConfig, StfConfiguration stfConfiguration)
        {
            var type = currentFieldSet.GetType();

            if (userConfig == null)
            {
                return currentFieldSet;
            }

            foreach (var uc in userConfig)
            {
                var property = type.GetProperty(uc.Key);
                var newValue = stfConfiguration.GetConfigValue(uc.Value.ConfigKeyPath, uc.Value.DefaultValue);

                if (property.PropertyType == typeof(string))
                {
                    property.SetValue(currentFieldSet, newValue);
                    continue;
                }

                try
                {
                    var valueToSet = string.IsNullOrEmpty(newValue)
                                   ? TypeDescriptor.GetDefaultProperty(property.PropertyType)
                                   : TypeDescriptor.GetConverter(property.PropertyType).ConvertFromInvariantString(newValue);

                    property.SetValue(currentFieldSet, valueToSet);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return currentFieldSet;
        }
    }
}

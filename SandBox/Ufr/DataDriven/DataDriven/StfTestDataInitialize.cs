// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfTestDataInitialize.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the StfTestDataInitialize type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DataDriven
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Linq;

    /// <summary>
    /// The stf test data initialize.
    /// </summary>
    public class StfTestDataInitialize
    {
        /// <summary>
        /// The init object.
        /// </summary>
        /// <param name="dataRow">
        /// The DataRow from the MsTest TestContext class
        /// </param>
        /// <typeparam name="T">
        /// A class decribing testdata needed for one iteration
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T InitObject<T>(T testdataObject = null) where T : IStfTestData, new()
        {
            var retVal = new T();

            var properties = typeof(T).GetProperties();

            foreach (var row in dataRow.Table.Columns)
            {
                var propertyName = row.ToString();
                var property = properties.FirstOrDefault(pp => pp.Name == propertyName);

                // did we find the correspondig property in the filterClass?
                if (property != null)
                {
                    var propertyType = property.PropertyType;
                    try
                    {
                        var val = TypeDescriptor.GetConverter(propertyType).ConvertFromString(dataRow[propertyName].ToString());

                        property.SetValue(retVal, val);
                    }
                    catch (Exception ex)
                    {
                        // ignored
                    }
                }
            }

            return retVal;
        }

        public static T InitObject<T>(T testdataObject) where T : IStfTestData
        {
            
        }
    }
}

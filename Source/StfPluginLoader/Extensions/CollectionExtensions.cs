// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollectionExtensions.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The collection extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Practices.Unity.InterceptionExtension;

namespace Mir.Stf.Utilities.Extensions
{
    /// <summary>
    /// The collection extensions.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// The to array.
        /// </summary>
        /// <param name="parameterCollection">
        /// The parameter collection.
        /// </param>
        /// <returns>
        /// The parameter collection as an array
        /// </returns>
        public static object[] ToArray(this IParameterCollection parameterCollection)
        {
            var array = new object[parameterCollection.Count];
            for (var i = 0; i < parameterCollection.Count; i++)
            {
                array[i] = parameterCollection[i];
            }

            return array;
        }
    }
}

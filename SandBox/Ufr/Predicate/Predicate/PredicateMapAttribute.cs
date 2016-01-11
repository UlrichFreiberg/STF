// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PredicateMapAttribute.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Predicate
{
    public enum CompareMethod
    {
        /// <summary>
        /// </summary>
        Equals,

        /// <summary>
        /// </summary>
        LessThan,
        GreaterThan,
        Contains,
        StartWith,
        EndsWith
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class PredicateMapAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets what properties that should used, when filtering
        /// </summary>
        public string EntityProperty { get; set; }

        public string CompareMethod { get; set; }

        public PredicateMapAttribute(string entityProperty)
        {
            EntityProperty = entityProperty;
        }
    }
}

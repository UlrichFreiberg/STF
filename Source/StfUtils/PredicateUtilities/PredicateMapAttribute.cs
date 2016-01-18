// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PredicateMapAttribute.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.PredicateUtilities
{
    using System;

    /// <summary>
    /// Enum to describe the operator of a predicate expression
    /// </summary>
    public enum CompareMethod
    {
        /// <summary>
        /// Equals operator
        /// </summary>
        Equals,

        /// <summary>
        /// LessThan operator
        /// </summary>
        LessThan,

        /// <summary>
        /// GreaterThan operator
        /// </summary>
        GreaterThan,

        /// <summary>
        /// String Contains operator
        /// </summary>
        Contains,

        /// <summary>
        /// String StartWith operator
        /// </summary>
        StartWith,

        /// <summary>
        /// String EndsWith operator
        /// </summary>
        EndsWith
    }

    /// <summary>
    /// An attribute used to map into another entity. Used when a base class is not a nullable type
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PredicateMapAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PredicateMapAttribute"/> class. 
        /// </summary>
        /// <param name="entityProperty">
        /// Name of the non-nullable property that this property maps to
        /// </param>
        public PredicateMapAttribute(string entityProperty)
        {
            EntityProperty = entityProperty;
        }

        /// <summary>
        /// Gets or sets what properties that should used, when filtering
        /// </summary>
        public string EntityProperty { get; set; }

        /// <summary>
        /// Gets or sets how should we compare values - When mapped 
        /// </summary>
        public string CompareMethod { get; set; }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Predicate.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Predicate
{
    using System;
    using System.Linq;
    using System.Linq.Dynamic;

    /// <summary>
    /// Utility class to handle predicates towards a list
    /// </summary>
    public class Predicate
    {
        /// <summary>
        /// </summary>
        /// <param name="predicateList">
        /// </param>
        public Predicate(string predicateList = null)
        {
            PredicateList = predicateList;
        }

        /// <summary>
        /// Gets or sets the full predicate
        /// </summary>
        public string PredicateList { get; set; }

        /// <summary>
        /// Override of ToString
        /// </summary>
        /// <returns>
        /// The predicate list
        /// </returns>
        public override string ToString()
        {
            return PredicateList;
        }

        /// <summary>
        /// Filter the list according to the filter object
        /// </summary>
        /// <param name="listOfEntities">
        /// The list to filter
        /// </param>
        /// <param name="filterClass">
        /// The object that contains the filter - one property per predicate
        /// </param>
        /// <typeparam name="TEntity">
        /// The Type for the entities in the list to filter
        /// </typeparam>
        /// <typeparam name="TFilter">
        /// The object that contains the filter
        /// </typeparam>
        /// <returns>
        /// The filtered list
        /// </returns>
        public List<TEntity> FilterList<TEntity, TFilter>(List<TEntity> listOfEntities, TFilter filterClass)
            where TEntity : new() 
            where TFilter : TEntity
        {
            //// foreach property in filterClass
            ////   if property is not null
            ////     filter according to property

            var retVal = listOfEntities;
            var properties = typeof(TFilter).GetProperties();

            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(filterClass);
                var propertyType = property.PropertyType;
                var propertyName = property.Name;
                var nullable = !propertyType.IsValueType || (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>));

                if (propertyValue == null || !nullable)
                {
                    continue;
                }

                // check for an attribute override...
                foreach (var attribute in property.GetCustomAttributes(true))
                {
                    var mapAttribute = attribute as PredicateMapAttribute;
                    if (mapAttribute != null)
                    {
                        var entityPropertyAttribute = mapAttribute;

                        propertyName = entityPropertyAttribute.EntityProperty;
                        break;
                    }
                }

                var predicate = string.Format("{0} = @0", propertyName);

                try
                {
                    retVal = retVal.Where(predicate, propertyValue).ToList();
                }
                catch (Exception ex)
                {
                    ;
                }
            }

            return retVal;
        }

        /// <summary>
        /// Returns a Filter initialized accordingly to a string predicate 
        /// </summary>
        /// <param name="predicate">
        /// The predicate
        /// </param>
        /// <typeparam name="TFilter">
        /// The filter class
        /// </typeparam>
        /// <returns>
        /// A initialized filter
        /// </returns>
        public TFilter ParsePredicate<TFilter>(string predicate) where TFilter : new()
        {
            //// foreach predicate in the predicate list
            ////     find the property in filterClass
            ////     set the filtvalue to the predicate value part
             
            // TODO: Implement:-)
            return new TFilter();
        }

        /// <summary>
        /// Enumerate all the predicate parts of the overall predicate
        /// </summary>
        /// <returns>
        /// A predicate part
        /// </returns>
        public IEnumerable<PredicatePart> Predicates()
        {
            if (!string.IsNullOrEmpty(PredicateList))
            {
                var predicates = PredicateList.Split(';');

                foreach (var predicate in predicates)
                {
                    var retVal = new PredicatePart(predicate);

                    yield return retVal;
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="filter">
        /// </param>
        /// <typeparam name="TFilter">
        /// </typeparam>
        /// <returns>
        /// </returns>
        public string GeneratePredicate<TFilter>(TFilter filter)
        {
            //// foreach nullable property with a value in filter 
            ////     Generate a predicate part "LHS = RHS"

            // TODO: Implement:-)
            return "NeedsToBeImplemented";            
        }
    }
}

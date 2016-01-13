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
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;

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
        ///     foreach predicate in the predicate list
        ///         find the property in filterClass
        ///             set the filtvalue to the predicate value part
        /// </summary>
        /// <param name="predicateList">
        /// The list of predicates
        /// </param>
        /// <typeparam name="TFilter">
        /// The filter class
        /// </typeparam>
        /// <returns>
        /// A initialized filter
        /// </returns>
        public TFilter ParsePredicate<TFilter>(string predicateList = null) where TFilter : new()
        {
            var retVal = new TFilter();
            var properties = typeof(TFilter).GetProperties();

            if (!string.IsNullOrEmpty(predicateList))
            {
                PredicateList = predicateList;
            }

            foreach (var predicatePart in Predicates())
            {
                var propertyName = predicatePart.Quantifier;
                var property = properties.FirstOrDefault(pp => pp.Name == propertyName);

                // did we find the correspondig property in the filterClass?
                if (property != null)
                {
                    var propertyType = property.PropertyType;
                    var nullable = !propertyType.IsValueType
                                   || (propertyType.IsGenericType
                                       && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>));

                    // for a property to be a filter property it needs to be nullable
                    if (nullable)
                    {
                        SetPropertyValue(propertyType, predicatePart.Value, property, retVal);
                        continue;
                    }
                }

                // hmm, no (nullable) filter property found, 
                // then another property is pointing to a non-nullable property - anotated with PredicateMapAttribute
                foreach (var prop in properties)
                {
                    // check for an attribute override...
                    if (prop.GetCustomAttributes(true)
                        .OfType<PredicateMapAttribute>()
                        .Select(entityPropertyAttribute => entityPropertyAttribute.EntityProperty)
                            .Any(mappedPropertyName => mappedPropertyName == predicatePart.Quantifier))
                    {
                        SetPropertyValue(prop.PropertyType, predicatePart.Value, prop, retVal);
                        break;
                    }
                }
            }

            return retVal;
        }

        /// <summary>
        /// </summary>
        /// <param name="propertyType">
        /// </param>
        /// <param name="predicatePart">
        /// </param>
        /// <param name="property">
        /// </param>
        /// <param name="retVal">
        /// </param>
        /// <typeparam name="TFilter">
        /// </typeparam>
        private void SetPropertyValue<TFilter>(
            Type propertyType,
            string value,
            PropertyInfo property,
            TFilter retVal) where TFilter : new()
        {
            // Read more here http://stackoverflow.com/questions/2961656/generic-tryparse
            try
            {
                var val = TypeDescriptor.GetConverter(propertyType).ConvertFromString(value);

                property.SetValue(retVal, val);
            }
            catch
            {
                ;
            }
        }

        /// <summary>
        /// Enumerate all the predicate parts of the overall predicate
        /// </summary>
        /// <param name="predicateList">
        /// List of predicates
        /// </param>
        /// <returns>
        /// A predicate part
        /// </returns>
        public IEnumerable<PredicatePart> Predicates(string predicateList = null)
        {
            if (!string.IsNullOrEmpty(predicateList))
            {
                PredicateList = predicateList;
            }

            if (string.IsNullOrEmpty(PredicateList))
            {
                yield break;
            }

            var predicates = PredicateList.Split(';');

            foreach (var retVal in predicates.Select(pred => new PredicatePart(pred)))
            {
                yield return retVal;
            }
        }

        /// <summary>
        ///     Generate the predicate from the given filter class.
        ///         foreach nullable property with a value in filter 
        ///             Generate a predicate part "LHS = RHS"
        /// </summary>
        /// <param name="filter">
        /// </param>
        /// <typeparam name="TFilter">
        /// </typeparam>
        /// <returns>
        /// </returns>
        public string GeneratePredicate<TFilter>(TFilter filter)
        {
            // TODO: Implement:-)
            return "NeedsToBeImplemented";
        }

        /// <summary>
        /// </summary>
        /// <param name="filename">
        /// </param>
        /// <returns>
        /// </returns>
        public string ReadPredicateFromFile(string filename)
        {
            var content = File.ReadAllText(filename);

            // get rid of comments
            var retVal = Regex.Replace(content, "'.*", string.Empty);

            // remove all newlines
            retVal = Regex.Replace(retVal, @"\s*[\r\n]+", ";", RegexOptions.Multiline);

            // remove empty predicate
            retVal = Regex.Replace(retVal, @";;+", ";");
            retVal = Regex.Replace(retVal, @"^;", string.Empty);
            retVal = Regex.Replace(retVal, @";$", string.Empty);

            PredicateList = retVal;
            return PredicateList;
        }
    }
}

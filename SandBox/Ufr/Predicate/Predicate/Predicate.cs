using System.Collections.Generic;

namespace Predicate
{
    using System;
    using System.Linq;
    using System.Linq.Dynamic;

    public class Predicate
    {
        public string PredicateList { get; set; }

        public override string ToString()
        {
            return PredicateList;
        }

        public List<TEntity> FilterList<TEntity, TFilter>(List<TEntity> listOfEntities, TFilter filterClass)
            where TEntity : new() where TFilter : TEntity
        {
            // foreach property in filterClass
            //   if property is not null
            //     filter according to property

            var retVal = listOfEntities;
            var properties = typeof(TFilter).GetProperties();

            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(filterClass);
                var propertyType = property.PropertyType;
                var nullable = !propertyType.IsValueType
                               || propertyType.IsGenericType
                               && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>);

                if (propertyValue == null || !nullable)
                {
                    continue;
                }

                var propertyName = property.Name;
    
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
    }
}

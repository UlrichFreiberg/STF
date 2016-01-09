using System;

namespace Predicate
{
    [AttributeUsage(AttributeTargets.All)]
    public class PredicateMapAttribute : Attribute
    {
        public string EntityProperty { get; set; }

        public PredicateMapAttribute(string entityProperty)
        {
            EntityProperty = entityProperty;
        }
    }
}

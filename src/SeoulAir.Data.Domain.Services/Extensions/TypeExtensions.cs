using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SeoulAir.Data.Domain.Services.Extensions
{
    public static class TypeExtensions
    {
        public static IEnumerable<PropertyInfo> GetPublicProperties(this Type type)
        {
            if (!type.IsInterface)
                return type.GetProperties();

            return (new Type[] { type })
                   .Concat(type.GetInterfaces())
                   .SelectMany(i => i.GetProperties());
        }

        public static bool HasPublicProperty(this Type type, string propertyName)
        {
            var tempType = type;
            var nestedLevel = propertyName.Split('.').ToList();
            while(nestedLevel.Count > 1)
            {
                tempType = tempType.GetProperty(nestedLevel[0]).PropertyType;
                nestedLevel.RemoveAt(0);
            }
            return tempType.GetPublicProperties().Any(property => property.Name == nestedLevel[0]);
        }
    }
}

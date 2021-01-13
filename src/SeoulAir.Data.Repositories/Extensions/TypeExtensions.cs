using System;
using System.Linq;
using System.Reflection;

namespace SeoulAir.Data.Repositories.Extensions
{
    public static class TypeExtensions
    {
        public static PropertyInfo GetNestedProperty(this Type type, string propertyName)
        {
            var tempType = type;
            var nestedLevel = propertyName.Split('.').ToList();
            while (nestedLevel.Count > 1)
            {
                tempType = tempType?.GetProperty(nestedLevel[0])?.PropertyType;
                nestedLevel.RemoveAt(0);
            }
            return tempType?.GetProperty(nestedLevel[0]);
        }
    }
}

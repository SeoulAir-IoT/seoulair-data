using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace SeoulAir.Data.Domain.Services.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum enumeration)
        {
            var attribute = enumeration.GetType()
                .GetMember(enumeration.ToString())
                .FirstOrDefault(member => member.MemberType == MemberTypes.Field)
                ?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() as DescriptionAttribute;

            return attribute?.Description ?? enumeration.ToString();
        }
    }
}

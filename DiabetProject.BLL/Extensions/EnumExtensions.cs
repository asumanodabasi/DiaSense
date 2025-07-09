using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DiabetProject.BLL.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()?
                            .GetName() ?? enumValue.ToString();
        }
        public static TEnum ParseEnum<TEnum>(object value) where TEnum : struct, Enum
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (Enum.TryParse(typeof(TEnum), value.ToString(), out var enumValue))
            {
                return (TEnum)enumValue;
            }

            throw new ArgumentException($"Invalid value '{value}' for enum '{typeof(TEnum).Name}'");
        }


    }
}

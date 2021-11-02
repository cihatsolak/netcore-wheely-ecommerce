using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Wheely.Core.Utilities
{
    public static class EnumHelper
    {
        public static string StringValueOf(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            if (fi.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }

        public static int ToInt(this Enum value)
        {
            return Convert.ToInt32(value);
        }

        public static TEnum ToEnum<TEnum>(this int value) where TEnum : Enum
        {
            return (TEnum)Enum.ToObject(typeof(TEnum), value);
        }
    }
}

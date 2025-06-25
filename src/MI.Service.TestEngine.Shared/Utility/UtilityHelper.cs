using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;

namespace MI.Service.TestEngine.Shared.Utility;

/// <summary>Utility Helper Class</summary>
public static class UtilityHelper
{
    /// <summary>Gets the enum description.</summary>
    /// <param name="value">The value.</param>
    /// <returns>
    ///   <para>Get Enum Description</para>
    /// </returns>
    public static string GetEnumDescription(Enum value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());
        DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
        if (attributes != null && attributes.Any())
        {
            return attributes.First().Description;
        }
        return value.ToString();
    }

    /// <summary>
    /// Gets the enum member.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static string GetEnumMemberValue(Enum value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());
        EnumMemberAttribute[] attributes = fi.GetCustomAttributes(typeof(EnumMemberAttribute), false) as EnumMemberAttribute[];
        if (attributes != null && attributes.Any())
        {
            return attributes.First().Value;
        }
        return value.ToString().Trim();
    }
}
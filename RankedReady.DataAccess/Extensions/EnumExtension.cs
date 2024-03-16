namespace RankedReadyApi.DataAccess.Extensions;

public static class EnumExtension
{
    public static TEnum ToEnum<TEnum>(this string value) where TEnum : struct, IConvertible
    {
        Type enumType = typeof(TEnum);
        return !enumType.IsEnum
            ? throw new Exception("The type could not be determined.")
            : Enum.TryParse(value, true, out TEnum val) ? val : default;
    }

    public static List<TEnum> ToEnums<TEnum>(this List<string> values) where TEnum : struct, IConvertible
    {
        var enums = new List<TEnum>();
        foreach (var value in values)
        {
            var enm = Enum.GetValues(typeof(TEnum)).Cast<TEnum>()
                .FirstOrDefault(enumValue => enumValue.ToString() == value);
            enums.Add(enm);
        }
        return enums;
    }
}

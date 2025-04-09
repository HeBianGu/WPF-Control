namespace H.Extensions.FontIcon;

public static class IconSegoeProvider
{
    public static IEnumerable<IconSegoe> GetIconSegoes()
    {
        return GetIconSegoes(0xE000, 0xEFFF);
    }

    public static IEnumerable<IconSegoe> GetIconSegoes(int from, int to)
    {
        for (int codePoint = from; codePoint <= to; codePoint++)
        {
            string unicodeChar = char.ConvertFromUtf32(codePoint);
            yield return new IconSegoe() { CodePoint = codePoint, Key = codePoint.ToKey(), Value = unicodeChar, CodeKey = codePoint.ToCodeKey() };
        }
    }

    private static string ToKey(this int v)
    {
        return $"&#x{v:X};";
    }

    public static string ToCodeKey(this int v)
    {
        return $"\\x{v:X}";
    }
    public static string ToIconSegoe(this int v)
    {
        return v.ToString().ToIconSegoe();
    }
    public static string ToIconSegoe(this string v)
    {
        string unicodeString = "E" + v;
        int unicodeValue = Convert.ToInt32(unicodeString, 16);
        string unicodeChar = char.ConvertFromUtf32(unicodeValue);
        return unicodeChar;
    }
}

// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Text;

namespace H.Extensions.Encryption.String;

public static class Base64Extension
{
    /// <summary>
    /// Base64编码
    /// </summary>
    /// <param name="code_type"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    public static string EncryptBase64(this string code)
    {
        string encode = "";
        byte[] bytes = Encoding.UTF8.GetBytes(code);
        try
        {
            encode = Convert.ToBase64String(bytes);
        }
        catch
        {
            encode = code;
        }
        return encode;
    }
    /// <summary>
    /// Base64解码
    /// </summary>
    /// <param name="code_type"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    public static string DecryptBase64(this string code)
    {
        string decode = "";
        byte[] bytes = Convert.FromBase64String(code);
        try
        {
            decode = Encoding.UTF8.GetString(bytes);
        }
        catch
        {
            decode = code;
        }
        return decode;
    }
    /// <summary>
}

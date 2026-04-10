// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Diagnostics;

namespace H.Extensions.Encryption.String;

public static class DebugExtension
{
    public static async Task<string> WriteLine(this string input, ConsoleColor consoleColor = ConsoleColor.Green)
    {
        if (string.IsNullOrEmpty(input))
            return input;
        Console.ForegroundColor = consoleColor;
        Console.WriteLine(input);
        Console.ResetColor();
        Debug.WriteLine(input);
        return await input;
    }
    public static async Task<string> Write(this string input, ConsoleColor consoleColor = ConsoleColor.Green)
    {
        if (string.IsNullOrEmpty(input))
            return input;
        Console.ForegroundColor = consoleColor;
        Console.Write(input);
        Console.ResetColor();
        Debug.Write(input);
        return await input;
    }
}

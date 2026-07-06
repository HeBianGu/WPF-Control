// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagrams.OpenCV.Extensions;

public static class ColorProvider
{
    public static IEnumerable<Color> GetColors()
    {
        yield return Colors.Red;
        yield return Colors.Blue;
        yield return Colors.Gray;
        yield return Colors.Orange;
        yield return Colors.DeepPink;
        yield return Colors.Green;
        yield return Colors.Purple;
        yield return Colors.Yellow;
        yield return Colors.Brown;
        yield return Colors.SkyBlue;
    }

    public static Color GetRandomColor()
    {
        List<Color> colors = GetColors().ToList();
        Random random = new Random();
        int index = random.Next(colors.Count);
        return colors[index];
    }
}


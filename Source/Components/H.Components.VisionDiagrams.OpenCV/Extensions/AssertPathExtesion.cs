// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagrams.OpenCV.Extensions;

public static class AssertPathExtesion
{
    public static string ToDataPath(this string dataPath)
    {
        return string.IsNullOrEmpty(dataPath) ? null : dataPath;
    }

    public static string ToAssetsPath(this string dataPath)
    {
        return string.IsNullOrEmpty(dataPath) ? null : Path.Combine("Assets", dataPath);
    }

    public static string ToOnnxPath(this string dataPath)
    {
        return string.IsNullOrEmpty(dataPath)
         ? null
         : Path.Combine("Assets", "Onnx", dataPath);
    }
}


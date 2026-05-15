// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.Visions.OpenCV.Base;
/// <summary>
/// 翻转校正结果
/// </summary>
/// <param name="RotatedRectResult"></param>
/// <param name="IsFlippedHorizontal"></param>
/// <param name="IsFlippedVertical"></param>
public readonly record struct RotatedRectFlipResult(
    RotatedRect RotatedRectResult,
    bool IsFlippedHorizontal,
    bool IsFlippedVertical);

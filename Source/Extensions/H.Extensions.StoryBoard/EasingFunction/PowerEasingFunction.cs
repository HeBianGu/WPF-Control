// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows;
using System.Windows.Media.Animation;

namespace H.Extensions.StoryBoard.EasingFunction;

/// <summary> 指定次幂 </summary>
public class PowerEasingFunction : EasingFunctionBase
{
    public PowerEasingFunction()
        : base()
    {

    }

    public int Pow { get; set; } = 7;

    protected override double EaseInCore(double normalizedTime)
    {
        return Math.Pow(normalizedTime, this.Pow);
    }

    protected override Freezable CreateInstanceCore()
    {
        return new PowerEasingFunction();
    }
}

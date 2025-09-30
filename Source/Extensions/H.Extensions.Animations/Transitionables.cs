// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Animations;

public static class Transitionables
{
    public static ITransitionable Opacity = new OpacityTransitionable();
    public static ITransitionable Scale = new ScaleTransitionable();
    public static ITransitionable Rotate = new RotateTransitionable();
    public static ITransitionable XTranslate = new TranslateTransitionable();
    public static ITransitionable YTranslate = new TranslateTransitionable()
    {
        From = new System.Windows.Point(0, -200),
        To = new System.Windows.Point(0, 200)
    };

    public static ITransitionable OpacityScale = new GroupTransitionable(Scale, Opacity)
    {

    };
}

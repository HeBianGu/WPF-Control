// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Media.Animation;

namespace H.Extensions.StoryBoard;

public static class StoryboardFactory
{
    public static Storyboard Create()
    {
        Storyboard storyboard = new Storyboard();
        Timeline.SetDesiredFrameRate(storyboard, StoryboardSetting.DesiredFrameRate);
        return storyboard;
    }

    public static DoubleAnimation CreateDoubleAnimation()
    {
        DoubleAnimation storyboard = new DoubleAnimation();
        Timeline.SetDesiredFrameRate(storyboard, StoryboardSetting.DesiredFrameRate);
        return storyboard;
    }
}

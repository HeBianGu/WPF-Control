// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Color;

namespace H.Controls.ColorBox
{
    public class ColorSourceExtension : GetDepthColorsExtension
    {
        public ColorSourceExtension()
        {
            this.Depthes = ColorBoxSetting.Instance.Depthes;
            this.From = ColorBoxSetting.Instance.From;
            this.To = ColorBoxSetting.Instance.To;
            this.Step = ColorBoxSetting.Instance.Step;
        }
    }
}

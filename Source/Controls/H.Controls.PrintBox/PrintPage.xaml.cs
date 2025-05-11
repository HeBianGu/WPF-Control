// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Controls;

namespace H.Controls.PrintBox
{
    [TemplatePart(Name = "PART_Host")]
    public class PrintPage : ContentControl
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(PrintPage), "S.PrintPage.Default");

        static PrintPage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PrintPage), new FrameworkPropertyMetadata(typeof(PrintPage)));
        }

        Border _border = null;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _border = this.Template.FindName("PART_Host", this) as Border;
        }

        public UIElement GetElement()
        {
            return this._border.Child;
        }
    }
}

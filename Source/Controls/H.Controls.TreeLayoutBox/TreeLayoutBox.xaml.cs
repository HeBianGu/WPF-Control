// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows;
using System.Windows.Controls;

namespace H.Controls.TreeLayoutBox
{
    public partial class TreeLayoutBox : TreeView
    {
        static TreeLayoutBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TreeLayoutBox), new FrameworkPropertyMetadata(typeof(TreeLayoutBox)));

        }
    }

    public partial class TreeLayoutBox
    {
        public static ComponentResourceKey VerticalKey => new ComponentResourceKey(typeof(TreeLayoutBox), "S.TreeLayoutBox.Vertical");

        public static ComponentResourceKey HorizontalKey => new ComponentResourceKey(typeof(TreeLayoutBox), "S.TreeLayoutBox.Horizontal");
    }
}

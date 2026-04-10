// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Presenters.Design.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Presenters.Design;
public class DesignBox : ContentControl
{
    public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(DesignBox), "S.DesignBox.Default");

    static DesignBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(DesignBox), new FrameworkPropertyMetadata(typeof(DesignBox)));
    }

    public bool UseDesign
    {
        get { return (bool)GetValue(UseDesignProperty); }
        set { SetValue(UseDesignProperty, value); }
    }

    public static readonly DependencyProperty UseDesignProperty =
        DependencyProperty.Register("UseDesign", typeof(bool), typeof(DesignBox), new FrameworkPropertyMetadata(true, (d, e) =>
        {
            DesignBox control = d as DesignBox;

            if (control == null) return;

            if (e.OldValue is bool o)
            {

            }

            if (e.NewValue is bool n)
            {

            }

        }));

}

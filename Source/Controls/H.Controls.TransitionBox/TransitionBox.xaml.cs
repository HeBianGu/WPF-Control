// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace H.Controls.TransitionBox
{
    public class TransitionBox : Control
    {
        static TransitionBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TransitionBox), new FrameworkPropertyMetadata(typeof(TransitionBox)));
        }
    }

}

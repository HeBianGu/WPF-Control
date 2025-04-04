﻿using H.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace H.Styles.Default
{
    //https://learn.microsoft.com/zh-cn/windows/apps/design/style/segoe-ui-symbol-font?WT.mc_id=MVP_380318
    public class FontIconTextBlock : TextBlock
    {
        static FontIconTextBlock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FontIconTextBlock), new FrameworkPropertyMetadata(typeof(FontIconTextBlock)));
        }
    }
}

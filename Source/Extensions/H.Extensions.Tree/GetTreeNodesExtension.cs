// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.ValueConverter;
using System.Globalization;
using System.Windows;

namespace H.Extensions.Tree;

public class GetTreeNodesExtension : MarkupValueConverterBase
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is ITree tree)
            return tree.GetTreeNodes();
        if (value is IEnumerable<ITree> trees)
            return trees.SelectMany(l => l.GetTreeNodes());
        return DependencyProperty.UnsetValue;
    }
}

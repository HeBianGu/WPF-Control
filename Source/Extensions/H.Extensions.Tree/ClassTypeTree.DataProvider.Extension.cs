// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Extensions.Mvvm.ViewModels.Tree;
global using System.Windows.Markup;

namespace H.Extensions.Tree;

public class ClassTypeTreeDataProviderExtension : MarkupExtension
{
    public Type Type { get; set; }
    public bool IsRecursion { get; set; } = false;
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        ClassTypeTree tree = new ClassTypeTree(this.Type);
        System.Collections.Generic.IEnumerable<ITreeNode> result = tree.GetTreeNodes(this.IsRecursion);
        return result;
    }
}

// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.MarkupExtension;

public class GetRangeExtension : System.Windows.Markup.MarkupExtension
{
    public int Start { get; set; }

    public int Count { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Enumerable.Range(this.Start, this.Count).ToList();
    }
}

public abstract class GetEnumerableExtension<T> : System.Windows.Markup.MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this.GetValues();
    }

    protected abstract IEnumerable<T> GetValues();
}

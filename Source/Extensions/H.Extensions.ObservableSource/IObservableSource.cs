// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.ObservableSource;

public interface IObservableSource
{
    int MaxValue { get; set; }
    int MinValue { get; set; }
    int PageCount { get; set; }
    int PageIndex { get; set; }
    int Total { get; set; }
    int TotalPage { get; set; }
    int Count { get; }
    int SelectedIndex { get; set; }
}
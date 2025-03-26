// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

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
// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using H.Common.Interfaces.Where;
global using System.Collections.ObjectModel;

namespace H.Extensions.ObservableSource;

public interface IObservableSource<T> : IObservableSource
{
    Predicate<T> Fileter { get; set; }
    T SelectedItem { get; set; }
    ObservableCollection<T> Source { get; set; }
    ObservableCollection<T> FilterSource { get; set; }
    void Add(params T[] value);
    void Clear();
    void Load(IEnumerable<T> source);
    void RefreshPage(Action after = null);
    void Remove(params T[] value);
    void RemoveAll(Func<T, bool> predicate);
    void Sort<TKey>(Func<T, TKey> keySelector, bool isdesc = false);
    IEnumerable<T> Where(Func<T, bool> predicate);
    T FirstOrDefault(Func<T, bool> predicate);
    void Foreach(Action<T> predicate);
    bool Any(Func<T, bool> predicate);
    IEnumerable<TResult> Select<TResult>(Func<T, TResult> predicate);

    void Next();
    void Previous();
    IFilterable Filter1 { get; set; }
    IFilterable Filter2 { get; set; }
    IFilterable Filter3 { get; set; }
    IFilterable Filter4 { get; set; }
    IFilterable Filter5 { get; set; }
    IFilterable Filter6 { get; set; }
    IFilterable Filter7 { get; set; }
    IFilterable Filter8 { get; set; }
    IFilterable Filter9 { get; set; }
}

// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using H.Providers.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace H.Extensions.ViewModel
{
    public interface IObservableSource<T>
    {
        Predicate<T> Fileter { get; set; }
        int Count { get; }
        int MaxValue { get; set; }
        int MinValue { get; set; }
        int PageCount { get; set; }
        int PageIndex { get; set; }
        int SelectedIndex { get; set; }
        T SelectedItem { get; set; }
        ObservableCollection<T> Source { get; set; }
        ObservableCollection<T> FilterSource { get; set; }
        int Total { get; set; }
        int TotalPage { get; set; }
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
        IDisplayFilter Filter1 { get; set; }
        IFilter Filter2 { get; set; }
        IFilter Filter3 { get; set; }
        IFilter Filter4 { get; set; }
        IFilter Filter5 { get; set; }
    }


}

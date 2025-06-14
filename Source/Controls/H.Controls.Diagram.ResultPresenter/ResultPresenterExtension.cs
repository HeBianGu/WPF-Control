// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Controls.Diagram.Presenter.NodeDatas.Base;
global using H.Mvvm.ViewModels.Base;

namespace H.Controls.Diagram.ResultPresenter.ResultPresenters;

public static class ResultPresenterExtension
{
    //public static IResultPresenter ToValueResultPresenter<T>(this T value, Action<ValueResultPresenterItem<T>> action = null)
    //{
    //    return value.ToValueResultPresenterItem(action).ToDataGridViewResultData();
    //}

    public static IResultPresenter ToResultPresenter<T>(this T value)
    {
        return new ValueResultPresenter<T>(value);
    }

    private static ValueResultPresenterItem<T> ToValueResultPresenterItem<T>(this T value, Action<ValueResultPresenterItem<T>> action = null)
    {
        var r = new ValueResultPresenterItem<T>() { Value = value };
        action?.Invoke(r);
        return r;
    }

    private static IEnumerable<ValueResultPresenterItem<string>> ToValueResultPresenterItems<T>(this IEnumerable<T> values, Func<T, ValueResultPresenterItem<string>> selector = null)
    {
        foreach (var value in values)
        {
            yield return selector?.Invoke(value);
        }
    }

    private static IEnumerable<ValueResultPresenterItem<T>> ToValueResultPresenterItems<T>(this IEnumerable<T> values, Func<T, ValueResultPresenterItem<T>> selector = null)
    {
        foreach (var value in values)
        {
            yield return selector?.Invoke(value);
        }
    }

    private static IEnumerable<ValueResultPresenterItem<T>> ToValueResultPresenterItems<T>(this IEnumerable<T> values, Action<T, ValueResultPresenterItem<T>> action = null)
    {
        foreach (var value in values)
        {
            ValueResultPresenterItem<T> item = new ValueResultPresenterItem<T>(value);
            action?.Invoke(value, item);
            yield return item;
        }
    }

    private static IResultPresenter ToDataGridResultPresenter<T>(this IEnumerable<T> values, Action<T, ValueResultPresenterItem<T>> action = null)
    {
        return values.ToValueResultPresenterItems(action).ToDataGridResultPresenter();
    }

    private static IResultPresenter ToDataGridResultPresenter<T>(this IEnumerable<T> values, Func<T, ValueResultPresenterItem<T>> selector = null)
    {
        return values.ToValueResultPresenterItems(selector).ToDataGridResultPresenter();
    }

    private static IResultPresenter ToDataGridResultPresenter<T>(this IEnumerable<T> values, Func<T, ValueResultPresenterItem<string>> selector = null)
    {
        return values.ToValueResultPresenterItems(selector).ToDataGridResultPresenter();
    }

    private static IResultPresenter ToDataGridResultPresenter<T>(this IEnumerable<T> values) where T : IResultPresenterItem
    {
        return new DataGridResultPresenter<T>(values);
    }

    private static IResultPresenter ToDataGridResultPresenter<T>(this T value) where T : IResultPresenterItem
    {
        return new DataGridResultPresenter<T>(new ObservableCollection<T>() { value });
    }

    public static IResultPresenter ToDataGridResultPresenter<T>(this IEnumerable<T> values, Func<T, string> valueSelector, Func<T, string> nameSelector)
    {
        return values.ToDataGridResultPresenter(x => new ValueResultPresenterItem<string>() { Name = nameSelector?.Invoke(x), Value = valueSelector?.Invoke(x) });
    }

}

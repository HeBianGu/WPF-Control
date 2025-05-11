// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Extensions.TypeConverter;
global using H.Presenters.Design.Base;
global using System.Collections.ObjectModel;

namespace H.Presenters.Design.Presenter;

[Display(Name = "DefinitionGrid")]
public class DefinitionGridPresenter : GridPresenterBase
{
    public DefinitionGridPresenter()
    {
        this.Rows = Enumerable.Range(0, 6).Select(x => new GridLength(1, GridUnitType.Star)).ToObservable();
        this.Columns = Enumerable.Range(0, 6).Select(x => new GridLength(1, GridUnitType.Star)).ToObservable();
    }
    private ObservableCollection<GridLength> _rows;
    [Display(Name = "行数", GroupName = "常用,样式")]
    [TypeConverter(typeof(ObservableCollectionTypeConverter<GridLength, GridLengthConverter>))]
    public ObservableCollection<GridLength> Rows
    {
        get { return _rows; }
        set
        {
            _rows = value;
            RaisePropertyChanged();
        }
    }

    private ObservableCollection<GridLength> _columns;
    [Display(Name = "列数", GroupName = "常用,样式")]
    [TypeConverter(typeof(ObservableCollectionTypeConverter<GridLength, GridLengthConverter>))]
    public ObservableCollection<GridLength> Columns
    {
        get { return _columns; }
        set
        {
            _columns = value;
            RaisePropertyChanged();
        }
    }
}

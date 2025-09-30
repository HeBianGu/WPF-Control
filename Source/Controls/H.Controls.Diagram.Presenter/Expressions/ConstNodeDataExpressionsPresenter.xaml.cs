// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Services.Setting;

namespace H.Controls.Diagram.Presenter.Expressions;

[Icon(FontIcons.Globe)]
[Display(Name = "全局变量", GroupName = SettingGroupNames.GroupData, Order = 0)]
public class ConstNodeDataExpressionsPresenter : DisplayBindableBase
{
    private ObservableCollection<IConstNodeDataExpression> _ConstNodeDataExpressions = new ObservableCollection<IConstNodeDataExpression>();
    public ObservableCollection<IConstNodeDataExpression> ConstNodeDataExpressions
    {
        get { return _ConstNodeDataExpressions; }
        set
        {
            _ConstNodeDataExpressions = value;
            RaisePropertyChanged();
        }
    }


    private IConstNodeDataExpression _SelectedItem;

    public IConstNodeDataExpression SelectedItem
    {
        get { return _SelectedItem; }
        set
        {
            _SelectedItem = value;
            RaisePropertyChanged();
        }
    }


    public string DefaultGroupName { get; set; } = "全局变量";

    public RelayCommand AddCommand => new RelayCommand(async x =>
    {
        TypeSelector typeSelector = new TypeSelector();
        var r = await IocMessage.Form.ShowEdit(typeSelector);
        if (r != true)
            return;

        IConstNodeDataExpression expression = this.Create(typeSelector.ConstType);
        r = await IocMessage.Form.ShowEdit(expression);
        if (r != true)
            return;
        expression.GroupName = this.DefaultGroupName;
        expression.UpdatePath();
        this.ConstNodeDataExpressions.Add(expression);
    });

    public RelayCommand EditCommand => new RelayCommand(async x =>
    {
        await IocMessage.Form.ShowEdit(this.SelectedItem);
    }, x => this.SelectedItem != null);


    public RelayCommand DeleteCommand => new RelayCommand(x =>
    {
        this.ConstNodeDataExpressions.Remove(this.SelectedItem);
    }, x => this.SelectedItem != null);

    public IConstNodeDataExpression Create(ConstType constType)
    {
        if (constType == ConstType.Int32)
            return new ConstNodeDataExpression<int>(0, this.DefaultGroupName) { Name = "默认名称" };
        if (constType == ConstType.Double)
            return new ConstNodeDataExpression<double>(0.0, this.DefaultGroupName) { Name = "默认名称" };
        return new ConstNodeDataExpression<string>(null, this.DefaultGroupName) { Name = "默认名称" };
    }
}

public class TypeSelector
{
    [Display(Name = "选择类型")]
    public ConstType ConstType { get; set; }
}

[TypeConverter(typeof(DisplayEnumConverter))]
public enum ConstType
{
    [Display(Name = "整型")]
    Int32,
    [Display(Name = "浮点数")]
    Double,
    [Display(Name = "字符串")]
    String
}

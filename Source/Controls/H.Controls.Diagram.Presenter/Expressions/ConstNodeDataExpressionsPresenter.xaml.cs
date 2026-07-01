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
    private ObservableCollection<IConstExpressionKey> _ConstNodeDataExpressions = new ObservableCollection<IConstExpressionKey>();
    public ObservableCollection<IConstExpressionKey> ConstNodeDataExpressions
    {
        get { return _ConstNodeDataExpressions; }
        set
        {
            _ConstNodeDataExpressions = value;
            RaisePropertyChanged();
            this.UpdateSearch();
        }
    }


    private IConstExpressionKey _SelectedItem;

    public IConstExpressionKey SelectedItem
    {
        get { return _SelectedItem; }
        set
        {
            _SelectedItem = value;
            RaisePropertyChanged();
        }
    }

    private string _SearchText;
    public string SearchText
    {
        get { return _SearchText; }
        set
        {
            _SearchText = value;
            RaisePropertyChanged();
            this.UpdateSearch();
        }
    }

    private ObservableCollection<IConstExpressionKey> _SearchedConstNodeDataExpressions = new ObservableCollection<IConstExpressionKey>();
    public ObservableCollection<IConstExpressionKey> SearchedConstNodeDataExpressions
    {
        get { return _SearchedConstNodeDataExpressions; }
        set
        {
            _SearchedConstNodeDataExpressions = value;
            RaisePropertyChanged();
        }
    }


    public void UpdateSearch()
    {
        this.SearchedConstNodeDataExpressions = this.ConstNodeDataExpressions.Where(x => string.IsNullOrWhiteSpace(this.SearchText) || x.Name.Contains(this.SearchText)).ToObservable();
    }



    public string DefaultGroupName { get; set; } = "全局变量";

    public RelayCommand AddCommand => new RelayCommand(async x =>
    {
        TypeSelector typeSelector = new TypeSelector();
        var r = await IocMessage.Form.ShowEdit(typeSelector);
        if (r != true)
            return;

        IConstExpressionKey expression = this.Create(typeSelector.ConstType);
        r = await IocMessage.Form.ShowEdit(expression, x =>
        {
            if (this.ConstNodeDataExpressions.Any(x => x.Name == expression.Name))
            {
                IocMessage.Snack.ShowError("名称重复，请修改名称");
                return false;
            }
            return true;
        });
        if (r != true)
            return;
        expression.GroupName = this.DefaultGroupName;
        //expression.UpdatePath();
        this.ConstNodeDataExpressions.Add(expression);
        this.UpdateSearch();
    });

    public RelayCommand EditCommand => new RelayCommand(async x =>
    {
        if (x is IConstExpressionKey constExpressionKey)
        {
            await IocMessage.Form.ShowEdit(constExpressionKey, x =>
            {
                if (this.ConstNodeDataExpressions.Any(y => y.Name == constExpressionKey.Name && y != constExpressionKey))
                {
                    IocMessage.Snack.ShowError("名称重复，请修改名称");
                    return false;
                }
                this.UpdateSearch();
                return true;
            });
        }

    }, x => x is IConstExpressionKey);


    public RelayCommand DeleteCommand => new RelayCommand(async x =>
    {
        if (x is IConstExpressionKey constExpressionKey)
        {
            var r = await IocMessage.Dialog.ShowDeleteDialog();
            if (r != true)
                return;
            this.ConstNodeDataExpressions.Remove(constExpressionKey);
            this.UpdateSearch();
        }
    }, x => x is IConstExpressionKey);

    public IConstExpressionKey Create(ConstType constType)
    {
        var name = this.ConstNodeDataExpressions.Select(x => x.Name).GetIndexSafeName("var");
        if (constType == ConstType.Int32)
            return new ConstExpressionKey<int>(0, this.DefaultGroupName) { Name = name };
        if (constType == ConstType.Double)
            return new ConstExpressionKey<double>(0.0, this.DefaultGroupName) { Name = name };
        return new ConstExpressionKey<string>(null, this.DefaultGroupName) { Name = name };
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

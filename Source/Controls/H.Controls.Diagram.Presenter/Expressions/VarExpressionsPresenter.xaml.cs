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
public class VarExpressionsPresenter : DisplayBindableBase
{
    private ObservableCollection<IVarExpression> _VarExpressions = new ObservableCollection<IVarExpression>();
    public ObservableCollection<IVarExpression> VarExpressions
    {
        get { return _VarExpressions; }
        set
        {
            _VarExpressions = value;
            RaisePropertyChanged();
            this.UpdateSearch();
        }
    }

    private IVarExpression _SelectedItem;
    public IVarExpression SelectedItem
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

    private ObservableCollection<IVarExpression> _SearchedVarExpressions = new ObservableCollection<IVarExpression>();
    public ObservableCollection<IVarExpression> SearchedVarExpressions
    {
        get { return _SearchedVarExpressions; }
        set
        {
            _SearchedVarExpressions = value;
            RaisePropertyChanged();
        }
    }


    public void UpdateSearch()
    {
        this.SearchedVarExpressions = this.VarExpressions.Where(x => string.IsNullOrWhiteSpace(this.SearchText) || x.Name.Contains(this.SearchText)).ToObservable();
    }

    public string DefaultGroupName { get; set; } = "全局变量";

    public RelayCommand AddCommand => new RelayCommand(async x =>
    {
        TypeSelector typeSelector = new TypeSelector();
        var r = await IocMessage.Form.ShowEdit(typeSelector);
        if (r != true)
            return;

        IVarExpression expression = this.Create(typeSelector.ConstType);
        r = await IocMessage.Form.ShowEdit(expression, x =>
        {
            if (this.VarExpressions.Any(x => x.Name == expression.Name))
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
        this.VarExpressions.Add(expression);
        this.UpdateSearch();
    });

    public RelayCommand EditCommand => new RelayCommand(async x =>
    {
        if (x is IVarExpression constExpressionKey)
        {
            await IocMessage.Form.ShowEdit(constExpressionKey, x =>
            {
                if (this.VarExpressions.Any(y => y.Name == constExpressionKey.Name && y != constExpressionKey))
                {
                    IocMessage.Snack.ShowError("名称重复，请修改名称");
                    return false;
                }
                this.UpdateSearch();
                return true;
            });
        }

    }, x => x is IVarExpression);


    public RelayCommand DeleteCommand => new RelayCommand(async x =>
    {
        if (x is IVarExpression constExpressionKey)
        {
            var r = await IocMessage.Dialog.ShowDeleteDialog();
            if (r != true)
                return;
            this.VarExpressions.Remove(constExpressionKey);
            this.UpdateSearch();
        }
    }, x => x is IVarExpression);

    public IVarExpression Create(ConstType constType)
    {
        var name = this.VarExpressions.Select(x => x.Name).GetIndexSafeName("var");
        if (constType == ConstType.Int32)
            return new VarExpression<int>(0, this.DefaultGroupName) { Name = name };
        if (constType == ConstType.Double)
            return new VarExpression<double>(0.0, this.DefaultGroupName) { Name = name };
        return new VarExpression<string>(null, this.DefaultGroupName) { Name = name };
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

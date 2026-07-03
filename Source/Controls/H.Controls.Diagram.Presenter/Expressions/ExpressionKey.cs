// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenter.Expressions;

public class ExpressionKey : BindableBase, IExpressionKey
{
    private string _GroupName;
    [ReadOnly(true)]
    [Display(Name = "分组")]
    public string GroupName
    {
        get { return _GroupName; }
        set
        {
            _GroupName = value;
            RaisePropertyChanged();
        }
    }
    private string _Name;
    [Display(Name = "名称")]
    public string Name
    {
        get { return _Name; }
        set
        {
            _Name = value;
            RaisePropertyChanged();
        }
    }

    public virtual string DisplayName => $"{this.GroupName}.{this.Name}";


    public override bool Equals(object obj)
    {
        if (obj is IExpressionKey expression)
            return this.Name == expression.Name && this.GroupName == expression.GroupName;
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.Name, this.GroupName);
    }
}


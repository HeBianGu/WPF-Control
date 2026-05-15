// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Components.Vision.Presenters;

namespace H.Components.Vision.Base;
public interface IHelpNodeData
{
    IHelpPresenter HelpPresenter { get; set; }
}

public abstract class HelpNodeDataBase : ShowPropertyNodeDataBase, IHelpNodeData
{
    protected HelpNodeDataBase()
    {
        this.HelpPresenter = this.CreateHelpPresenter();
    }
    private IHelpPresenter _helpPresenter;
    [JsonIgnore]
    [Browsable(false)]
    public IHelpPresenter HelpPresenter
    {
        get { return _helpPresenter; }
        set
        {
            _helpPresenter = value;
            RaisePropertyChanged();
        }
    }
    public virtual IHelpPresenter CreateHelpPresenter()
    {
        string fullName = this.GetType().FullName;
        return new HelpPresenter()
        {
            Url = "https://hebiangu.github.io/WPF-VisionMaster-Doc/api/" + fullName + ".html"
        };
    }
}


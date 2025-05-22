// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Attach;
using H.Modules.Guide.Base;
using H.Mvvm.ViewModels.Base;
namespace H.Modules.Guide;

[Icon("\xEC92")]
[Display(Name = "功能列表", Description = "显示版本功能列表")]
public class GuideTreePresenter : DisplayBindableBase
{
    private readonly GuideTree _guideTree;
    public GuideTreePresenter(GuideTree guideTree)
    {
        this._guideTree = guideTree;
        this.RefreshData();
    }

    public GuideTree GuideTree => this._guideTree;

    private ObservableCollection<GuideData> _guideDatas = new ObservableCollection<GuideData>();
    public ObservableCollection<GuideData> GuideDatas
    {
        get { return _guideDatas; }
        set
        {
            _guideDatas = value;
            RaisePropertyChanged("GuideDatas");
        }
    }

    public void RefreshData()
    {
        this.GuideDatas = this.CreateGuideDatas().ToObservable();
    }

    public IEnumerable<GuideData> CreateGuideDatas()
    {
        var nodes = this._guideTree.GetGuideTreeNodes();
        foreach (var node in nodes)
        {
            GuideData guideData = new GuideData();
            guideData.Tilte = Cattach.GetGuideTitle(node.Element);
            guideData.Icon = Cattach.GetGuideIcon(node.Element);
            guideData.Data = Cattach.GetGuideData(node.Element);
            guideData.DataTemplate = Cattach.GetGuideDataTemplate(node.Element);
            guideData.Version = Cattach.GetGuideAssemblyVersion(node.Element);
            guideData.Element = node.Element;
            yield return guideData;
        }
    }
}

public class GuideData : BindableBase
{
    public object Tilte { get; set; }

    public string Icon { get; set; }

    public object Data { get; set; }

    public DataTemplate DataTemplate { get; set; }

    public Version Version { get; set; }

    public UIElement Element { get; set; }
}

// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.App.AIDI.Presenters;
using H.App.AIDI.Presenters.Components;
using H.Common.Attributes;
using H.Extensions.Common;
using H.Extensions.FontIcon;
using H.Extensions.Mvvm.Commands;
using H.Services.Message;
using H.Services.Message.Notice;
using System.Text.Json.Serialization;

namespace H.App.AIDI.Base;
public interface IModulePresenter : IPagePresenter
{
    IModuleViewPagePresenter ModuleViewPagePresenter { get; }
}

public abstract class ModulePresenterBase : PagePresenterBase, IModulePresenter
{
    public ModulePresenterBase()
    {
        this.ModuleViewPagePresenter = this.CreateModuleViewPagePresenter();
    }
    public ModulePresenterBase(AIDIProjectItem projcetItem) : base(projcetItem)
    {
        this.ModuleViewPagePresenter = this.CreateModuleViewPagePresenter();
    }

    private IModuleViewPagePresenter CreateModuleViewPagePresenter()
    {
        return new ModuleViewPagePresenter(this.ProjectItem, this);
    }

    private IModuleViewPagePresenter _ModuleViewPagePresenter;
    public IModuleViewPagePresenter ModuleViewPagePresenter
    {
        get { return _ModuleViewPagePresenter; }
        set
        {
            _ModuleViewPagePresenter = value;
            RaisePropertyChanged();
        }
    }

    [Icon(FontIcons.People)]
    [Display(Name = "训练", GroupName = "操作", Description = "执行训练任务")]
    public DisplayCommand TrainingCommand => new DisplayCommand(x =>
    {
        IocMessage.Notify.ShowStringDemo();
    });

    [Icon(FontIcons.Play)]
    [Display(Name = "推理", GroupName = "操作", Description = "执行推理任务")]
    public DisplayCommand InferenceCommand => new DisplayCommand(x =>
    {
        IocMessage.Notify.ShowStringDemo();
    });
}


public abstract class TabModulePresenterBase : ModulePresenterBase, ITabModulePresenter
{
    public TabModulePresenterBase()
    {
        this.InitTabs();
    }
    public TabModulePresenterBase(AIDIProjectItem projcetItem) : base(projcetItem)
    {
        this.InitTabs();
    }

    private void InitTabs()
    {
        this.TabPresenters = this.CreateTabPresenters().ToObservable();
        this.SelectedTabPresenter = this.TabPresenters?.FirstOrDefault();
        this.ModuleTabComponentPresenter = this.CreateModuleTabComponentPresenter();
    }

    protected virtual IEnumerable<ITabPresenter> CreateTabPresenters()
    {
        yield return new LabelManagerTabPresenter(this);
        yield return new TrainManagerTabPresenter(this);
        yield return new EvaluationTabPresenter(this);
    }

    protected virtual ModuleTabComponentPresenter CreateModuleTabComponentPresenter()
    {
        return new ModuleTabComponentPresenter(this); ;
    }

    private ModuleTabComponentPresenter _ModuleTabComponentPresenter;
    [JsonIgnore]
    public ModuleTabComponentPresenter ModuleTabComponentPresenter
    {
        get { return _ModuleTabComponentPresenter; }
        set
        {
            _ModuleTabComponentPresenter = value;
            RaisePropertyChanged();
        }
    }

    private ObservableCollection<ITabPresenter> _TabPresenters = new ObservableCollection<ITabPresenter>();
    [JsonIgnore]
    public ObservableCollection<ITabPresenter> TabPresenters
    {
        get { return _TabPresenters; }
        set
        {
            _TabPresenters = value;
            RaisePropertyChanged();
        }
    }

    private ITabPresenter _SelectedTabPresenter;
    [JsonIgnore]
    public ITabPresenter SelectedTabPresenter
    {
        get { return _SelectedTabPresenter; }
        set
        {
            _SelectedTabPresenter = value;
            RaisePropertyChanged();
        }
    }
}

[Icon(FontIcons.Dial9)]
[Display(Name = "非监督分割(未实现)", GroupName = "深度学习功能", Description = "模块介绍:定位且对目标进行分类，可以检测多种类型\r\n的缺陷\r\n应用场景:成块特征检测;边缘模糊缺陷的检测，散点\r\n状缺陷的检测")]
public class SegmentUnsupervisedModulePresenter : ModulePresenterBase
{
    public SegmentUnsupervisedModulePresenter()
    {

    }
    public SegmentUnsupervisedModulePresenter(AIDIProjectItem projcetItem) : base(projcetItem)
    {
    }
}


[Icon(FontIcons.OEM)]
[Display(Name = "分类 [未实现]", GroupName = "深度学习功能", Description = "模块介绍:用于在图像中对不同的缺陷进行分类，也可\r\n以对图片整体进行分级\r\n应用场景:缺陷分类、产品分等级")]
public class ClassicModulePresenter : ModulePresenterBase
{
    public ClassicModulePresenter()
    {

    }
    public ClassicModulePresenter(AIDIProjectItem projcetItem) : base(projcetItem)
    {
    }
}

[Icon(FontIcons.Filter)]
[Display(Name = "非监督分类 [未实现]", GroupName = "深度学习功能", Description = "模块介绍:定位且对目标进行分类，可以检测多种类型\r\n的缺陷\r\n应用场景:成块特征检测;边缘模糊缺陷的检测，散点\r\n状缺陷的检测")]
public class ClassicUnsupervisedModulePresenter : ModulePresenterBase
{
    public ClassicUnsupervisedModulePresenter()
    {

    }
    public ClassicUnsupervisedModulePresenter(AIDIProjectItem projcetItem) : base(projcetItem)
    {
    }
}

[Icon(FontIcons.Location)]
[Display(Name = "定位 [未实现]", GroupName = "深度学习功能", Description = "模块介绍:定位且对目标进行分类，可以检测多种类型\r\n的缺陷\r\n应用场景:成块特征检测;边缘模糊缺陷的检测，散点\r\n状缺陷的检测")]
public class LocationModulePresenter : ModulePresenterBase
{
    public LocationModulePresenter()
    {

    }
    public LocationModulePresenter(AIDIProjectItem projcetItem) : base(projcetItem)
    {
    }
}

[Icon(FontIcons.ExploreContentSingle)]
[Display(Name = "字符识别 [未实现]", GroupName = "深度学习功能", Description = "模块介绍:定位且对目标进行分类，可以检测多种类型\r\n的缺陷\r\n应用场景:成块特征检测;边缘模糊缺陷的检测，散点\r\n状缺陷的检测")]
public class OCRModulePresenter : ModulePresenterBase
{
    public OCRModulePresenter()
    {

    }
    public OCRModulePresenter(AIDIProjectItem projcetItem) : base(projcetItem)
    {
    }
}

[Icon(FontIcons.FileExplorer)]
[Display(Name = "装配检查 [未实现]", GroupName = "深度学习功能", Description = "模块介绍:定位且对目标进行分类，可以检测多种类型\r\n的缺陷\r\n应用场景:成块特征检测;边缘模糊缺陷的检测，散点\r\n状缺陷的检测")]
public class AssemblyInspectionModulePresenter : ModulePresenterBase
{
    public AssemblyInspectionModulePresenter()
    {

    }
    public AssemblyInspectionModulePresenter(AIDIProjectItem projcetItem) : base(projcetItem)
    {
    }
}

[Icon(FontIcons.Favicon2)]
[Display(Name = "综合判定节点 [未实现]", GroupName = "工具", Description = "模块介绍:定位且对目标进行分类，可以检测多种类型\r\n的缺陷\r\n应用场景:成块特征检测;边缘模糊缺陷的检测，散点\r\n状缺陷的检测")]
public class ComprehensiveEvaluationModulePresenter : ModulePresenterBase
{
    public ComprehensiveEvaluationModulePresenter()
    {

    }
    public ComprehensiveEvaluationModulePresenter(AIDIProjectItem projcetItem) : base(projcetItem)
    {
    }
}





// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagram.NodeDatas;

[Icon(FontIcons.Dial6)]
[Display(Name = "循环执行", GroupName = "逻辑控制", Description = "按指定的起始和结束索引重复执行后续流程", Order = 20)]
public class ForNodeData : InhertImageVisionNodeDataBase
{
    private int _from = 0;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "循环起始位置", GroupName = VisionTabNames.RunParameters, Description = "设置循环索引的起始值", Order = 1000)]
    public int From
    {
        get { return _from; }
        set
        {
            _from = value;
            RaisePropertyChanged();
        }
    }

    private int _to = 5;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "循环结束位置", GroupName = VisionTabNames.RunParameters, Description = "设置循环索引的结束边界值", Order = 1000)]
    public int To
    {
        get { return _to; }
        set
        {
            _to = value;
            RaisePropertyChanged();
        }
    }

    private int _currentIndex;
    [Expressionable]
    [Tab(VisionTabNames.ResultParameters)]
    [Display(Name = "当前循环索引", GroupName = VisionTabNames.ResultParameters, Description = "输出当前循环迭代使用的索引值", Order = 1000)]
    public int CurrentIndex
    {
        get { return _currentIndex; }
        set
        {
            _currentIndex = value;
            RaisePropertyChanged();
        }
    }


    protected override IFlowableResult InvokeInhert()
    {
        for (int i = this.From; i < this.To; i++)
        {
            this.CurrentIndex = i + this.From;
            //this.InvokeFrameMatAsync(false).Wait();
        }
        return this.OK();
    }
}


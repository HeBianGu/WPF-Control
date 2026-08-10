// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagram.NodeDatas;

[Icon(FontIcons.Dial6)]
[Display(Name = "循环次数", GroupName = "逻辑模块", Description = "设置像素阈值，根据阈值执行不同路径逻辑", Order = 20)]
public class ForNodeData : InhertImageVisionNodeDataBase
{
    private int _from = 0;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "循环起始位置", GroupName = VisionTabNames.RunParameters, Order = 1000)]
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
    [Display(Name = "循环结束位置", GroupName = VisionTabNames.RunParameters, Order = 1000)]
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
    [Display(Name = "循环结束位置", GroupName = VisionTabNames.ResultParameters, Order = 1000)]
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


// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagram.NodeDatas;

public abstract class ForNodeDataBase<T> : WaitFromVisionNodeData<T> where T : class, IVisionImage
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


    protected override FlowableResult<T> Invoke(T fromImage)
    {
        for (int i = this.From; i < this.To; i++)
        {
            this.CurrentIndex = i + this.From;
            this.InvokeFrameMatAsync(fromImage, false).Wait();
        }
        return this.OK(fromImage);
    }
}


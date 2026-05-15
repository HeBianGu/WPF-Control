// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.Vision.Base;

public abstract class VisionNodeDataBase : StateNodeDataBase
{
    ~VisionNodeDataBase()
    {
        this.Dispose();
    }

    private int _previewMillisecondsDelay = 0;
    [DefaultValue(0)]
    [Display(Name = "预览延迟", GroupName = VisionPropertyGroupNames.FlowParameters, Description = "设置生成图像后预览等待时间")]
    public int PreviewMillisecondsDelay
    {
        get { return _previewMillisecondsDelay; }
        set
        {
            _previewMillisecondsDelay = value;
            RaisePropertyChanged();
        }
    }

    private int _invokeMillisecondsDelay = 0;
    [DefaultValue(0)]
    [Display(Name = "执行延迟", GroupName = VisionPropertyGroupNames.FlowParameters, Description = "执行完成后等待时间")]
    public int InvokeMillisecondsDelay
    {
        get { return _invokeMillisecondsDelay; }
        set
        {
            _invokeMillisecondsDelay = value;
            RaisePropertyChanged();
        }
    }

    private bool _useInvokedPart = true;
    [DefaultValue(true)]
    [Display(Name = "启用输出历史记录", GroupName = VisionPropertyGroupNames.DisplayParameters, Description = "用于控制是否输出到历史记录和预览图像")]
    public bool UseInvokedPart
    {
        get { return _useInvokedPart; }
        set
        {
            _useInvokedPart = value;
            RaisePropertyChanged();
        }
    }
}


// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenter.NodeDatas.Base;

public abstract class PropertyResultNodeDataBase : FlowableNodeData
{
    private object _objectResult;
    [ReadOnly(true)]
    [Display(Name = "测试参数结果", GroupName = "模块结果", Description = "执行完成后等待时间")]
    public object ObjectResult
    {
        get { return _objectResult; }
        set
        {
            _objectResult = value;
            RaisePropertyChanged();
        }
    }
}

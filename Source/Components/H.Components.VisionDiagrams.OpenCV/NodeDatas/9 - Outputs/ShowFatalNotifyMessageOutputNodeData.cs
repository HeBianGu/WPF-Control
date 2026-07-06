// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagrams.OpenCV.NodeDatas.Other;
[Icon(FontIcons.DefenderApp)]
[Display(Name = "提示严重错误消息", Description = "输出提示消息", Order = 10410)]
public class ShowFatalNotifyMessageOutputNodeData : OutputNodeDataBase, IOutputGroupableNodeData
{
    private string _value = "运行严重错误";
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "消息信息", GroupName = VisionTabNames.RunParameters, Description = "用于设置输出提示消息")]
    public string Value
    {
        get { return _value; }
        set
        {
            _value = value;
            RaisePropertyChanged();
        }
    }

    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            IocMessage.Notify.ShowFatal(this.Value);
        });
        return this.OK(fromImage.Clone(), this.Value);
    }
}


// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagrams.OpenCV.NodeDatas.Other;
[Icon(FontIcons.OverwriteWordsFillKorean)]
[Display(Name = "提示警告消息", GroupName = "输出", Description = "使用警告级别通知显示流程消息", Order = 10410)]
public class ShowWarnNotifyMessageOutputNodeData : OutputNodeDataBase, IOutputGroupableNodeData
{
    private string _value = "运行警告";
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
            IocMessage.Notify.ShowWarn(this.Value);
        });
        return this.OK(fromImage.Clone(), this.Value);
    }
}


// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagrams.OpenCV.NodeDatas.Other;
[Icon(FontIcons.Unknown)]
[Display(Name = "提示对话框消息", GroupName = "输出", Description = "使用对话框显示流程提示消息", Order = 10410)]
public class ShowDialogNotifyMessageOutputNodeData : OutputNodeDataBase, IOutputGroupableNodeData
{
    private string _value = "是否继续运行流程";
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
        return this.OK(fromImage);
    }

    public override async Task<IFlowableResult> InvokeAsync(IFlowableLinkData previors, IFlowableDiagramData diagram)
    {
        var result = await base.InvokeAsync(previors, diagram);
        var r = await IocMessage.Notify.ShowDialog(this.Value);
        if (r != true && result is FlowableResult<Mat> matResult)
        {
            return this.Error(matResult.Value, "用户取消运行流程");
        }
        return result;
    }
}


// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Components.VisionDiagram.Base;
using H.Controls.Form.PropertyItem.Attribute;
using H.Iocable;

namespace H.Components.VisionDiagram.NodeDatas;

public abstract class FromImageVisionNodeDataBase<T> : ResultDisplayVisionNodeDataBase<T> where T : class, IVisionImage
{
    private IExpressionKey _fromImageExpression;
    [GetMethodNameSource(nameof(GetImageFromExpressions))]
    [PropertyItem(typeof(InputExpressionComboBoxTextPropertyItem))]
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "图像输入", GroupName = VisionTabNames.RunParameters, Description = "选择前序结果图像作为此流程图像源")]
    public IExpressionKey FromImageExpression
    {
        get { return _fromImageExpression; }
        set
        {
            _fromImageExpression = value;
            RaisePropertyChanged();
        }
    }

    public IEnumerable<IExpressionKey> GetImageFromExpressions()
    {
        yield return new InheritanceExpressionKey();
        foreach (var item in this.GetFromExpressionKeys<T>())
        {
            yield return item;
        }
    }

    protected override FlowableResult<T> Invoke(IStartVisionNodeData srcImageNodeData, IVisionNodeData from, IFlowableDiagramData diagram)
    {
        var resultImage = this.GetVisionImage(from);
        T fromImage = this.GetExpressionFromImage(resultImage);
        if (from == srcImageNodeData)
            return this.Invoke(srcImageNodeData, from, fromImage, diagram);
        if (!this.IsFromImageValid(fromImage))
            return this.Error(fromImage, "输入图像不正确");
        return this.Invoke(srcImageNodeData, from, fromImage, diagram);
    }

    protected virtual bool IsFromImageValid(T fromImage)
    {
        return fromImage?.IsValid() == true;
    }

    private T GetExpressionFromImage(T from = default)
    {
        var r = this.GetExpressionValue<T>(this.FromImageExpression);
        if (r.success)
            return r.value;
        return from;
    }

    protected virtual FlowableResult<T> Invoke(IStartVisionNodeData srcImageNodeData, IVisionNodeData from, T fromImage, IFlowableDiagramData diagram)
    {
        return this.Invoke(fromImage);
    }


    protected abstract FlowableResult<T> Invoke(T fromImage);
}


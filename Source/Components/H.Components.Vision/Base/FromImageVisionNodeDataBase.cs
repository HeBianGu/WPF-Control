// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.Vision.Base;
public abstract class FromImageVisionNodeDataBase<T> : VisionNodeData<T> where T : class, IVisionImage
{
    private NodeDataExpression _fromImageExpression;
    [GetMethodNameSource(nameof(GetImageFromExpressions))]
    [PropertyItem(typeof(ExpressionComboBoxPropertyItem))]
    [Display(Name = "选择输入图像", GroupName = VisionPropertyGroupNames.RunParameters, Description = "选择前序结果图像作为此流程图像源")]
    public NodeDataExpression FromImageExpression
    {
        get { return _fromImageExpression; }
        set
        {
            _fromImageExpression = value;
            RaisePropertyChanged();
        }
    }

    public IEnumerable<NodeDataExpression> GetImageFromExpressions()
    {
        yield return new NoneNodeDataExpression();
        foreach (var item in this.GetFromExpressions<T>())
        {
            yield return item;
        }
    }

    protected override FlowableResult<T> Invoke(IStartVisionNodeData<T> srcImageNodeData, IVisionNodeData<T> from, IFlowableDiagramData diagram)
    {
        T fromImage = this.GetExpressionFromImage(from?.ResultImage);
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
        if (this.TryGetExpressionValue(this.FromImageExpression, out T image))
            return image;
        return from;
    }

    protected virtual FlowableResult<T> Invoke(IStartVisionNodeData<T> srcImageNodeData, IVisionNodeData<T> from, T fromImage, IFlowableDiagramData diagram)
    {
        return this.Invoke(fromImage);
    }


    protected abstract FlowableResult<T> Invoke(T fromImage);
}


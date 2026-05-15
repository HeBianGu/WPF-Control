// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.Vision.Base;

public class FromNodeValidationAttribute<T> : FromNodeValidationAttributeBase
{
    public override (bool success, string message) IsValid(INodeData fromNodeData)
    {
        var r = fromNodeData is T;
        return (r, r ? string.Empty : $"输入节点数据类型错误，期望类型：{typeof(T).FullName}，实际类型：{fromNodeData?.GetType().FullName}");
    }
}

public abstract class FromNodeValidationAttributeBase : ValidationAttribute
{
    public abstract (bool success, string message) IsValid(INodeData fromNodeData);
}

public interface IThresholdNodeData
{

}

public interface IGrayNodeData
{

}

/// <summary>
/// 限制输入节点数据类型为 IThresholdNodeData 二值化处理后的图像。
/// </summary>
public class ThresholdFromNodeValidationAttribute : FromNodeValidationAttributeBase
{
    public override (bool success, string message) IsValid(INodeData fromNodeData)
    {
        return fromNodeData is IThresholdNodeData
            ? (true, string.Empty)
            : (false, $"输入节点数据类型错误，期望类型：{typeof(IThresholdNodeData).FullName}，实际类型：{fromNodeData?.GetType().FullName}");
    }
}

/// <summary>
/// 限制输入节点数据类型为 IGrayNodeData 灰度处理后图像。
/// </summary>
public class GrayFromNodeValidationAttribute : FromNodeValidationAttributeBase
{
    public override (bool success, string message) IsValid(INodeData fromNodeData)
    {
        return fromNodeData is IGrayNodeData
            ? (true, string.Empty)
            : (false, $"输入节点数据类型错误，期望类型：{typeof(IGrayNodeData).FullName}，实际类型：{fromNodeData?.GetType().FullName}");
    }
}
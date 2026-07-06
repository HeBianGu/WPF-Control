// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.PropertyItem.Attribute;

namespace H.Components.VisionDiagram.NodeDatas;
public abstract class ForeachNodeDataBase<T, V> : WaitFromVisionNodeData<T> where T : class, IVisionImage
{
    private IExpressionKey _ItemSourceExpression;
    [GetMethodNameSource(nameof(GetItemSourceExpressions))]
    [PropertyItem(typeof(InputExpressionComboBoxTextPropertyItem))]
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "数据列表", GroupName = VisionTabNames.RunParameters, Description = "用来演示如何增加节点表达式参数")]
    public IExpressionKey ItemSourceExpression
    {
        get { return _ItemSourceExpression; }
        set
        {
            _ItemSourceExpression = value;
            RaisePropertyChanged();
        }
    }
    public IEnumerable<IExpressionKey> GetItemSourceExpressions() => this.GetFromExpressionKeys<List<V>>();

    private V _currentValue;
    [JsonIgnore]
    [Expressionable]
    [Tab(VisionTabNames.ResultParameters)]
    [Display(Name = "当前值", GroupName = VisionTabNames.ResultParameters, Order = 1000)]
    public V CurrentValue
    {
        get { return _currentValue; }
        set
        {
            _currentValue = value;
            RaisePropertyChanged();
        }
    }


    protected override FlowableResult<T> Invoke(T fromImage)
    {
        if (this.ItemSourceExpression == null)
            this.ItemSourceExpression = this.GetItemSourceExpressions().FirstOrDefault();

        var r = this.GetExpressionValue<List<V>>(this.ItemSourceExpression);
        if (!r.success)
            return this.Error(fromImage, "没有获取到数据列表");
        foreach (var item in r.value)
        {
            this.CurrentValue = item;
            this.InvokeFrameMatAsync(fromImage, false).Wait();
        }
        return this.OK(fromImage);
    }
}


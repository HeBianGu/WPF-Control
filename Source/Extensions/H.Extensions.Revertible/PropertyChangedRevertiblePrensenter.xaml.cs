// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Revertible;

public abstract class PropertyChangedRevertiblePrensenterBase
{

}
public class PropertyChangedRevertiblePrensenter<T> : PropertyChangedRevertiblePrensenterBase
{
    public PropertyChangedRevertiblePrensenter(string perpertyName, T oldValue, T newValue)
    {
        this.PropertyName = perpertyName;
        this.OldValue = oldValue;
        this.NewValue = newValue;
    }
    public string PropertyName { get; set; }
    public T OldValue { get; set; }
    public T NewValue { get; set; }
}

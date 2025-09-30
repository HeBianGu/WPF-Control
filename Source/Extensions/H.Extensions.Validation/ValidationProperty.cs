// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Validation;

/// <summary> 用于绑定验证Model的实体，可以应用Model中的验证特性判断输入数据是否合法</summary>
public class ValidationProperty<V> : ValidationPropertyBase
{

    private V _value;
    /// <summary> 值  </summary>
    public V Value
    {
        get { return _value; }
        set
        {
            _value = value;
            //  Do ：应用实体验证数据有效性
            this.ValueChanged?.Invoke(value);
            RaisePropertyChanged("Value");
        }
    }

    public Action<V> ValueChanged;
}

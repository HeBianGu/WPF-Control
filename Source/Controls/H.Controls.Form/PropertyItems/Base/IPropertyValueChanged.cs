// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Form.PropertyItems.Base;

/// <summary>
/// 用于Form Object注册相应属性值变化事件
/// </summary>
public interface IPropertyValueChanged
{
    void OnPropertyValueChanged(PropertyInfo propertyInfo, object o, object n);
}

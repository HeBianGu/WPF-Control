// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Controls.Form.PropertyItems.Base;

/// <summary>
/// 用于定义是否触发Form的ValueChanged事件
/// </summary>
public interface IValueChangeable
{
    Action<object> ValueChanged { get; set; }
}

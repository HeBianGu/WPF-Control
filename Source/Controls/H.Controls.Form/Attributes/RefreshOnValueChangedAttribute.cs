// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Controls.Form.Attributes;

public class RefreshOnValueChangedAttribute : Attribute
{
    public RefreshOnValueChangedAttribute(bool canRefresh = true)
    {
        this.CanRefresh = canRefresh;
    }
    public bool CanRefresh { get; }
}

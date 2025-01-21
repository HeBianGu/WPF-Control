// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;

namespace H.Controls.Form
{
    public class RefreshOnValueChangedAttribute : Attribute
    {
        public RefreshOnValueChangedAttribute(bool canRefresh = true)
        {
            this.CanRefresh = canRefresh;
        }
        public bool CanRefresh { get; }
    }
}

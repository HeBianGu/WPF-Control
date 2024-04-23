// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System;

namespace H.Controls.FilterBox
{
    public interface IPropertyConditonable : IConditionable
    {
        event EventHandler<IConditionable> ConditionChanged;
    }
}

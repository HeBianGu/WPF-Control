// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase


using System;

namespace H.Controls.FilterBox
{
    public interface IPropertyConditonable : IConditionable
    {
        event EventHandler<IConditionable> ConditionChanged;
    }
}

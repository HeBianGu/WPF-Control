// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Controls.FilterBox
{
    public interface IPropertyConfidtion : IConditionable
    {
        IPropertyFilter Filter { get; set; }
    }
}

// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



using System.Reflection;

namespace H.Controls.FilterBox
{
    public interface IPropertyFilter : IDisplayFilter
    {
        bool IsSelected { get; set; }
        string PropertyName { get; set; }
        FilterOperate Operate { get; set; }
        PropertyInfo PropertyInfo { get; set; }
        IFilterable Copy();
    }
}

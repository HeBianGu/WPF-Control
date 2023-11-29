// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase


using H.Providers.Ioc;
using System.Reflection;

namespace H.Controls.FilterBox
{
    public interface IPropertyFilter : IDisplayFilter
    {
        bool IsSelected { get; set; }
        string Name { get; set; }
        FilterOperate Operate { get; set; }
        //PropertyInfo PropertyInfo { get; set; }
        //void SetValue(object value);
        //object GetValue();
        IFilter Copy();
    }
}

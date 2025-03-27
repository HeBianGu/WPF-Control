// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Controls.Form.PropertyItems.Base;


public interface IPropertyItem
{
    string Name { get; set; }
    int Order { get; set; }
    string TabGroup { get; set; }
    string GroupName { get; set; }
    PropertyInfo PropertyInfo { get; set; }
    object Obj { get; set; }
}

public interface IPropertyViewItem : IPropertyItem
{

}
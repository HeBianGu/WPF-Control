// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Message.Form;

public interface IFormOption
{
    string ExceptPropertyNames { get; set; }
    double MessageWidth { get; set; }
    object SelectObject { get; set; }
    bool UseArray { get; set; }
    bool UseAsync { get; set; }
    bool UseBoolen { get; set; }
    bool UseClass { get; set; }
    bool UseCommand { get; set; }
    bool UseCommandOnly { get; set; }
    bool UseDateTime { get; set; }
    bool UseDeclaredOnly { get; set; }
    bool UseDisplayOnly { get; set; }
    bool UseEnum { get; set; }
    bool UseEnumerator { get; set; }
    bool UseGroup { get; set; }
    string UseGroupNames { get; set; }
    IComparer<string> GroupOrderComparer { get; set; }
    bool UseInterface { get; set; }
    bool UseNull { get; set; }
    bool UseOrder { get; set; }
    bool UseOrderByName { get; set; }
    bool UseOrderByType { get; set; }
    bool UsePresenter { get; set; }
    bool UsePrimitive { get; set; }
    string UsePropertyNames { get; set; }
    bool UsePropertyView { get; set; }
    bool UseString { get; set; }
    bool UseTypeConverter { get; set; }
    bool UseTypeConverterOnly { get; set; }
    double MinWidth { get; set; }
    double Width { get; set; }
    Thickness Margin { get; set; }
    Thickness Padding { get; set; }
    HorizontalAlignment HorizontalAlignment { get; set; }
    VerticalAlignment VerticalAlignment { get; set; }
}

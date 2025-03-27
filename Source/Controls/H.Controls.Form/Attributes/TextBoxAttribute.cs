// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Controls.Form.Attributes;

public class TextBoxAttribute : Attribute
{
    //public TextBoxAttribute(TextWrapping textWrapping)
    //{
    //    this.TextWrapping = textWrapping;
    //}
    public TextWrapping TextWrapping { get; set; }
    public bool UseClear { get; set; }
}

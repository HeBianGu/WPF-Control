// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Windows;

namespace H.Controls.Form
{
    public class TextBoxAttribute : Attribute
    {
        //public TextBoxAttribute(TextWrapping textWrapping)
        //{
        //    this.TextWrapping = textWrapping;
        //}
        public TextWrapping TextWrapping { get; set; }
        public bool UseClear { get; set; }
    }
}

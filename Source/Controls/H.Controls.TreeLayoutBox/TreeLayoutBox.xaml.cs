// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace H.Controls.TreeLayoutBox
{
    public partial class TreeLayoutBox : TreeView
    {
        static TreeLayoutBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TreeLayoutBox), new FrameworkPropertyMetadata(typeof(TreeLayoutBox)));

           
        }
    }

    public partial class TreeLayoutBox
    {
        public static ComponentResourceKey VerticalKey => new ComponentResourceKey(typeof(TreeLayoutBox), "S.TreeLayoutBox.Vertical");

        public static  ComponentResourceKey HorizontalKey => new ComponentResourceKey(typeof(TreeLayoutBox), "S.TreeLayoutBox.Horizontal");
    }
}

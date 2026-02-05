// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Markup;

namespace H.Presenters.Design.PrintPresenter
{
    [Display(Name = "数据表")]
    [ContentProperty("ItemsSource")]
    [DefaultProperty("ItemsSource")]
    public class PrintDataGridPresenter : DataGridPresenterBase
    {
        public override void LoadDefault()
        {
            base.LoadDefault();
            this.GridHeaderBackground = Brushes.Black;
            this.GridHeaderForeground = Brushes.White;
            this.HorizontalGridLinesBrush = Brushes.LightGray;
            this.VerticalGridLinesBrush = Brushes.LightGray;
            this.GridForeground = Brushes.Black;
            this.GridBackground = Brushes.White;
            this.AlternatingRowBackground = Brushes.WhiteSmoke;
            this.ColumnHeaderHeight = 40;
            this.RowHeight = 35;
            this.GridBorderBrush = Brushes.LightGray;
            this.ColumnHorizontalContentAlignment = HorizontalAlignment.Left;
            this.CellHorizontalContentAlignment = HorizontalAlignment.Left;
        }
    }
}

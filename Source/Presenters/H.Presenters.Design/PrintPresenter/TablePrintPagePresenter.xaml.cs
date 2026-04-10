// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Mvvm.Commands;
using System.Collections;

namespace H.Presenters.Design.PrintPresenter
{
    //[Icon(fonticon)]
    [Display(Name = "表格分页")]
    public class TablePrintPagePresenter : PrintDataGridPresenter, IPrintPagePresenter
    {
        public TablePrintPagePresenter()
        {
            this.UseFixedPageHeight = false;
        }
        private string _title= "表格分页";
        [Display(Name = "标题", GroupName = "常用,数据")]
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

    }
}

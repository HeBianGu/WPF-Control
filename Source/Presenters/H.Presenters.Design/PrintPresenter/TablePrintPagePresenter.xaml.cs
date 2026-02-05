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
        private string _title;
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

        private string _user;
        [Display(Name = "用户", GroupName = "常用,数据")]
        public string User
        {
            get { return _user; }
            set
            {
                _user = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _date = DateTime.Now;
        [Display(Name = "日期", GroupName = "常用,数据")]
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                RaisePropertyChanged();
            }
        }

        private int _total;
        [Browsable(false)]
        public int Total
        {
            get { return _total; }
            set
            {
                _total = value;
                RaisePropertyChanged();
            }
        }

        [Display(Name = "删除")]
        public new RelayCommand DeleteCommand => new RelayCommand(x =>
        {
            if (x is ContentControl project)
            {
                Adorner adorner = project.GetParent<Adorner>();
                ItemsControl source = adorner.AdornedElement.GetParent<ItemsControl>();
                source.GetItemsSource<IList>().Remove(project.Content);
            }
        });

        //public object Clone()
        //{
        //    return this.CloneBy(x => x.GetCustomAttribute<BrowsableAttribute>()?.Browsable != false);
        //}
    }
}

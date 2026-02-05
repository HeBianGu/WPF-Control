// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Interfaces;
using H.Controls.Adorner.Adorner;
using H.Mvvm.Commands;
using System.Collections;
using System.Windows.Markup;

namespace H.Presenters.Design.PrintPresenter
{
    public interface IItemsControlPrintPagePresenter: IPanelDesignPresenter
    {
       
    }

    [ContentProperty("Presenters")]
    [DefaultProperty("Presenters")]
    [Display(Name = "列表分页")]
    public class ItemsControlPrintPagePresenter : PrintPagePresenterBase, IGetDropAdorner, IItemsControlPrintPagePresenter
    {
        private ObservableCollection<IDesignPresenter> _presenters = new ObservableCollection<IDesignPresenter>();
        [Display(Name = "数据列表")]
        [Browsable(false)]
        public ObservableCollection<IDesignPresenter> Presenters
        {
            get { return _presenters; }
            set
            {
                _presenters = value;
                RaisePropertyChanged();
            }
        }

        public void Delete(IDesignPresenter designPresenter)
        {
            if (this.Presenters.Contains(designPresenter))
                this.Presenters.Remove(designPresenter);
        }

        public Adorner GetDropAdorner(UIElement element)
        {
            ItemsControl items = element.GetChild<ItemsControl>();
            return new LineAdorner(items) { Dock = Dock.Bottom };
        }

        public void RemoveDropAdorner(UIElement element)
        {
            ItemsControl items = element.GetChild<ItemsControl>();
            items?.ClearAdorner(x => x.GetType() == typeof(LineAdorner));
        }
    }
}

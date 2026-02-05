// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Adorner.Adorner;
using H.Mvvm.Commands;
using System.Collections;
using System.Windows.Markup;

namespace H.Presenters.Design.PrintPresenter
{
    [ContentProperty("Presenters")]
    [DefaultProperty("Presenters")]
    [Display(Name = "列表分页")]
    public class ListPrintPagePresenter : PrintPagePresenterBase, IGetDropAdorner
    {
        public RelayCommand DeleteCommand => new RelayCommand(x =>
        {
            if (x is ContentControl project)
            {
                Adorner adorner = project.GetParent<Adorner>();
                ItemsControl source = adorner.AdornedElement.GetParent<ItemsControl>();
                source.GetItemsSource<IList>().Remove(project.Content);
            }
        });

        private ObservableCollection<object> _presenters = new ObservableCollection<object>();
        [Display(Name = "数据列表")]
        [Browsable(false)]
        public ObservableCollection<object> Presenters
        {
            get { return _presenters; }
            set
            {
                _presenters = value;
                RaisePropertyChanged();
            }
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

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
    public interface IContentPrintPagePresenter: IContentDesignPresenter
    {
       
    }

    [ContentProperty("Content")]
    [DefaultProperty("Content")]
    [Display(Name = "内容分页")]
    public class ContentPrintPagePresenter : PrintPagePresenterBase, IGetDropAdorner, IContentPrintPagePresenter
    {
        private IDesignPresenter _content;
        [Display(Name = "内容")]
        [Browsable(false)]
        public IDesignPresenter Content
        {
            get { return _content; }
            set
            {
                _content = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<IDesignPresenter> Presenters => this.Content.ToEnumerable().ToObservable();
        public void Delete(IDesignPresenter designPresenter)
        {
           if(designPresenter==this.Content)
                this.Content = null;
        }

        public Adorner GetDropAdorner(UIElement element)
        {
            //UIElement items = element.GetChild<UIElement>();
            return new LineAdorner(element) { Dock = Dock.Bottom };
        }

        public void RemoveDropAdorner(UIElement element)
        {
            //UIElement items = element.GetChild<UIElement>();
            element?.ClearAdorner(x => x.GetType() == typeof(LineAdorner));
        }
    }
}

// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Common;
using System.Windows.Input;

namespace H.Presenters.Design.PrintPresenter
{
    public class PrintPagePresenterBase : DeletableDesignPresenterBase, IPrintPagePresenter, ICloneable
    {
        public PrintPagePresenterBase()
        {
            ////this.PageMargin = PrintBoxMessage.Instance.PageMargin;
            //this.Foreground = PrintBoxMessage.Instance.Foreground;
            //this.Background = PrintBoxMessage.Instance.Background;
            //this.FontSize = PrintBoxMessage.Instance.FontSize;
            //this.BorderBrush = PrintBoxMessage.Instance.BorderBrush;
        }

        public object Clone()
        {
          return this.ClonePrimitive();
        }

        public virtual void Refresh()
        {

        }
    }
}

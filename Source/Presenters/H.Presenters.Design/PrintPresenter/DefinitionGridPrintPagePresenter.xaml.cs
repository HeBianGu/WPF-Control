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
using H.Presenters.Design.Presenter;
using System.Collections;
using System.Windows.Markup;

namespace H.Presenters.Design.PrintPresenter
{
    [ContentProperty("Content")]
    [DefaultProperty("Content")]
    [Display(Name = "内容分页")]
    public class DefinitionGridPrintPagePresenter : PrintPagePresenterBase, IPrintPagePresenter
    {
        public DefinitionGridPrintPagePresenter()
        {
            this.UseFixedPageHeight = true;
        }
        private DefinitionGridPresenter _content = new DefinitionGridPresenter() { UseTool = true };
        [Display(Name = "内容")]
        [Browsable(false)]
        public DefinitionGridPresenter Content
        {
            get { return _content; }
            set
            {
                _content = value;
                RaisePropertyChanged();
            }
        }
    }
}

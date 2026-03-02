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
using System.Text.Json.Serialization;
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
        private PrintDefinitionGridPresenter _content = new PrintDefinitionGridPresenter();
        [Display(Name = "内容")]
        [Browsable(false)]
        public PrintDefinitionGridPresenter Content
        {
            get { return _content; }
            set
            {
                _content = value;
                RaisePropertyChanged();
            }
        }
        [JsonIgnore]
        [Browsable(false)]
        public IEnumerable<IDesignPresenter> Presenters => this.Content.ToEnumerable();

        public override object Clone()
        {
            var r = base.Clone();
            if (r is DefinitionGridPrintPagePresenter presenter)
                presenter.Content = this.Content.Clone() as PrintDefinitionGridPresenter;
            return r;
        }


    }

    public interface IPrintPageContentPresenter
    {

    }

    public class PrintDefinitionGridPresenter : DefinitionGridPresenter, IPrintPageContentPresenter
    {

    }
}

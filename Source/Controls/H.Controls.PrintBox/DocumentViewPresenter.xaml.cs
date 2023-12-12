// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase


using H.Providers.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace H.Controls.PrintBox
{
    public class DocumentViewPresenter : NotifyPropertyChangedBase
    {
        private IDocumentPaginatorSource _document;
        public IDocumentPaginatorSource Document
        {
            get { return _document; }
            set
            {
                _document = value;
                RaisePropertyChanged();
            }
        }
    }
}

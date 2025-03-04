// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Mvvm.ViewModels.Base;
using System.Windows.Documents;

namespace H.Controls.PrintBox
{
    public class DocumentViewPresenter : BindableBase
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

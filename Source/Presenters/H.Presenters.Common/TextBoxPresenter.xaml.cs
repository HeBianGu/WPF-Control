using H.Mvvm;
using H.Mvvm.Attributes;
using H.Mvvm.ViewModels.Base;

namespace H.Presenters.Common
{
    [Icon("\xE70F")]
    public class TextBoxPresenter:DisplayBindableBase
    {
        public string Text { get; set; }
    }
}

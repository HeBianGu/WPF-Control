using H.Mvvm.ViewModels.Base;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace H.Presenters.Common
{
    public interface ISelectItemPresenter
    {
        string DisplayMemberPath { get; set; }
        object SelectedItem { get; set; }
        IEnumerable Source { get; set; }
    }

    public class SelectItemPresenter : DisplayBindableBase, ISelectItemPresenter
    {
        public SelectItemPresenter()
        {

        }
        public SelectItemPresenter(IEnumerable source, object selectedItem)
        {
            this.Source = source;
            this.SelectedItem = selectedItem;
        }

        private IEnumerable _source;
        public IEnumerable Source
        {
            get { return _source; }
            set
            {
                _source = value;
                RaisePropertyChanged();
            }
        }

        private string _displayMemberPath;
        public string DisplayMemberPath
        {
            get { return _displayMemberPath; }
            set
            {
                _displayMemberPath = value;
                RaisePropertyChanged();
            }
        }

        private object _selectedItem;
        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }
    }


    public static partial class DialogServiceExtension
    {
        public static async Task<bool?> ShowSelectItem(this IDialogMessageService service, Action<ISelectItemPresenter> option, Action<ISelectItemPresenter> sumitAction, Action<IDialog> builder = null, Func<bool> canSumit = null)
        {
            return await service.ShowDialog<SelectItemPresenter>(option, sumitAction, builder, canSumit);
        }
    }
}

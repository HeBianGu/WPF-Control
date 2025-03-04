using H.Mvvm.ViewModels.Base;
using System.Collections;

namespace H.Presenters.Common
{
    public class SelectItemPresenter : DisplayBindableBase
    {
        public SelectItemPresenter(IEnumerable source, object selectedItem)
        {
            this.Source = source;
            this.SelectedItem = selectedItem;
        }

        private IEnumerable _source;
        /// <summary> 说明  </summary>
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
        /// <summary> 说明  </summary>
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
        /// <summary> 说明  </summary>
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
}

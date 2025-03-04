using H.Mvvm.ViewModels.Base;
using System.Collections.ObjectModel;

namespace H.Controls.TagBox
{
    public class TagsPresenter : BindableBase
    {
        private ObservableCollection<ITag> _collection = new ObservableCollection<ITag>();
        /// <summary> 说明  </summary>
        public ObservableCollection<ITag> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged();
            }
        }

        private string _groupName;
        /// <summary> 说明  </summary>
        public string GroupName
        {
            get { return _groupName; }
            set
            {
                _groupName = value;
                RaisePropertyChanged();
            }
        }
    }
}

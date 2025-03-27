using H.Mvvm;
using H.Mvvm.Commands;
using H.Mvvm.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Linq;

namespace H.Test.SideMenu
{
    public class MainViewModel : Bindable
    {
        public MainViewModel()
        {
            this.Collection.Add(new StudentManager());
            this.Collection.Add(new TeatherManager());
            this.Collection.Add(new LogisticManager());
            this.Collection.Add(new LeaderManager());

            this.Collection.Add(new UserManager());
            this.Collection.Add(new RoleManager());
            this.Collection.Add(new AuthorManager());

            this.Collection.Add(new DebugManager());
            this.Collection.Add(new ErrorManager());
            this.Collection.Add(new FatalManager());
            this.Collection.Add(new InfoManager());
            this.Collection.Add(new WarnManager());

        }
        private ObservableCollection<IManager> _collection = new ObservableCollection<IManager>();
        public ObservableCollection<IManager> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged();
            }
        }


        private IManager _selectedManager;
        public IManager SelectedManager
        {
            get { return _selectedManager; }
            set
            {
                _selectedManager = value;
                if (_selectedManager != null)
                    _selectedManager.IsVisibleInTab = true;
                RaisePropertyChanged();
            }
        }

        public RelayCommand HideItemCommand => new RelayCommand(e =>
        {
            if (e is IManager manager)
                manager.IsVisibleInTab = false;
        }, x =>
        {
            return this.Collection.Count(x => x.IsVisibleInTab) > 1;
        });

    }

}

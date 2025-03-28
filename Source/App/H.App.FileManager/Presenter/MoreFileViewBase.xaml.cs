global using System.Collections.ObjectModel;
global using System.IO;
global using H.Mvvm.ViewModels;
global using H.Services.Message;
global using H.Services.Message.Dialog;

namespace H.App.FileManager
{
    public abstract class MoreFileViewBase : ModelBindable<fm_dd_file>
    {
        protected MoreFileViewBase(fm_dd_file t) : base(t)
        {
            this.More = this.CreateMore().ToObservable();
            this.SelectedItem = this.More?.FirstOrDefault();
        }

        private ObservableCollection<IFileView> _more = new ObservableCollection<IFileView>();
        public ObservableCollection<IFileView> More
        {
            get { return _more; }
            set
            {
                _more = value;
                RaisePropertyChanged();
            }
        }

        private IFileView _selectedItem;
        public IFileView SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }


        public RelayCommand SelectionChangedCommand => new RelayCommand(e=>
        {
            if (e is fm_dd_file file)
            {
                file.Watched = true;
                file.LastPlayTime = DateTime.Now;
                file.PlayCount = file.PlayCount + 1;
            }
        });

        public virtual RelayCommand MouseDoubleClickCommand => new RelayCommand(async x =>
        {
            if (x is fm_dd_file file)
            {
                var view = Ioc.GetService<IFileToViewService>().ToView(file);
                await IocMessage.Dialog.Show(view, x => x.DialogButton = DialogButton.None);
            }
        });

        protected virtual IEnumerable<IFileView> CreateMore()
        {
            List<IFileView> mores = new List<IFileView>();
            IEnumerable<fm_dd_file> files = FileRepositoryBindable.Instance.Collection.Select(x => x.Model);

            var t = this.Model;
            var filtToView = Ioc.GetService<IFileToViewService>();
            mores.Add(filtToView.ToView(this.Model));
            {
                IEnumerable<fm_dd_file> finds = files.Where(x => t.FavoritePath != null && x.FavoritePath != null && (x.FavoritePath?.StartsWith(t.FavoritePath) == true || t.FavoritePath?.StartsWith(x.FavoritePath) == true));
                mores.AddRange(finds.Select(l => filtToView.ToView(l, "同收藏夹")));
            }

            {
                IEnumerable<fm_dd_file> finds = files.Where(x => Path.GetDirectoryName(x.Url) == Path.GetDirectoryName(t.Url) && x.Url != t.Url).OrderBy(x =>
                {
                    if (x.Name == this.Model.Name)
                        return -1;
                    if (x.Extend == this.Model.Extend)
                        return 0;
                    if (x.Url.IsImage() == this.Model.Extend.IsImage() || x.Url.IsVedio() == this.Model.Extend.IsVedio() || x.Url.IsAudio() == this.Model.Extend.IsAudio())
                        return 1;
                    return 2;
                });
                mores.AddRange(finds.Select(l => filtToView.ToView(l, "同文件夹")));
            }

            {
                IEnumerable<ITag> ctags = IocTagService.Instance.ToTags(t.Tags);
                IEnumerable<fm_dd_file> finds = files.Where(x =>
                {
                    IEnumerable<ITag> xtags = IocTagService.Instance.ToTags(x.Tags);
                    IEnumerable<ITag> jtags = ctags.Join(xtags, l => l.Name, l => l.Name, (l, k) => l);
                    if (jtags.Count() > 0)
                        return true;

                    return false;
                });
                mores.AddRange(finds.Select(l => filtToView.ToView(l, "同标签")));
            }


            {
                int playCount = files.Max(x => x.PlayCount);
                IEnumerable<fm_dd_image> finds = files.OfType<fm_dd_image>().Where(x => x.PlayCount == playCount);
                mores.AddRange(finds.Select(l => filtToView.ToView(l, "播放最多")).Take(5));
            }

            {
                int score = files.Max(x => x.Score);
                IEnumerable<fm_dd_file> finds = files.Where(x => x.Score == score);
                mores.AddRange(finds.Select(l => filtToView.ToView(l, "评分最高")).Take(5));
            }

            return mores;
        }


        public RelayCommand NextCommand => new RelayCommand(l =>
        {
            if (this.SelectedItem == null)
            {
                this.SelectedItem = this.More.FirstOrDefault();
                return;
            }

            int index = this.More.IndexOf(this.SelectedItem);
            if (index >= this.More.Count - 1)
                this.SelectedItem = this.More.FirstOrDefault();
            else
                this.SelectedItem = this.More[index + 1];

        }, x => this.More.Count > 0);


        public RelayCommand PreviousCommand => new RelayCommand(l =>
        {
            if (this.SelectedItem == null)
            {
                this.SelectedItem = this.More.LastOrDefault();
                return;
            }

            int index = this.More.IndexOf(this.SelectedItem);
            if (index <= 0)
                this.SelectedItem = this.More.LastOrDefault();
            else
                this.SelectedItem = this.More[index - 1];
        }, x => this.More.Count > 0);
    }
}

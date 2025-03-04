using H.Mvvm;
using System;
using System.IO;
using System.Linq;

namespace H.App.FileManager
{
    //public class MoreVideoView : ModelViewModel<fm_dd_video>
    //{
    //    public MoreVideoView(fm_dd_video t) : base(t)
    //    {

    //        //List<VideoView> moreVideos = new List<VideoView>();
    //        //this.SelectedItem = new VideoView(t);
    //        //moreVideos.Add(this.SelectedItem);
    //        //IEnumerable<fm_dd_video> videos = FileRepositoryViewModel.Instance.Collection.Select(x => x.Model).OfType<fm_dd_video>().Where(x => x != t);
    //        //if (videos.Count() == 0)
    //        //    return;
    //        //{
    //        //    IEnumerable<fm_dd_video> finds = videos.Where(x => t.FavoritePath != null && x.FavoritePath != null && (x.FavoritePath?.StartsWith(t.FavoritePath) == true || t.FavoritePath?.StartsWith(x.FavoritePath) == true));
    //        //    moreVideos.AddRange(finds.Select(l => new VideoView(l) { Descption = "同收藏夹" }));
    //        //}

    //        //{
    //        //    IEnumerable<fm_dd_video> finds = videos.Where(x =>
    //        //    {
    //        //        IEnumerable<ITag> ctags = IocTagService.Instance.ToTags(t.Object);
    //        //        IEnumerable<ITag> xtags = IocTagService.Instance.ToTags(x.Object);
    //        //        IEnumerable<ITag> jtags = ctags.Join(xtags, l => l.Name, l => l.Name, (l, k) => l);
    //        //        if (jtags.Count() > 0)
    //        //            return true;
    //        //        return false;
    //        //    });
    //        //    moreVideos.AddRange(finds.Select(l => new VideoView(l) { Descption = "同对象" }));
    //        //}

    //        //{
    //        //    IEnumerable<fm_dd_video> finds = videos.Where(x =>
    //        //    {
    //        //        IEnumerable<ITag> ctags = IocTagService.Instance.ToTags(t.Tags);
    //        //        IEnumerable<ITag> xtags = IocTagService.Instance.ToTags(x.Tags);
    //        //        IEnumerable<ITag> jtags = ctags.Join(xtags, l => l.Name, l => l.Name, (l, k) => l);
    //        //        if (jtags.Count() > 0)
    //        //            return true;

    //        //        return false;
    //        //    });
    //        //    moreVideos.AddRange(finds.Select(l => new VideoView(l) { Descption = "同标签" }));
    //        //}

    //        //{
    //        //    IEnumerable<fm_dd_video> finds = videos.Where(x =>
    //        //    {
    //        //        IEnumerable<ITag> ctags = IocTagService.Instance.ToTags(t.Articulation);
    //        //        IEnumerable<ITag> xtags = IocTagService.Instance.ToTags(x.Articulation);
    //        //        IEnumerable<ITag> jtags = ctags.Join(xtags, l => l.Name, l => l.Name, (l, k) => l);
    //        //        if (jtags.Count() > 0)
    //        //            return true;

    //        //        return false;
    //        //    });
    //        //    moreVideos.AddRange(finds.Select(l => new VideoView(l) { Descption = "同清晰度" }));
    //        //}

    //        //{
    //        //    IEnumerable<fm_dd_video> finds = videos.Where(x => Path.GetDirectoryName(x.Url) == Path.GetDirectoryName(t.Url) && x.Url.Equals(t.Url) == false);
    //        //    moreVideos.AddRange(finds.Select(l => new VideoView(l) { Descption = "同文件夹" }));
    //        //}

    //        //{
    //        //    IEnumerable<fm_dd_video> finds = videos.Where(x =>
    //        //    {
    //        //        IEnumerable<ITag> ctags = IocTagService.Instance.ToTags(t.Area);
    //        //        IEnumerable<ITag> xtags = IocTagService.Instance.ToTags(x.Area);
    //        //        IEnumerable<ITag> jtags = ctags.Join(xtags, l => l.Name, l => l.Name, (l, k) => l);
    //        //        if (jtags.Count() > 0)
    //        //            return true;

    //        //        return false;
    //        //    });
    //        //    moreVideos.AddRange(finds.Select(l => new VideoView(l) { Descption = "同国家" }));
    //        //}

    //        //{
    //        //    int playCount = videos.Max(x => x.PlayCount);
    //        //    IEnumerable<fm_dd_video> finds = videos.Where(x => x.PlayCount == playCount);
    //        //    moreVideos.AddRange(finds.Select(l => new VideoView(l) { Descption = "播放最多" }).Take(5));
    //        //}

    //        //{
    //        //    int score = videos.Max(x => x.Score);
    //        //    IEnumerable<fm_dd_video> finds = videos.Where(x => x.Score == score);
    //        //    moreVideos.AddRange(finds.Select(l => new VideoView(l) { Descption = "评分最高" }).Take(5));
    //        //}

    //        //this.More = moreVideos.ToObservable();
    //    }

    //    //private ObservableCollection<VideoView> _more = new ObservableCollection<VideoView>();
    //    //public ObservableCollection<VideoView> More
    //    //{
    //    //    get { return _more; }
    //    //    set
    //    //    {
    //    //        _more = value;
    //    //        RaisePropertyChanged();
    //    //    }
    //    //}


    //    //private VideoView _selectedItem;
    //    //public VideoView SelectedItem
    //    //{
    //    //    get { return _selectedItem; }
    //    //    set
    //    //    {
    //    //        _selectedItem = value;
    //    //        RaisePropertyChanged();
    //    //    }
    //    //}

    //    public RelayCommand ShootCutCommand => new RelayCommand(e=>
    //    {
    //        if (e is RoutedEventArgs<Tuple<string, long>> arg)
    //        {
    //            this.Model.Images.Add(new fm_dd_video_image()
    //            {
    //                Name = Path.GetFileNameWithoutExtension(arg.Entity.Item1),
    //                Url = arg.Entity.Item1,
    //                TimeStamp = arg.Entity.Item2,
    //                Extend = Path.GetExtension(arg.Entity.Item1)
    //            });

    //            this.Model.Images.OrderBy(x => x.TimeStamp);
    //        }
    //    });

    //    //public RelayCommand SelectionChangedCommand => new RelayCommand(e=>
    //    //{
    //    //    if (e is fm_dd_file file)
    //    //    {
    //    //        file.Watched = true;
    //    //        file.LastPlayTime = DateTime.Now;
    //    //        file.PlayCount = file.PlayCount + 1;
    //    //    }
    //    //});

    //    //public RelayCommand MouseDoubleClickCommand => new RelayCommand(async (s, e) =>
    //    //{
    //    //    if (e is fm_dd_file file)
    //    //    {
    //    //        var view = Ioc.GetService<IFileToViewService>().ToView(file);
    //    //        await IocMessage.Dialog.Show(view);
    //    //    }
    //    //});
    //}

    public class VideoView : FileView<fm_dd_video>
    {
        public VideoView(fm_dd_video t) : base(t)
        {
        }

        public RelayCommand ShootCutCommand => new RelayCommand(e=>
        {
            if (e is RoutedEventArgs<Tuple<string, long>> arg)
            {
                this.Model.Images.Add(new fm_dd_video_image()
                {
                    Name = Path.GetFileNameWithoutExtension(arg.Entity.Item1),
                    Url = arg.Entity.Item1,
                    TimeStamp = arg.Entity.Item2,
                    Extend = Path.GetExtension(arg.Entity.Item1)
                });

                this.Model.Images.OrderBy(x => x.TimeStamp);
            }
        });
    }
}

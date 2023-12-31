
using H.Controls.TagBox;
using H.Providers.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace H.App.FileManager
{
    public interface IFileView
    {

    }

    public class ImageView : ModelViewModel<fm_dd_image>, IFileView
    {
        public ImageView(fm_dd_image t) : base(t)
        {
            List<MoreImage> mores = new List<MoreImage>();

            IEnumerable<fm_dd_image> videos = FileRepositoryViewModel.Instance.Collection.Select(x => x.Model).OfType<fm_dd_image>();
            if (videos.Count() == 0)
                return;

            {
                IEnumerable<fm_dd_image> finds = videos.Where(x => t.FavoritePath != null && x.FavoritePath != null && (x.FavoritePath?.StartsWith(t.FavoritePath) == true || t.FavoritePath?.StartsWith(x.FavoritePath) == true));
                mores.AddRange(finds.Select(l => new MoreImage(l) { Descption = "同收藏夹" }));
            }

            {
                IEnumerable<fm_dd_image> finds = videos.Where(x =>
                {
                    IEnumerable<ITag> ctags = IocTagService.Instance.ToTags(t.Object);
                    IEnumerable<ITag> xtags = IocTagService.Instance.ToTags(x.Object);
                    IEnumerable<ITag> jtags = ctags.Join(xtags, l => l.Name, l => l.Name, (l, k) => l);
                    if (jtags.Count() > 0)
                        return true;

                    return false;
                });
                mores.AddRange(finds.Select(l => new MoreImage(l) { Descption = "同对象" }));
            }

            {
                IEnumerable<fm_dd_image> finds = videos.Where(x =>
                {
                    IEnumerable<ITag> ctags = IocTagService.Instance.ToTags(t.Tags);
                    IEnumerable<ITag> xtags = IocTagService.Instance.ToTags(x.Tags);
                    IEnumerable<ITag> jtags = ctags.Join(xtags, l => l.Name, l => l.Name, (l, k) => l);
                    if (jtags.Count() > 0)
                        return true;

                    return false;
                });
                mores.AddRange(finds.Select(l => new MoreImage(l) { Descption = "同标签" }));
            }

            {
                IEnumerable<fm_dd_image> finds = videos.Where(x =>
                {
                    IEnumerable<ITag> ctags = IocTagService.Instance.ToTags(t.Articulation);
                    IEnumerable<ITag> xtags = IocTagService.Instance.ToTags(x.Articulation);
                    IEnumerable<ITag> jtags = ctags.Join(xtags, l => l.Name, l => l.Name, (l, k) => l);
                    if (jtags.Count() > 0)
                        return true;

                    return false;
                });
                mores.AddRange(finds.Select(l => new MoreImage(l) { Descption = "同清晰度" }));
            }

            {
                IEnumerable<fm_dd_image> finds = videos.Where(x => Path.GetDirectoryName(x.Url) == Path.GetDirectoryName(t.Url) && x.Url != t.Url);
                mores.AddRange(finds.Select(l => new MoreImage(l) { Descption = "同文件夹" }));
            }

            {
                IEnumerable<fm_dd_image> finds = videos.Where(x =>
                {
                    IEnumerable<ITag> ctags = IocTagService.Instance.ToTags(t.Area);
                    IEnumerable<ITag> xtags = IocTagService.Instance.ToTags(x.Area);
                    IEnumerable<ITag> jtags = ctags.Join(xtags, l => l.Name, l => l.Name, (l, k) => l);
                    if (jtags.Count() > 0)
                        return true;

                    return false;
                });
                mores.AddRange(finds.Select(l => new MoreImage(l) { Descption = "同国家" }));
            }

            {
                int playCount = videos.Max(x => x.PlayCount);
                IEnumerable<fm_dd_image> finds = videos.Where(x => x.PlayCount == playCount);
                mores.AddRange(finds.Select(l => new MoreImage(l) { Descption = "播放最多" }).Take(5));
            }

            {
                int score = videos.Max(x => x.Score);
                IEnumerable<fm_dd_image> finds = videos.Where(x => x.Score == score);
                mores.AddRange(finds.Select(l => new MoreImage(l) { Descption = "评分最高" }).Take(5));
            }

            this.More = mores.ToObservable();
        }

        private ObservableCollection<MoreImage> _more = new ObservableCollection<MoreImage>();
        public ObservableCollection<MoreImage> More
        {
            get { return _more; }
            set
            {
                _more = value;
                RaisePropertyChanged();
            }
        }
    }


    public class MoreImage : SelectViewModel<fm_dd_image>
    {
        public MoreImage(fm_dd_image t) : base(t)
        {
        }

        private string _descption;
        public string Descption
        {
            get { return _descption; }
            set
            {
                _descption = value;
                RaisePropertyChanged();
            }
        }

    }
}

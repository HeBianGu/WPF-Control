
using H.Controls.TagBox;
using H.Extensions.Common;
using H.Services.Common;
using H.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace H.App.FileManager
{
    public class MoreImageView : MoreFileViewBase
    {
        public MoreImageView(fm_dd_image t) : base(t)
        {

        }

        private bool _useImageOnly;
        public bool UseImageOnly
        {
            get { return _useImageOnly; }
            set
            {
                _useImageOnly = value;
                RaisePropertyChanged();
            }
        }


        protected override IEnumerable<IFileView> CreateMore()
        {
            List<IFileView> mores = new List<IFileView>();
            IEnumerable<fm_dd_file> files = FileRepositoryBindable.Instance.Collection.Select(x => x.Model);

            var t = this.Model as fm_dd_image;
            if (this.UseImageOnly)
                files = files.OfType<fm_dd_image>();
            var filtToView = Ioc.GetService<IFileToViewService>();
            mores.Add(filtToView.ToView(this.Model));
            {
                IEnumerable<fm_dd_file> finds = files.Where(x => t.FavoritePath != null && x.FavoritePath != null && (x.FavoritePath?.StartsWith(t.FavoritePath) == true || t.FavoritePath?.StartsWith(x.FavoritePath) == true));
                mores.AddRange(finds.Select(l => filtToView.ToView(l, "同收藏夹")));
            }

            {
                IEnumerable<fm_dd_image> finds = files.OfType<fm_dd_image>().Where(x =>
                {
                    IEnumerable<ITag> ctags = IocTagService.Instance.ToTags(t.Object);
                    IEnumerable<ITag> xtags = IocTagService.Instance.ToTags(x.Object);
                    IEnumerable<ITag> jtags = ctags.Join(xtags, l => l.Name, l => l.Name, (l, k) => l);
                    if (jtags.Count() > 0)
                        return true;

                    return false;
                });
                mores.AddRange(finds.Select(l => filtToView.ToView(l, "同对象")));
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
                IEnumerable<ITag> ctags = IocTagService.Instance.ToTags(t.Articulation);
                IEnumerable<fm_dd_image> finds = files.OfType<fm_dd_image>().Where(x =>
                {
                    IEnumerable<ITag> xtags = IocTagService.Instance.ToTags(x.Articulation);
                    IEnumerable<ITag> jtags = ctags.Join(xtags, l => l.Name, l => l.Name, (l, k) => l);
                    if (jtags.Count() > 0)
                        return true;

                    return false;
                });
                mores.AddRange(finds.Select(l => filtToView.ToView(l, "同清晰度")));
            }

            {
                IEnumerable<ITag> ctags = IocTagService.Instance.ToTags(t.Area);
                IEnumerable<fm_dd_image> finds = files.OfType<fm_dd_image>().Where(x =>
                {
                    IEnumerable<ITag> xtags = IocTagService.Instance.ToTags(x.Area);
                    IEnumerable<ITag> jtags = ctags.Join(xtags, l => l.Name, l => l.Name, (l, k) => l);
                    if (jtags.Count() > 0)
                        return true;

                    return false;
                });
                mores.AddRange(finds.Select(l => filtToView.ToView(l, "同国家")));
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


        public override RelayCommand MouseDoubleClickCommand => new RelayCommand(async x =>
        {
            if (x is fm_dd_file file)
            {
                //var view = Ioc.GetService<IFileToViewService>().ToView(file);
                await IocMessage.Dialog.Show(this, x => x.DialogButton = DialogButton.None);
            }
        });
    }
}

using H.Extensions.Torrent;
using H.Providers.Mvvm;

namespace H.App.FileManager
{
    public class TorrentView : FileView<fm_dd_file>
    {
        private TorrentInfo _torrentInfo;
        public TorrentView(fm_dd_file t) : base(t)
        {
            //TorrentInfo = IocTorrentService.Instance.CreateInfo(t.Url);
        }

        public TorrentInfo TorrentInfo => this._torrentInfo == null ? this._torrentInfo = IocTorrentService.Instance.CreateInfo(this.Model.Url) : this._torrentInfo;
    }
}

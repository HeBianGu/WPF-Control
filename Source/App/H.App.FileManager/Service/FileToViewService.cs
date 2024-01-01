using H.Extensions.Torrent;

namespace H.App.FileManager
{
    public class FileToViewService : IFileToViewService
    {
        public object ToView(fm_dd_file file)
        {
            if (file is fm_dd_video video)
                return new VideoView(video);
            if (file is fm_dd_image image)
                return new ImageView(image);
            if (file.Extend == ".torrent")
                return IocTorrentService.Instance.CreateInfo(file.Url);
            return new FileView(file);
        }
    }
}

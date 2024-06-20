namespace H.App.FileManager
{
    public class FileToViewService : IFileToViewService
    {
        public IFileView ToView(fm_dd_file file, string desc = null)
        {
            if (file is fm_dd_video video)
                return new VideoView(video) { Description = desc };
            if (file is fm_dd_image image)
                return new ImageView(image) { Description = desc };
            if (file.Extend == ".torrent")
                return new TorrentView(file) { Description = desc };
            return new FileView(file) { Description = desc };
        }
    }
}

namespace H.App.FileManager
{
    public class FileToMoreViewService : IFileToMoreViewService
    {
        public object ToView(fm_dd_file file)
        {
            if (file is fm_dd_video video)
                return new MoreImageView(video);
            if (file is fm_dd_image image)
                return new MoreImageView(image);
            return new MoreFileView(file);
        }
    }
}

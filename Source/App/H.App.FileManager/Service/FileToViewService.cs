namespace H.App.FileManager
{
    public class FileToViewService : IFileToViewService
    {
        public IFileView ToView(fm_dd_file file)
        {
            if (file is fm_dd_video video)
            {
                return new VideoView(video);
            }
            if (file is fm_dd_image image)
            {
                return new ImageView(image);
            }
            return new FileView(file);
        }
    }
}

using H.Extensions.Common;
using System.IO;

namespace H.App.FileManager
{
    public class FileToEntityService : IFileToEntityService
    {
        public fm_dd_file ToEntity(string file)
        {
            fm_dd_file result = new fm_dd_file();
            if (file.IsImage())
            {
                fm_dd_image image = new fm_dd_image();
                image.PixelHeight = 200;
                image.PixelWidth = 400;
                result = image;
            }
            else if (file.IsVedio())
            {
                result = new fm_dd_video();
            }
            else if (file.IsAudio())
            {
                result = new fm_dd_audio();
            }

            result.Name = Path.GetFileNameWithoutExtension(file);
            result.Url = Path.GetFullPath(file);
            result.Extend = Path.GetExtension(file);
            result.Size = new FileInfo(file).Length;
            return result;
        }
    }
}

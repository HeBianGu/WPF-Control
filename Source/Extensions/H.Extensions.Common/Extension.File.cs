// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
    public static class FileExtension
    {
        public const string ImageExtension = "jpg jpeg png gif pdf tga tif svg tga bmp dds eps webp";
        public const string VedioExtension = "wmv asf asx rm rmvb mpg mpeg mpe 3gp mov mp4 m4v avi dat mkv flv vob dat bdmv";
        public const string AudioExtension = "mp3 mpeg3 wav wave flac fla aiff aif aifc aac adt adts m2ts mp2 3g2 3gp2 3gp 3gpp m4a m4v mp4v mp4 mov asf wm wmv wma mp1 avi ac3 ec3";

        public static string[] ImageExtensions => ImageExtension.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        public static string[] VedioExtensions => VedioExtension.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        public static string[] AudioExtensions => AudioExtension.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

        public static IEnumerable<string> GetAllImages(this string foldPath)
        {
            var files = foldPath.GetAllFiles(x => x.FullName.IsImage());
            return files;
        }

        public static bool IsImage(this string filePath)
        {
            return ImageExtensions.Any(x => x.Equals(Path.GetExtension(filePath).Trim('.'), StringComparison.OrdinalIgnoreCase));
        }

        public static IEnumerable<string> GetAllVedios(string foldPath)
        {
            var files = foldPath.GetAllFiles(x => x.FullName.IsVedio());
            return files;
        }


        public static bool IsVedio(this string filePath)
        {
            return VedioExtensions.Any(x => x.Equals(Path.GetExtension(filePath).Trim('.'), StringComparison.OrdinalIgnoreCase));
        }

        public static IEnumerable<string> GetAllAudios(string foldPath)
        {
            var files = foldPath.GetAllFiles(x => x.FullName.IsAudio());
            return files;
        }
        public static bool IsAudio(this string filePath)
        {
            return AudioExtensions.Any(x => x.Equals(Path.GetExtension(filePath).Trim('.'), StringComparison.OrdinalIgnoreCase));
        }
        public static string ImageExtensionsFilter => $"图像文件({ImageExtension}) |{GetExtension(ImageExtensions)}|所有文件(*.*)|*.*";
        public static string VedioExtensionsFilter => $"视频文件({VedioExtension}) |{GetExtension(VedioExtensions)}|所有文件(*.*)|*.*";
        public static string AudioExtensionsFilter => $"音频文件({AudioExtension}) |{GetExtension(AudioExtensions)}|所有文件(*.*)|*.*";

        public static string GetExtension(string[] extensions)
        {
            return extensions.Select(x => $" *.{x};").Aggregate((x, y) => x + y);
        }

        /// <summary> 获取当前文件夹下所有匹配的文件 </summary>
        public static List<string> GetAllFiles(this string dirPath, Predicate<FileInfo> match = null)
        {
            List<string> ss = new List<string>();
            if (!Directory.Exists(dirPath))
                return ss;
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            foreach (FileSystemInfo d in dir.GetFileSystemInfos())
            {
                if (d is DirectoryInfo)
                {
                    DirectoryInfo dd = d as DirectoryInfo;
                    ss.AddRange(dd.FullName.GetAllFiles(match));
                }
                else if (d is FileInfo)
                {
                    FileInfo dd = d as FileInfo;
                    if (match == null || match(dd))
                        ss.Add(d.FullName);
                }
            }
            return ss;
        }
    }
}

// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace H.Extensions.Common
{
    public static partial class FileExtension
    {
        public const string ImageExtension = "jpg jpeg png gif pdf tga tif svg tga bmp dds eps webp";
        public const string VedioExtension = "wmv asf asx rm rmvb mpg mpeg mpe 3gp mov mp4 m4v avi dat mkv flv vob dat bdmv";
        public const string AudioExtension = "mp3 mpeg3 wav wave flac fla aiff aif aifc aac adt adts m2ts mp2 3g2 3gp2 3gp 3gpp m4a m4v mp4v mp4 mov asf wm wmv wma mp1 avi ac3 ec3";

        public static string[] ImageExtensions => ImageExtension.Split(new char[] { ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
        public static string[] VedioExtensions => VedioExtension.Split(new char[] { ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
        public static string[] AudioExtensions => AudioExtension.Split(new char[] { ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);

        public static IEnumerable<string> GetAllImages(this string foldPath)
        {
            var files = foldPath.ToDirectoryEx().GetAllFiles(x => x.FullName.IsImage());
            return files;
        }

        public static bool IsImage(this string filePath)
        {
            return ImageExtensions.Any(x => x.Equals(Path.GetExtension(filePath).Trim('.'), StringComparison.OrdinalIgnoreCase));
        }

        public static IEnumerable<string> GetAllVedios(string foldPath)
        {
            var files = foldPath.ToDirectoryEx().GetAllFiles(x => x.FullName.IsVedio());
            return files;
        }


        public static bool IsVedio(this string filePath)
        {
            return VedioExtensions.Any(x => x.Equals(Path.GetExtension(filePath).Trim('.'), StringComparison.OrdinalIgnoreCase));
        }

        public static IEnumerable<string> GetAllAudios(string foldPath)
        {
            var files = foldPath.ToDirectoryEx().GetAllFiles(x => x.FullName.IsAudio());
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
    }


    public static partial class FileExtension
    {
        public static long GetDirectorySize(this DirectoryInfo dirInfo)
        {
            long size = 0;
            FileInfo[] files;
            try
            {
                files = dirInfo.GetFiles();
            }
            catch (UnauthorizedAccessException)
            {
                return 0;
            }
            catch (Exception e)
            {
                return 0;
            }

            foreach (FileInfo file in files)
            {
                try
                {
                    size += file.Length;
                }
                catch (UnauthorizedAccessException)
                {

                }
                catch (Exception e)
                {

                }
            }
            DirectoryInfo[] subDirs;
            try
            {
                subDirs = dirInfo.GetDirectories();
            }
            catch (UnauthorizedAccessException)
            {
                return size;
            }
            catch (Exception e)
            {
                return size;
            }

            foreach (DirectoryInfo subDir in subDirs)
            {
                size += GetDirectorySize(subDir);
            }
            return size;
        }
    }

    public static partial class FileExtension
    {
        public static DirectoryEx ToDirectoryEx(this string folder)=> new DirectoryEx(folder);

        public static FileEx ToFileEx(this string filePath)=> new FileEx(filePath);
    }

    public class DirectoryEx
    {
        public DirectoryEx(string folder)
        {
            this.Folder = folder;
        }
        public string Folder { get; }

        public DirectoryEx CreateDirectoryIfNotExsit()
        {
            if (!Directory.Exists(Folder))
                Directory.CreateDirectory(Folder);
            return this;
        }

        public void BackupToDirectory(string destFolder, Action<string> logAction = null)
        {
            string sourceFolder = this.Folder;
            int totalFiles = 0;
            int totalSubFolders = 0;
            int totalSuccess = 0;
            int totalFailed = 0;
            int totalSkipped = 0;
            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);
            string[] files = null;
            try
            {
                files = Directory.GetFiles(sourceFolder);
            }
            catch (UnauthorizedAccessException ex)
            {
                IocLog.Instance?.Error(ex);
                logAction?.Invoke($"{sourceFolder}\r\nAccess Denied\r\n");
            }
            catch (Exception e)
            {
                IocLog.Instance?.Error(e);
                logAction?.Invoke($"{sourceFolder}\r\n{e.Message}\r\n");
            }

            if (files != null && files.Length > 0)
            {
                totalFiles += files.Length;

                foreach (string file in files)
                {
                    try
                    {
                        string name = Path.GetFileName(file);
                        string dest = Path.Combine(destFolder, name);

                        if (File.Exists(dest))
                        {
                            DateTime srcWriteTime = File.GetLastWriteTime(file);
                            DateTime destWriteTime = File.GetLastWriteTime(dest);

                            if (srcWriteTime > destWriteTime)
                            {
                                File.Copy(file, dest, true);

                                FileInfo fileInfo = new FileInfo(file);
                                double fileSize = (double)fileInfo.Length / 1048576.0;  // Convert to MB
                                logAction?.Invoke($"{fileSize:000.000} MB - (updated) {file}");
                                totalSuccess++;
                            }
                            else
                            {
                                totalSkipped++;
                            }
                        }
                        else
                        {
                            File.Copy(file, dest, true);

                            FileInfo fileInfo = new FileInfo(file);
                            double fileSize = (double)fileInfo.Length / 1048576.0;  // Convert to MB
                            logAction?.Invoke($"{fileSize:000.000} MB - {file}");

                            totalSuccess++;
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {
                        logAction?.Invoke($"{file}\r\nAccess Denied\r\n");
                        totalFailed++;
                    }
                    catch (Exception e)
                    {
                        totalFailed++;
                        logAction?.Invoke($"{file}\r\n{e.Message}\r\n");
                    }
                }
            }
            string[] folders = null;

            try
            {
                folders = Directory.GetDirectories(sourceFolder);
            }
            catch (UnauthorizedAccessException)
            {
                logAction?.Invoke($"{sourceFolder}\r\nAccess denied\r\n");

            }
            catch (Exception e)
            {
                logAction?.Invoke($"{sourceFolder}\r\nAccess {e.Message}\r\n");
            }

            if (folders != null && folders.Length > 0)
            {
                totalSubFolders += folders.Length;

                foreach (string folder in folders)
                {
                    try
                    {
                        string name = Path.GetFileName(folder);
                        string dest = Path.Combine(destFolder, name);
                        folder.ToDirectoryEx().BackupToDirectory(dest, logAction);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        logAction?.Invoke($"{folder}\r\nAccess denied\r\n");
                    }
                    catch (Exception e)
                    {
                        logAction?.Invoke($"{sourceFolder}\r\nAccess {e.Message}\r\n");
                    }
                }
            }
        }

        public List<string> GetAllFiles(Predicate<FileInfo> match = null)
        {
            List<string> ss = new List<string>();
            if (!Directory.Exists(this.Folder))
                return ss;
            DirectoryInfo dir = new DirectoryInfo(this.Folder);
            foreach (FileSystemInfo d in dir.GetFileSystemInfos().Where(d => !d.Attributes.HasFlag(FileAttributes.Hidden | FileAttributes.System)))
            {
                if (d is DirectoryInfo)
                {
                    DirectoryInfo dd = d as DirectoryInfo;
                    ss.AddRange(dd.FullName.ToDirectoryEx().GetAllFiles(match));
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

    public class FileEx
    {
        public FileEx(string fullPath)
        {
            this.FullPath = fullPath;
        }
        public string FullPath { get; }

        public FileEx CreateDirectoryIfNotExsit()
        {
            string folder = Path.GetDirectoryName(FullPath);
            folder.ToDirectoryEx().CreateDirectoryIfNotExsit();
            return this;
        }

        public string GetFileHashMD5()
        {
            using (FileStream fs = new FileStream(this.FullPath, FileMode.Open, FileAccess.Read))
            {
                using (MD5 sha256 = MD5.Create())
                {
                    byte[] hashBytes = sha256.ComputeHash(fs);
                    return BitConverter.ToString(hashBytes).Replace("-", "");
                }
            }
        }

        public string GetFileHashSHA256()
        {
            using (FileStream fs = new FileStream(this.FullPath, FileMode.Open, FileAccess.Read))
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hashBytes = sha256.ComputeHash(fs);
                    return BitConverter.ToString(hashBytes).Replace("-", "");
                }
            }
        }

        public string GetFileSizeToDisplay()
        {
            double TB = 1024 * 1024 * 1024 * 1024.0;
            int GB = 1024 * 1024 * 1024;
            int MB = 1024 * 1024;
            int KB = 1024;

            if (!File.Exists(this.FullPath))
                return null;
            long KSize = new FileInfo(this.FullPath).Length;
            bool isMinus = KSize < 0;
            string result;
            KSize = Math.Abs(KSize);
            if (KSize / TB >= 1)
                result = Math.Round(KSize / (float)TB, 2).ToString() + "T";
            else if (KSize / GB >= 1)
                result = Math.Round(KSize / (float)GB, 2).ToString() + "G";
            else if (KSize / MB >= 1)

                result = Math.Round(KSize / (float)MB, 2).ToString() + "MB";
            else if (KSize / KB >= 1)

                result = Math.Round(KSize / (float)KB, 2).ToString() + "KB";
            else
                result = KSize.ToString() + "Byte";
            return isMinus ? "-" + result : result;
        }
    }
}

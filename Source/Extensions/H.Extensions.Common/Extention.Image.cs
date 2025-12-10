// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace H.Extensions.Common;

public static partial class ImageExtention
{
    public static ImageEx ToImageEx(this string filePath) => new ImageEx(filePath);

    public static void ToCompressImage(this string filePath, string destPath, int quality = 30)
    {
        var bitmap = new BitmapImage(new Uri(filePath));
        var encoder = new JpegBitmapEncoder();
        encoder.QualityLevel = quality;
        encoder.Frames.Add(BitmapFrame.Create(bitmap));
        using (var stream = new FileStream(destPath, FileMode.Create))
        {
            encoder.Save(stream);
        }
    }

    public static (int width, int height) GetImageResolution(this string imagePath)
    {
        if (!imagePath.IsImage())
            return (0, 0);
        try
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bitmap.EndInit();
            return (bitmap.PixelWidth, bitmap.PixelHeight);
        }
        catch { return (0, 0); }
    }

    public static void ToCroppedSetClipboard(this BitmapSource bitmapSource, Int32Rect cropArea)
    {
        var croppedBitmap = new CroppedBitmap(bitmapSource, cropArea);
        Clipboard.SetImage(croppedBitmap);
    }

    public static string ToCroppedImageBase64String(this BitmapSource bitmapSource, Int32Rect cropArea)
    {
        return Convert.ToBase64String(bitmapSource.ToCroppedImage(cropArea));
    }

    public static byte[] ToCroppedImage(this BitmapSource bitmapSource, Int32Rect cropArea)
    {
        return bitmapSource.ToCroppedImageStream(cropArea).ToArray();
    }

    public static MemoryStream ToCroppedImageStream(this BitmapSource bitmapSource, Int32Rect cropArea)
    {
        var croppedBitmap = new CroppedBitmap(bitmapSource, cropArea);
        var encoder = new JpegBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(croppedBitmap));
        using (var memoryStream = new MemoryStream())
        {
            encoder.Save(memoryStream);
            return memoryStream;
        }
    }

    public static ImageSource ToBase64ImageSource(this string base64String)
    {
        if (string.IsNullOrEmpty(base64String))
            return null;
        byte[] binaryData = Convert.FromBase64String(base64String);
        var bitmapImage = new BitmapImage();
        using (var memoryStream = new MemoryStream(binaryData))
        {
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.StreamSource = memoryStream;
            bitmapImage.EndInit();
        }
        return bitmapImage;
    }

    public static ImageSource ToFilePathImageSource(this string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            return null;
        ImageSourceConverter converter = new ImageSourceConverter();
        return converter.ConvertFromInvariantString(filePath) as ImageSource;
    }
}

public static partial class ImageExtention
{
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern bool CreateHardLink(string lpFileName, string lpExistingFileName, IntPtr lpSecurityAttributes);

    public static (bool success, string message) CreateHardLinkTo(this string sourcePath, string destinationPath)
    {
        try
        {
            // 1. 基本检查
            if (!File.Exists(sourcePath))
                return (false, "源文件不存在");

            // 2. 文件系统检查
            if (!IsFileSystemSupported(sourcePath))
                return (false, "文件系统不支持硬链接");

            // 3. 磁盘分区检查
            if (!CanCreateHardLink(sourcePath, destinationPath))
                return (false, "不能跨磁盘分区创建硬链接");

            // 4. 目录准备
            string destDir = Path.GetDirectoryName(destinationPath);
            if (!Directory.Exists(destDir))
            {
                try
                {
                    Directory.CreateDirectory(destDir);
                }
                catch (Exception ex)
                {
                    return (false, $"创建目录失败: {ex.Message}");
                }
            }

            // 5. 删除已存在的目标文件
            if (File.Exists(destinationPath))
            {
                try
                {
                    File.Delete(destinationPath);
                }
                catch (Exception ex)
                {
                    return (false, $"删除已存在文件失败: {ex.Message}");
                }
            }

            // 6. 创建硬链接
            bool apiSuccess = CreateHardLink(destinationPath, sourcePath, IntPtr.Zero);

            if (!apiSuccess)
            {
                int errorCode = Marshal.GetLastWin32Error();
                string errorMsg = GetErrorMessage(errorCode);
                return (false, $"API调用失败 (错误代码 {errorCode}): {errorMsg}");
            }

            // 7. 验证结果
            if (!File.Exists(destinationPath))
                return (false, "API调用成功但文件未创建");

            // 8. 最终验证
            return VerifyHardLink(sourcePath, destinationPath);
        }
        catch (Exception ex)
        {
            return (false, $"异常: {ex.Message}");
        }
    }

    private static (bool success, string message) VerifyHardLink(string sourcePath, string destinationPath)
    {
        try
        {
            FileInfo sourceInfo = new FileInfo(sourcePath);
            FileInfo destInfo = new FileInfo(destinationPath);

            if (sourceInfo.Length != destInfo.Length)
                return (false, "文件大小不匹配");

            // 简单的内容验证（对于图片文件）
            if (sourceInfo.Length > 0)
            {
                using (var sourceStream = File.OpenRead(sourcePath))
                using (var destStream = File.OpenRead(destinationPath))
                {
                    if (sourceStream.Length != destStream.Length)
                        return (false, "文件内容长度验证失败");
                }
            }

            return (true, "硬链接创建并验证成功");
        }
        catch (Exception ex)
        {
            return (false, $"验证失败: {ex.Message}");
        }
    }

    private static string GetErrorMessage(int errorCode)
    {
        return errorCode switch
        {
            5 => "拒绝访问 (ERROR_ACCESS_DENIED)",
            80 => "文件已存在 (ERROR_FILE_EXISTS)",
            183 => "文件已存在 (ERROR_ALREADY_EXISTS)",
            17 => "目标文件系统不支持硬链接",
            1327 => "需要提升的权限",
            _ => $"未知错误: {errorCode}"
        };
    }

    public static bool IsFileSystemSupported(string path)
    {
        try
        {
            DriveInfo drive = new DriveInfo(Path.GetPathRoot(path));
            string fileSystem = drive.DriveFormat;
            Console.WriteLine($"文件系统: {fileSystem}");

            // NTFS 支持硬链接，FAT32、exFAT 不支持
            bool supported = fileSystem.Equals("NTFS", StringComparison.OrdinalIgnoreCase);
            Console.WriteLine($"支持硬链接: {supported}");

            return supported;
        }
        catch
        {
            return false;
        }
    }
    public static bool CanCreateHardLink(string sourcePath, string destinationPath)
    {
        try
        {
            DriveInfo sourceDrive = new DriveInfo(Path.GetPathRoot(sourcePath));
            DriveInfo destDrive = new DriveInfo(Path.GetPathRoot(destinationPath));

            bool sameDrive = sourceDrive.Name.Equals(destDrive.Name, StringComparison.OrdinalIgnoreCase);
            Console.WriteLine($"同磁盘分区: {sameDrive}");

            if (!sameDrive)
            {
                Console.WriteLine("错误: 硬链接不能跨磁盘分区创建!");
                return false;
            }

            return true;
        }
        catch
        {
            return false;
        }
    }
}

public class ImageEx
{
    private static List<Tuple<string, int, int, BitmapImage>> _fileCache = new List<Tuple<string, int, int, BitmapImage>>();

    public ImageEx(string fullPath)
    {
        this.FullPath = fullPath;
    }

    public void ClearCache()
    {
        _fileCache.Clear();
    }

    public string FullPath { get; }

    public Tuple<int, int> GetImagePixel()
    {
        if (!File.Exists(this.FullPath) || this.FullPath.IsImage() == false)
            return null;
        BitmapImage bmp = new BitmapImage(new Uri(this.FullPath, UriKind.RelativeOrAbsolute));
        if (bmp == null)
            return null;
        return Tuple.Create(bmp.PixelWidth, bmp.PixelHeight);
    }

    public ImageSource GetImageSource(int decodePixelWidth = 0, int decodePixelHeight = 0, bool useCache = true)
    {
        if (!File.Exists(this.FullPath))
            return null;

        if (this.FullPath.IsImage() == false)
            return null;

        if (useCache)
        {
            Tuple<string, int, int, BitmapImage> find = _fileCache.FirstOrDefault(k => k.Item1 == this.FullPath && k.Item2 == decodePixelWidth && k.Item3 == decodePixelHeight);
            if (find != null && find.Item4 != null)
                return find.Item4;
        }

        try
        {
            using (FileStream filestream = File.Open(this.FullPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (BinaryReader reader = new BinaryReader(filestream))

                //using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open,FileAccess.ReadWrite, FileShare.ReadWrite)))
                {
                    FileInfo fi = new FileInfo(this.FullPath);
                    byte[] bytes = reader.ReadBytes((int)fi.Length);
                    reader.Close();
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    if (decodePixelWidth > 0)
                        bitmapImage.DecodePixelWidth = decodePixelWidth;
                    if (decodePixelHeight > 0)
                        bitmapImage.DecodePixelHeight = decodePixelHeight;
                    bitmapImage.StreamSource = new MemoryStream(bytes);
                    bitmapImage.EndInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    if (useCache)
                        _fileCache.Add(Tuple.Create(this.FullPath, decodePixelWidth, decodePixelHeight, bitmapImage));
                    return bitmapImage;
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.Assert(false, ex.Message);
            return null;
            //var result = new BitmapImage(new Uri(filePath, UriKind.Absolute));
            //if (decodePixelWidth > 0)
            //    result.DecodePixelWidth = decodePixelWidth;
            //if (decodePixelHeight > 0)
            //    result.DecodePixelHeight = decodePixelHeight;
            //return new BitmapImage(new Uri(filePath, UriKind.Absolute));
        }
    }

}

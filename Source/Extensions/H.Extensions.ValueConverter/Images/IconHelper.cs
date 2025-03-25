using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace H.Extensions.ValueConverter.Images;

/// <summary> 获取文件关联图标 </summary>
public static class IconHelper
{
    /// <summary> 返回系统设置的图标 </summary>  
    /// <param name="pszPath">文件路径 如果为""  返回文件夹的</param>  
    /// <param name="dwFileAttributes">0</param>  
    /// <param name="psfi">结构体</param>  
    /// <param name="cbSizeFileInfo">结构体大小</param>  
    /// <param name="uFlags">枚举类型</param>  
    /// <returns>-1失败</returns>  
    [DllImport("shell32.dll")]
    public static extern nint SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

    /// <summary> 获取文件图标 </summary>  
    public static Icon GetFileIcon(string p_Path)
    {
        SHFILEINFO _SHFILEINFO = new SHFILEINFO();
        nint _IconIntPtr = SHGetFileInfo(p_Path, 0, ref _SHFILEINFO, (uint)Marshal.SizeOf(_SHFILEINFO), (uint)(SHGFI.SHGFI_ICON | SHGFI.SHGFI_LARGEICON | SHGFI.SHGFI_USEFILEATTRIBUTES));
        if (_IconIntPtr.Equals(nint.Zero)) return null;
        Icon _Icon = Icon.FromHandle(_SHFILEINFO.hIcon);
        return _Icon;
    }

    /// <summary> 获取文件夹图标 </summary>  
    public static Icon GetDirectoryIcon()
    {
        SHFILEINFO _SHFILEINFO = new SHFILEINFO();
        nint _IconIntPtr = SHGetFileInfo(@"", 0, ref _SHFILEINFO, (uint)Marshal.SizeOf(_SHFILEINFO), (uint)(SHGFI.SHGFI_ICON | SHGFI.SHGFI_LARGEICON));
        if (_IconIntPtr.Equals(nint.Zero)) return null;
        Icon _Icon = Icon.FromHandle(_SHFILEINFO.hIcon);
        return _Icon;
    }

    /// <summary> 获取系统图标 </summary>  
    public static Icon GetSystemInfoIcon(string p_Path)
    {
        if (System.IO.Path.HasExtension(p_Path))
        {
            try
            {
                return Icon.ExtractAssociatedIcon(p_Path);
            }
            catch
            {
                return null;
            }

        }
        else
        {
            return GetDirectoryIcon();
        }
    }


    public static ImageSource GetIconToImageSource(Icon icon)
    {
        if (icon == null)
            return null;
        ImageSource imageSource =
            System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
            icon.Handle,
            Int32Rect.Empty,
            BitmapSizeOptions.FromEmptyOptions());
        return imageSource;
    }


}

[StructLayout(LayoutKind.Sequential)]
public struct SHFILEINFO
{
    public nint hIcon;
    public nint iIcon;
    public uint dwAttributes;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
    public string szDisplayName;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
    public string szTypeName;
}


public enum SHGFI
{
    SHGFI_ICON = 0x100,
    SHGFI_LARGEICON = 0x0,
    SHGFI_USEFILEATTRIBUTES = 0x10
}

using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace H.App.FileManager
{
    public class FileView : FileView<fm_dd_file>
    {
        private FileInfo _file;
        public FileView(fm_dd_file t) : base(t)
        {
            _file = new FileInfo(t.Url);
        }

        [Display(Name = "名称")]
        public string FileName => this._file.Name;
        [Display(Name = "位置")]

        public string FullName => this._file.FullName;
        [Display(Name = "文件夹")]

        public string DirectoryName => this._file.DirectoryName;
        [Display(Name = "创建时间")]

        public DateTime CreationTime => this._file.CreationTime;

        [Display(Name = "修改时间")]
        public DateTime LastWriteTime => this._file.LastWriteTime;

        [Display(Name = "访问时间")]
        public DateTime LastAccessTime => this._file.LastAccessTime;

        [Display(Name = "是否存在")]
        public bool Exists => this._file.Exists;

        [Display(Name = "只读")]
        public bool IsReadOnly => this._file.IsReadOnly;

        [Display(Name = "类型")]
        public string Extension => this._file.Extension;

        [Display(Name = "大小")]
        public long Length => this.Exists ? this._file.Length : 0;

        //public UnixFileMode UnixFileMode => this._file.GetAccessControl().GetOwner;
        [Display(Name = "特性")]
        public FileAttributes FileAttributes => this._file.Attributes;

        [Display(Name = "代码")]
        public int HashCode => this._file.GetHashCode();

        [Display(Name = "LinkTarget")]
        public string LinkTarget => this._file.LinkTarget;

        [Display(Name = "UnixFileMode")]
        public UnixFileMode UnixFileMode => this._file.UnixFileMode;
    }
}

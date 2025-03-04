// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Modules.Login;
using H.Services.Common;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using H.Mvvm.ViewModels.Base;

namespace H.Modules.Project
{
    public abstract class ProjectItemBase : BindableBase, IProjectItem
    {
        private string _title;
        [Required]
        [Display(Name = "标题", Order = 4)]
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        private string _path;
        [ReadOnly(true)]
        [Required]
        [Browsable(false)]
        [Display(Name = "工程文件路径", Order = 4)]
        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                RaisePropertyChanged();
            }
        }

        private bool _isFixed;
        [Display(Name = "是否固定", Order = 4)]
        public bool IsFixed
        {
            get { return _isFixed; }
            set
            {
                _isFixed = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _createTime = DateTime.Now;
        [Browsable(false)]
        [ReadOnly(true)]
        [Display(Name = "创建时间", Order = 4)]
        public DateTime CreateTime
        {
            get { return _createTime; }
            set
            {
                _createTime = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _updateTime = DateTime.Now;
        [Browsable(false)]
        [ReadOnly(true)]
        [Display(Name = "修改时间", Order = 4)]
        public DateTime UpdateTime
        {
            get { return _updateTime; }
            set
            {
                _updateTime = value;
                RaisePropertyChanged();
            }
        }

        public string Name => this.Title;

        public virtual bool Save(out string message)
        {
            message = null;
            return true;
        }

        public virtual bool Load(out string message)
        {
            message = null;
            return true;
        }

        public virtual bool Close(out string message)
        {
            message = null;
            return true;
        }

        public virtual string GetFilePath()
        {
            return System.IO.Path.Combine(this.Path, this.Title + "" + ProjectOptions.Instance.Extenstion);
        }

        public virtual void Dispose()
        {

        }

        public virtual bool Delete(out string message)
        {
            var r = this.Close(out message);
            if (r == false)
                return false;
            if (File.Exists(this.Path))
                File.Delete(this.Path);
            if (!string.IsNullOrEmpty(this.Path))
            {
                var find = this.GetFilePath();
                if (File.Exists(find))
                    File.Delete(find);
            }
            return true;
        }
    }
}

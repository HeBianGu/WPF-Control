// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

#if NETFRAMEWORK
using System.Data.Entity;
#endif

#if NETCOREAPP
#endif
using H.DataBases.Share;
using H.Services.AppPath;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace H.DataBases.Sqlite
{
    public abstract class SqliteSettableBase<TSetting> : DbSettableBase<TSetting>, ISqliteSettable where TSetting : new()
    {
        public override void LoadDefault()
        {
            base.LoadDefault();
            this.FilePath = AppPaths.Instance.Data;
        }

        private string _filePath;
        [Display(Name = "数据库文件夹路径", Description = "数据库保存的文件夹路径")]
        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                RaisePropertyChanged();
            }
        }

        private string _initialCatalog;
        [DefaultValue("data.db")]
        [Display(Name = "数据库名称", Description = "数据库文件的名称")]
        public string InitialCatalog
        {
            get { return _initialCatalog; }
            set
            {
                _initialCatalog = value;
                RaisePropertyChanged();
            }
        }

        public override string GetConnect()
        {
            if (string.IsNullOrEmpty(this.FilePath))
                return $"Data Source={this.InitialCatalog}";
            return $"Data Source={Path.Combine(this.FilePath, this.InitialCatalog)}";
        }
    }
}

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
using H.Services.Setting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.DataBases.SqlServer
{
    public abstract class SqlServerSettable<TSetting> : DbSettableBase<TSetting>, ISqlServerOption where TSetting : new()
    {

        private string _server;
        [Display(Name = "服务器名称")]
        public string Server
        {
            get { return _server; }
            set
            {
                _server = value;
                RaisePropertyChanged();
            }
        }

        private string _initialCatalog;
        [Display(Name = "数据库")]
        public string InitialCatalog
        {
            get { return _initialCatalog; }
            set
            {
                _initialCatalog = value;
                RaisePropertyChanged();
            }
        }

        private string _userName;
        [DefaultValue("sa")]
        [Display(Name = "用户名")]
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged();
            }
        }

        private string _password;
        [DefaultValue(".")]
        [Display(Name = "密码")]
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged();
            }
        }

        public override string GetConnect()
        {
            string connect = $"Server={this.Server}; Trusted_Connection=False; uid={this.UserName}; pwd={this.Password};Initial Catalog={this.InitialCatalog}; MultipleActiveResultSets=true;";
            return connect;
        }
    }


    public class SqlServerSettable : SqlServerSettable<SqlServerSettable>, ISqlServerOption
    {

    }
}

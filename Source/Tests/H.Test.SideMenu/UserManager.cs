using H.Data.Test;
using H.Mvvm.ViewModels.Base;
using HeBianGu.Systems.Logger;
using System;
using System.ComponentModel.DataAnnotations;

namespace H.Test.SideMenu
{
    public interface IManager
    {
        bool IsVisibleInTab { get; set; }
    }

    public abstract class ManagerBase : DisplayBindableBase, IManager
    {
        private bool _isVisibleInTab;
        public bool IsVisibleInTab
        {
            get { return _isVisibleInTab; }
            set
            {
                _isVisibleInTab = value;
                RaisePropertyChanged();
            }
        }
    }

    public abstract class ManagerBase<T> : ManagerBase
    {
        public Type Type { get; } = typeof(T);
    }

    [Display(Name = "用户管理", GroupName = "身份认证", Description = "用户管理")]
    public class UserManager : ManagerBase<hi_dd_user>
    {

    }

    [Display(Name = "角色管理", GroupName = "身份认证", Description = "角色管理")]
    public class RoleManager : ManagerBase<hi_dd_role>
    {

    }

    [Display(Name = "权限管理", GroupName = "身份认证", Description = "权限管理")]
    public class AuthorManager : ManagerBase<hi_dd_author>
    {

    }

    [Display(Name = "记录日志", GroupName = "日志记录", Description = "权限管理")]
    public class DebugManager : ManagerBase<hl_dm_debug>
    {

    }
    [Display(Name = "错误日志", GroupName = "日志记录", Description = "权限管理")]
    public class ErrorManager : ManagerBase<hl_dm_error>
    {

    }
    [Display(Name = "严重错误", GroupName = "日志记录", Description = "权限管理")]
    public class FatalManager : ManagerBase<hl_dm_fatal>
    {

    }
    [Display(Name = "运行日志", GroupName = "日志记录", Description = "权限管理")]
    public class InfoManager : ManagerBase<hl_dm_info>
    {

    }
    [Display(Name = "警告日志", GroupName = "日志记录", Description = "权限管理")]
    public class WarnManager : ManagerBase<hl_dm_warn>
    {

    }


    [Display(Name = "学生管理", GroupName = "人员管理", Description = "权限管理",Order =0)]
    public class StudentManager : ManagerBase<Student>
    {

    }


    [Display(Name = "后勤管理", GroupName = "人员管理", Description = "权限管理", Order = 3)]
    public class LogisticManager : ManagerBase<Student>
    {

    }


    [Display(Name = "教师管理", GroupName = "人员管理", Description = "权限管理", Order = 1)]
    public class TeatherManager : ManagerBase<Student>
    {

    }


    [Display(Name = "管理人员", GroupName = "人员管理", Description = "权限管理", Order = 2)]
    public class LeaderManager : ManagerBase<Student>
    {

    }
}
